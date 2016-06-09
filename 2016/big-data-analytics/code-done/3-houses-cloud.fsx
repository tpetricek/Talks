#nowarn "211"
#load "packages/FsLab/Themes/AtomChester.fsx"
#load "packages/FsLab/FsLab.fsx"
#load "utils/mbrace.fsx"
#r "paket-files/github.com/BlueMountainCapital/Deedle.BigDemo/src/Deedle.BigSources/bin/Release/Deedle.BigSources.dll"
open System
open Deedle
open MBrace.Core
open MBrace.Azure
open XPlot.GoogleCharts

/// Returns DateTimeOffset for the specified date and time in GMT
let dt (y, m, d) =
  DateTimeOffset(DateTime(y, m, d), TimeSpan.FromMinutes(0.0))

/// Turns virtual frame into in-memory frame
let materialize (df:Frame<_, _>) =
  df.Rows |> Series.observations |> Frame.ofRows

// ------------------------------------------------------------------------------------------------

let cluster = Config.GetCluster()

let getHouses() = BigDeedle.Houses.GetFrame()

let averagePrice dt1 dt2 town = cloud {
  let houses = getHouses()
  let part =
    houses.Rows.[dt1 .. dt2]
    |> Frame.filterRows (fun k row ->
        row.GetAs("Town") = town &&
        row.GetAs("Duration") = "F")
  return Stats.mean part?Price }

let camAll =
  [ for y in 1995 .. 2016 -> cloud {
      let! avg = averagePrice (dt(y,4,1)) (dt(y,5,1)) "CAMBRIDGE"
      return y, avg } ]
  |> Cloud.Parallel
  |> cluster.CreateProcess

let ldnAll =
  [ for y in 1995 .. 2016 -> cloud {
      let! avg = averagePrice (dt(y,4,1)) (dt(y,5,1)) "LONDON"
      return y, avg } ]
  |> Cloud.Parallel
  |> cluster.CreateProcess

camAll.Status
ldnAll.Status

[ldnAll.Result; camAll.Result]
|> Chart.Line
|> Chart.WithLabels ["London"; "Cambridge"]








// Basic demo

let cam =
  averagePrice (dt(2010,4,1)) (dt(2010,4,10)) "CAMBRIDGE"
  |> cluster.CreateProcess

cam.Status
cam.Result
