#r "packages/Suave/lib/net40/Suave.dll"
open System
open System.IO
open Suave
open Suave.Web
open Suave.Http
open Suave.Successful
open Suave.RequestErrors
open Suave.Operators
open Suave.Filters


// ------------------------------------------------------------------

type ChatMessage =
  | Post of string * string
  | Retrieve of AsyncReplyChannel<string>

let startChat () =
  MailboxProcessor.Start(fun inbox ->
    let rec loop messages = async {
      let! msg = inbox.Receive()
      match msg with
      | Post(name, text) ->
          let html = sprintf "<li><strong>%s</strong>: %s</li>" name text
          return! loop (html::messages)
      | Retrieve(repl) ->
          repl.Reply(String.concat "\n" messages)
          return! loop messages }
    loop [] )

// ------------------------------------------------------------------

type AgentDict = Map<string, MailboxProcessor<ChatMessage>>

type RouterMessage =
  | Send of string * ChatMessage
  | List of AsyncReplyChannel<string>

let (|MapFind|_|) map key = Map.tryFind key map

let router =
  MailboxProcessor.Start(fun inbox ->
    let rec loop (agents:AgentDict) = async {
      let! msg = inbox.Receive()
      match msg with
      | List(repl) ->
          [ for KeyValue(k, _) in agents ->
              sprintf "<li><a href=\"/%s/\">%s</a></li>" k k ]
          |> String.concat "" |> repl.Reply
          return! loop agents
      | Send(MapFind agents agent, msg) ->
          agent.Post(msg)
          return! loop agents
      | Send(room, msg) ->
          let agent = startChat()
          agent.Post(msg)
          return! loop (Map.add room agent agents) }
    loop Map.empty )

// ------------------------------------------------------------------

let getRooms ctx = async {
  let! html = router.PostAndAsyncReply(List)
  return! OK html ctx }

let getMessages room ctx = async {
  let! body = router.PostAndAsyncReply(fun ch -> Send(room, Retrieve ch))
  let html = "<ul>" + body + "</ul>"
  return! OK html ctx }

let postMessage room ctx = async {
  let name = ctx.request.url.Query.Substring(1)
  use sr = new StreamReader(new MemoryStream(ctx.request.rawForm))
  let text = sr.ReadToEnd()
  router.Post(Send(room, Post(name, text)))
  return! ACCEPTED "OK" ctx }

let index = File.ReadAllText(__SOURCE_DIRECTORY__ + "/web/chat.html")

// ------------------------------------------------------------------

let noCache =
  Writers.setHeader "Cache-Control" "no-cache, no-store, must-revalidate"
  >=> Writers.setHeader "Pragma" "no-cache"
  >=> Writers.setHeader "Expires" "0"

let app =
  choose
    [ path "/" >=> Writers.setMimeType "text/html" >=> OK index
      path "/chat" >=> GET >=> noCache >=> getMessages "Home"
      path "/post" >=> POST >=> noCache >=> postMessage "Home"
      path "/rooms" >=> GET >=> noCache >=> getRooms

      pathScan "/%s/" (fun _ -> Writers.setMimeType "text/html" >=> OK index)
      pathScan "/%s/chat" (fun room -> GET >=> noCache >=> getMessages (room.Trim('/')))
      pathScan "/%s/post" (fun room -> POST >=> noCache >=> postMessage (room.Trim('/')))
      NOT_FOUND "Found no handlers" ]
