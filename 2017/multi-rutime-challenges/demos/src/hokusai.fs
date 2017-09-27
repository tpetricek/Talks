module ManLang.Demo1

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open ManLang.Utils

// ----------------------------------------------------------------------------
// Magic constants
// ----------------------------------------------------------------------------

let c = Complex(-0.70176, -0.3842)
let w = -1.75, 2.75
let h = -1., 2.95
let width = 400.0
let height = 300.0

// ----------------------------------------------------------------------------
// Helper functions
// ----------------------------------------------------------------------------

let transition (r1,g1,b1) count (r2,g2,b2) = [
  for c in 0 .. count - 1 ->
    let k = float c / float count
    let mid v1 v2 =
      (float v1 + ((float v2) - (float v1)) * k)
    (mid r1 r2, mid g1 g2, mid b1 b2) ]

let palette =
  [| yield! transition (245,219,184) 3  (245,219,184)
     yield! transition (245,219,184) 4  (138,173,179)
     yield! transition (138,173,179) 4  (2,12,74)
     yield! transition (2,12,74)     4  (61,102,130)
     yield! transition (61,102,130)  8  (249,243,221)
     yield! transition (249,243,221) 32 (138,173,179)
     yield! transition (138,173,179) 32 (61,102,130) |]

let setPixel (img:ImageData) x y width (r, g, b) =
  let index = (x + y * int width) * 4
  img.data.[index+0] <- r
  img.data.[index+1] <- g
  img.data.[index+2] <- b
  img.data.[index+3] <- 255.0

// ----------------------------------------------------------------------------
// Fractals <3 <3
// ----------------------------------------------------------------------------

let iterate x y =
  // TODO: generate sequence from Complex(x, y) 
  // TODO: using c1 = current^2 + c0
  ()

let countIterations max x y =
  iterate x y
  // TODO: length of take max-1 while abs(v) < 2 

let render (canv:HTMLCanvasElement) =
  // DEMO: Render fractal
  ()

let hokusai () = 
  let go : HTMLButtonElement = unbox (document.getElementById("go"))
  let canv : HTMLCanvasElement = unbox (document.getElementById("hokusai"))
  if go <> null then go.addEventListener_click(fun _ ->
    render canv
    null )