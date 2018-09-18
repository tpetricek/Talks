#load "packages/FsLab/FsLab.fsx"
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

let wb = WorldBankData.GetDataContext()

wb.Countries.``United States``.Indicators.``GINI index (World Bank estimate)``
|> Chart.Line

[ for c in wb.Countries -> c.Name, c.Indicators.``GINI index (World Bank estimate)``.[2010] ]
|> List.filter (fun (_, v) -> not (System.Double.IsNaN v))
|> Chart.Geo