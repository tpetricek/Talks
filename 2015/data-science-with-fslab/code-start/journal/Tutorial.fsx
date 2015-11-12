(*** hide ***)
#load "packages/FsLab/FsLab.fsx"
open FsLab
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

// DEMO: Add some Markdown comments

let wb = WorldBankData.GetDataContext()

let pop2000 = series [ for c in wb.Countries -> 
  c.Name, c.Indicators.``Population, total``.[2000] ]
let pop2010 = series [ for c in wb.Countries -> 
  c.Name, c.Indicators.``Population, total``.[2010] ]

// DEMO: Embed a geo chart
// DEMO: Add another chart

// DEMO: Indicator correlations section
// DEMO: GDP and Life expectancy correlation
