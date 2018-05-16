#load "packages/FsLab/FsLab.fsx"
open FSharp.Data.Runtime.WorldBank
open System.Collections.Generic
open FSharp.Data
open XPlot.GoogleCharts

let wb = WorldBankData.GetDataContext()

wb.Countries.Germany.Indicators.``CO2 emissions (kt)``
|> Chart.Line

wb.Countries.``United States``.Indicators.``CO2 emissions (kt)``
|> Chart.Line


type W = JsonProvider<"http://api.openweathermap.org/data/2.5/forecast/daily?q=Prague&mode=json&units=metric&cnt=10&APPID=cb63a1cf33894de710a1e3a64f036a27">

let getTemps city = 
  let w = W.Load("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + city + "&mode=json&units=metric&cnt=10&APPID=cb63a1cf33894de710a1e3a64f036a27")
  [ for d in w.List -> d.Temp.Day ]

[ Seq.indexed (getTemps "Berlin")
  Seq.indexed (getTemps "London")
  Seq.indexed (getTemps "Prague") ]
|> Chart.Column
|> Chart.WithLabels ["Berlin"; "London"; "Prague"]
