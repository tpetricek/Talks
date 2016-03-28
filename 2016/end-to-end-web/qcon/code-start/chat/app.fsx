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


// DEMO: Add handlers for REST API
// TODO: Handle /chat with GET & no chache using getMessage
// TODO: Handle /post with POST & no cache using postMessage
// TODO: Otherwise, report NOT_FOUND

// TODO: What is the ChatMessage that chat agent handles?
// TODO: Implement chat agent to store the room state
// (Format messages as "<li><strong>%s</strong>: %s</li>")

// DEMO: Add support for multiple chat rooms
// DEMO: Add routing for multiple rooms

// ------------------------------------------------------------------

let index = File.ReadAllText(__SOURCE_DIRECTORY__ + "/web/chat.html")
let app = path "/" >=> Writers.setMimeType "text/html" >=> OK index
