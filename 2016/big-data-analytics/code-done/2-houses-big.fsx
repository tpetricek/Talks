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

// Filter the columns to show only useful ones
let houses = housesAll.Columns.[ ["Price";"Postcode";
  "Type";"Duration";"Street";"Town"] ]

// In Ionide - show the results as inline frame
houses.GetColumn<string>("Street")
houses

// Find most expensive houses in a town
houses.Rows.[dt(2010,4,1) .. dt(2010,5,1)]
|> Frame.filterRows (fun k row ->
    row.GetAs("Town") = "CAMBRIDGE" &&
    row.GetAs("Duration") = "F")
|> Frame.sortRows "Price"


// Get a range of data and materialize it
let april2010 =
  houses.Rows.[dt(2010,4,1) .. dt(2010,5,1)]
  |> materialize


// Create a frame with average prices and total
// counts for each town in the given data frame
let avgPrices =
  april2010
  |> Frame.aggregateRowsBy ["Town"] ["Price"] Stats.mean
  |> Frame.indexRowsString "Town"

let counts =
  april2010
  |> Frame.aggregateRowsBy ["Town"] ["Price"] Stats.count
  |> Frame.indexRowsString "Town"

let merged =
  avgPrices |> Frame.addCol "Count" (counts.GetColumn<int>("Price"))

// Get 20 most expensive towns with at least 20 sales
let top20 =
  merged
  |> Frame.filterRows (fun k row -> row?Count > 100.0)
  |> Frame.sortRows "Price"
  |> Frame.takeLast 20

// Draw a pretty chart using XPlot
top20?Price
|> Series.observations
|> Chart.Geo
|> Chart.WithOptions
    (Options(region="GB", displayMode="markers",
      colorAxis=ColorAxis(colors=[|"#4D5B2F";"#7F4931";"#6B1A20"|])))
