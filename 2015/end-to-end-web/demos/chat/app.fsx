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

type ChatMessage =
  | Add of name:string * text:string
  | Get of AsyncReplyChannel<string>

let chat = MailboxProcessor.Start(fun inbox ->
  let rec loop lines = async {
    let! msg = inbox.Receive()
    match msg with
    | Add(name, text) ->
        let line = sprintf "<li><strong>%s</strong>: %s</li>" name text
        return! loop (line::lines)
    | Get(repl) ->
        repl.Reply(String.concat "\n" lines)
        return! loop lines }
  loop [] )

let getMessages : WebPart = fun ctx -> async {
  let! html = chat.PostAndAsyncReply(Get)
  return! ctx |> OK ("<ul>" + html + "</ul>")
}

let postMessage : WebPart = fun ctx -> async {
  let (Choice1Of2 name) = ctx.request.queryParam "name"
  let text = StreamReader(MemoryStream(ctx.request.rawForm)).ReadToEnd()
  chat.Post(Add(name, text))
  return! ctx |> ACCEPTED "OK"
}

let index = File.ReadAllText(__SOURCE_DIRECTORY__ + "/web/chat.html")
let jquery = File.ReadAllText(__SOURCE_DIRECTORY__ + "/web/jquery.js")

let noCache =
  Writers.setHeader "Cache-Control" "no-cache, no-store, must-revalidate"
  >>= Writers.setHeader "Pragma" "no-cache"
  >>= Writers.setHeader "Expires" "0"

let app =
  choose
    [ path "/" >>= Writers.setMimeType "text/html" >>= OK index
      path "/jquery.js" >>= Writers.setMimeType "text/javascript" >>= OK jquery
      path "/chat" >>= GET >>= noCache >>= getMessages
      path "/post" >>= POST >>= noCache >>= postMessage
      NOT_FOUND "Found no handlers" ]
