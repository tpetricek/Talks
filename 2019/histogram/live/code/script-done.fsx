#nowarn "85" "10001"
#load "packages/FsLab/FsLab.fsx"
open Deedle
open XPlot.Plotly

System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

// TODO: Load clean/avia.csv
// TODO: Filter rows to get non aggregate KILs
// TODO: Sum and drop missing columns
// DEMO: Do the same for rail transport
// DEMO: Create a column chart

let rail = Frame.ReadCsv("clean/rail.csv")
let avia = Frame.ReadCsv("clean/avia.csv")

let rk = 
  rail 
  |> Frame.filterRows (fun _ row -> 
    row.GetAs "victim" = "KIL" && row.GetAs "accident" = "TOTAL" && 
      row.GetAs "geo" <> "EU28" && row.GetAs "pers_inv" = "TOTAL")
  |> Stats.sum
  |> Series.dropMissing


let ak = 
  avia
  |> Frame.filterRows (fun _ row -> 
    row.GetAs "victim" = "KIL" && row.GetAs "geo" <> "EU28" )
  |> Stats.sum
  |> Series.dropMissing

let aligned = 
  frame [ "Train" => rk; "Air" => ak ] 
  |> Frame.mapRowKeys int
  |> Frame.sortRowsByKey
  |> Frame.filterRows (fun k _ -> k > 2005)

Chart.Column
  [ Series.observations aligned?Train
    Series.observations aligned?Air ]
|> Chart.WithLabels ["Train"; "Air"]
