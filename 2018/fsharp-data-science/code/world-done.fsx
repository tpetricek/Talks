#load "packages/FsLab/FsLab.fsx"
open System
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

// ----------------------------------------------------------------------------
// Getting and visualizing population data from WorldBank
// ----------------------------------------------------------------------------

let wb = WorldBankData.GetDataContext()
wb.Countries.Afghanistan.Indicators.

// Get population for all countries in 2000 and 2010
let pop2000 = series [ for c in wb.Countries -> c.Name => c.Indicators.``Population, total``.[2000]]
let pop2010 = series [ for c in wb.Countries -> c.Name => c.Indicators.``Population, total``.[2010]]

// Visualize the population using Geo chart
Chart.Geo(Series.observations pop2010)

// Calculate growth and visualize growth
let growth = (pop2010 - pop2000) / pop2000 * 100.0
Chart.Geo(Series.observations growth)

// ----------------------------------------------------------------------------
// Getting data for clustering countries
// ----------------------------------------------------------------------------

// Get frame with multiple indiciators about countries
let world = 
  [ for c in wb.Countries ->
      c.Name => series [ 
        "Electricity" => c.Indicators.``Access to electricity, rural (% of population)``.[2010]
        "Life" => c.Indicators.``Life expectancy at birth, total (years)``.[2010]
        "GDP" => c.Indicators.``GDP per capita (current US$)``.[2010]
        "Growth" => c.Indicators.``GDP per capita growth (annual %)``.[2010]
        "CO2" => c.Indicators.``CO2 emissions (metric tons per capita)``.[2010]
        "Births" => c.Indicators.``Population growth (annual %)``.[2010] ] ]
  |> Frame.ofRows

// Explore the data interactively
let lo = world |> Stats.min
let hi = world |> Stats.max
let avg = world |> Stats.mean

// Scale data to a range 0.0 .. 1.0
let norm = 
  world.Rows |> Series.mapValues (fun r ->
      let vs = r.As<float>() |> Series.fillMissingUsing avg.Get
      (vs - lo) / (hi - lo) )
  |> Frame.ofRows

// Draw a scatter plot comparing GDP and Life expectancy
Seq.zip norm?GDP.Values norm?Life.Values
|> Chart.Scatter
|> Chart.WithOptions(Options(hAxis=Axis(title="GDP"), vAxis=Axis(title="Expectancy")))

// Draw correlations using the R type provider
open RProvider
open RProvider.graphics

R.plot(world)

// ----------------------------------------------------------------------------
// Implementing the K-means clustering algorithm
// ----------------------------------------------------------------------------

/// Result of a clustering consisting of cluster assignments for each
/// of the inputs, number of iterations and average distance
type Clustering = 
  { Assignment : int[]
    Iterations : int
    AverageDist : float }

/// Performs the k-means clustering using randomly picked observations as
/// initial clusters and the specified `distance` and `aggregator` functions
/// (See http://clear-lines.com/blog/post/K-Means-Clustering-in-FSharp.aspx)
let kmeans distance aggregator clusterCount (data:_[]) = 

  // Generate initial centroids
  let centroids = 
    let rnd = System.Random()
    [| for i in 1 .. clusterCount -> data.[rnd.Next(data.Length)] |]

  // Find the centroid closer to the specified input
  let closest centroids input = 
    centroids 
    |> Seq.mapi (fun i v -> i, v)
    |> Seq.minBy (fun (i, cent) -> distance cent input)
    |> fst

  // Recursively update centroids until they stop changing      
  let rec update iters centroids assignment =
    let centroids = 
      centroids |> Array.mapi (fun idx cent ->
        let items = 
          Seq.zip assignment data
          |> Seq.filter (fun (c, data) -> c = idx) 
          |> Seq.map snd
          |> Array.ofSeq
        aggregator cent items )
    let next = Array.map (closest centroids) data
    if next = assignment then iters, next, centroids
    else update (iters+1) centroids next

  // Run the algorithm, calculate average distance & return
  let iters, assign, centroids = 
    update 0 centroids (Array.map (closest centroids) data)
  let avgDist = (data, assign) ||> Array.map2 (fun d c -> 
    distance d centroids.[c]) |> Seq.average
  { Assignment = assign; Iterations = iters; AverageDist = avgDist }


// ----------------------------------------------------------------------------
// Clustering countries
// ----------------------------------------------------------------------------

/// Extracts the name and values associated with a country in a tuple
let (|Country|) (name:string, vals:Series<string, float>) = name, vals

/// Calculate the distance between indicators of two countries
let distance (Country(_, s1)) (Country(_, s2)) = 
  (s1 - s2) * (s1 - s2) |> Stats.sum

/// Aggregate indicators in a specified cluster to get a centroid
let aggregator (Country(_, centr)) items = 
  let avg = 
    if Array.isEmpty items then centr 
    else Seq.map snd items
         |> Frame.ofRowsOrdinal
         |> Stats.mean
  "", avg

// Run the k-mena clustering on the countries & visualize
let input = norm.GetRows<float>() |> Series.observations |> Array.ofSeq
let clusters = kmeans distance aggregator 3 input

Seq.zip norm.RowKeys clusters.Assignment
|> Chart.Geo 
|> Chart.WithOptions(Options(colorAxis=ColorAxis(colors=[|"red";"blue";"orange"|])))