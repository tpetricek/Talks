module Todo
open Elmish

// ------------------------------------------------------------------------------------------------
// Domain model - update events and application state
// ------------------------------------------------------------------------------------------------

type Update =
  | Input of string

type Model =
  { Input : string }

// ------------------------------------------------------------------------------------------------
// Given an old state and update event, produce a new state
// ------------------------------------------------------------------------------------------------

let update state = function
  | Input s -> { state with Input = s }

// ------------------------------------------------------------------------------------------------
// Render page based on the current state
// ------------------------------------------------------------------------------------------------

let render trigger state =
  h?div [] [
    h?ul [] [
      h?li [] [
        text "First work item"
        h?a ["href" => "#"; "onclick" =!> fun _ -> () ] [ h?span [] [ text "X" ] ]
      ]
      h?li [] [
        text "Second work item"
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

let initial = { Input = "" }

app "todo" initial render update
