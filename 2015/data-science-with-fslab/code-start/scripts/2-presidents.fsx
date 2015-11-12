#load "packages/FsLab/FsLab.fsx"
open System
open Deedle
open FSharp.Data
open XPlot.GoogleCharts
open XPlot.GoogleCharts.Deedle
open System.Text.RegularExpressions

// ----------------------------------------------------------------------------
// Getting debt data
// ----------------------------------------------------------------------------


// TODO: Read frame from CSV: __SOURCE_DIRECTORY__ + "/data/us-debt.csv"
// DEMO: Index by Year and rename columns
// TODO: Get WorldBank data and add to a frame as GDP_WB
// DEMO: Create a chart for comparison & get 'debt'


// ----------------------------------------------------------------------------
// Get information about US presidents from Freebase
// ----------------------------------------------------------------------------


// TODO: Get information about presidents based on
//   http://en.wikipedia.org/wiki/List_of_Presidents_of_the_United_States
// DEMO: Filter noise and parse years 
// TODO: Return list of name, start year, end year, party
// DEMO: Create data from records, index and filter


// ----------------------------------------------------------------------------
// Analysing debt change during presidential terms
// ----------------------------------------------------------------------------


// DEMO: For each year, find the corresponding president
// DEMO: Group by name and visualize
// DEMO: Calculate average debt per party