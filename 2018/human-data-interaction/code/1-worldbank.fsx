#load "packages/FsLab/FsLab.fsx"
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

let wb = WorldBankData.GetDataContext()

// GINI index in the US and UK over time
// GINI index in 2010 in all countries (filter nan)

[ for c in wb.Countries do
    let v = c.Indicators.``GINI index (World Bank estimate)``.[2010]
    if not (System.Double.IsNaN v) then
      yield c.Name, v ]
|> Chart.Geo
