module Spreadsheet
open Fable.Core
open Fable.Import
open Elmish
open Parsec

// ----------------------------------------------------------------------------

type Position = char * int

type State = 
  { Cols : char list
    Rows : int list 
    Active : Position option
    Cells : Map<Position, string>  }

type Event = 
  | StartEdit of Position
  | UpdateValue of Position * string

type Expr = 
  | Number of int
  | Binary of Expr * char * Expr 
  | Reference of Position

// ----------------------------------------------------------------------------

let operator =
  char '+' <|>  char '-' <|>  char '*' <|>  char '/' 

let number = integer |> map Number

let exprSetter, expr = slot ()
let brack = char '(' <*>> anySpace <*>> expr <<*> anySpace <<*> char ')'
let reference = letter <*> integer |> map Reference
let term = number <|> reference <|> brack 
let binary = 
  term <<*> anySpace <*> operator <<*> anySpace <*> term 
  |> map (fun ((l,op), r) -> Binary(l, op, r))
let exprAux = binary <|> term
exprSetter.Set exprAux

let formula = char '=' <*>> anySpace <*>> expr
let equation = anySpace <*>> (formula <|> number) <<*> anySpace 

// ----------------------------------------------------------------------------

let rec evaluate cells visited expr =
  match expr with
  | Number(n) -> Some n

  | Reference(pos) when Set.contains pos visited ->
      None

  | Reference(pos) ->
      Map.tryFind pos cells |> Option.bind (fun input ->
        run equation input |> Option.bind (fun expr ->
          evaluate cells (Set.add pos visited) expr ))

  | Binary(l, op, r) ->
      evaluate cells visited l |> Option.bind (fun lv ->
        evaluate cells visited r |> Option.map (fun rv ->
          let ops = dict [ '+', (+); '-', (-); '/', (/); '*', (*) ]
          ops.[op] lv rv ) )

// ----------------------------------------------------------------------------

let renderEditor trigger pos value =
  h?td ["class" => "selected"] [ 
    h?input [
      "id" => "celled"; "focused" => "true"
      "oninput" =!> fun d -> trigger (UpdateValue(pos, unbox d?target?value))
      "value" => value ] []
  ]

let renderView (trigger:Event -> unit) pos value = 
  h?td [
    "onclick" =!> fun _ -> trigger(StartEdit pos)
    "style" => if value = None then "color:red" else ""
  ] [ text (defaultArg value "#ERR") ]

let renderCell trigger pos state =
  let value = Map.tryFind pos state.Cells
  if state.Active = Some pos then
    let value = defaultArg value ""
    renderEditor trigger pos value
  else 
    match value with 
    | None -> 
        renderView trigger pos (Some "")
    | Some value ->
        let res = 
          run equation value 
          |> Option.bind (fun expr ->
              evaluate state.Cells Set.empty expr
              |> Option.map string )              
        renderView trigger pos res

let render trigger state = 
  h?table [] [
    yield h?tr [] [
      yield h?td [] []
      for col in state.Cols -> h?th [] [ text (string col) ]
    ]
    for row in state.Rows -> 
      h?tr [] [
        yield h?th [] [ text (string row) ]
        for col in state.Cols -> renderCell trigger (col, row) state
      ]
  ]

// ----------------------------------------------------------------------------

let update state = function
  | StartEdit(pos) -> { state with Active = Some pos }
  | UpdateValue(pos, value) ->
      let newCells = Map.add pos value state.Cells
      { state with Cells = newCells }

// ----------------------------------------------------------------------------

let initial = 
  { Rows = [ 1 .. 12 ]
    Cols = [ 'A' .. 'K' ]
    Active = None
    Cells = Map.empty }

app "main" initial render update
