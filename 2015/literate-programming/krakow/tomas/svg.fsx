module FsReveal.SmartArt

#r "System.Xml.Linq.dll"
open System
open System.Globalization
open System.Drawing
open System.Xml.Linq

// --------------------------------------------------------------------------------------
// Helpers
// --------------------------------------------------------------------------------------

let (!) s = XName.Get(s)

let writeHtml (html:string) =
  let template = """
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<body>
  <svg id="root" height="100" width="100">[SVG]</svg>
</body>
</html>"""
  let res = template.Replace("[SVG]", html)
  System.IO.File.WriteAllText("C:\\temp\\diagrams.html", res)

// --------------------------------------------------------------------------------------
// SVG domain model
// --------------------------------------------------------------------------------------

type Size = 
  | Relative of float 
  | Absolute of float

type Margin = 
  { Top : Size
    Right : Size
    Bottom : Size
    Left : Size }

type SplitKind = 
  | Vertical 
  | Horizontal

type LineCap = Butt | Square | Round
type LineJoin = Miter | Bevel | Round

type Fill = 
  | Color of Color
  //| Gradient
  //| Pattern

type Stroke = 
  { Color : Color 
    Width : float
    LineCap : LineCap
    LineJoin : LineJoin
    DashArray : int list }

type Text = string

type TextAlignment = 
  | TopLeft 
  | TopMiddle
  | TopRight
  | CenterLeft
  | Center
  | CenterRight
  | BottomLeft
  | BottomMiddle
  | BottomRight

type Graphics =
  | Empty
  | Rectangle
  | RoundedRectangle of Size * Size
  | Ellipse
  | Text of Text

  | Margin of Margin * Graphics

  | Combine of Graphics list
  | Split of SplitKind * Size list option * Graphics list

  | Align of TextAlignment * Graphics
  | Fill of Fill * Graphics
  | Stroke of Stroke * Graphics

type SizedGraphics =
  { Graphics : Graphics 
    Width : int
    Height : int }

// --------------------------------------------------------------------------------------
// SVG rendering
// --------------------------------------------------------------------------------------

type Rectangle =
  { Left : float
    Top : float
    Width : float
    Height : float }

type RenderContext =
  { BoundingBox : Rectangle 
    Alignment : TextAlignment
    Stroke : Stroke 
    Fill : Fill }

let size total = function
  | Absolute size -> size
  | Relative size -> total * size / 100.0

let applyMargin (margin:Margin) (rect:Rectangle) = 
  let l, t = size rect.Width margin.Left, size rect.Height margin.Top
  let r, b = size rect.Width margin.Right, size rect.Height margin.Bottom
  { Rectangle.Left = rect.Left + l
    Top = rect.Top + t
    Width = rect.Width - l - r
    Height = rect.Height - t - b }

let formatColor (color:Color) = 
  (float color.A) / 255.0, sprintf "rgb(%d,%d,%d)" color.R color.G color.B

let formatFill (fill:Fill) = 
  match fill with 
  | Fill.Color color ->
      let opacity, color = formatColor color
      [ XAttribute(!"fill", color)
        XAttribute(!"fill-opacity", opacity) ]
  
let formatStroke (stroke:Stroke) = 
  if stroke.Width > 0.0 then
    let opacity, color = formatColor stroke.Color
    [ XAttribute(!"stroke", color)
      XAttribute(!"stroke-opacity", opacity)
      XAttribute(!"stroke-linecap", (sprintf "%A" stroke.LineCap).ToLower())
      XAttribute(!"stroke-linejoin", (sprintf "%A" stroke.LineJoin).ToLower())
      XAttribute(!"stroke-width", stroke.Width) ] @
    match stroke.DashArray with
    | [] -> []
    | nums -> [XAttribute(!"stroke-dasharray", nums |> Seq.map (sprintf "%d") |> String.concat ",") ]
  else []

