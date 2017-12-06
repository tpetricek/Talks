module Drawing

open Fable.Core
open Fable.Import
open Fable.Import.Browser
open Http
open Elmish

// ------------------------------------------------------------------------------------------------
// Domain model
// ------------------------------------------------------------------------------------------------

type Item = 
  { Id : int
    Item : string }

type Update =
  | Input of string
  // TODO: add
  // TODO: remove

type Model =
  { Counter : int
    Input : string 
    Items : Item list }

// ------------------------------------------------------------------------------------------------
// Update function
// ------------------------------------------------------------------------------------------------

let update trigger state = function
  | Input s -> 
      { state with Input = s }
  // TODO: hanlde add and remove

// ------------------------------------------------------------------------------------------------
// Render function
// ------------------------------------------------------------------------------------------------

let render trigger state =
  h?div [] [
    h?ul [] [
      // TODO: render items
      h?li [] [
        text "Go to LambdUp!"
        h?a ["href" => "#"; "onclick" =!> fun _ -> () ] [ h?span [] [ text "X" ] ]
      ]
      h?li [] [
        text "Have fun!"
        h?a ["href" => "#"; "onclick" =!> fun _ -> () ] [ h?span [] [ text "X" ] ]
      ]
    ]
    h?input [
      "value" => state.Input
      "oninput" =!> fun d -> trigger (Input(unbox d?target?value)) ] []
    h?button
      [ "onclick" =!> fun _ -> () ]
      [ text "Add" ]
  ]

// ------------------------------------------------------------------------------------------------
// Start the application with initial state
// ------------------------------------------------------------------------------------------------

let initial = { Input = ""; Items = []; Counter = 0 }
let trigger = app "todo" initial render update
// DEMO: service integration