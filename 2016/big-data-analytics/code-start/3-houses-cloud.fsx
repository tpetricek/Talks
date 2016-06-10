#nowarn "211"
#load "packages/FsLab/Themes/AtomChester.fsx"
#load "packages/FsLab/FsLab.fsx"
#load "utils/mbrace.fsx"
#r "bigdeedle/Deedle.BigSources.dll"
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

/// Returns Big Deedle frame with UK houuse prices
let getHouses() = BigDeedle.Houses.GetFrame()

/// Returns connection to Azure cluster
let cluster = Config.GetCluster()



// DEMO: Average prices in range for a town
// TODO: Make it a cloud function & create process it
// TODO: Average April prices in CAM in 1995 .. 2016 (return year, value)
// TODO: Compare Cambridge and London (Line, WithLabels)





















//
