#load "packages/FsLab/FsLab.fsx"
open Deedle
open FSharp.Data
open XPlot.GoogleCharts

let wb = WorldBankData.GetDataContext()

// GINI index in the US and UK over time
// GINI index in 2010 in all countries (filter nan)
