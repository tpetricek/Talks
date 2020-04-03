module Demo.App
open Demo.Html

// DEMO: Implementing an Elmish counter

type Model = 
  { Count : int option }

type Event = 
  | Update of int 
  | Reset

let init = 
  { Count = None }

let render trigger state = 
  match state.Count with
  | None ->
      h?div [] [
        yield h?h1 [] [ text "Welcome to Counter!" ]
        yield h?button [ "click" =!> fun _ _ -> trigger Reset ] [ text "Start" ]
      ]
  | Some n ->
      h?div [] [
        yield h?h1 [] [ text ("Current count: " + string n) ]
        yield h?button [ "click" =!> fun _ _ -> trigger (Update 1)] [ text"+1" ]
        yield h?button [ "click" =!> fun _ _ -> trigger (Update -1)] [ text"-1" ]
      ]

let update state msg = 
  match msg, state.Count with
  | Reset, _ -> { Count = Some 0 }
  | Update by, Some count -> { Count = Some(count + by) }
  | Update _, _ -> state

Html.startElmishApp "out" init render update