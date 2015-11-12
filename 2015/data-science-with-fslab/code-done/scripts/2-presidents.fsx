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
//let [<Literal>] Sample = __SOURCE_DIRECTORY__ + "/data/presidents.html"
type Presidents = HtmlProvider<Sample>

let filterNoise s = 
  Regex.Replace(s, "(\[[^\]]*\])|(\([^\)]*\))", "").Trim()

let parseYears (s:string) = 
  match filterNoise(s).Split('-') with
  | [| f; t |] when t.Trim() = "Incumbent" -> DateTime.Parse(f),  DateTime.Today
  | [| f; t |] -> DateTime.Parse(f),  DateTime.Parse(t)
  | _ -> failwith "Unexpected date format"

let presidents = 
  [ for p in Presidents.GetSample().Tables.``List of presidents``.Rows do
      let f, t = parseYears p.``Term of office``
      yield filterNoise p.President2, f.Year, t.Year, p.Party ]
  |> Seq.distinctBy id
  |> Frame.ofRecords
  |> Frame.indexColsWith ["Name"; "Start"; "End"; "Party"]
  |> Frame.filterRowValues (fun row -> row?End > 1900.0)  

// ----------------------------------------------------------------------------
// Analysing debt change during presidential terms
// ----------------------------------------------------------------------------

// For each year, find the corresponding president and party
let byStart = presidents |> Frame.indexRowsInt "Start"
let yearDebt = debt.Join(byStart, JoinKind.Left, Lookup.ExactOrSmaller)

let groups = 
  yearDebt
  |> Frame.groupRowsByString "Name"
  |> Frame.nest
  |> Series.observations

Chart.Column [for k, v in groups -> v?Debt ]
|> Chart.WithLabels [for k, v in groups -> k ]
|> Chart.WithOptions(Options(isStacked=true, legend=Legend(textStyle=TextStyle(fontSize=10))))
|> Chart.WithLegend(true)

// Get debt at the end of the presidential term
let byEnd = presidents |> Frame.indexRowsInt "End"
let presDebt = byEnd.Join(debt, JoinKind.Left)


// Calculate change for each president
presDebt?Difference <-
  presDebt?Debt 
  |> Series.pairwiseWith (fun _ (prev, curr) -> curr - prev)

// Compare Republican and Democratic presidents
let partyDebts =
  presDebt
  |> Frame.groupRowsByString "Party"
  |> Frame.getCol "Debt"
  |> Stats.levelMean fst

Chart.Pie(partyDebts)
|> Chart.WithLabels partyDebts.Keys
|> Chart.WithLegend true
