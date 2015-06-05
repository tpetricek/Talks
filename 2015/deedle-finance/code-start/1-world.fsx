#load "packages/FsLab/FsLab.fsx"
open System
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

// ----------------------------------------------------------------------------
// Getting and visualizing population data from WorldBank
// ----------------------------------------------------------------------------

// TODO: Get population for all countries in 2000 and 2010
// TODO: Visualize the population using Geo chart
// TODO: Calculate growth and visualize it


// ----------------------------------------------------------------------------
// Getting data for clustering countries
// ----------------------------------------------------------------------------

// DEMO: Get frame with multiple indiciators about countries
// TODO: Explore the data ('lo', 'hi' and 'avg')
// TODO: Draw correlations using the R type provider
// DEMO: Fill missing data with averages
// DEMO: Draw scatter plot comparing GDP & Life (Values)