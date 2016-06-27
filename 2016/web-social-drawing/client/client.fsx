#nowarn "40"
#r "node_modules/fable-core/Fable.Core.dll"
open System
open Fable.Core
open Fable.Import.Browser
module FsOption = Microsoft.FSharp.Core.Option

// -------------------------------------------------------------------------------------------------
// Additional JavaScript functions and bindings
// -------------------------------------------------------------------------------------------------

[<Emit("JSON.stringify($0)")>]
let jsonStringify json : string = failwith "JS Only"

[<Emit("JSON.parse($0)")>]
let jsonParse<'R> (str:string) : 'R = failwith "JS Only"

module Async =
  /// Await the first occurrence of the specified event on the
  /// given element and return the event object casted to 'T
  let AwaitDomEvent<'T>(el:HTMLElement, event) =
    Async.FromContinuations(fun (cont, _, _) ->
      let mutable listener = EventListener(ignore)
      listener <- EventListener(fun e ->
        el.removeEventListener(event, U2.Case1 listener)
        cont(unbox<'T> e) )
      el.addEventListener(event, U2.Case1 listener) )

module Http =
  /// Send HTTP request asynchronously
  /// (does not handle errors properly)
  let Request(meth, url, data) =
    Async.FromContinuations(fun (cont, _, _) ->
      let xhr = XMLHttpRequest.Create()
      xhr.``open``(meth, url)
      xhr.onreadystatechange <- fun _ ->
        if xhr.readyState > 3. && xhr.status = 200. then
          cont(xhr.responseText)
        obj()
      xhr.send(defaultArg data "") )


// -------------------------------------------------------------------------------------------------
// State and helper functions for rendering etc.
// -------------------------------------------------------------------------------------------------

// We keep a mutable array of rectangles & a currently drawn rectangle
type Rectangle =
  { x1:float; y1:float; x2:float; y2:float; }

let rectangles = ResizeArray<Rectangle>()
let mutable selection = None

let mutable shape = "rect"
let circBtn = document.getElementById("circ") :?> HTMLButtonElement
let rectBtn = document.getElementById("rect") :?> HTMLButtonElement
circBtn.onclick <- fun _ -> shape <- "circ"; null
rectBtn.onclick <- fun _ -> shape <- "rect"; null

// Initialize the canvas & cancel selection (when drawing starts)
let canvas =  document.getElementsByTagName_canvas().[0]
canvas.onselectstart <- fun _ -> box false
let ctx = canvas.getContext_2d()

/// Add drawn rectangle and report it to the server too
let addRectangle rect =
  Http.Request("POST", "/addrect", Some(jsonStringify rect))
  |> Async.Ignore |> Async.StartImmediate
  rectangles.Add(rect)

/// Fill rectangle with the given colour
let fillRectangle r color =
  ctx.fillStyle <- U3.Case1 color
  ctx.fillRect (r.x1, r.y1, r.x2 - r.x1, r.y2 - r.y1)

/// Draw the current stated - erase, draw all rectangles & selection
let drawRectangles () =
  ctx.fillStyle <- U3.Case1 "rgb(0,0,0)"
  ctx.fillRect (0., 0., 1000., 1000.)
  rectangles |> Seq.iter (fun rect ->
    fillRectangle rect "rgba(255, 230, 120, 0.5)")
  selection |> FsOption.iter (fun rect ->
    fillRectangle rect "rgba(128, 128, 128, 0.2)")


// -------------------------------------------------------------------------------------------------
// User interaction - drawing rectangles & polling for refresh
// -------------------------------------------------------------------------------------------------

/// We are waiting for mouse down to start drawing
let rec waiting () = async {
  drawRectangles ()
  let! e = Async.AwaitDomEvent<MouseEvent>(canvas, "mousedown")
  let startPos = e.x - canvas.offsetLeft, e.y - canvas.offsetTop
  return! drawing startPos }

/// We are waiting for mouse move/up to continue or finish drawing
and drawing (x1, y1) = async {
  let! e = Async.AwaitDomEvent<MouseEvent>(canvas, "mousemove")
  let x2, y2 = e.x - canvas.offsetLeft, e.y - canvas.offsetTop
  let rect = { x1=x1; y1=y1; x2=x2; y2=y2 }
  if e.buttons > 0. then
    drawRectangles ()
    selection <- Some rect
    return! drawing (x1, y1)
  else
    addRectangle rect
    selection <- None
    return! waiting() }

/// Infinite loop that refreshes rectangles from the server repeatedly
let refreshing () = async {
  while true do
    let! res = Http.Request("GET", "/getrects", None)
    let rects = jsonParse<Rectangle[]>(res)
    rectangles.Clear()
    for r in rects do rectangles.Add(r)
    drawRectangles ()
    do! Async.Sleep(250) }


refreshing () |> Async.StartImmediate
waiting () |> Async.StartImmediate
