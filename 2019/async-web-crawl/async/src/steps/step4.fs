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

// --------------------------------------------------------

// DEMO: Add 'download' and 'parsePage' functions
// TODO: 'crawl' that runs two cralws in parallel
// TODO: Continuation-based version of crawl

// --------------------------------------------------------

let regexLink = Regex("\<a href=\"/wiki/[^\"]*\"")
let regexTitle = Regex("\<title\>[^\<]*\<")
let parsePage html = 
  let title = regexTitle.Match(html).Value
  let title = 
    if not (title.Contains("-")) then title
    else title.Substring(7, title.LastIndexOf('-')-8)
  let links = 
    [ for link in unbox<Match[]> (regexLink.Matches(html)) do
        let atag = link.Value
        if not (atag.Contains(":")) then
          let prefix = "http://localhost:8011"
          yield prefix + atag.Split('"').[1] ]
  title, links

let download url cont = 
  let xhr = XMLHttpRequest.Create()
  xhr.addEventListener_readystatechange(fun e ->
    if xhr.readyState = 4. && xhr.status = 200. then
      cont (parsePage (string xhr.response))
  )
  xhr.``open``("GET", url)
  xhr.send("")

let root = "http://localhost:8011/wiki/Microsoft"
let rnd = System.Random()

let rec crawl n source url cont = 
  if n > 0 then 
    download url (fun (title, links) ->
      trigger(AddPage(source, title))
      crawl (n-1) title links.[rnd.Next(links.Length)] (fun () ->
        crawl (n-1) title links.[rnd.Next(links.Length)] cont )
    )
  else cont()

crawl 5 "" root ignore

// --------------------------------------------------------

// TODO: Define 'Async<'T>', 'afterwards', 'unit', 'start' 
// TODO: Refactoring of 'crawl' using asyncs
// TODO: Define 'AsyncBuilder' and use it!
// TODO: Add 'For' member to 'AsyncBuilder'

// --------------------------------------------------------
