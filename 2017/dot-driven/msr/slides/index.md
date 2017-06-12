- title : The Gamma: Democratizing Data Science
- description : 
- author : Tomas Petricek
- theme : night
- transition : none

***************************************************************************************************
 - data-background : images/post-fact.jpg
 - class : withbackground

## The Gamma: _Data exploration<br /> through dot-driven development_

<br /><br /><br /><br /><br />

#### Tomas Petricek<br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) | [thegamma.net](http://thegamma.net)

***************************************************************************************************
 - data-background : images/kiss.jpg
 - class : withbackground

<div class="fragment">

# Welcome to the _post-fact_ world

</div>

***************************************************************************************************
 - data-background : images/guardian.jpg
 - class : withbackground

In recent years, divergent levels of trust in statistics has become one of 
the key schisms that have opened up in western liberal democracies.

_William Davies, The Guardian, January 2017_

***************************************************************************************************
 - data-background : images/rowing.jpg
 - class : withbackground

<img src="images/nyt.png" style="background:transparent;width:900px;" />

***************************************************************************************************
 - data-background : images/rowing.jpg
 - class : withbackground

#### Can the result be _reproduced_?
   
#### Is the visualization _misleading_?
 
#### Can it be done by _non-experts_?

#### Can the reader _explore further_?

***************************************************************************************************
 - data-background : images/rowing.jpg
 - class : withbackground

<h1>
  <img src="images/gamma-logo.png" style="width:100px;position:relative;top:37px;margin-right:30px"/>
  The Gamma
</h1>  
  
***************************************************************************************************
 - data-background : images/glacier.jpg
 - class : withbackground

# *DEMO*

## Understanding carbon emissions

***************************************************************************************************
 - data-background : images/glacier.jpg
 - class : withbackground

### Language that _understands data_

### Simple programming _using dot_

***************************************************************************************************
 - data-background : images/rio.jpg
 - class : withbackground

# Dot-driven _data exploration_

***************************************************************************************************
 - data-background : images/rio-dark.jpg
 - class : withbackground

<img src="images/pl1.png" style="background:transparent;width:100%" />
<style>.ref p { font-size:18pt; margin:20px 0px 0px 0px; text-align:right; }</style>
<div class="ref">

