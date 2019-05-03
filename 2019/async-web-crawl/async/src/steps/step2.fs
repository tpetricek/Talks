module App
open Elmish
open Fable.Import.Browser
open System.Text.RegularExpressions

// --------------------------------------------------------

// TODO: Elmish Hello World with state string and no events
// TODO: Domain model with Pages & AddPage event
// TODO: Implement 'update' and 'render' functions
// DEMO: Add sample pages 
// TODO: Track 'visited' pages to avoid stack overflow 

// --------------------------------------------------------

type Model = { Pages : Map<string, string list> }
type Event = AddPage of string * string

let initial = { Pages = Map.empty }

let update model event = 
  match event with
  | AddPage(source, target) ->
      let links = defaultArg (model.Pages.TryFind(source)) []
      { Pages = model.Pages.Add(source, links @ [target]) }

let rec renderPage visited page model = 
  h?li [] [
    h?p [] [ text page ]
    h?ul [] [
      for p in defaultArg (model.Pages.TryFind(page)) [] do
        if not (Set.contains p visited) then
          yield renderPage (Set.add p visited) p model
    ]
  ]

let render trigger model =
  h?ul [] [
    renderPage Set.empty "Microsoft" model
  ]

let trigger = createVirtualDomApp "out" initial render update  

trigger(AddPage("Microsoft", "Bill Gates"))
trigger(AddPage("Microsoft", "Visual F# Enterprise Edition"))
trigger(AddPage("Microsoft", "Windows"))
trigger(AddPage("Windows", "Windows 3.11"))
trigger(AddPage("Windows", "Windows ME"))
trigger(AddPage("Windows", "Windows Vista"))

// --------------------------------------------------------

// DEMO: Add 'download' and 'parsePage' functions
// TODO: 'crawl' that runs two cralws in parallel
// TODO: Continuation-based version of crawl

// --------------------------------------------------------

// TODO: Define 'Async<'T>', 'afterwards', 'unit', 'start' 
// TODO: Refactoring of 'crawl' using asyncs
// TODO: Define 'AsyncBuilder' and use it!
// TODO: Add 'For' member to 'AsyncBuilder'

// --------------------------------------------------------
