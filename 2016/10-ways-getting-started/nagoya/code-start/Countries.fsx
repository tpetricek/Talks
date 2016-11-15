#load "packages/FsLab/FsLab.fsx"
open Deedle
open FSharp.Data
open XPlot.GoogleCharts
open XPlot.GoogleCharts.Deedle

let wb = WorldBankData.GetDataContext()

// DEMO: Population growth in Brazil and Argentina