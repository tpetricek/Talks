#load "packages/FsLab/FsLab.fsx"
open System
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

// ----------------------------------------------------------------------------
// Getting the data
// ----------------------------------------------------------------------------

let wb = WorldBankData.GetDataContext()

let rnd = Random()
let indicators = 
  [ for c in wb.Countries ->
      c.Name => series [ 
        "Agriculture" => c.Indicators.``Agricultural land (% of land area)``.[2010]
        "Electricity" => c.Indicators.``Access to electricity, rural (% of population)``.[2010]
        "Life" => c.Indicators.``Life expectancy at birth, total (years)``.[2010]
        "GDP" => c.Indicators.``GDP per capita (current US$)``.[2010]
        "Growth" => c.Indicators.``GDP per capita growth (annual %)``.[2010]
        "University" => c.Indicators.``School enrollment, tertiary (% gross)``.[2010]
        "CO2" => c.Indicators.``CO2 emissions (metric tons per capita)``.[2010]
        "Unemployment" => c.Indicators.``Unemployment, total (% of total labor force) (national estimate)``.[2010]
        "Births" => c.Indicators.``Population growth (annual %)``.[2010] ] ]
  |> Frame.ofRows

let lo = indicators |> Stats.min
let hi = indicators |> Stats.max
let avg = indicators |> Stats.mean

let norm = 
  indicators 
  |> Frame.mapRows (fun _ r ->
    (r.As<float>() - lo) / (hi - lo)
    |> Series.fillMissingUsing avg.Get)
  |> Frame.ofRows

(*
type Country = { Name : string; Indicators : float[] }

let normalize (data:Country[]) = 
  let indRanges = 
    [| for i in 0 .. data.[0].Indicators.Length-1 ->
         let values = data |> Array.map (fun c -> c.Indicators.[i]) |> Array.filter (Double.IsNaN >> not)
         Array.min values, Array.max values, Array.average values |]
  data |> Array.map (fun c ->
      let newInds = (c.Indicators, indRanges) ||> Array.map2 (fun v (lo, hi, avg) ->
          let v = if Double.IsNaN v then avg else v
          (v - lo) / (hi - lo) )
      { c with Indicators = newInds })
*)


//  |> normaliz

// ----------------------------------------------------------------------------
// Looking at the data
// ----------------------------------------------------------------------------

Seq.zip norm?CO2.Values norm?Births.Values
|> Chart.Scatter

Seq.zip norm?GDP.Values norm?University.Values
|> Chart.Scatter

// ----------------------------------------------------------------------------
// Clustering algorithm
// ----------------------------------------------------------------------------

type Clustering = 
  { Assignment : int[]
    Iterations : int
    AverageDist : float }

let kmeans distance aggregator clusterCount (data:_[]) = 
  let centroids = 
    let rnd = System.Random()
    [| for i in 1 .. clusterCount -> data.[rnd.Next(data.Length)] |]

  let closest centroids input = 
    centroids 
    |> Seq.mapi (fun i v -> i, v)
    |> Seq.minBy (fun (i, cent) -> distance cent input)
    |> fst

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

  let iters, assign, centroids = update 0 centroids (Array.map (closest centroids) data)
  let avgDist = (data, assign) ||> Array.map2 (fun d c -> distance d centroids.[c]) |> Seq.average
  { Assignment = assign; Iterations = iters; AverageDist = avgDist }

// ----------------------------------------------------------------------------
// Clustering countries
// ----------------------------------------------------------------------------

let (|Obs|) (name:string, vals:Series<string, float>) = name, vals

let distance (Obs(n, s1)) (Obs(n, s2)) = 
  (s1 - s2) * (s1 - s2) |> Stats.sum

let aggregator (Obs(_, centr)) items = 
  let len = Array.length items
  if len = 0 then "", centr else 
    let avg =
      [ for Obs(_, s) in items -> s ]
      |> Frame.ofRowsOrdinal
      |> Stats.mean
    "", avg

norm 
|> Frame.toArray2D
|> Frame.ofArray2D
|> Frame.indexRowsWith norm.RowKeys
|> Frame.indexColsWith norm.ColumnKeys

let input = norm.GetRows<float>() |> Series.observations |> Array.ofSeq
let clusters = kmeans distance aggregator 3 input

Seq.zip norm.RowKeys clusters.Assignment
|> Chart.Geo 
|> Chart.WithOptions(Options(colorAxis=ColorAxis(colors=[|"red";"green";"blue";"orange"|])))