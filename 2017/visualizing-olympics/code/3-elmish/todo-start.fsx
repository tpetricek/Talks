#r "node_modules/fable-core/Fable.Core.dll"
#load "node_modules/fable-import-virtualdom/Fable.Helpers.Virtualdom.fs"
#load "elmish.fsx"
open System
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
  | Remove of Guid
  | Create 

type Model = 
  { Items : (Guid * string) list 
    Input : string }

// ------------------------------------------------------------------------------------------------
// Given an old state and update event, produce a new state
// ------------------------------------------------------------------------------------------------

let update state action = 
  state

// ------------------------------------------------------------------------------------------------
// Render page based on the current state
// ------------------------------------------------------------------------------------------------

let render trigger state =
  h?div [] [
    h?ul [] [
      h?li [] [
        text "Fake work #1"
        h?a ["href" => "#"]  [ h?span [] [ text "X" ] ]
      ]
      h?li [] [
        text "Fake work #2"
        h?a ["href" => "#"]  [ h?span [] [ text "X" ] ]
      ]
    ]
    h?input [
      "value" => state.Input
      "oninput" =!> fun d -> trigger (Input(unbox d?target?value)) ] []
    h?button
      [ "onclick" =!> fun _ -> trigger Create ]
      [ text "Add" ]
  ]

// ------------------------------------------------------------------------------------------------
// Start the application with initial state
// ------------------------------------------------------------------------------------------------

let initial =
  { Input = ""; 
    Items = 
      [ Guid.NewGuid(), "First work item"
        Guid.NewGuid(), "Second work item" ] }

app "todo" initial render update
