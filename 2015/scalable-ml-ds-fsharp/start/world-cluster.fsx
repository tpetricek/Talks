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
// TODO: Visualize using Geo chart and Series.observations
// TODO: Calculate and visualize growth



// ----------------------------------------------------------------------------
// Getting data for clustering countries
// ----------------------------------------------------------------------------

// DEMO: Get indicators for the whole world
// TODO: Calculate 'lo', 'hi' and 'avg'
// DEMO: Scale the values to 0 .. 1 range
// TODO: Plot correlations using R 'graphics' & 'plot'




















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


// TODO: Write 'distance' function (sum of square diffs)
// DEMO: Write 'aggregator' function for countries
// DEMO: Run the algorithm and visualize results