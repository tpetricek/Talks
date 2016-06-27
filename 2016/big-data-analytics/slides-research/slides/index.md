- title : Analysing Big Data in the Cloud
- description : Working with small time-series data is fun. You can easily load daily Microsoft stock
    prices into memory and find the most successful year it its history.  But what if you have prices
    at millisecond frequency for thousands of stocks or high-resolution temperatures for the entire globe?
- author : Tomas Petricek
- theme : night
- transition : none

***************************************************************************************************
 - data-background : images/houses3.jpg
 - class : withbackground

# Analysing<br /> _Big Time-Series Data_ <br /> in the Cloud

<br />
<br />
<br />
<br />
<br />

### Tomas Petricek<br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) 

***************************************************************************************************

## _Programming experience_ research

***************************************************************************************************

### The _most interesting_ programming experience is about _telling stories with data_

***************************************************************************************************

<img src="images/paper1.png" style="width:400px" />
<img src="images/paper2.png" style="width:400px" />

<div class="fragment">

Distinguished paper at _PLDI 2016_

</div>

***************************************************************************************************
 - data-background : images/houses1.jpg
 - class : withbackground

# DEMO

## UK housing crisis<br />(April 2016)

***************************************************************************************************

<img src="images/ionide1.png" />

***************************************************************************************************

# www.**ionide**.io

<br/>
<div class="fragment">

_Atom_ and _VS Code_ bindings for F#

_Open_ with support for _community tooling_

<br /><br />
</div>

***************************************************************************************************

## HTML formatters

<br/>
<div class="fragment">

Supported in _Ionide_ and _FsLab Journals_

Planned support in _Jupyter_ notebooks

</div>

***************************************************************************************************

## HTML formatters

<br/>

    fsi.AddHtmlPrinter(fun (table:Table) ->
      [ "style", "<style>td { color:red; }</style>" ],
      table.InlineHtml )

***************************************************************************************************
 - data-background : images/houses3.jpg
 - class : withbackground

# DEMO

## UK housing crisis<br />(1995 - 2016)

***************************************************************************************************

<img src="images/ionide2.png" />

***************************************************************************************************

## Deedle and Big Deedle

<br/>
<div class="fragment">

Exploratory _data frame_ and _time-series_ library

_In-memory_ data and _virtual_ data sources

<br />

Define addressing `IRangeKeyOperations<'A>` <br />
and data souurces `IVirtualVectorSource<'T>`

</div>


***************************************************************************************************
 - data-background : images/clouds.jpg
 - class : withbackground

# DEMO

## Processing house <br />prices in the cloud

***************************************************************************************************

<img src="images/ionide3.png" />

***************************************************************************************************

<code class="fragment" style="font-size:70pt">async { .. }</code>

***************************************************************************************************

<code style="font-size:70pt;color:#ec7c4B;">cloud { .. }</code>

***************************************************************************************************

# www.**mbrace**.io

<br />

## Run your _computations_ <br />where your _data_ is

***************************************************************************************************

## Analysing big data with F#

 - _fsharp.org_ for expressive, efficient & correct code
 - _ionide.io_ for modern extensible tooling
 - _fslab.org_ for all things data science
 - _mbrace.io_ for interactive scalable computing<div style="margin-bottom:30px"></div>
 - _Deedle_ for time-series and data frames
 - _R Provider_ for world-class stats packages
 - _XPlot_ for rich HTML5 charting

***************************************************************************************************

# **Thank you!**

<br /><br />

Full-length version of the talk:<br />
[https://vimeo.com/171317247](https://vimeo.com/171317247)

<br /><br />

<h3 style="position:relative;top:20px">Tomas Petricek</h3>

[tomasp.net](http://tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomas@tomasp.net](mailto:tomas@tomasp.net)
