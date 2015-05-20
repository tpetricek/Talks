#load "packages/FsLab/FsLab.fsx"
#load "utils/Credentials.fsx"
#load "utils/MBrace.fsx"

open System
open Credentials
open MBrace
open MBrace.Flow
open MBrace.Azure.Client
open FSharp.Data

let cluster = Runtime.GetHandle(config)

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

let getIndicators () = cloud {
  let wb = WorldBankData.GetDataContext()
  let! countryData = 
    wb.Countries
    |> Array.ofSeq
    |> CloudFlow.ofArray
    |> CloudFlow.map (fun c ->
        let indicators = 
           [| c.Indicators.``Agricultural land (% of land area)``.[2010]
              c.Indicators.``Access to electricity, rural (% of population)``.[2010]
              c.Indicators.``Life expectancy at birth, total (years)``.[2010]
              c.Indicators.``GDP per capita (current US$)``.[2010]
              c.Indicators.``GDP per capita growth (annual %)``.[2010]
              c.Indicators.``School enrollment, tertiary (% gross)``.[2010]
              c.Indicators.``CO2 emissions (metric tons per capita)``.[2010]
              c.Indicators.``Population growth (annual %)``.[2010]
              c.Indicators.``Unemployment, total (% of total labor force) (national estimate)``.[2010] |]
        { Name = c.Name; Indicators = indicators } )
    |> CloudFlow.toArray
  let countryData = normalize countryData
  let! cell = CloudCell.New countryData 
  return cell }



let indCellProc = getIndicators() |> cluster.CreateProcess
indCellProc.Completed
indCellProc.ShowInfo()
cluster.ShowWorkers()
cluster.ShowProcesses()
let indCell = indCellProc.AwaitResult()

type Clustering = 
  { Assignment : int[]
    Iterations : int
    AverageDist : float }

let kmeans distance aggregator clusterCount (dataCell:CloudCell<_[]>) = cloud {
  let! data = CloudCell.Read dataCell
    
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
  return { Assignment = assign; Iterations = iters; AverageDist = avgDist } }


let inline distance c1 c2 = 
  Array.map2 (fun v1 v2 -> (v1 - v2)*(v1 - v2)) c1.Indicators c2.Indicators |> Seq.sum

let inline aggregator centroid items = 
  let len = Seq.length items
  if len = 0 then centroid else 
    let newInds =
      items 
      |> Seq.map (fun c -> c.Indicators)
      |> Seq.reduce (Array.map2 (+))
      |> Array.map (fun v -> v/float len)
    { centroid with Indicators = newInds }


let resProc = 
  [ for i in 0 .. 20 -> kmeans distance aggregator 3 indCell ]
  |> Cloud.Parallel
  |> cluster.CreateProcess

resProc.Completed
resProc.ShowInfo()
cluster.ShowProcesses()
let res = resProc.AwaitResult() 
let best = res |> Seq.maxBy (fun r -> r.AverageDist)   

let read f = 
  cloud { let! data = CloudCell.Read indCell
          return [ for c in data -> f c ] }
  |> cluster.Run

let names = read (fun c -> c.Name)

open XPlot.GoogleCharts
// Chart.Scatter(scatter)

Seq.zip names best.Assignment
|> Chart.Geo 
|> Chart.WithOptions(Options(colorAxis=ColorAxis(colors=[|"red";"green";"blue";"orange"|])))

