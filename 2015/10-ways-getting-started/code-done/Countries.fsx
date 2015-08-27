#load "packages/FsLab/FsLab.fsx"
open Deedle
open FSharp.Data
open XPlot.GoogleCharts
open XPlot.GoogleCharts.Deedle

let wb = WorldBankData.GetDataContext()

[ wb.Countries.Brazil.Indicators.``Population growth (annual %)``
  wb.Countries.Argentina.Indicators.``Population growth (annual %)`` ]
|> Chart.Line

let pop2000 = 
  [ for c in wb.Countries -> 
      c.Name, c.Indicators.``Total Population (in number of people)``.[2000] ] |> series
let pop2010 = 
  [ for c in wb.Countries -> 
      c.Name, c.Indicators.``Total Population (in number of people)``.[2010] ] |> series

Chart.Geo( (pop2010 - pop2000)/pop2000 * 100.0)
