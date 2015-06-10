#r "packages/Suave/lib/net40/Suave.dll"
open System
open System.IO
open Suave
open Suave.Web
open Suave.Types
open Suave.Http
open Suave.Http.Successful
open Suave.Http.RequestErrors
open Suave.Http.Applicatives

// ------------------------------------------------------------------

type ChatMessage = 
  | Post of string * string
  | Retrieve of AsyncReplyChannel<string>

// ------------------------------------------------------------------

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

let router = 
  MailboxProcessor.Start(fun inbox -> 
    let rec loop (agents:AgentDict) = async { 
      let! channel, msg = inbox.Receive()
      match Map.tryFind channel agents with
      | Some agent -> 
          agent.Post(msg)
          return! loop agents
      | None ->
          let agent = startChat()
          agent.Post(msg)
          return! loop (Map.add channel agent agents) }
    loop Map.empty
  )
// ------------------------------------------------------------------

let getMessages room ctx = async {
  let! body = router.PostAndAsyncReply(fun ch -> room, Retrieve ch)
  let html = "<ul>" + body + "</ul>"
  return! OK html ctx }

let postMessage room ctx = async {
  let name = 
    match ctx.request.queryParam "name" with
    | Choice1Of2 n -> n
    | _ -> "Anonymous"
  use ms = new MemoryStream(ctx.request.rawForm)
  use sr = new StreamReader(ms)
  let text = sr.ReadToEnd()  
  router.Post(room, Post(name, text))
  return! ACCEPTED "OK" ctx }

// ------------------------------------------------------------------

let index = File.ReadAllText(__SOURCE_DIRECTORY__ + "/web/chat.html")

let noCache =
  Writers.setHeader "Cache-Control" "no-cache, no-store, must-revalidate"
  >>= Writers.setHeader "Pragma" "no-cache"
  >>= Writers.setHeader "Expires" "0"

let app =
  choose
    [ // Routing for default URLs without room names
      path "/" >>= Writers.setMimeType "text/html" >>= OK index
      path "/chat" >>= GET >>= noCache >>= getMessages "default"
      path "/post" >>= POST >>= noCache >>= postMessage "default"

      // Routing for URLs that start with room name
      pathScan "/%s/" (fun _ -> Writers.setMimeType "text/html" >>= OK index)
      pathScan "/%s/chat" (fun room -> GET >>= noCache >>= getMessages room)
      pathScan "/%s/post" (fun room -> POST >>= noCache >>= postMessage room)
      NOT_FOUND "Found no handlers" ]