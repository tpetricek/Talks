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
// Getting data for clustering countries
// ----------------------------------------------------------------------------

// Get frame with multiple indiciators about countries
let world = 
  [ for c in wb.Countries ->
      c.Name => series [ 
        "Electricity" => c.Indicators.``Access to electricity, rural (% of population)``.[2010]
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


let norm = 
  world.Columns 
  |> Series.map (fun k r ->
        r |> Series.fillMissingWith (avg.[k]) )
  |> Frame.ofColumns

// Draw a scatter plot comparing GDP and Life expectancy
Seq.zip norm?GDP.Values norm?Life.Values
|> Chart.Scatter
|> Chart.WithOptions(Options(hAxis=Axis(title="GDP"), vAxis=Axis(title="Expectancy")))
