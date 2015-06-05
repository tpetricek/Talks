(*** hide ***)
#load "packages/FsLab/FsLab.fsx"
open System
open FsLab
open Deedle
open FSharp.Data
open XPlot.GoogleCharts
open XPlot.GoogleCharts.Deedle
(**
Analysing stock prices with F#
==============================

This journal demonstrates how to generate elegant reports from your FsLab
data analysis. In this demo, we use CSV type provider to read stock prices
from Yahoo finance, then we analyze the data using Deedle and we create a 
couple of charts to visualize the results using R type provider and F# Charting.

Microsoft stock prices
----------------------
We start by looking at individual time series - here, we use MSFT stock prices.
The following snippet shows how to read the data and get time series for the
year 2013:
*)
type Stocks = CsvProvider<"c:/data/stocks/fb.csv">

let stockPrices name = 
  let prices = Stocks.Load("c:/data/stocks/" + name + ".csv")
  [ for p in prices.Rows -> p.Date, float p.Open ]
  |> List.rev |> series

let msft = stockPrices "AAPL"
let msft13 = msft.[DateTime(2013, 1, 1) ..]
(*** include-value:msft13 |> Series.mapKeys (fun d -> d.ToShortDateString())***)
(** 
Now we can calculate the average price and draw a line chart: 
*)
(*** define-output: line ***)
Chart.Line(msft13)
(*** include-it: line ***)
(**
Next, we look at calculating daily returns for the MSFT stock in the year
2013. To do that, we use `Series.shift` to create a series containing values
for the preceding day. Then we calculate the daily returns. We also use the
R type provider to show a histogram with the values:
*)
(*** define-output: rhist ***)
open RProvider
open RProvider.graphics

let rets = (msft13 - Series.shift 1 msft13) / msft13 * 100.0
R.hist(rets)
(*** include-output: rhist ***)
(**
Analysing stocks by sector
--------------------------
In the next example, we look at working with entire data _frames_ rather than
with just individual time _series_. Deedle makes it easy to write calculations
over entire data frames, so the code will look very similar to what we wrote
before.

We start by loading data for a number of companies in different sectors:
*)
let names = 
  [ "Technology", "YHOO"; "Technology", "GOOG"; "Technology", "MSFT"; "Technology", "FB"
    "Financial", "PRU"; "Financial", "V"; "Financial", "AXP.MX";
    "Consumer Goods", "AAPL"; "Consumer Goods", "CCE"; "Consumer Goods", "MCD" ]

let stocks = 
  frame [ for cat, stock in names -> (cat, stock) => stockPrices stock ]
  |> Frame.sortRowsByKey |> Frame.sortColsByKey

let stocksAll = Frame.dropSparseRows stocks
(*** include-value:stocksAll |> Frame.mapRowKeys (fun d -> d.ToShortDateString()) ***)
(**
Standard F# numerical operators can work on frames just like they worked on series.
This means that the calculation of returns on frames looks just like the calculation
on series. We just need to replace `Series.shift` with `Frame.shift`:
*)
let stocksAllRets = (stocksAll - (Frame.shift 1 stocksAll)) / stocksAll * 100.0
(**
Finally, we calculate averages and visualize the summarized results using the F# Charting
library. The following snippet creates a chart showing averages per companies:
*)
(*** define-output: percomp ***)
stocksAllRets
|> Stats.mean
|> Series.mapKeys snd
|> Chart.Column
(*** include-it: percomp ***)
(**
The following snippet performs similar calculation, but averages returns per sector:
*)
(*** define-output: persec ***)
stocksAllRets
|> Stats.mean
|> Stats.levelMean fst
|> Chart.Column
(*** include-it: persec ***)