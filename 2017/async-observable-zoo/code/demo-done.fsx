#r "node_modules/fable-core/Fable.Core.dll"
#load "elmish.fsx"
open System
open Fable.Core
open Elmish
open System.Collections.Generic
module B = Fable.Import.Browser

// ------------------------------------------------------------------------------------------------
// Infrastructure
// ------------------------------------------------------------------------------------------------

module Section1 = 
  let light = B.document.getElementById("light") :?> B.HTMLDivElement

module Section2 = 
  let up = B.document.getElementById("up") :?> B.HTMLButtonElement
  let down = B.document.getElementById("down") :?> B.HTMLButtonElement
  let lbl = B.document.getElementById("out") :?> B.HTMLParagraphElement

module Section3 = 
  let up = B.document.getElementById("up2") :?> B.HTMLButtonElement
  let down = B.document.getElementById("down2") :?> B.HTMLButtonElement
  let lbl = B.document.getElementById("out2") :?> B.HTMLParagraphElement
  let stop = B.document.getElementById("stop") :?> B.HTMLButtonElement
  let start = B.document.getElementById("start") :?> B.HTMLButtonElement

module Section4 = 
  let light = B.document.getElementById("light2") :?> B.HTMLDivElement
  let next = B.document.getElementById("next") :?> B.HTMLButtonElement

module Section5 = 
  let current = B.document.getElementById("current") :?> B.HTMLParagraphElement
  let next = B.document.getElementById("next2") :?> B.HTMLButtonElement

[<Emit("JSON.parse($0)")>]
let jsonParse<'R> (str:string) : 'R = failwith "JS Only"

let show sec = 
  let secs = B.document.getElementsByTagName("section")
  for i in 0 .. int secs.length - 1 do (secs.[i] :?> B.HTMLTableSectionElement).style.display <- "none"
  B.document.getElementById(sec).style.display <- ""

// ------------------------------------------------------------------------------------------------
// Async
// ------------------------------------------------------------------------------------------------

