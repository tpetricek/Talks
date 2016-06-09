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

### Tomas Petricek, fsharpWorks <br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) | [fsharpworks.com](http://fsharpworks.com)

***************************************************************************************************

## F# is a _general purpose_ language

***************************************************************************************************

## Why F# for _analytical components_?

***************************************************************************************************

## F# Software Foundation

 - _Language_ design and _compiler_
 - _Editor and tool_ creators
 - _Commercial user_ contributions
 - _Open-source_ community

<br /><br /><br />

### [www.fsharp.org](http://fsharp.org) | [@fsharporg](http://twitter.com/fsharporg) | [#fsharp](https://twitter.com/search?q=%23fsharp)

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

<br/>
<div class="fragment">

Data _scripting_ for the _cloud_

_Cloud_ computations, _data flow_ streams

<br /><br />
</div>




***************************************************************************************************
 - data-background : images/london.jpg
 - class : withbackground

# DEMO

## Tick trades with bid/ask<br />(WDC 2010-2015)

***************************************************************************************************

## Run your _computations_ <br />where your _data_ is

***************************************************************************************************

## Integrate with anything<br /> via _type providers_

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
 - class : wordcloud

software stacks _trainings_

_mac and linux_ **cross platform** tutorials

## F# Software Foundation

user groups **open source** _Xamarin_

community **[www.fsharp.org](http://www.fsharp.org)** research

support  _contributions_ diversity

***************************************************************************************************

# Thank you!

<br />

 - _FP Lab Hour_ - 13:40, room 10
 - _FP Session_ - Elixir and more, room 3
 - _fsharpWorks_ - trainings & consulting

<br /><br /><br />

<h3 style="position:relative;top:20px">Tomas Petricek</h3>

[tomasp.net](http://tomasp.net) | [fsharpworks.com](http://www.fsharpworks.com)<br />
[@tomaspetricek](http://twitter.com/tomaspetricek) | [tomas@tomasp.net](mailto:tomas@tomasp.net)
