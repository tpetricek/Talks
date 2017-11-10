#load "packages/FsLab/FsLab.fsx"
open Deedle
open FSharp.Data
open XPlot.Plotly

let wb = WorldBankData.GetDataContext()

wb.Countries.China.Indicators.``CO2 emissions (kt)``
|> Chart.Line

[ wb.Countries.``United Kingdom``.Indicators.``CO2 emissions (metric tons per capita)``
  wb.Countries.``United States``.Indicators.``CO2 emissions (metric tons per capita)``
  wb.Countries.``China``.Indicators.``CO2 emissions (metric tons per capita)`` ]
|> Chart.Line
|> Chart.WithLabels ["UK"; "USA"; "China"]