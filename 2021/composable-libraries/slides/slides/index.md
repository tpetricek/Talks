- title : Designing composable functional libraries: not just for data visualization
- description : Designing composable functional libraries: not just for data visualization
- author : Tomas Petricek
- theme : simple
- transition : none

****************************************************************************************************

<img src="images/ndc.png" />
<div style="margin:50px 0px 150px">

# _<span style="font-weight:300">Designing composable functional libraries</span> <br /> not just for visualization_

</div>

**Tomas Petricek**

University of Kent & fsharpWorks<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

****************************************************************************************************
- class: part

# _Motivation_
## Functional thinking about charts

----------------------------------------------------------------------------------------------------

<img src="images/gamma.png" class="nb" style="max-width:600px;float:left;margin:30px 30px 100px 0px" />

**What is this  
talk about?**

<div class="fragment" style="margin-top:40px">

Making facts great again

_Building more open, transparent and engaging data visualization_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/code.png" class="nb" style="max-width:600px;float:left;margin:30px 30px 100px 0px" />

**What is this  
talk about?**

<div class="fragment" style="margin-top:40px">

Functional thinking!

_New look at an interesting and tricky domain_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/chart1.png" class="nb fragment" style="max-width:600px;float:left;margin:30px 30px 100px 0px" />

**What is a chart?**

<div class="fragment" style="margin-top:40px">

A very long list...

_Bar chart_  
_Column chart_  
_Line chart_  
_Area chart_  
_Scatter chart_  
_Histogram_  
_Combo chart???_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/chart2.png" class="nb fragment" style="max-width:600px;float:left;margin:30px 30px 100px 0px" />

**What is a chart?**

<div class="fragment" style="margin-top:40px">

Uh...

_Maybe chart is just an SVG graphics with text, shapes and pixel coordinates?_

</div>

----------------------------------------------------------------------------------------------------

# _What is a chart?_


**D3 is a too low-level answer**

```
x = d3.scaleLinear([0, m - 1], [0, width])
y = d3.scaleLinear([0, 1], [height, 0])
z = d3.interpolateCool
d3.area().x((d, i) => x(i)).y0(d => y(d[0])).y1(d => y(d[1]))
```

**Google Charts is a too high-level answer**

```javascript
var options = {
  vAxis: {title: 'Cups'}, hAxis: {title: 'Month'},
  seriesType: 'bars', series: {5: {type: 'line'}} };
chart.draw(data, options);
```

----------------------------------------------------------------------------------------------------

<img src="images/chart3.png" class="nb fragment" style="max-width:600px;float:left;margin:30px 30px 100px 0px" />

**What is a chart?**

<div class="fragment" style="margin-top:40px">

Also interactivity!

_A chart where the reader has to make a guess before seeing the answer._

</div>

----------------------------------------------------------------------------------------------------

# _What is a chart_
## Fundamentals of a chart

_<i class="fa fa-map"></i>_ Projections _from domain values to pixels_

_<i class="fa fa-shapes"></i>_ Shapes _such as areas and lines_

_<i class="fa fa-link"></i>_ Composition _of multiple shapes and text_

_<i class="fa fa-mouse-pointer"></i>_ Interactivity _state depends on user input_

****************************************************************************************************
- class: part

# _Composing shapes_
## Fundamentals of a chart

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Creating a bar chart

----------------------------------------------------------------------------------------------------

# _Scales_
## Continuous and categorical scales

<img src="images/scales.png" class="nb" style="max-width:600px;margin:0px 0px 0px 20px" />

----------------------------------------------------------------------------------------------------

# _Scales_
## Continuous and categorical scales

```
type Value =
  | Categorical of string * float
  | Continuous of float

type Scale =
  | Continuous of float * float
  | Categorical of string[]
```

----------------------------------------------------------------------------------------------------

# _Modelling charts_
## A chart is an algebraic data type

```
type Shape =
  | Style of (Style -> Style) * Shape
  | Line of seq<Value * Value>
  | Shape of seq<Value * Value>
  | Axes of (bool * bool * bool * bool) * Shape
  | Padding of (float * float * float * float) * Shape
  | Layered of seq<Shape>
```

----------------------------------------------------------------------------------------------------

# _Modelling charts_
## Avoiding X and Y value mix-up!

