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

open RProvider
open RProvider.stats
open RProvider.quantmod
open RDotNet


// Disable or enable logging of downloads
BigDeedle.Trades.EnableLogging()
BigDeedle.Trades.DisableLogging()

/// Returns connection to Azure cluster
let cluster = Config.GetCluster()

/// Returns DateTimeOffset for the specified date and time in GMT
let dt (y, m, d) (hh, mm, ss) =
  DateTimeOffset(DateTime(y, m, d, hh, mm, ss), TimeSpan.FromMinutes(0.0))

// Get Big Deedle frame with WDC trades
let wdc = BigDeedle.Trades.GetFrame("WDC")

// TODO: Explore WDC ask, rows
wdc
wdc?Ask
wdc.Rows

wdc.Rows
|> Series.map (fun _ row ->
    Math.Round(row?Ask - row?Bid, 2))

// TODO: Add Diff column with ask - bid prices
wdc?Diff <- ((wdc?Ask - wdc?Bid) / wdc?Ask) * 100.0

// DEMO: Get one day of data
// TODO: Plot moving mean 500 with DT keys
wdc.Rows.[dt (2009,9,28) (9,45,0) .. dt (2009,9,28) (15,40,0)]?Diff
|> Series.mapKeys (fun k -> k.DateTime)
|> Stats.movingMean 500
|> Series.observations
|> Chart.Line

// DEMO: 1 minute resampled averages
let vals =
  wdc.Rows.[dt (2009,9,28) (9,45,0) .. dt (2009,9,28) (15,40,0)]?Ask
  |> Series.sampleTimeInto (TimeSpan.FromMinutes 1.0) Direction.Forward Stats.mean
  |> Series.values

// TODO: Open quantmod, call Delt, AsNumeric, Series.ofValues
Series.ofValues(R.Delt(vals).AsNumeric()) * 10000.0


// TODO: RESET REPL (to avoid uploading data to cloud), #time

// DEMO: Mean mintue returns function
let meanMinuteReturns id day =
  let prices = BigDeedle.Trades.GetFrame(id)?Price
  let byMinute =
    prices.[dt day (10,0,0) .. dt day (15,0,0)]
    |> Series.sampleTimeInto (TimeSpan.FromMinutes 1.0)
        Direction.Forward Stats.mean
  R.mean(R.na_omit(R.Delt(byMinute.Values))).AsNumeric().[0]

// TODO: Run locally on IVE (2009,9,28) vs. cloud
meanMinuteReturns "IVE" (2009,9,28)

cloud { return meanMinuteReturns "IVE" (2009,9,28) }
|> cluster.Run

// DEMO: Returns for whole range of Q1 2015
let returnsForMonth id y m =
  let lastDay = DateTime(y,m,1).AddMonths(1).AddDays(-1.0).Day
  [| for d in 1 .. lastDay -> local {
      try
        do! Cloud.Logf "Processing %d/%d/%d" y m d
        let res = meanMinuteReturns id (y, m, d)
        return d, res
      with e ->
        do! Cloud.Logf "Failed: %A" e
        return 0, nan } |] |> Cloud.Parallel

// TODO: Status, print GetLogs() with "T" for DateTime
let q1 =
  [ returnsForMonth "WDC" 2015 1
    returnsForMonth "WDC" 2015 2
    returnsForMonth "WDC" 2015 3 ]
  |> Cloud.Parallel |> cluster.CreateProcess

q1.Result
q1.Status

for log in q1.GetLogs() do
  printfn "[%s] %s" (log.DateTime.ToString("T")) log.Message

// DEMO: Line chart with cleaned up data
q1.Result
|> Array.map (series >> Series.realign [0 .. 31] >> Series.fillMissingWith 0.0)
|> Chart.Line



// ..
