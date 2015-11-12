#load "packages/FsLab/FsLab.fsx"
open System
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

// ----------------------------------------------------------------------------
// Getting and visualizing population data from WorldBank
// ----------------------------------------------------------------------------

let wb = WorldBankData.GetDataContext()

// Get population for all countries in 2000 and 2010
let pop2000 = series [ for c in wb.Countries -> c.Name => c.Indicators.``Population, total``.[2000]]
let pop2010 = series [ for c in wb.Countries -> c.Name => c.Indicators.``Population, total``.[2010]]

// Visualize the population using Geo chart
Chart.Geo(Series.observations pop2010)

// Calculate growth and visualize growth
let growth = (pop2010 - pop2000) / pop2000 * 100.0
Chart.Geo(Series.observations growth)


// ----------------------------------------------------------------------------
// Using JSON type provider to get weather information 
// ----------------------------------------------------------------------------

// Get a type for calling OpenWeatherMap API
type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/weather?q=Stockholm&APPID=cb63a1cf33894de710a1e3a64f036a27">

// Function to get weather in a specified city/country
let getWeather n = 
  let w = Weather.Load("http://api.openweathermap.org/data/2.5/weather?APPID=cb63a1cf33894de710a1e3a64f036a27&q=" + n)
  float w.Main.Temp - 273.15

// Get current temperature in World's capitals
let temps = 
  [ for c in wb.Countries -> 
      printfn "%s" c.Name
      c.Name, getWeather(c.CapitalCity + "," + c.Name) ]

Chart.Geo(temps)


// ----------------------------------------------------------------------------
// Getting data for clustering countries
// ----------------------------------------------------------------------------

// Get frame with multiple indiciators about countries
let world = 
  [ for c in wb.Countries ->
      c.Name => series [ 
        "Electricity" => c.Indicators.``Access to electricity (% of population)``.[2010]
        "Life" => c.Indicators.``Life expectancy at birth, total (years)``.[2010]
        "GDP" => c.Indicators.``GDP per capita (current US$)``.[2010]
        "Growth" => c.Indicators.``GDP per capita growth (annual %)``.[2010]
        "CO2" => c.Indicators.``CO2 emissions (metric tons per capita)``.[2010]
        "Births" => c.Indicators.``Population growth (annual %)``.[2010] ] ]
  |> Frame.ofRows

// Explore the data interactively
let lo = world |> Stats.min
let hi = world |> Stats.max
let avg = world |> Stats.mean


// Draw correlations using the R type provider
open RProvider
open RProvider.graphics

R.plot(world)

// Fill missing values with averages
let norm = 
  world.Columns 
  |> Series.map (fun k r ->
        r |> Series.fillMissingWith (avg.[k]) )
  |> Frame.ofColumns

// Draw a scatter plot comparing GDP and Life expectancy
let options = 
  Options
    ( pointSize=4, colors=[|"#3B8FCC"|], 
      trendlines=[|Trendline(opacity=0.5,lineWidth=10,color="#C0D9EA")|],
      hAxis=Axis(title="Log of GDP (per capita)"), 
      vAxis=Axis(title="Life expectancy (years)") )

Series.zipInner (log10 norm?GDP) norm?Life
|> Series.values
|> Chart.Scatter
|> Chart.WithOptions(options)