Based on [Transcript: End-user programming of social apps](https://www.youtube.com/watch?v=XBpwysZtkkQ)<br />
Jonathan Edwards, YOW 2015

</div>

***************************************************************************************************
 - data-background : images/rio-dark.jpg
 - class : withbackground

<img src="images/pl2.png" style="background:transparent;width:100%" />
<div class="ref">

Based on [Transcript: End-user programming of social apps](https://www.youtube.com/watch?v=XBpwysZtkkQ)<br />
Jonathan Edwards, YOW 2015

</div>

***************************************************************************************************
 - data-background : images/rio.jpg
 - class : withbackground

# *DEMO*

## Visualizing Olympic medals

***************************************************************************************************
 - data-background : images/science.jpg
 - class : withbackground

### _Type providers_ for member generation

### _Laziness_ for scaling to large hierarchies

### _Fancy types_ for the masses!

***************************************************************************************************
- data-background : images/science2.jpg
- class : withbackground

### _Pivot type provider_

<br /><div class="fragment">

$L$ maps names to definitions and nested contexts

<div style="padding:10px 0px 50px 60px">

$
\definecolor{mc}{RGB}{255,255,102}
L(C) = {\color{mc}\text{type}}~C(x:\tau) = \overline{m}, L'
$

</div></div><div class="fragment">

Pivot takes schema and provides a class with context

<div style="padding:10px 0px 50px 60px">

$
\text{pivot}(F) = C, L
$

</div></div>

***************************************************************************************************
- data-background : images/science2.jpg
- class : withbackground

### _Pivot type provider_

Generate class that constructs a query

<br />
<div style="padding:0px 0px 0px 0px">
<img src="images/rules.png" style="background:transparent" />
</div>
<br />

***************************************************************************************************
- data-background : images/science.jpg
- class : withbackground

# *DEMO*

## Fancy types for the masses

***************************************************************************************************
- data-background : images/science2.jpg
- class : withbackground

Row types to track names and types of fields

<div style="padding:10px 0px 0px 0px;margin-top:-50px;margin-left:-150px;transform:scale(0.8)">

$$$
\definecolor{cc}{RGB}{255,255,102}
\frac
  {\Gamma \vdash e : {\color{cc}[f_1:\tau_1, \ldots, f_n:\tau_n]}}
  {\Gamma \vdash e.\text{drop}~f_i : {\color{cc} [f_1:\tau_1, \ldots, f_{i-1}:\tau_{i-1}, f_{i+1}:\tau_{i+1}, \ldots, f_n:\tau_n]}}

</div><div class="fragment">
<br />

Embed row types in provided nominal types

<div style="padding:10px 0px 0px 0px;margin-top:-50px;margin-left:-150px;transform:scale(0.8)">

$$$
\frac
  {\Gamma \vdash e : {\color{mc} C_1}}
  {\Gamma \vdash e.\text{drop}~f_i : {\color{mc} C_2}}
\quad{\small \text{where}}

$$$
\begin{array}{l}
{fields({\color{mc} C_1}) = {\color{mc} \{f_1:\tau_1, \ldots, f_n:\tau_n\}}}\\[-0.25em]
{fields({\color{mc} C_2}) = {\color{mc} \{f_1:\tau_1, \ldots, f_{i-1}:\tau_{i-1}, f_{i+1}:\tau_{i+1}, \ldots, f_n:\tau_n\}}}
\end{array}

</div></div>

***************************************************************************************************
 - data-background : images/clock.jpg
 - class : withbackground

# Unifying programming<br />and  _spreadsheets_

***************************************************************************************************
 - data-background : images/clock.jpg
 - class : withbackground

<img src="images/excel1.gif" style="background:transparent;width:80%" />

***************************************************************************************************
 - data-background : images/clock.jpg
 - class : withbackground

<img src="images/excel2.gif" style="background:transparent;width:80%" />

***************************************************************************************************
- data-background : images/clock.jpg
- class : withbackground

# *DEMO*

## Programming and spreadsheets

***************************************************************************************************
 - data-background : images/clock.jpg
 - class : withbackground

<img src="images/pl1.png" style="background:transparent;width:100%" />
<style>.ref p { font-size:18pt; margin:20px 0px 0px 0px; text-align:right; }</style>
<div class="ref">

Based on [Transcript: End-user programming of social apps](https://www.youtube.com/watch?v=XBpwysZtkkQ)<br />
Jonathan Edwards, YOW 2015

</div>

***************************************************************************************************
 - data-background : images/budget.jpg
 - class : withbackground

# Making data more _engaging_

***************************************************************************************************
 - data-background : images/budget.jpg
 - class : withbackground

<img src="images/obama.png" style="background:transparent;width:800px;" />

***************************************************************************************************
 - data-background : images/budget.jpg
 - class : withbackground

# *DEMO*

## Guess the government expenditure

***************************************************************************************************
 - data-background : images/budget.jpg
 - class : withbackground

### Interactivity for _engagement_

### Adapt to be _more relevant_


***************************************************************************************************
- data-background : images/rio.jpg
- class : withbackground

# Summary

***************************************************************************************************
- data-background : images/trump.jpg
- class : withbackground

# Make _facts_ great again!

***************************************************************************************************
- data-background : images/tggh.png
- class : withbackground

#### Technology democratized _opinions_

#### Can it also democratize _facts_?


<br /><br /><br /><br /><br />

#### Tomas Petricek, [tomas@tomasp.net](tomas@tomasp.net)<br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [thegamma.net](http://thegamma.net) | [tomasp.net](http://tomasp.net) 


***************************************************************************************************
- data-background : images/trump.jpg
- class : withbackground

# Make _facts_ great again!

***************************************************************************************************
- data-background : images/rio.jpg
- class : withbackground

# Summary
