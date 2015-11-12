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


// ----------------------------------------------------------------------------
// Using JSON type provider to get weather information 
// ----------------------------------------------------------------------------


// TODO: Get current weather using OpenWeatherMap API
//   http://api.openweathermap.org/data/2.5/weather?q=Stockholm&APPID=cb63a1cf33894de710a1e3a64f036a27
// TODO: Write getWeather function and plot weather in all countries


// ----------------------------------------------------------------------------
// Getting data for clustering countries
// ----------------------------------------------------------------------------


// DEMO: Get frame with multiple indiciators about countries
// TODO: Calculate mean of the data sets
// TODO: Plot correlation using R plot
// DEMO: Draw a scatter plot comparing GDP and Life expectancy
