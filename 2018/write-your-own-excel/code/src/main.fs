module Spreadsheet
open Fable.Core
open Fable.Import
open Elmish
open Parsec

// ----------------------------------------------------------------------------

type Position = char * int

type Expr =
  | Number of int
  | Binary of Expr * char * Expr
  | Reference of Position

type State = 
  { Cols : char list
    Rows : int list 
    Active : Position option
    Cells : Map<Position, string> }

type Event = 
  | StartEdit of Position
  | UpdateValue of Position * string

// ----------------------------------------------------------------------------

let number = integer |> map Number
let operator = 
  char '*' <|> char '+' <|>
  char '-' <|> char '/'
let reference = 
  letter <*> integer |> map Reference  

let exprSetter, expr = slot ()
let brack = 
  char '(' <*>> anySpace <*>> expr <<*> 
    anySpace <<*> char ')'

let term = number <|> brack <|> reference

let binary = 
  term <<*> anySpace <*> operator <<*> 
    anySpace <*> term 
  |> map (fun ((l,op), r) -> Binary(l, op, r))

let exprAux = binary <|> term
exprSetter.Set exprAux

let formula = char '=' <*>> anySpace <*>> expr
let equation = 
  anySpace <*>> (formula <|> number) <<*> anySpace 

// ----------------------------------------------------------------------------

let rec evaluate refs cells = function 
  | Number(n) -> 
      Some n

  | Reference(pos) when Set.contains pos refs ->
      None

  | Reference(pos) ->
      Map.tryFind pos cells |> Option.bind (fun code ->
        run equation code |> Option.bind (fun parsed ->
          evaluate (Set.add pos refs) cells parsed))

  | Binary(l, op, r) ->
      let ops = dict ['+', (+); '-', (-); '*', (*); '/', (/)]
      evaluate refs cells l |> Option.bind (fun le ->
        evaluate refs cells r |> Option.map (fun re ->
          ops.[op] le re))

// ----------------------------------------------------------------------------

let renderEditor trigger pos value =
  h?td ["class" => "selected"] [ 
    h?input [
      "oninput" =!> fun d -> trigger (UpdateValue(pos, unbox d?target?value))
      "id" => "celled"; "focused" => "true"
      "value" => value ] []
  ]

let renderView trigger pos value = 
  h?td [
    "onclick" =!> fun _ -> trigger(StartEdit pos)
    "style" => if Option.isNone value then "background:#ffe0e0" else ""
  ] [ text (defaultArg value "#ERR") ]

let renderCell trigger pos state = 
  let value = Map.tryFind pos state.Cells
  if state.Active = Some pos then 
    renderEditor trigger pos (defaultArg value "")
  else 
    match value with 
    | None -> renderView trigger pos (Some "")
    | Some value ->
        let res = 
          run equation value |> Option.bind (fun parsed ->
            evaluate Set.empty state.Cells parsed 
            |> Option.map string) 
        renderView trigger pos res

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

// ----------------------------------------------------------------------------

let update state = function
  | UpdateValue(pos, value) ->
      let newCells = Map.add pos value state.Cells
      { state with Cells = newCells }
  | StartEdit(pos) ->
      { state with Active = Some pos }

// ----------------------------------------------------------------------------

let initial = 
  { Rows = [ 1 .. 15 ]
    Cols = [ 'A' .. 'K' ]
    Active = None
    Cells = Map.empty }


app "main" initial render update 
