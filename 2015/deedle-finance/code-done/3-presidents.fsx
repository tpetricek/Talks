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

// Loading US debt data from CSV file & making the data set nicer
let debtData = 
  Frame.ReadCsv(__SOURCE_DIRECTORY__ + "/data/us-debt.csv")
  |> Frame.indexRowsInt "Year"
  |> Frame.indexColsWith ["GDP"; "Population"; "Debt"; "Kind" ]

// Compare the GDP data with what we can get from WorldBank
let wb = WorldBankData.GetDataContext()
let wbGdp = series wb.Countries.``United States``.Indicators.``GDP (current US$)``

// Compare data from us-debt CSV file and WorldBank
debtData?GDP_WB <- wbGdp / 1.0e9

Chart.Line [ debtData?GDP; debtData?GDP_WB ]
|> Chart.WithLabels ["CSV file"; "WorldBank"]
|> Chart.WithLegend true

// For the rest of the analysis, we need just the Debt
let debt = debtData.Columns.[ ["Debt"] ]

// ----------------------------------------------------------------------------
// Get information about US presidents from Freebase
// ----------------------------------------------------------------------------

let [<Literal>] Sample = "http://en.wikipedia.org/wiki/List_of_Presidents_of_the_United_States"
let [<Literal>] Sample = __SOURCE_DIRECTORY__ + "/data/presidents.html"
type Presidents = HtmlProvider<Sample>

let parseName t = 
  Regex.Match(t, "[^\(]*").Groups.[0].Value
let parseYear t = 
  let y = Regex.Match(t, ".*([0-9][0-9][0-9][0-9])").Groups.[1].Value
  if y = "" then DateTime.Now.Year else int y

let presidents = 
  [ for p in Presidents.GetSample().Tables.``List of presidents``.Rows do
      yield parseName p.President2, parseYear p.``Took office``, parseYear p.``Left office``, p.Party ]
  |> Seq.distinctBy id
  |> Frame.ofRecords
  |> Frame.indexColsWith ["Name"; "Start"; "End"; "Party"]
  |> Frame.filterRowValues (fun row -> row?End > 1900.0)  

// ----------------------------------------------------------------------------
// Analysing debt change during presidential terms
// ----------------------------------------------------------------------------

// Get debt at the end of the presidential term
let byEnd = presidents |> Frame.indexRowsInt "End"
let presDebt = byEnd.Join(debt, JoinKind.Left)

// For each year, find the corresponding president and party
let byStart = presidents |> Frame.indexRowsInt "Start"
let yearDebt = debt.Join(byStart, JoinKind.Left, Lookup.ExactOrSmaller)

// Calculate change for each president
presDebt?Difference <-
  presDebt?Debt 
  |> Series.pairwiseWith (fun _ (prev, curr) -> curr - prev)

// Compare Republican and Democratic presidents
let debtByParty = 
  presDebt
  |> Frame.groupRowsByString "Party"
  |> Frame.getCol "Debt"
  |> Stats.levelMean fst
(*

open XPlot.GoogleCharts

let byName = 
  yearDebt
  |> Frame.groupRowsByString "Name"
  |> Frame.nest

Chart.Column(byName.Values |> Seq.map (fun f -> Series.observations f?Debt) |> Seq.take 2)
|> Chart.WithLabels ["Yo"; "yoyo"]

Chart.Column
  (  )
|> Chart.WithLegend true

|> Chart.WithLabels byName.Keys

*)