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

type Model = string
type Event = unit

let initial = "Hello world"

let update model event = model

let render trigger model =
  h?h1 [] [ text model ]

let _ = createVirtualDomApp "out" initial render update  

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
