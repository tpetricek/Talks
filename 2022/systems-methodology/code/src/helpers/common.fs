module GammaDemo.Common

open Fable.Core
open Fable.Import.JS
open Fable.Import.Browser
open System.Collections.Generic
module FsOption = Microsoft.FSharp.Core.Option

[<Emit("$0[$1]=$2")>]
let setProperty<'T> (obj:obj) (name:string) (v:'T) : unit = failwith "never"

[<Emit("$0[$1]")>]
let getProperty<'T> (obj:obj) (name:string) : 'T = failwith "never"

[<Emit("$0.apply($1,$2)")>]
let apply<'T> (f:obj) (self:obj) (args:obj[]) : 'T = failwith "never"

[<Emit("console.log.apply(console, $0)")>]
let consoleLog (args:obj[]) : unit = 
  let format = args.[0] :?> string
  let mutable argIndex = 1
  let mutable charIndex = 0
  let mutable res = ""
  while charIndex < format.Length do
    if format.[charIndex] = '%' then
      res <- res +
        match format.[charIndex+1] with
        | 'c' -> ""
        | 's' -> args.[argIndex].ToString()
        | 'O' -> sprintf "%A" (args.[argIndex])
        | _ -> failwith "consoleLog: Unsupported formatter"
      argIndex <- argIndex + 1
      charIndex <- charIndex + 2
    else 
      res <- res + format.[charIndex].ToString()
      charIndex <- charIndex + 1
  printfn "%s" res

[<Emit("typeof window == 'undefined'")>]
let windowUndefined () : bool = true

let isLocalHost() = 
  windowUndefined () ||
  window.location.hostname = "localhost" || 
  window.location.hostname = "127.0.0.1"

let mutable enabledCategories = 
  if not (isLocalHost ()) then set []
  else set ["*"] 

let getColor =   
  let colorMap = System.Collections.Generic.Dictionary<_, _>()
  let mutable index = -1
  let colors = 
    [| "#393b79"; "#637939"; "#8c6d31"; "#843c39"; "#7b4173" 
       "#3182bd"; "#31a354"; "#756bb1"; "#636363"; "#e6550d" |]
  fun cat -> 
    if not (colorMap.ContainsKey(cat)) then
      index <- (index + 1) % colors.Length
      colorMap.Add(cat, colors.[index])
    colorMap.[cat]
  
type Log =
  static member setEnabled(cats) = enabledCategories <- cats

  static member message(level:string, category:string, msg:string, [<System.ParamArray>] args) = 
    let args = if args = null then [| |] else args
    let category = category.ToUpper()
    if level = "EXCEPTION" || level = "ERROR" || enabledCategories.Contains "*" || enabledCategories.Contains category then
      let dt = System.DateTime.Now
      let p2 (s:int) = (string s).PadLeft(2, '0')
      let p4 (s:int) = (string s).PadLeft(4, '0')
      let prefix = sprintf "[%s:%s:%s:%s] %s: " (p2 dt.Hour) (p2 dt.Minute) (p2 dt.Second) (p4 dt.Millisecond) category
      let color = 
        match level with
        | "TRACE" -> "color:" + getColor category
        | "EXCEPTION" -> "color:#c00000"
        | "ERROR" -> "color:#900000"
        | _ -> ""
      consoleLog(FSharp.Collections.Array.append [|box ("%c" + prefix + msg); box color|] args)

  static member trace(category:string, msg:string, [<System.ParamArray>] args) = 
    Log.message("TRACE", category, msg, args)

  static member exn(category:string, msg:string, [<System.ParamArray>] args) = 
    Log.message("EXCEPTION", category, msg, args)

  static member error(category:string, msg:string, [<System.ParamArray>] args) = 
    Log.message("ERROR", category, msg, args)

/// Symbol is a unique immutable identiifer (we use JavaScript symbols)
type Symbol = interface end

[<Emit("Symbol()")>]
let createSymbol () = { new Symbol }

type ListDictionaryNode<'K, 'T> = 
  { mutable Result : 'T option
    Nested : Dictionary<'K, ListDictionaryNode<'K, 'T>> }

type ListDictionary<'K, 'V> = Dictionary<'K, ListDictionaryNode<'K, 'V>>

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module ListDictionary = 
  let tryFind ks dict = 
    let rec loop ks node =
      match ks, node with
      | [], { Result = Some r } -> Some r
      | k::ks, { Nested = d } when d.ContainsKey k -> loop ks (d.[k])
      | _ -> None
    loop ks { Nested = dict; Result = None }

  let set ks v dict =
    let rec loop ks (dict:ListDictionary<_, _>) = 
      match ks with
      | [] -> failwith "Empty key not supported"
      | k::ks ->
          if not (dict.ContainsKey k) then dict.[k] <- { Nested = Dictionary<_, _>(); Result = None }
          if List.isEmpty ks then dict.[k].Result <- Some v
          else loop ks (dict.[k].Nested)
    loop ks dict

  let count (dict:ListDictionary<_, _>) = 
    let rec loop node = 
      let nest = node.Nested |> Seq.sumBy (fun kv -> loop kv.Value)
      if node.Result.IsSome then 1 + nest else nest
    dict |> Seq.sumBy (fun kv -> loop kv.Value)

type Future<'T> = 
  abstract Then : ('T -> unit) * (exn -> unit) -> unit

type Microsoft.FSharp.Control.Async with
  static member AwaitFuture (f:Future<'T>) = Async.FromContinuations(fun (cont, econt, _) ->
    f.Then(cont, econt))

  static member Future (n:string option) op start = 
    let mutable res = Choice1Of3()
    let mutable handlers = []
    let mutable running = false

    let trigger (cont, econt) = 
      match res with
      | Choice1Of3 () -> handlers <- (cont, econt)::handlers 
      | Choice2Of3 v -> cont v
      | Choice3Of3 e -> econt e

    let ensureStarted() = 
      if not running then 
        n |> FsOption.iter (fun n -> Log.trace("system", "Starting future '%s'....", n))
        running <- true
        async { try 
                  let! r = op
                  n |> FsOption.iter (fun n -> Log.trace("system", "Future '%s' evaluated to: %O", n, r))
                  res <- Choice2Of3 r                  
                with e ->
                  Log.exn("system", "Evaluating future failed: %O", e)
                  res <- Choice3Of3 e
                for h in handlers do trigger h } |> Async.StartImmediate
    if start = true then ensureStarted()

    { new Future<_> with
        member x.Then(f, e) = 
          ensureStarted()
          trigger(f, e) }

  static member CreateFuture(op) = Async.Future None op false
  static member StartAsFuture(op) = Async.Future None op true
  static member CreateNamedFuture name op = Async.Future (Some name) op false
  static member StartAsNamedFuture name op = Async.Future (Some name) op true
