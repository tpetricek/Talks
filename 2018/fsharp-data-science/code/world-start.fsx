#load "packages/FsLab/FsLab.fsx"
open System
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

// ----------------------------------------------------------------------------
// Getting and visualizing population data from WorldBank
// ----------------------------------------------------------------------------

let wb = WorldBankData.GetDataContext()

// TODO: Get population for all countries in 2000 and 2010
// TODO: Visualize the population using Geo chart
// TODO: Calculate growth and visualize growth



// ----------------------------------------------------------------------------
// Getting data for clustering countries
// ----------------------------------------------------------------------------

// DEMO: Get frame with multiple indiciators about countries
// TODO: Explore the data interactively (lo, hi, avg)
// DEMO: Scale data & draw a scatter plot



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

/// Calculate the distance between indicators of two countries
let distance (_, s1:Series<_, float>) (_, s2:Series<_, float>) = 
  // TODO: Sum of squares of differences
  0.0

/// Aggregate indicators in a specified cluster to get a centroid
let aggregator centr items = 
  // DEMO: Average items to get a new centroid
  centr

// Run the k-mena clustering on the countries & visualize
let input = norm.GetRows<float>() |> Series.observations |> Array.ofSeq
let clusters = kmeans distance aggregator 3 input

Seq.zip norm.RowKeys clusters.Assignment
|> Chart.Geo 
|> Chart.WithOptions(Options(colorAxis=ColorAxis(colors=[|"red";"blue";"orange"|])))