let formatRectangle (rx, ry) (rect:Rectangle) = 
  [ yield XAttribute(!"width", rect.Width)
    yield XAttribute(!"height", rect.Height)
    yield XAttribute(!"x", rect.Left)
    yield XAttribute(!"y", rect.Top) 
    if rx <> Relative 0.0 && rx <> Absolute 0.0 then 
      yield XAttribute(!"rx", size rect.Width rx)
    if ry <> Relative 0.0 && ry <> Absolute 0.0 then
      yield XAttribute(!"ry", size rect.Height ry) ]

let formatEllipse (rect:Rectangle) =
  [ XAttribute(!"rx", rect.Width/2.0)
    XAttribute(!"ry", rect.Height/2.0)
    XAttribute(!"cx", rect.Left + rect.Width/2.0)
    XAttribute(!"cy", rect.Top + rect.Height/2.0) ]

let formatTextAlignment (rect:Rectangle) align =
  [ match align with 
    | TopLeft | TopMiddle | TopRight ->
        yield XAttribute(!"y", rect.Top)
        yield XAttribute(!"dy", "1em")
    | CenterLeft | Center | CenterRight ->
        yield XAttribute(!"y", rect.Top + rect.Height/2.0)
        yield XAttribute(!"dy", "0.30em")
    | BottomLeft | BottomMiddle | BottomRight ->
        yield XAttribute(!"y", rect.Top + rect.Height) 
    match align with
    | TopLeft | CenterLeft | BottomLeft ->
        yield XAttribute(!"x", rect.Left)
        yield XAttribute(!"text-anchor", "start") 
    | TopMiddle | Center | BottomMiddle ->
        yield XAttribute(!"x", rect.Left + rect.Width/2.0)
        yield XAttribute(!"text-anchor", "middle") 
    | TopRight | CenterRight | BottomRight ->
        yield XAttribute(!"x", rect.Left + rect.Width)
        yield XAttribute(!"text-anchor", "end") ]

let formatText ctx (t:string) = 
  [ for attr in formatTextAlignment ctx.BoundingBox ctx.Alignment do
      yield box attr
    yield box t ]

let rec formatGraphics ctx = function
  | Empty -> []
  | RoundedRectangle(rx, ry) -> 
      [ XElement(!"rect", formatRectangle (rx, ry) ctx.BoundingBox, formatStroke ctx.Stroke, formatFill ctx.Fill) ]
  | Rectangle -> 
      [ XElement(!"rect", formatRectangle (Absolute 0.0, Absolute 0.0) ctx.BoundingBox, formatStroke ctx.Stroke, formatFill ctx.Fill) ]
  | Ellipse -> 
      [ XElement(!"ellipse", formatEllipse ctx.BoundingBox, formatStroke ctx.Stroke, formatFill ctx.Fill) ]
  | Text t ->
      [ XElement(!"text", formatText ctx t, formatStroke ctx.Stroke, formatFill ctx.Fill) ]

  | Align(align, graphics) -> formatGraphics { ctx with Alignment = align } graphics
  | Fill(fill, graphics) -> formatGraphics { ctx with Fill = fill } graphics
  | Stroke(stroke, graphics) -> formatGraphics { ctx with Stroke = stroke } graphics

  | Margin(margin, graphics) ->
      let bbox = applyMargin margin ctx.BoundingBox
      let ctx = { ctx with BoundingBox = bbox }
      formatGraphics ctx graphics

  | Combine(graphics) ->
      graphics |> List.collect (formatGraphics ctx)

  | Split(Horizontal, sizes, graphics) ->
      let count = float graphics.Length
      let sizes =
        match sizes with
        | None -> [ for _ in graphics -> Absolute (ctx.BoundingBox.Width / count) ]
        | Some s -> s
      let left = ref ctx.BoundingBox.Left
      Seq.zip graphics sizes
      |> Seq.mapi (fun i (gr, subSize) ->
        let subSize = size ctx.BoundingBox.Width subSize
        let bbox = { ctx.BoundingBox with Width = subSize }
        let ctx = { ctx with BoundingBox = { bbox with Left = left.Value } }
        left := left.Value + subSize
        formatGraphics ctx gr )
      |> Seq.concat |> List.ofSeq 

  | Split(Vertical, sizes, graphics) ->
      let count = float graphics.Length
      let sizes =
        match sizes with
        | None -> [ for _ in graphics -> Absolute (ctx.BoundingBox.Height / count) ]
        | Some s -> s
      let top = ref ctx.BoundingBox.Top
      Seq.zip graphics sizes
      |> Seq.mapi (fun i (gr, subSize) ->
        let subSize = size ctx.BoundingBox.Height subSize
        let bbox = { ctx.BoundingBox with Height = subSize }
        let ctx = { ctx with BoundingBox = { bbox with Top = top.Value } }
        top := top.Value + subSize
        formatGraphics ctx gr )
      |> Seq.concat |> List.ofSeq 

