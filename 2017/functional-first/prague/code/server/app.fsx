#r "packages/Suave/lib/net40/Suave.dll"
#r "packages/FSharp.Data/lib/net45/FSharp.Data.dll"
open System
open System.Text
open FSharp.Data

// -------------------------------------------------------------------------------------------------
// Keeping state 
// -------------------------------------------------------------------------------------------------

type Todo = JsonProvider<"""{"Id":123,"Item":"Do stuff"}""">

type Message =
  | DropItem of int
  | AddItem of Todo.Root
  | GetItems of AsyncReplyChannel<list<Todo.Root>>

let agent = MailboxProcessor.Start(fun inbox ->
  let rec loop items = async {
    let! msg = inbox.Receive()
    match msg with
    | AddItem item -> 
        return! loop (item::items)
    | DropItem id -> 
        return! items |> List.filter (fun it -> it.Id <> id) |> loop
    | GetItems repl ->
        repl.Reply(items)
        return! loop items }
  loop [] )

// -------------------------------------------------------------------------------------------------
// The web server 
// -------------------------------------------------------------------------------------------------

open Suave
open Suave.Filters
open Suave.Writers
open Suave.Operators

let app =
  Successful.OK "Hello world"
  // TODO: path
  // TODO: choose
  // TODO: pathScan
  // DEMO: todo REST service