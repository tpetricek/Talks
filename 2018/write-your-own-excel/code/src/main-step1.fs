module Spreadsheet
open Fable.Core
open Fable.Import
open Elmish
open Parsec

// ----------------------------------------------------------------------------

// STEP #1
//
// TODO: Define Position and State with Rows, Cols, Active & Cells
// DEMO: Create an initial state
// DEMO: app "main" initial render update
// TODO: Define silly `render` and `update` functions  
// TODO: Render grid in `render` and call `renderCell trigger pos state`

// ----------------------------------------------------------------------------

type Position = char * int

type State = 
  { Rows : int list
    Cols : char list 
    Active : Position option 
    Cells : Map<Position, string> }

let initial = 
  { Rows = [ 1 .. 15 ]
    Cols = [ 'A' .. 'K' ]
    Active = None
    Cells = Map.empty }

let renderCell trigger pos state = 
  h?td [] [ text "..." ]

let render trigger state = 
  h?table [] [
    yield h?tr [] [
      yield h?th [] []
      for col in state.Cols -> h?th [] [ text (string col) ]
    ]
    for row in state.Rows -> h?tr [] [
      yield h?th [] [ text (string row) ]
      for col in state.Cols -> renderCell trigger (col, row) state

    ]
  ]

let update state event = state

app "main" initial render update

// ----------------------------------------------------------------------------

// STEP #2
//
// DEMO: Rendering functions `renderEditor` and `renderView` 
// TODO: Define `Event` with `StartEdit` and `UpdateValue`
// TODO: "onclick" =!> fun _ -> trigger(StartEdit(pos)) & update `update` function
// DEMO: "oninput" =!> fun d -> trigger (UpdateValue(pos, unbox d?target?value)) & `update`

// ----------------------------------------------------------------------------

// STEP #3
//
// TODO: Define `Expr` as either `Number` or `Binary`
// TODO: Operator is `char '+'` etc.
// TODO: Number is `integer` mapped into `Number`
// TODO: Define binary and expr is number or binary
// TODO: `run expr input`

// ----------------------------------------------------------------------------

// STEP #4
//
// TODO: Add recursive evaluator
// DEMO: let ops = dict ['+', (+); '-', (-); '*', (*); '/', (/)]
// DEMO: Parse expressions with brackets
// TODO: Add `Reference of Position` case
// TODO: `reference = letter <*> integer |> map Reference` in the parser

// ----------------------------------------------------------------------------

// STEP #5
//
// TODO: Return option from `evaluate` and use `Option.bind`
// DEMO: "style" => if Option.isNone value then "background:#ffe0e0" else ""
// TODO: Add recursion check to the evaluator (using a set)

// ----------------------------------------------------------------------------