module Palette =
  let Vega10 = seq {
      while true do
        for c in [ "ff1f77b4"; "ffff7f0e"; "ff2ca02c"; "ffd62728"; "ff9467bd"; "ff8c564b"; "ffe377c2"; "ff7f7f7f"; "ffbcbd22"; "ff17becf" ] do
          yield Color.FromArgb(Int32.Parse(c, NumberStyles.HexNumber)) }
  let Vega20 = seq {
      while true do
        for c in [ "ff1f77b4"; "ffaec7e8"; "ffff7f0e"; "ffffbb78"; "ff2ca02c"; "ff98df8a"; "ffd62728"; "ffff9896"; "ff9467bd"; "ffc5b0d5"; "ff8c564b"; "ffc49c94"; "ffe377c2"; "fff7b6d2"; "ff7f7f7f"; "ffc7c7c7"; "ffbcbd22"; "ffdbdb8d"; "ff17becf"; "ff9edae5" ] do
          yield Color.FromArgb(Int32.Parse(c, NumberStyles.HexNumber)) }


module Chart = 
  let Bar(yvalues) = 
    let lo, hi = Seq.min yvalues, Seq.max yvalues
    let margin = 
      { Margin.Top = Absolute 0.0; Left = Absolute 0.0; Bottom = Absolute 0.0; Right = Absolute 1.0 }
    let bars = 
      [ for yval, clr in Seq.zip yvalues Palette.Vega10 ->
          let margin = { margin with Top = Relative (100.0 - yval/hi*100.0) }
          let body = Combine[Rectangle; Align(TopMiddle, Text(string yval)) ]
          Fill(Fill.Color clr, Margin(margin, body)) ]
    Split(Horizontal, None, bars)

let rect = { Rectangle.Left = 0.0; Top = 0.0; Width = 100.0; Height = 100.0 }
let stroke = { Stroke.Color = Color.MidnightBlue; LineCap = LineCap.Round; LineJoin = LineJoin.Round; Width = 0.0; DashArray = [] }
let fill = Fill.Color Color.LightSteelBlue
let ctx = { BoundingBox = rect; Stroke = stroke; Fill = fill; Alignment = TopRight }


// let m = { Margin.Left = Relative 10.0; Right = Relative 10.0; Top = Relative 20.0; Bottom = Relative 20.0 }
// let svg = Split(Horizontal, [Rectangle; Margin(m, Rectangle); Ellipse])

//let svg = Chart.Bar [ 10.0; 40.0; 15.0; 60.0; 55.0; 25.0 ]
//let svg = Split(Vertical, [ Combine [ Rectangle; Text("Hello world") ]; Ellipse ])

let data = [ 60.0; 50.0; 30.0; 20.0; 10.0; 5.0; ]
let half = (Seq.sum data) / 2.0
let prefixes = [ for i in 1 .. data.Length-2 -> abs((data |> Seq.take i |> Seq.sum)-half)]
let count = prefixes |> Seq.mapi (fun i v -> i, v) |> Seq.minBy snd

