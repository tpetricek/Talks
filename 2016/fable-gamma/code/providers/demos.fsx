#nowarn "58"
#load "packages/FsLab/FsLab.fsx"
#r "lib/TheGamma.RestProvider.dll"
open TheGamma
open FSharp.Data
open XPlot.GoogleCharts

// TODO: World bank and type providers
let wb = WorldBankData.GetDataContext()

wb.Countries.``United States``
  .Indicators
  .``GDP per capita (current US$)``
|> Seq.toList
|> Chart.Line

// DEMO: Pivot and REST providers
type olympics = 
  RestProvider<"http://localhost:10042/pivot", 1000, 
    "source=http://localhost:10042/olympics">

let o = 
  olympics.data
    .``group data``.``by Team``
        .``average Year``
        .``sum Gold``
        .``sum Silver``
        .``sum Bronze``
        .``count distinct Athlete``
        .``then``
    .``sort data``.``by Gold descending``.``then``
    .``get the data``

for c in o do
  printfn "%s (%A)" c.Team c.Year
