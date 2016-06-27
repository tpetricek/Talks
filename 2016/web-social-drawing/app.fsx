#I "packages/Suave/lib/net40"
#r "packages/Suave/lib/net40/Suave.dll"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open System
open System.IO
open Suave
open Suave.Filters
open Suave.Operators
open FSharp.Data

// -------------------------------------------------------------------------------------------------
// Agent for keeping the state of the drawing
// -------------------------------------------------------------------------------------------------

// Using JSON provider to get a type for rectangles with easy serialization
type Rect = JsonProvider<"""{"x1":0.0,"y1":0.0,"x2":10.0,"y2":10.0}""">

// We can add new rectangle or request a list of all rectangles
type Message =
  | AddRect of Rect.Root
  | GetRects of AsyncReplyChannel<list<Rect.Root>>

// Agent that keeps the state and handles 'Message' requests
let agent = MailboxProcessor.Start(fun inbox ->
  let rec loop rects = async {
    let! msg = inbox.Receive()
    match msg with
    | AddRect(r) -> return! loop (r::rects)
    | GetRects(repl) ->
        repl.Reply(rects)
        return! loop rects }
  loop [] )

// -------------------------------------------------------------------------------------------------
// The web server - REST api and static file hosting
// -------------------------------------------------------------------------------------------------

let webRoot = Path.Combine(__SOURCE_DIRECTORY__, "web")
let clientRoot = Path.Combine(__SOURCE_DIRECTORY__, "client")

let noCache =
  Writers.setHeader "Cache-Control" "no-cache, no-store, must-revalidate"
  >=> Writers.setHeader "Pragma" "no-cache"
  >=> Writers.setHeader "Expires" "0"

let getRectangles ctx = async {
  let! rects = agent.PostAndAsyncReply(GetRects)
  let json = JsonValue.Array [| for r in rects -> r.JsonValue |]
  return! ctx |> Successful.OK(json.ToString()) }

let addRectangle ctx = async {
  use ms = new StreamReader(new MemoryStream(ctx.request.rawForm))
  agent.Post(AddRect(Rect.Parse(ms.ReadToEnd())))
  return! ctx |> Successful.OK "added" }

let app =
  choose [
    // REST API for adding/getting rectangles
    GET >=> path "/getrects" >=> getRectangles
    POST >=> path "/addrect" >=> addRectangle

    // Serving the generated JS and source maps
    path "/out/client.js" >=> noCache >=> Files.browseFile clientRoot (Path.Combine("out", "client.js"))
    path "/out/client.js.map" >=> noCache >=> Files.browseFile clientRoot (Path.Combine("out", "client.js.map"))
    pathScan "/node_modules/%s.js" (sprintf "/node_modules/%s.js" >> Files.browseFile clientRoot)

    // Serving index and other static files
    path "/" >=> Files.browseFile webRoot "index.html"
    Files.browse webRoot
  ]











//
