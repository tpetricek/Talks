module AsyncReactive.Helpers
open Fable.Import.Browser
open Fable.Core

// ------------------------------------------------------------------------------------------------
// Infrastructure
// ------------------------------------------------------------------------------------------------

module Section1 = 
  let light = document.getElementById("light") :?> HTMLDivElement
  let next = document.getElementById("next") :?> HTMLButtonElement

module Section2 = 
  let current = document.getElementById("current") :?> HTMLParagraphElement
  let next = document.getElementById("next2") :?> HTMLButtonElement

let show sec = 
  let secs = document.getElementsByTagName("section")
  for i in 0 .. int secs.length - 1 do (secs.[i] :?> HTMLTableSectionElement).style.display <- "none"
  document.getElementById(sec).style.display <- ""

module Async = 
  let AwaitGuiEvent (f:System.Func<'T, obj> -> unit) =
    Async.FromContinuations(fun (cont, econt, ccont) ->
      let mutable called = false
      f (System.Func<'T, obj>(fun _ -> 
        if not called then cont()
        called <- true
        obj())) )


[<Emit("JSON.parse($0)")>]
let jsonParse<'R> (str:string) : 'R = failwith "JS Only"

type AsyncSeq<'T> = Async<AsyncSeqInner<'T>> 
and AsyncSeqInner<'T> =
  | Nil
  | Cons of 'T * AsyncSeq<'T>

module AsyncSeq = 

  [<GeneralizableValue>]
  let empty<'T> : AsyncSeq<'T> = 
    async { return Nil }

  let pairwise (seq:AsyncSeq<'T>) = 
    let rec loop prev seq = async {
      let! v = seq
      match v with 
      | Nil -> return Nil
      | Cons (h,t) ->
          return Cons((prev, h), loop h t) }
    async {
      let! first = seq
      match first with 
      | Nil -> return Nil
      | Cons(f, t) -> return! loop f t }         
 
  let singleton (v:'T) : AsyncSeq<'T> = 
    async { return Cons(v, empty) }

  let rec append (seq1: AsyncSeq<'T>) (seq2: AsyncSeq<'T>) : AsyncSeq<'T> = 
    async { let! v1 = seq1
            match v1 with 
            | Nil -> return! seq2
            | Cons (h,t) -> return Cons(h,append t seq2) }

  let start (asq:AsyncSeq<unit>) = 
    let rec loop asq = async {
      let! res = asq
      match res with 
      | Nil -> ()
      | Cons(_, t) -> return! loop t }
    loop asq |> Async.StartImmediate

open AsyncSeq

let readJson<'T> fn =
  Async.FromContinuations(fun (cont, econt, ccont) ->
    let xh = XMLHttpRequest.Create()
    xh.addEventListener_readystatechange(fun p -> 
      if xh.readyState > 3. && xh.status = 200. then
        cont (jsonParse<'T> xh.responseText)
      null)
    xh.``open``("GET", fn, true)
    xh.send("") )

type AsyncSeqBuilder() =
  member x.Yield(v) = singleton v
  member x.YieldFrom(s) = s
  member x.Zero () = empty
  member x.Bind (inp:Async<'T>, body : 'T -> AsyncSeq<'U>) : AsyncSeq<'U> = 
    async.Bind(inp, body)
  member x.Combine (seq1:AsyncSeq<'T>,seq2:AsyncSeq<'T>) = 
    append seq1 seq2
  member x.While (gd, seq:AsyncSeq<'T>) = 
    if gd() then x.Combine(seq,x.Delay(fun () -> x.While (gd, seq))) else x.Zero()
  member x.Delay (f:unit -> AsyncSeq<'T>) = 
    async.Delay(f)
  member x.For(seq:seq<'T>, f) = 
    let enum = seq.GetEnumerator()
    x.While((fun () -> enum.MoveNext()), x.Delay(fun () -> 
      f enum.Current))

let asyncSeq = new AsyncSeqBuilder()

module AsyncSeq2 = 
  let rec collect f (input : AsyncSeq<'T>) : AsyncSeq<'TResult> = asyncSeq {
    let! v = input
    match v with
    | Nil -> ()
    | Cons(h, t) ->
        yield! f h
        yield! collect f t }
        
type AsyncSeqBuilder with
  member x.For(source, body) = AsyncSeq2.collect body source