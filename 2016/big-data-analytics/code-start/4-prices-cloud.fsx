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
open RDotNet
open RProvider

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
// TODO: Add Diff column with ask - bid prices
// DEMO: Get one day of data
// TODO: Plot moving mean 500 with DT keys
// DEMO: 1 minute resampled averages
// TODO: Open quantmod, call Delt, AsNumeric, Series.ofValues




// TODO: RESET REPL, #time
// DEMO: Mean mintue returns function
// TODO: Run locally on IVE (2009,9,28) vs. cloud
// DEMO: Returns for whole range of Q1 2015
// TODO: Status, print GetLogs() with "T" for DateTime
// DEMO: Line chart with cleaned up data
