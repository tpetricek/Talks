- title : Scalable data science with F#
- description : In this presentation, we look how to use F# and F# data science tools.
     The talk introduces business reasons for using F# and then looks at a number of
     practical examples of accessing, exploring and visualizing data and scaling your
     computations to the cloud.
- author : Tomas Petricek
- theme : white
- transition : none

****************************************************************************************************

# F# + FsLab + M-Brace

## Scalable data science with F#

<img src="images/fsharpworks.png" style="width:300px;position:absolute;right:0px;margin-top:250px" />
<div style="position:absolute;text-align:left;left:0px;margin-top:220px">

Tomas Petricek  
[www.fsharpworks.com](http://www.fsharpworks.com)

</div>
<style>
.vtop td { vertical-align:top; }
.libs h2 { color:black; }
.libs { position:absolute; left:30px; }
</style>

----------------------------------------------------------------------------------------------------

# About us

<div style="float:right">
<img src="images/rwfp.png" style="width:200px" /><br />
<img src="images/deepdives.png" style="width:200px" />
</div>

## Tomas Petricek

 - F# book author and developer
 - Contributed to F# at _Microsoft_
 - Working on _data science tools_  
   with _BlueMountain Capital_

## fsharpWorks

 - Provides _F# training & consulting_
 - _Finance_, _machine learning_, etc.
 - Working mostly in _UK_ and _US_

****************************************************************************************************

# The F# Software Foundation

## [www.fsharp.org](http://www.fsharp.org)

The mission of the _F# Software Foundation_ is to promote, protect, and
advance the F# programming language.

<br />

 - Independent registered non-profit
 - Open-source community & contributors
 - Microsoft, Xamarin, Tachyus, Jet.com,...

----------------------------------------------------------------------------------------------------

# F# in the news

## ThoughtWorks radar, March 2012

_F# is excellent at expressing business logic._ Developers may opt to express
their domain in F# with the plumbing code in C#.

## TIOBE index, March 2014

F# has risen to the 12th spot in this month's rankings.
As the index headline notes, _"F# is on its way to the Top 10."_

----------------------------------------------------------------------------------------------------

# F# for Analytical Components

**Time-to-market**

 - According to testimonials, F# makes development faster

**Correctness**

 - Type system prevents many errors including `null`

**Complexity**

 - Functional style makes it easier to write complex logic

**Efficiency**

 - Performance profile of C# with async and parallelism

----------------------------------------------------------------------------------------------------

# F# Testimonials

## [fsharp.org/testimonials](http://fsharp.org/testimonials/)

"I have delivered three business critical projects written in F#.  
I am still _waiting for the first bug_ to come in."
([source](http://fsharp.org/testimonials/#simon-cousins-2))

"I wrote the first prototype of the click prediction system
deployed in Microsoft AdCenter in F# in _a few days_."
([source](http://fsharp.org/testimonials/#simard-1))

On-line real-time action game: _6 months uptime_, 30B+ requests, _0 exception, 0 crash, 0 reset_
([source](https://twitter.com/viet2nt/status/604310788631736320))

----------------------------------------------------------------------------------------------------

# Why are they using F#

** <i class="fa fa-car"></i> Tachyus** oil & gas startup

 - time-to-market, cross-platform, data analysis

** <i class="fa fa-dollar"></i> Credit Suisse** finance industry

 - complexity, correctness, efficiency

** <i class="fa fa-facebook-official"></i> GameSys** building MMORPG games

 - time-to-market, scalability, efficiency

****************************************************************************************************

# FsLab: F# tools for data science

## www.fslab.org

FsLab is a collection of _libraries for data-science_. It provides a rapid development envi­ronment
that lets you write advanced analysis with a few lines of production-quality code.

----------------------------------------------------------------------------------------------------

# [FsLab.org](http://www.fslab.org): Data science with F#

The data science workflow

<img src="images/bmc.gif" style="float:right;" />

 - _Data access_ integrated in the language
 - _Interactive analysis_ using .NET & R
 - _Visualization_ with HTML & PDF reports

High-quality open-source libraries

<div style="margin-top:30px;margin-left:50px">
<img src="images/logo-fslab.png" style="width:150px;margin-right:20px;" />
<img src="images/logo-fsdata.png" style="width:150px;margin-right:20px;" />
<img src="images/logo-deedle.png" style="width:150px;margin-right:20px;" />
<img src="images/logo-rprovider.png" style="width:150px;margin-right:20px;" />
</div>

----------------------------------------------------------------------------------------------------

<table class="vtop"><tr><td>

# _DATA ACCESS_

## Visualizing world indicators

</td><td>
<img src="images/world.jpg" style="width:500px;border-style:none;background:transparent;" />
</td></tr></table>

---------------------------------------------------------------------------------------------------

# Data access

<table class="libs"><tr><td style="padding-right:30px;padding-top:30px;border-style:none">
<img src="images/logo-fsdata.png" style="width:150px;margin-right:20px;" />
</td><td style="padding:left:30px;border-style:none">

## F# Data

First-class data access

CSV, XML, JSON and more

</td></tr><tr><td style="padding-right:30px;padding-top:30px;border-style:none">
<img src="images/logo-deedle.png" style="width:150px;margin-right:20px;" />
</td><td style="padding:left:30px;border-style:none">

## Deedle

Time-series and data-frames

C# and F# friendly

</td></tr></table>

----------------------------------------------------------------------------------------------------

<table class="vtop"><tr><td>

# _MACHINE LEARNING_

## Clustering countries

</td><td>
<img src="images/world.jpg" style="width:500px;border-style:none;background:transparent;" />
</td></tr></table>

---------------------------------------------------------------------------------------------------

# Machine learning

<br />
      
    distance  : ('T -> 'T -> float)  ->
    aggregate : ('T -> 'T[] -> 'T) ->
      count:int -> input:'T[] -> Clustering

----------------------------------------------------------------------------------------------------

<table class="vtop"><tr><td>

# _CLOUD READY_

## Scaling with M-Brace

</td><td>
<img src="images/world.jpg" style="width:500px;border-style:none;background:transparent;" />
</td></tr></table>

---------------------------------------------------------------------------------------------------

# Asynchronous programming

<br /><br />
<p style="margin-left:100px;font:40pt consolas"><span style="color:#3060c0">async</span> { <span class="o">..</span> }</p>
<br /><br /><br />

---------------------------------------------------------------------------------------------------

# Cloud programming

<br /><br />
<p style="margin-left:100px;font:40pt consolas"><span style="color:#3060c0">cloud</span> { <span class="o">..</span> }</p>
<br /><br /><br />

****************************************************************************************************

# Why choose F# for data science?

<div class="fragment">

## Data access and exploration

_First-class data support_ in the language

Environment both _scientists and programmers_ understand

</div><div class="fragment">

## Complexity and correctness

_Simple code_ to solve _complex problems_

_Powerful libraries_ and tools!

</div>

---------------------------------------------------------------------------------------------------

# Thank you!

<br />

<table><tr><td style="vertical-align:middle;padding-right:30px;">
<img src="images/ad-fworks.png" style="width:250px;background:transparent;border:none;" />
</td><td style="padding-left:30px;">
<img src="images/ad-qsh.png" style="width:190px;background:transparent;border:none;" />
</td></tr></table>

<br />

**Tomas Petricek**, fsharpWorks  

See [functional-programming.net](http://functional-programming.net/) for books & trainings!  
[@tomaspetricek](http://twitter.com/tomaspetricek)
| [tomasp.net](http://tomasp.net)
| [fsharpworks.com](http://fsharpworks.com)  
