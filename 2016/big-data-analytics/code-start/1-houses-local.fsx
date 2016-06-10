#nowarn "211"
#load "packages/FsLab/Themes/AtomChester.fsx"
#load "packages/FsLab/FsLab.fsx"
open Deedle
open System
open XPlot.GoogleCharts
open FSharp.Data

let data = __SOURCE_DIRECTORY__ + "/../data/"

// TODO: Read CSV pp-monthly-april-2016.csv
// TODO: Columns "Price";"Date";"Postcode";"Type";"Duration";"Street";"Town"
// TODO: Sort by Price, get CAMBRIDGE, F, T and 2016 sales
// DEMO: Define 'cleaned' (Duration=F, Year=2016)
// TODO: Aggregate rows by Town, mean Price, index by Town (dtto for Stats.count)
// TODO: Add column count (addCol)
// DEMO: Take top 20 sales & UK geo chart