type Graphics with
  member x.Render(w, h) = 
    let rect = { Rectangle.Left = 0.0; Top = 0.0; Width = float w; Height = float h }
    let stroke = { Stroke.Color = Color.MidnightBlue; LineCap = LineCap.Round; LineJoin = LineJoin.Round; Width = 0.0; DashArray = [] }
    let fill = Fill.Color Color.LightSteelBlue
    let ctx = { BoundingBox = rect; Stroke = stroke; Fill = fill; Alignment = TopRight }
    let html = 
      x |> formatGraphics ctx
        |> Seq.map (fun e -> e.ToString()) |> String.concat ""
    sprintf """<svg id="root" height="%d" width="%d">%s</svg>""" h w html
  member x.ToHtml() = x.Render(100, 100)

type SizedGraphics with
  member x.ToHtml() = x.Graphics.Render(x.Width, x.Height)
    
let WithSize(w, h, g) = { Graphics = g; Width = w; Height = h }
// svg |> formatGraphics ctx |> writeHtml

type Tree = Tree of (Graphics * Color * Color) * Tree list

let smallMarg = { Margin.Top = Relative 5.0; Left = Relative 5.0; Bottom = Relative 5.0; Right = Relative 5.0 }
let largeMarg = { Margin.Top = Relative 30.0; Left = Relative 5.0; Bottom = Relative 30.0; Right = Relative 5.0 }

let rec depth = function
  | Tree(_, []) -> 1
  | Tree(_, ch) -> 1 + (List.map depth ch |> List.max)

let rec countLeafs = function
  | Tree(_, []) -> 1
  | Tree(_, ch) -> List.sumBy countLeafs ch

let rec drawTree ((Tree((text, backColor, textColor), children)) as t) =
  let box = Combine [Fill(Color backColor, RoundedRectangle(Absolute 10.0,Absolute 10.0)); Fill(Color textColor,text)]
  let marg = if children.Length <= 1 then smallMarg else largeMarg
  if children = [] then
    Margin(smallMarg, box)
  else
    let d = float (depth t)
    let childCounts = List.map (countLeafs >> float) children
    let childTotal = childCounts |> List.sum
    let splitSizes = childCounts |> List.map (fun c -> Relative(100.0/childTotal*c))
    let nested = Split(Vertical, Some splitSizes, List.map drawTree children)
    let split = Some [Relative(100.0/d); Relative(100.0-100.0/d)]
    Split(Horizontal, split, [Margin(marg, box); nested])

let (==>) root children = Tree(root, children)
let nd root = Tree(root, [])
let tx s = Text(s)

let Draw(diag) = Align(Center, drawTree diag)
let color ht = ColorTranslator.FromHtml(ht)

(*
let diag = 
  ("Source", color "#1F5B56", Color.White) ==>
    [ ("Tex", color "#325E6B", Color.White) ==>
        [ nd("Exe", color "#437E8E", Color.White) ]
      ("Pas", color "#31683F", Color.White) ==>
        [ nd("Pdf", color "#438E49", Color.White) 
          nd("Html", color "#438E49", Color.White) ] ]
let out = WithSize(400, 300, Draw(diag))

//let marg = { Margin.Top = Relative 5.0; Left = Relative 5.0; Bottom = Relative 5.0; Right = Relative 5.0 }
//Align(Center, Split(Horizontal, [Margin(marg, Combine [RoundedRectangle(Absolute 10.0,Absolute 10.0); Text "hi"]); Margin(marg, Combine [Rectangle; Text "hi"])]))
*)

module NumericLiteralG =
  let FromInt32 n = Absolute(float n)

let HtmlColor c = Color(ColorTranslator.FromHtml(c))
let WithMargin (t,r,b,l) c = Margin({Top=t; Right=r;Bottom=b;Left=l}, c)
(*
let MakeBox(backColor, content) =
  [ Fill(HtmlColor backColor, RoundedRectangle(10G, 10G)); 
    Fill(HtmlColor "#FFFFFF", Text content)]
  |> Combine
  |> WithMargin (10G, 10G, 10G, 10G)
let svg = 
  Split(Horizontal, None, [
    MakeBox("#1F5B56", "Diagrams")
    MakeBox("#325E6B", "..are..")
    MakeBox("#31683F", "fun!")
  ])
let out = WithSize(700, 450, Align(Center, svg))
out.ToHtml() |> writeHtml
*)