type Async<'T> = 
  abstract Start : ('T -> unit) -> unit

module Async = 
  // Step 1

  let sleep n =
    { new Async<unit> with
        member x.Start(f) = 
          B.window.setTimeout((fun _ -> f ()), n) |> ignore }

  // Step 2

  let bind (f:'a -> Async<'b>) (a:Async<'a>) : Async<'b> = 
    { new Async<'b> with
        member x.Start(g) =
          a.Start(fun a ->
            let ab = f a
            ab.Start(g) ) }

  let unit v = 
    { new Async<_> with
        member x.Start(f) = f v }

  let start (a:Async<_>) =
    a.Start(fun () -> ())

  // Step 3

  type AsyncBuilder() = 
    member x.Return(v) = unit v
    member x.Bind(a, f) = bind f a
    member x.Zero() = unit ()
    member x.For(vals, f) =
      match vals with
      | [] -> unit ()
      | v::vs -> f v |> bind (fun () -> x.For(vs, f))
    member x.Delay(f:unit -> Async<_>) =
      { new Async<_> with
          member x.Start(h) = f().Start(h) }
    member x.While(c, f) = 
      if not (c ()) then unit ()
      else f |> bind (fun () -> x.While(c, f))
    member x.ReturnFrom(a) = a
          
let async = Async.AsyncBuilder()


//show "section1"


//(Async.sleep 1000).Start(fun _ ->
//  Section1.light.style.backgroundColor <- "red")


//Async.sleep 5000 
//|> Async.bind (fun _ ->
//  Section1.light.style.backgroundColor <- "red"
//  Async.unit () )
//|> Async.start


//async {
//  while true do
//    for clr in ["green";"orange";"red"] do
//      do! Async.sleep 1000
//      Section1.light.style.backgroundColor <- clr } |> Async.start


// ------------------------------------------------------------------------------------------------
// Tasks
// ------------------------------------------------------------------------------------------------

type Task<'T> = 
  abstract Value : option<'T> 
  abstract OnCompleted : (unit -> unit) -> unit

module Task =

  let sleep n =
    let mutable value = None
    let mutable handlers = ResizeArray<_>()
    B.window.setTimeout((fun _ -> 
        value <- Some ()
        for h in handlers do h () ), n) |> ignore
    { new Task<_> with
        member x.Value = value
        member x.OnCompleted f = handlers.Add(f) }


// ------------------------------------------------------------------------------------------------
// Events
// ------------------------------------------------------------------------------------------------

type IEvent<'T> = 
  abstract AddHandler : ('T -> unit) -> unit

module Event = 
  // Step 1

  let onClick (btn:B.HTMLButtonElement) = 
    let handlers = ResizeArray<_>()
    btn.onclick <- fun e -> (for h in handlers do h e); null
    { new IEvent<_> with
        member x.AddHandler(h) = handlers.Add(h) }

  let add f (e:IEvent<_>) = 
    e.AddHandler(fun e -> f e)

  // Step 2

  let map f (e:IEvent<_>) = 
    { new IEvent<_> with
        member x.AddHandler(h) = e.AddHandler(fun x -> h (f x))  }

  let merge (e1:IEvent<_>) (e2:IEvent<_>) = 
    { new IEvent<_> with
        member x.AddHandler(h) = e1.AddHandler(h); e2.AddHandler(h)  }
  
  let scan v f (e:IEvent<_>) = 
    { new IEvent<_> with
        member x.AddHandler(h) = 
          let mutable value = v
          e.AddHandler(fun x -> value <- f value x; h value) }


//show "section2"

//Event.onClick Section2.up |> Event.add (fun _ -> Section2.lbl.innerText <- "UP")
//Event.onClick Section2.down |> Event.add (fun _ -> Section2.lbl.innerText <- "DOWN")


//let e1 = Event.onClick Section2.up |> Event.map (fun _ -> 1)
//let e2 = Event.onClick Section2.down |> Event.map (fun _ -> -1)
//
//Event.merge e1 e2
//|> Event.scan 0 (+)
//|> Event.map (sprintf "Count: %d")
//|> Event.add (fun s -> Section2.lbl.innerText <- s)

// ------------------------------------------------------------------------------------------------
// Observables
// ------------------------------------------------------------------------------------------------

type IObservable<'T> = 
  abstract AddHandler : ('T -> unit) -> (unit -> unit)

module Observable = 
  // Step 1

  let onClick (btn:B.HTMLButtonElement) = 
    let handlers = ResizeArray<_>()
    btn.onclick <- fun e -> (for h in handlers do h e); null
    { new IObservable<_> with
        member x.AddHandler(h) = 
          handlers.Add(h) 
          (fun () -> handlers.Remove(h) |> ignore) }

  let map f (e:IObservable<_>) = 
    { new IObservable<_> with
        member x.AddHandler(h) = e.AddHandler(fun x -> h (f x))  }

  let merge (e1:IObservable<_>) (e2:IObservable<_>) = 
    { new IObservable<_> with
        member x.AddHandler(h) = 
          let d1 = e1.AddHandler(h)
          let d2 = e2.AddHandler(h) 
          fun () -> d1 (); d2 () }
  
  let scan v f (e:IObservable<_>) = 
    { new IObservable<_> with
        member x.AddHandler(h) = 
          let mutable value = v
          e.AddHandler(fun x -> value <- f value x; h value) }

  let add f (e:IObservable<_>) = 
    e.AddHandler(fun e -> f e)

  // Step 2

  let stateful (e:IObservable<_>) = 
    let handlers = ResizeArray<_>()
    e.AddHandler(fun v -> for h in handlers do h v) |> ignore
    { new IObservable<_> with
        member x.AddHandler(h) = 
          handlers.Add(h) 
          (fun () -> handlers.Remove(h) |> ignore) }   


module Async2 =
  // Step 3

  let awaitObservable (e:IObservable<_>) = 
    { new Async<_> with 
        member x.Start(f) = 
          let mutable d = ignore
          d <- e.AddHandler(fun v -> d (); f v) }

//show "section3"
//
//let e1 = Observable.onClick Section3.up |> Observable.map (fun _ -> 1)
//let e2 = Observable.onClick Section3.down |> Observable.map (fun _ -> -1)
//
//let evt = 
//  Observable.merge e1 e2
//  |> Observable.scan 0 (+)
//  |> Observable.map (sprintf "Count: %d")
//  |> Observable.stateful
//
//let mutable d = ignore
//
//Observable.onClick Section3.start |> Observable.add (fun _ -> 
//  d <- evt.AddHandler(fun s -> Section3.lbl.innerText <- s))
//Observable.onClick Section3.stop |> Observable.add (fun _ -> 
//  d () )



//show "section4"
//
//async {
//  while true do
//    for clr in ["green";"orange";"red"] do
//      let! e = Async2.awaitObservable (Observable.onClick Section4.next)
//      Section4.light.style.backgroundColor <- clr } |> Async.start

// ------------------------------------------------------------------------------------------------
// Async sequences - motivation
// ------------------------------------------------------------------------------------------------

let readJson fn =
  { new Async<_> with
      member x.Start(f) =
        let xh = B.XMLHttpRequest.Create()
        xh.addEventListener_readystatechange(fun p -> 
          if xh.readyState > 3. && xh.status = 200. then
            f xh.responseText
          null)
        xh.``open``("GET", fn, true)
        xh.send("") }

type Rate =
  { code : string
    value : float }

type Prices = 
  { rates : Rate[] }

//show "section5"
//
//async {
//  for i in [0 .. 364] do
//    let! p = readJson (sprintf "/data/%d.json" i)
//    let p = jsonParse<Prices> p 
//    do! Async.sleep 100
//    let gbp = p.rates |> Array.find (fun r -> r.code = "GBP")
//    Section5.current.innerText <- sprintf "GBP: %A" gbp.value }
//|> Async.start

// ------------------------------------------------------------------------------------------------
// Async sequences - code
// ------------------------------------------------------------------------------------------------

type AsyncSeq<'T> = Async<AsyncSeqRes<'T>>
and AsyncSeqRes<'T> = 
  | Nil
  | Cons of 'T * AsyncSeq<'T>

module AsyncSeq = 
  // Step 1

  let rec readFiles files : AsyncSeq<_> = async {
    match files with
    | [] -> return Nil
    | f::files ->
        let! d = readJson f
        return Cons(d, readFiles files) }

  let rec run f (aseq:AsyncSeq<_>) = async { 
    let! next = aseq
    match next with
    | Nil -> return ()
    | Cons(v, vs) -> 
        f v 
        return! run f vs }

  // Step 2

  let rec map f (aseq:AsyncSeq<_>) = async { 
    let! next = aseq
    match next with
    | Nil -> return Nil
    | Cons(v, vs) -> return Cons(f v, map f vs) }

  let rec delay op (aseq:AsyncSeq<_>) = async {
    let! next = aseq
    match next with
    | Nil -> return Nil
    | Cons(v, vs) ->
        do! op
        return Cons(v, delay op vs) }

  // Step 3

  let startAsObservable aseq = 
    { new IObservable<_> with
        member x.AddHandler(f) = 
          run f aseq |> Async.start; fun () -> () }

show "section5"

//[ for i in 0 .. 364 -> sprintf "/data/%d.json" i ]
//|> AsyncSeq.readFiles
//|> AsyncSeq.run (fun f ->
//    let p = jsonParse<Prices> f
//    let gbp = p.rates |> Array.find (fun r -> r.code = "GBP")
//    Section5.current.innerText <- sprintf "GBP: %A" gbp.value )
//|> Async.start

let ignore a = async {
  let! _ = a
  return () }

//[ for i in 0 .. 364 -> sprintf "/data/%d.json" i ]
//|> AsyncSeq.readFiles
//|> AsyncSeq.delay (ignore (Async2.awaitObservable (Observable.onClick Section5.next)))
//|> AsyncSeq.delay (Async.sleep 100)
//|> AsyncSeq.map jsonParse<Prices>
//|> AsyncSeq.map (fun p -> p.rates |> Array.find (fun r -> r.code = "GBP"))
//|> AsyncSeq.run (fun gbp -> Section5.current.innerText <- sprintf "GBP: %A" gbp.value)
//|> Async.start

[ for i in 0 .. 364 -> sprintf "/data/%d.json" i ]
|> AsyncSeq.readFiles
|> AsyncSeq.delay (Async.sleep 250)
|> AsyncSeq.map jsonParse<Prices>
|> AsyncSeq.map (fun p -> 
    let gbp = p.rates |> Array.find (fun r -> r.code = "GBP") in gbp.value)
|> AsyncSeq.startAsObservable 
|> Observable.scan [] (fun st v -> v::st |> List.truncate 10)
|> Observable.map (fun list -> 
    let first = List.head list
    let last = List.last list
    first, if first > last then "green" else "red")
|> Observable.add (fun (p, dir) -> 
    Section5.current.style.color <- dir
    Section5.current.innerText <- sprintf "GBP: %A" p) 
