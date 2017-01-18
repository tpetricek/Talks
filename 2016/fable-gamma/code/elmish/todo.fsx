#r "node_modules/fable-core/Fable.Core.dll"
#load "node_modules/fable-import-virtualdom/Fable.Helpers.Virtualdom.fs"
#load "elmish.fsx"
open Fable.Core
open Fable.Import
open Fable.Import.Browser
open Fable.Helpers
open Elmish

// ------------------------------------------------------------------------------------------------
// Domain model - update events and application state
// ------------------------------------------------------------------------------------------------

type Update = 
  | Input of string
  | Remove of int
  | Create 

type Model = 
  { Input : string
    Items : (int * string) list
    NextId : int }

// ------------------------------------------------------------------------------------------------
// Given an old state and update event, produce a new state
// ------------------------------------------------------------------------------------------------

let update state = function
  | Input s -> { state with Input = s }
  | Create -> 
      let items = state.Items @ [state.NextId, state.Input]
      { state with Items = items; Input = ""; NextId = state.NextId + 1 }
  | Remove removed -> 
      let items = 
        state.Items 
        |> List.choose (fun (i, v) -> 
            if i <> removed then Some(i, v) 
            else None)
      { state with Items = items }

// ------------------------------------------------------------------------------------------------
// Render page based on the current state
// ------------------------------------------------------------------------------------------------

let render trigger state =
  h?div [] [
    h?ul [] [
      for id, it in state.Items ->
        h?li [] [ 
          text it 
          h?a ["href" => "#"; "onclick" =!> fun _ -> 
            trigger (Remove id) ] [ h?span [] [ text "X" ] ]
        ]
    ]
    h?input [
      "value" => state.Input + "A"
      "onchange" =!> fun d -> trigger (Input(unbox d?target?value)) ] []
    h?button 
      [ yield "onclick" =!> fun _ -> trigger Create 
        if state.Input = "" then 
          yield "disabled" => "disabled" ] 
      [ text "Add" ]
  ]

// ------------------------------------------------------------------------------------------------
// Start the application with initial state
// ------------------------------------------------------------------------------------------------

let initial = 
  { Items = [0, "Do some work"; 1, "Do even more work"]
    Input = ""
    NextId = 2 }

app "todo" initial render update