<div class="fragment">

```
type Shape<[<Measure>] 'vx, [<Measure>] 'vy> =
  | Style of (Style -> Style) * Shape<'vx, 'vy>
  | Line of seq<Value<'vx> * Value<'vy>>
  | Shape of seq<Value<'vx> * Value<'vy>>
  | Axes of (bool * bool * bool * bool) * Shape<'vx, 'vy>
  | Padding of (float * float * float * float) * Shape<'vx, 'vy>
  | Layered of seq<Shape<'vx, 'vy>>
```

</div>

----------------------------------------------------------------------------------------------------

# _Projections_
## From domain space to pixel space

<div class="bigcode">

```
type Projection<'vx, 'vy, 'ux, 'uy> = (...)
```

<div class="fragment">

```
val project :
     Scale<'vx> * Scale<'vy>
  -> Value<'vx> * Value<'vy>
  -> Projection<'vx, 'vy, 'ux, 'uy>
  -> Value<'ux> * Value<'uy>
```

</div>
</div>

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Line chart with background

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Adding a chart title

----------------------------------------------------------------------------------------------------

# _Composition_
## There are more ways than one!

<div class="fragment">

```
type Shape<[<Measure>] 'vx, [<Measure>] 'vy> =
  | (* ... *)
  | InnerScale of Scale<'vx> * Scale<'vy> * Shape<'vx, 'vy>
  | OuterScale of Scale<'vx> * Scale<'vy> * Shape<'vx, 'vy>
  | Layered of seq<Shape<'vx, 'vy>>
```

</div>

----------------------------------------------------------------------------------------------------

# _Domain modelling_
## Functional thinking about charts

_<i class="fa fa-chart-line"></i>_ Domain primitives _rather than graphics primitives_

_<i class="fa fa-chart-bar"></i>_ Domain values _rather than pixels!_

_<i class="fa fa-link"></i>_ Composition _in multiple different ways_

_<i class="fa fa-ruler"></i>_ Units of measure _so that I can implement it :-)_


****************************************************************************************************
- class: part

# _Library design_
## Three functional design patterns

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Refactoring chart title

----------------------------------------------------------------------------------------------------

# _Layers of abstraction_
## From charts to pixels

<img src="images/layers.png" class="nb" style="max-width:500px;margin:0px 0px 0px 20px" />

----------------------------------------------------------------------------------------------------

# _Transformations_
## Shape rendering pipeline

<img src="images/transformations.png" class="nb" style="max-width:500px;margin:0px 0px 0px 20px" />

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Creating animated bar chart

----------------------------------------------------------------------------------------------------

# _State + Update_
## Elm-based application architecture

<img src="images/elmish.png" class="nb" style="max-width:800px;margin:0px 0px 0px 20px" />

****************************************************************************************************
- class: part

# _Implementation_
## JavaScript libraries in F#

----------------------------------------------------------------------------------------------------

<img src="images/compost.png" class="nb" style="max-width:570px;float:left;margin:30px 30px 100px 0px" />

**Wait, not everyone uses F# & Fable?**

<div class="fragment" style="margin-top:40px">

_Add a lightweight wrapper API_

Compile to plain JavaScript library!

</div>

----------------------------------------------------------------------------------------------------

# _Supporting JavaScript_
## Lightweight wrapper API

```
type JsCompost =
  abstract nest : obj * obj * obj * obj * Shape -> Shape
  abstract scale : Scale * Scale * Shape -> Shape
  abstract overlay : Shape[] -> Shape
  abstract font : string * string * Shape -> Shape
  abstract column : string * float -> Shape
  abstract bar : float * string -> Shape
  abstract shape : obj[][] -> Shape
  abstract line : obj[][] -> Shape
  abstract axes : string * Shape -> Shape
  abstract render : string * Shape -> unit
  // (and a few more but not too many!)
```

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Using Compost from JavaScript

****************************************************************************************************
- class: part

# _Summary_
## Functional thinking about charts

----------------------------------------------------------------------------------------------------

# _Composable libraries_

Fundamental question  
_What is the thing we're working with?_

Domain modelling  
_Primitives and composition (with units)_

Functional patterns  
_Multiple layers, transformations, states and updates_

<br />

**Tomas Petricek**, University of Kent & fsharpWorks<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)
