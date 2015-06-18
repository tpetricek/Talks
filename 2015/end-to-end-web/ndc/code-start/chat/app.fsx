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

// TODO: What is the ChatMessage that chat agent handles?
// TODO: Implement chat agent to store the room state
// (Format messages as "<li><strong>%s</strong>: %s</li>")
// DEMO: Add support for multiple chat rooms

let getMessages room ctx = async {
  let body = "<li><strong>System</string>: Nothing!</li>"
  let html = "<ul>" + body + "</ul>"
  return! OK html ctx }

let postMessage room ctx = async {
  return! ACCEPTED "OK" ctx }

// ------------------------------------------------------------------

let index = File.ReadAllText(__SOURCE_DIRECTORY__ + "/web/chat.html")
let app = path "/" >>= Writers.setMimeType "text/html" >>= OK index
