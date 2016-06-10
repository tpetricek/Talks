#nowarn "211"
#load "packages/FsLab/Themes/AtomChester.fsx"
#load "packages/FsLab/FsLab.fsx"
#r "bigdeedle/Deedle.BigSources.dll"

open System
open Deedle
open XPlot.GoogleCharts
open XPlot.GoogleCharts.Deedle

// Disable or enable logging of downloads
BigDeedle.Houses.DisableLogging()
BigDeedle.Houses.EnableLogging()

/// Returns DateTimeOffset for the specified date and time in GMT
let dt (y, m, d) =
  DateTimeOffset(DateTime(y, m, d), TimeSpan.FromMinutes(0.0))

/// Turns virtual frame into in-memory frame
let materialize (df:Frame<_, _>) =
  df.Rows |> Series.observations |> Frame.ofRows

// Get all house price data
let housesAll = BigDeedle.Houses.GetFrame()



// TODO: Columns "Price";"Date";"Postcode";"Type";"Duration";"Street";"Town"
// TODO: Look at the frame & Street column
// TODO: Most expensive F, CAM houses in (2010,4,1) .. (2010,5,1)
// TODO: Materialize (2010,4,1) .. (2010,5,1) as 'april2010'
// DEMO: Take top 20 and draw a chart

























//
