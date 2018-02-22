module GammaDemo.Images

open Fable.Core
open Fable.Import
open Fable.Import.Browser
open GammaDemo.Common

let private w, private h = 665, 1000
let canvas = Browser.document.getElementById("loader") :?> HTMLCanvasElement
canvas.width <- float w
canvas.height <- float h

let canvasOut = Browser.document.getElementById("canv") :?> HTMLCanvasElement
canvasOut.width <- float w
canvasOut.height <- float h

let ctx = canvas.getContext_2d()
let ctxOut = canvasOut.getContext_2d()

let loadImage url = 
  Async.FromContinuations(fun (cont, _, _) ->
    let img = Browser.Image.Create(float w, float h)
    img.src <- url
    img.onload <- fun _ ->
      ctx.drawImage(U3.Case1 img, 0., 0., float w, float h, 0., 0., canvas.width, canvas.height)
      let src = ctx.getImageData(0., 0., float w, float h)
      cont (Array.init (int src.data.length) (fun i -> uint8(src.data.[i])))
      null )

let greyScale (data:uint8[]) =
  let res = Array.create data.Length 255uy
  for i in 0 .. 4 .. int data.Length-1 do
    let avg = (data.[i] + data.[i + 1] + data.[i + 2]) / 3uy
    res.[i + 0] <- avg
    res.[i + 1] <- avg
    res.[i + 2] <- avg
  res

let blur pixels (data:uint8[]) = 
  let count = float ((1 + pixels * 2) * (1 + pixels * 2))
  let res = Array.create data.Length 255uy
  for y in 0 .. int h-1 do
    for x in 0 .. int w-1 do
      let mutable sumr = 0.
      let mutable sumg = 0.
      let mutable sumb = 0.
      let mutable suma = 0.
      for dx in -pixels .. pixels do
        for dy in -pixels .. pixels do
          sumr <- sumr + float data.[((y+dy)*int w + (x+dx))*4 + 0]
          sumg <- sumg + float data.[((y+dy)*int w + (x+dx))*4 + 1]
          sumb <- sumb + float data.[((y+dy)*int w + (x+dx))*4 + 2]
          suma <- suma + float data.[((y+dy)*int w + (x+dx))*4 + 3]
      res.[(y*int w+x)*4+0] <- uint8 (sumr / count)
      res.[(y*int w+x)*4+1] <- uint8 (sumg / count)
      res.[(y*int w+x)*4+2] <- uint8 (sumb / count)
      res.[(y*int w+x)*4+3] <- uint8 (suma / count)
  res

let combine r (data1:uint8[]) (data2:uint8[]) = 
  Array.map2 (fun u1 u2 -> uint8 ((float u1 * r + float u2 * (1. - r)))) data1 data2

let mapFuture f (data:Future<_>) = Async.StartAsFuture <| async {
  let! v = Async.AwaitFuture data
  return f v }

let mapFuture2 f (data1:Future<_>) (data2:Future<_>) = Async.StartAsFuture <| async {
  let! v1 = Async.AwaitFuture data1
  let! v2 = Async.AwaitFuture data2
  return f v1 v2 }

type GammaImage =
  { Data : Future<uint8[]> }
  member x.greyScale() =
    { Data = mapFuture greyScale x.Data }
  member x.blur(pixels) =
    { Data = mapFuture (blur pixels) x.Data }
  member x.combine(img2, r) = 
    { Data = mapFuture2 (combine (float r/100.)) x.Data img2.Data }
  member x.render(f) =
    x.Data.Then(fun res ->
      let src = ctxOut.getImageData(0., 0., float w, float h)
      for i in 0 .. res.Length-1 do src.data.[i] <- float res.[i]
      ctxOut.putImageData(src, 0., 0.)
      f()
    )

type GammaImageConstructor() =
  member x.load(url) = 
    { Data = Async.StartAsFuture(loadImage url) }
    