#load "packages/FsLab/FsLab.fsx"
open System
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

// ----------------------------------------------------------------------------
// Getting and visualizing population data from WorldBank
// ----------------------------------------------------------------------------

// TODO: Plot population in 2000 and 2010
// TODO: Calculate and plot population growth

let wb = WorldBankData.GetDataContext()

let pop2000 = 
  series [ for c in wb.Countries -> 
              c.Name, c.Indicators.``Population, total``.[2000] ]
let pop2010 = 
  series [ for c in wb.Countries -> 
              c.Name, c.Indicators.``Population, total``.[2010] ]

let growth = (pop2010 - pop2000) / pop2000 * 100.0

Chart.Geo(growth)

// ----------------------------------------------------------------------------
// Using JSON type provider to get weather information 
// ----------------------------------------------------------------------------


// TODO: Get current weather using OpenWeatherMap API
//   http://api.openweathermap.org/data/2.5/weather?q=Stockholm&APPID=cb63a1cf33894de710a1e3a64f036a27
// TODO: Write getWeather function and plot weather in all countries

type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/weather?q=London&APPID=cb63a1cf33894de710a1e3a64f036a27">

let getWeather n = 
  let w = Weather.Load("http://api.openweathermap.org/data/2.5/weather?APPID=cb63a1cf33894de710a1e3a64f036a27&q=" + n)
  float w.Main.Temp - 273.15

let temps = 
  [ for c in wb.Countries ->
      try
        printfn "%s" c.Name
        Some(c.Name, getWeather(c.CapitalCity + "," + c.Name) )
      with _ -> None ] |> List.choose id

Chart.Geo(temps)

// ----------------------------------------------------------------------------
// Getting data for clustering countries
// ----------------------------------------------------------------------------


// DEMO: Get frame with multiple indiciators about countries
// TODO: Calculate mean of the data sets
// TODO: Plot correlation using R plot
// DEMO: Draw a scatter plot comparing GDP and Life expectancy
