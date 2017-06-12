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

// TODO: Async (with Start)

module Async = 
  // Step 1
  // TODO: sleep

  // Step 2
  // TODO: bind & unit & start

  // Step 3
  // TODO: AsyncBuilder (+Zero,ReturnFrom,Delay,For)
  // DEMO:
  //type AsyncBuilder() = 
  //  member x.Return(v) = unit v
  //  member x.Bind(a, f) = bind f a
  do ()
            
// TODO: section1
//(Async.sleep 1000).Start(fun _ ->
//  Section1.light.style.backgroundColor <- "red")
// TODO: Rewrite using bind & unit & start

// TODO: let async = Async.AsyncBuilder()
// DEMO:
//async {
//  for clr in ["green";"orange";"red"] do
//    do! Async.sleep 1000
//    Section1.light.style.backgroundColor <- clr } |> Async.start


// ------------------------------------------------------------------------------------------------
// Events
// ------------------------------------------------------------------------------------------------

// TODO: IEvent<'T>

module Event = 
  // Step 1
  // TODO: onClick & add

  // Step 2
  // TODO: map, merge & scan
  do ()

// TODO: section2
// DEMO:
//Event.onClick Section2.up |> Event.add (fun _ -> Section2.lbl.innerText <- "UP")
//Event.onClick Section2.down |> Event.add (fun _ -> Section2.lbl.innerText <- "DOWN")

// DEMO:
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

// TODO: IObservable<'T> 

module Observable = 
  // Step 1
  // TODO: Port Event code to Observable code

  // Step 2
  // TODO: stateful 
  do ()

module Async2 =
  // Step 3
  // TODO: awaitObservable (IObservable<'T> -> Async<'T>)
  do ()

// TOOO: section3
// DEMO: 
//let e1 = Observable.onClick Section3.up |> Observable.map (fun _ -> 1)
//let e2 = Observable.onClick Section3.down |> Observable.map (fun _ -> -1)
//
//let evt = 
//  Observable.merge e1 e2
//  |> Observable.scan 0 (+)
//  |> Observable.map (sprintf "Count: %d")


// DEMO:
//let mutable d = ignore
//
//Observable.onClick Section3.start |> Observable.add (fun _ -> 
//  d <- evt.AddHandler(fun s -> Section3.lbl.innerText <- s))
//Observable.onClick Section3.stop |> Observable.add (fun _ -> 
//  d () )

// TODO: section4
// TODO: traffic light with awaitObservable onClick Section4.next

// ------------------------------------------------------------------------------------------------
// Async sequences - motivation
// ------------------------------------------------------------------------------------------------

//let readJson fn =
//  { new Async<_> with
//      member x.Start(f) =
//        let xh = B.XMLHttpRequest.Create()
//        xh.addEventListener_readystatechange(fun p -> 
//          if xh.readyState > 3. && xh.status = 200. then
//            f xh.responseText
//          null)
//        xh.``open``("GET", fn, true)
//        xh.send("") }

//type Rate =
//  { code : string
//    value : float }
//
//type Prices = 
//  { rates : Rate[] }

// TODO: section5
// TODO: async & add sleep

// DEMO:
//  for i in [0 .. 364] do
//    let! p = readJson (sprintf "/data/%d.json" i)
//    let p = jsonParse<Prices> p 
//    let gbp = p.rates |> Array.find (fun r -> r.code = "GBP")
//    Section5.current.innerText <- sprintf "GBP: %A" gbp.value 

// ------------------------------------------------------------------------------------------------
// Async sequences - code
// ------------------------------------------------------------------------------------------------

// TODO: AsyncSeq and AsyncSeqRes definitions

module AsyncSeq = 
  // Step 1
  // TODO: readFiles & run

  // Step 2
  // TODO: map and delay (taking async) 

  // Step 3
  // TODO: startAsObservable
  do ()

// Step 1

// TODO: section5
// TODO: [ for i in 0 .. 364 -> sprintf "/data/%d.json" i ]
// TODO: readFiles & run & Async.start

// Step 2

// TODO: Use map and add delay
// TODO (optional): Ignore and wait for clicks

// Step 3

// TODO: startAsObservable
// DEMO:
//|> Observable.scan [] (fun st v -> v::st |> List.truncate 10)
//|> Observable.map (fun list -> 
//    let first = List.head list
//    let last = List.last list
//    first, if first > last then "green" else "red")
//|> Observable.add (fun (p, dir) -> 
//    Section5.current.style.color <- dir
//    Section5.current.innerText <- sprintf "GBP: %A" p) 
