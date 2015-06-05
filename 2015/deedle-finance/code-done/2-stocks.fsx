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

let [<Literal>] Sample = __SOURCE_DIRECTORY__ + "/data/stocks/fb.csv"
type Stocks = CsvProvider<Sample>

// Get historical stock prices using F# Data
let stockPrices name = 
  let prices = Stocks.Load(__SOURCE_DIRECTORY__ + "/data/stocks/" + name + ".csv")
  [ for p in prices.Rows -> p.Date, float p.Open ]
  |> List.rev |> series

// Basic statistics using series
let msft = stockPrices "MSFT"
let msft13 = msft.[DateTime(2013, 1, 1) ..]

Stats.mean msft
Stats.mean msft13
Chart.Line(msft13)

// Compare current price with the mean over 2013
let avg13 = Stats.mean msft13

Chart.Combine
  [ Chart.Line(msft13)
    Chart.Line(msft13 - msft13 + avg13) ]

// Plot the current price together with weekly floating window
let msft13w = msft13 |> Series.windowInto 5 Stats.mean

Chart.Combine
  [ Chart.Line(msft13)
    Chart.Line(msft13w) ]

// Calculate the daily returns
let returns = (msft13 - (Series.shift 1 msft13)) / msft13 * 100.0 
Chart.Line(returns)

// Average daily returns over 1 month
returns
|> Series.windowInto 20 Stats.mean
|> Chart.Line

// ----------------------------------------------------------------------------
// Working with entire data frames
// ----------------------------------------------------------------------------

// Get data frame with some technology stocks
let tech =
  let stocks = [ "YHOO"; "GOOG"; "MSFT"; "FB" ]
  [ for stock in stocks -> stock => stockPrices stock ]
  |> frame 

// Calculate the daily returns for days when we have all data
let techAll = tech |> Frame.dropSparseRows
let techRet = (techAll - (Frame.shift 1 techAll)) / techAll * 100.0

// Average daily returns per company
techRet |> Stats.mean |> Chart.Column

// Average daily returns per day
techRet |> Frame.transpose |> Stats.mean |> Chart.Column

// ----------------------------------------------------------------------------
// Hierarchical indexing
// ----------------------------------------------------------------------------
  
let names = 
  [ "Technology", "YHOO"; "Technology", "GOOG"; "Technology", "MSFT"; "Technology", "FB"
    "Financial", "PRU"; "Financial", "V"; "Financial", "AXP.MX";
    "Consumer Goods", "AAPL"; "Consumer Goods", "CCE"; "Consumer Goods", "MCD" ]

let stocks = 
  [ for cat, stock in names ->
      (cat, stock) => stockPrices stock ]
  |> frame 

// Calculate the daily returns for days when we have all data
let stocksAll = stocks |> Frame.dropSparseRows
let stocksRet = (stocksAll - (Frame.shift 1 stocksAll)) / stocksAll * 100.0

// Average daily returns per company
let perComp = stocksRet |> Stats.mean
Chart.Column(perComp |> Series.mapKeys snd)

// Average daily returns per sector
perComp 
|> Stats.levelMean fst
|> Chart.Column
