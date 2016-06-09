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

open RProvider
open RProvider.stats
open RProvider.quantmod
open RDotNet

BigDeedle.Trades.EnableLogging()
let cluster = Config.GetCluster()

let dt (y, m, d) (hh, mm, ss) =
  DateTimeOffset(DateTime(y, m, d, hh, mm, ss), TimeSpan.FromMinutes(0.0))

// ------------------------------------------------------------------------------------------------
// LOCAL STUFF
// ------------------------------------------------------------------------------------------------

let wdc = BigDeedle.Trades.GetFrame("WDC")

wdc

wdc?Ask

wdc.Rows

wdc.Rows
|> Series.map (fun _ row ->
    Math.Round(row?Ask - row?Bid, 2))

wdc?Diff <- ((wdc?Ask - wdc?Bid) / wdc?Ask) * 100.0

wdc.Rows.[dt (2009,9,28) (9,45,0) .. dt (2009,9,28) (15,40,0)]?Diff
|> Series.mapKeys (fun k -> k.DateTime)
|> Stats.movingMean 500
|> Series.observations
|> Chart.Line


let vals =
  wdc.Rows.[dt (2009,9,28) (9,45,0) .. dt (2009,9,28) (15,40,0)]?Ask
  |> Series.sampleTimeInto (TimeSpan.FromMinutes 1.0) Direction.Forward Stats.mean
  |> Series.values

Series.ofValues(R.Delt(vals).AsNumeric()) * 10000.0

// ------------------------------------------------------------------------------------------------
// RESET REPL
// ------------------------------------------------------------------------------------------------

#time

let meanMinuteReturns id day =
  let prices = BigDeedle.Trades.GetFrame(id)?Price
  let byMinute =
    prices.[dt day (10,0,0) .. dt day (15,0,0)]
    |> Series.sampleTimeInto (TimeSpan.FromMinutes 1.0)
        Direction.Forward Stats.mean
  R.mean(R.na_omit(R.Delt(byMinute.Values))).AsNumeric().[0]

meanMinuteReturns "IVE" (2009,9,28)

cloud { return meanMinuteReturns "IVE" (2009,9,28) }
|> cluster.Run

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

// Get the returns for a single month
let q1 =
  [ returnsForMonth "WDC" 2015 1
    returnsForMonth "WDC" 2015 2
    returnsForMonth "WDC" 2015 3 ]
  |> Cloud.Parallel |> cluster.CreateProcess

q1.Result
q1.Status

for log in q1.GetLogs() do
  printfn "[%s] %s" (log.DateTime.ToString("T")) log.Message

q1.Result
|> Array.map (series >> Series.realign [0 .. 31] >> Series.fillMissingWith 0.0)
|> Chart.Line



// ..
