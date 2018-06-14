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

type Position = char * int

type Expr = 
  | Number of int
  | Binary of Expr * char * Expr
  | Reference of Position

type Event = 
  | StartEdit of Position
  | UpdateValue of Position * string

type State = 
  { Cols : char list 
    Rows : int list 
    Active : Position option
    Cells : Map<Position, string> }

let number = integer |> map Number
let operator = char '+' <|> char '*' <|> char '-' <|> char '/'
let reference = letter <*> integer |> map Reference

let exprSetter, expr = slot ()
let brack = char '(' <*>> anySpace <*>> expr <<*> anySpace <<*> char ')'
let term = number <|> brack <|> reference
let binary = term <<*> anySpace <*> operator <<*> anySpace <*> term |> map (fun ((l,op), r) -> Binary(l, op, r))
let exprAux = binary <|> term
exprSetter.Set exprAux

let formula = char '=' <*>> anySpace <*>> expr
let equation = anySpace <*>> (formula <|> number) <<*> anySpace 

let initial = 
  { Rows = [ 1 .. 15 ]
    Cols = [ 'A' .. 'K' ]
    Active = None
    Cells = Map.empty }

let rec evaluate cells = function
  | Number(n) -> n

  | Reference(pos) ->
      let code = Map.find pos cells
      let expr = run equation code
      evaluate cells expr.Value

  | Binary(l, op, r) ->
      let ops = dict ['+', (+); '-', (-); '*', (*); '/', (/)]
      let l, r = evaluate cells l, evaluate cells r
      ops.[op] l r

let renderEditor trigger pos value =
  h?td ["class" => "selected"] [ 
    h?input [
      "id" => "celled"; "focused" => "true"
      "oninput" =!> fun d -> trigger (UpdateValue(pos, unbox d?target?value))
      "value" => value ] []
  ]

let renderView trigger pos value = 
  h?td [
    "onclick" =!> fun _ -> trigger(StartEdit pos)      
  ] [ text value ]

let renderCell trigger pos state = 
  let value = Map.tryFind pos state.Cells
  if state.Active = Some pos then 
    renderEditor trigger pos (defaultArg value "")
  else 
    match value with
    | None -> renderView trigger pos ""
    | Some code ->
        let expr = run equation code
        let res = evaluate state.Cells expr.Value
        renderView trigger pos (string res)

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

let update state = function
  | StartEdit(pos) -> { state with Active = Some pos }
  | UpdateValue(pos, value) ->
      let newCells = Map.add pos value state.Cells
      { state with Cells = newCells }

app "main" initial render update

// ----------------------------------------------------------------------------

// STEP #5
//
// TODO: Return option from `evaluate` and use `Option.bind`
// DEMO: "style" => if Option.isNone value then "background:#ffe0e0" else ""
// TODO: Add recursion check to the evaluator (using a set)

// ----------------------------------------------------------------------------




