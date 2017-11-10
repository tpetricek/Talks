module Counter

open Fable.Core
open Fable.Import
open Fable.Import.Browser

open Compost
open Compost.Interactive

// ----------------------------------------------------------------------------
// Wrappers and helpers for creating charts
// ----------------------------------------------------------------------------

let render viz = 
  let el = document.getElementById("out")
  let svg = Compost.createSvg false false (el.clientWidth, el.clientHeight) viz
  svg |> Html.renderTo el

let series d = Array.ofList [ for x, y in d -> unbox x, unbox y ]
let rnd = System.Random()
let numv v = COV(CO v)
let catv n s  = CAR(CA s, n)

// ----------------------------------------------------------------------------
// Basich and interactive charts 
// ----------------------------------------------------------------------------

let ds = [| for x in 0.0 .. 0.1 .. 6.28 -> x, sin x |]
compost.charts.line(ds).show("")

let db = series ["Good", 13.0; "Bad", 14.0; "Evil", 4.0]
youguess.bars(db).show("")

// ----------------------------------------------------------------------------
// Basich charts and interactive charts
// ----------------------------------------------------------------------------

let viz1 = Derived.Column(CA "Good", CO(100.0))
//render viz1














(*
let viz2 = 
  Shape.Layered [
    Shape.Style
      ( (fun s -> { s with Fill = Solid(1.0, HTML "red") }),
        Derived.Column(CA("Good"), CO(100.0)) )
    Shape.Style
      ( (fun s -> { s with Fill = Solid(1.0, HTML "blue") }),
        Derived.Column(CA("Bad"), CO(20.0)) )
  ]

render (Shape.Axes(false, false, true, true, viz))
*)



let data = 
  [ ("Good", "#2ca02c", 13.0)
    ("Bad", "#ff7f0e", 14.0)
    ("Evil", "#8c564b", 4.0) ]

let viz3 = 
  [ for l, c, v in data ->
      Shape.Padding
        ( (0.0, 5.0, 0.0, 5.0),
          Shape.Style
            ( (fun s -> { s with Fill = Solid(1.0, HTML c) }),
              Derived.Column(CA(l), CO(v)) )) ]

// render (Shape.Axes(false, false, true, true, Shape.Layered viz3))
