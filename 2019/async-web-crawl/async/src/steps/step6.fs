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

type Async<'T> = Async of (('T -> unit) -> unit)

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

let download url = Async(fun cont -> 
  let xhr = XMLHttpRequest.Create()
  xhr.addEventListener_readystatechange(fun e ->
    if xhr.readyState = 4. && xhr.status = 200. then
      cont (parsePage (string xhr.response))
  )
  xhr.``open``("GET", url)
  xhr.send("")
)

let root = "http://localhost:8011/wiki/Microsoft"
let rnd = System.Random()

// --------------------------------------------------------

let afterwards f a = Async(fun cont ->
  let (Async g1) = a
  g1 (fun r1 -> 
    let (Async g2) = f r1
    g2 cont))

let unit v = Async(fun cont -> cont v)
let start (Async f) = f (fun () -> ())

type AsyncBuilder() = 
  member x.Bind(a, f) = afterwards f a
  member x.Return(v) = unit v
  member x.Zero() = unit ()
  member x.For(l, f) =  
    match l with 
    | [] -> unit ()
    | l::ls -> x.Bind(f l, fun () -> x.For(ls, f))

let async = AsyncBuilder()

// --------------------------------------------------------

let rec crawl n source url = async {
  if n > 0 then 
    let! title, links = download url 
    trigger(AddPage(source, title))
    for i in [1..4] do
      do! crawl (n-1) title links.[rnd.Next(links.Length)] 
  }

crawl 4 "" root |> start

// --------------------------------------------------------

// TODO: Define 'Async<'T>', 'afterwards', 'unit', 'start' 
// TODO: Refactoring of 'crawl' using asyncs
// TODO: Define 'AsyncBuilder' and use it!
// TODO: Add 'For' member to 'AsyncBuilder'

// --------------------------------------------------------
