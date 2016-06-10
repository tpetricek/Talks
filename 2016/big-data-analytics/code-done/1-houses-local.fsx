#nowarn "211"
#load "packages/FsLab/Themes/AtomChester.fsx"
#load "packages/FsLab/FsLab.fsx"
open Deedle
open System
open XPlot.GoogleCharts
open FSharp.Data

let data = __SOURCE_DIRECTORY__ + "/../data/"

// TODO: Read CSV pp-monthly-april-2016.csv
let apr16all =
  Frame.ReadCsv(data + "pp-monthly-april-2016.csv")

// TODO: Columns "Price";"Date";"Postcode";"Type";"Duration";"Street";"Town"
let apr16 = apr16all.Columns.[ ["Price";"Date";
  "Postcode";"Type";"Duration";"Street";"Town"] ]

// TODO: Sort by Price, get CAMBRIDGE, F, T and 2016 sales
apr16
|> Frame.sortRows "Price"

apr16
|> Frame.filterRows (fun k row ->
    row.GetAs("Town") = "CAMBRIDGE" &&
    row.GetAs("Duration") = "F" &&
    row.GetAs("Type") = "T" &&
    row.GetAs<DateTime>("Date").Year = 2016)
|> Frame.sortRows "Price"

// DEMO: Define 'cleaned' (Duration=F, Year=2016)
let cleaned =
  apr16
  |> Frame.filterRows (fun k row ->
    row.GetAs("Duration") = "F" &&
    row.GetAs<DateTime>("Date").Year = 2016)

// TODO: Aggregate rows by Town, mean Price, index by Town (dtto for Stats.count)
let avgPrices =
  cleaned
  |> Frame.aggregateRowsBy ["Town"] ["Price"] Stats.mean
  |> Frame.indexRowsString "Town"

let counts =
  cleaned
  |> Frame.aggregateRowsBy ["Town"] ["Price"] Stats.count
  |> Frame.indexRowsString "Town"

// TODO: Add column count (addCol)
let merged =
  avgPrices |> Frame.addCol "Count" (counts.GetColumn<int>("Price"))

// DEMO: Take top 20 sales & UK geo chart
let top20 =
  merged
  |> Frame.filterRows (fun k row -> row?Count > 100.0)
  |> Frame.sortRows "Price"
  |> Frame.takeLast 20

top20?Price
|> Series.observations
|> Chart.Geo
|> Chart.WithOptions
    (Options(region="GB", displayMode="markers",
      colorAxis=ColorAxis(colors=[|"#3B7732";"#7A6942";"#7C1C1C"|])))
