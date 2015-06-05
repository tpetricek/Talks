#load "packages/FsLab/FsLab.fsx"
open FsLab
open System
open Deedle
open RProvider
open FSharp.Data
open FSharp.Charting

// ----------------------------------------------------------------------------
// Getting data
// ----------------------------------------------------------------------------

// TODO: Use CSV type provider on C:/data/stocks/fb.csv
// DEMO: Load historical stock prices
// TODO: Get "MSFT" (msft) from 2013-01-01 (msft13), mean & chart
// TODO: Chart prices with weekly average (Stats.movingMean)
// DEMO: Calculate daily returns


// ----------------------------------------------------------------------------
// Working with entire data frames
// ----------------------------------------------------------------------------

// DEMO: Get data frame and drop sparse rows
// TODO: Calculate daily returns over a frame
// TODO: Average daily return per company


// ----------------------------------------------------------------------------
// Hierarchical indexing
// ----------------------------------------------------------------------------
  
// DEMO: Get data with hierarchical index
// DEMO: Average prices per company & per sector
