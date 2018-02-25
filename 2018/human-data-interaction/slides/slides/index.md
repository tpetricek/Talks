- title : Data exploration through dot-driven development 
- description : 
- author : Tomas Petricek
- theme : white
- transition : none

***************************************************************************************************

<h1 style="font-size:38pt">Programming as human data interaction</h1>

<h2 style="font-size:30pt"><em>Making data analysis accessible to ‘mere mortals’</em><h2>

<h4 style="margin-bottom:0px;margin-top:300px">Dr Tomas Petricek</h4>

_[http://tomasp.net](http://tomasp.net/academic) <span style="margin:0px 6px 0px 6px">|</span>
[tomas@tomasp.net](mailto:tomas@tomasp.net) <span style="margin:0px 6px 0px 6px">|</span>
[@tomaspetricek](http://twitter.com/tomaspetricek)_

***************************************************************************************************

## <span class="circ"><span>1</span></span> Long term – _Programming as interaction_ 

## <span class="circ"><span>2</span></span> Short term – _Easy, trustworthy data science_

---------------------------------------------------------------------------------------------------

<img src="images/inequality.png" style="height:700px;margin-left:150px;margin-top:-20px" />

---------------------------------------------------------------------------------------------------

<div style="background:black;position:absolute;width:200%;right:51%;height:400%;top:-100%;z-index:-1000;"></div>

<table style="margin-top:140px"><tr><td style="width:45%;color:white" class="fragment">

<h3 style="color:white;">Spreadsheets</h3>

<div class="narrow" style="padding:20px 0px 105px 0px;">
<p><i class="fa fa-birthday-cake"></i> <em>Easy to use</em></p>
<p><i class="fa fa-table"></i> <em>Simple problems only</em></p>
<p><i class="fa fa-redo-alt"></i> <em>Not reproducible</em></p>
</div>

</td><td style="width:45%" class="fragment">

### Programming

<div class="narrow" style="padding:20px 0px 105px 0px;">
<p><i class="fa fa-university"></i> <em>Requires expert skills</em></p>
<p><i class="fa fa-globe"></i> <em>Internet-scale</em></p>
<p><i class="fa fa-code"></i> <em>Reproducible &amp; open</em></p>
</div>

</td></tr></table>

---------------------------------------------------------------------------------------------------

_The sweet spot between spreadsheets and programming?_
<h3 style="margin-top:-10px">Focus on programming and interactions!</h3>

---------------------------------------------------------------------------------------------------

### [<span style="color:black">DEMO</span>](https://notebooks.azure.com/tomasp/libraries/interview-demo)

_Inequality index using F# Data <span class="ref"><span>PLDI 2016</span></span>_

---------------------------------------------------------------------------------------------------

## Programming for data science

_<span class="circ"><span>1</span></span> Data analytics is an interactive process_ 

_<span class="circ"><span>2</span></span> Program against data, not abstract symbols_

***************************************************************************************************

# Research vision
_Programming as human data interaction_

---------------------------------------------------------------------------------------------------

### The essence of human data interaction
_Lambda calculus, but for programming interactions_

<div class="narrow" style="padding:20px 0px 105px 0px;">
<p><i class="fa fa-user"></i> <em>Human centric problem</em></p>
<p><i class="fa fa-pencil-alt"></i> <em>Formal mathematical perspective</em></p>
<p><i class="fa fa-wrench"></i> <em>Enables new tools and methods</em></p>
</div>

> Two research paradigms that ignore each other - join them!
> 
> Just like the lambda calculus is the key to functional programming research, a core set of formal abstractions
> capturing human data interaction is central to transforming how we work with data.

---------------------------------------------------------------------------------------------------

## Formal mathematical perspective

_<span class="circ"><span>1</span></span> Relative type safety_

_<span class="circ"><span>2</span></span> Provenance <span style="font-size:75%"><span class="ref"><span>ICFP 2014</span></span> <span class="ref"><span>ICALP 2013</span></span></span>_

_<span class="circ"><span>3</span></span> Interaction properties_

---------------------------------------------------------------------------------------------------

## Human centric perspective

_<span class="circ"><span>1</span></span> Programming by direct manipulation_

_<span class="circ"><span>2</span></span> Cognitive cost of interactions_

***************************************************************************************************

# Research plan
_Current and future research projects_

---------------------------------------------------------------------------------------------------

### [<span style="color:black">DEMO</span>](http://gamma.turing.ac.uk/playground/)

_Data exploration via dot-driven development <span class="ref"><span>ECOOP 2017</span></span>_

---------------------------------------------------------------------------------------------------

## My next three papers

Efficient environment for live programming  
_<span class="sp"></span> Type checker and interpreter in TheGamma_ 

Interactive AI assistants for data wrangling  
_<span class="sp"></span> Alan Turing Institute collaboration_

History and philosophy of programming errors  
_<span class="sp"></span> Revised paper for ACM HOPL IV_

---------------------------------------------------------------------------------------------------

### Programming as human data interaction

<div style="margin:60px 0px 100px 0px">

### <span class="circ"><span>1</span></span> _Focus on programming, not programs_
### <span class="circ"><span>2</span></span> _The essence of interactions_
### <span class="circ"><span>3</span></span> _Easy, trustworthy data science_
 
</div>

_[http://tomasp.net](http://tomasp.net/academic) <span style="margin:0px 6px 0px 6px">|</span>
[tomas@tomasp.net](mailto:tomas@tomasp.net) <span style="margin:0px 6px 0px 6px">|</span>
[@tomaspetricek](http://twitter.com/tomaspetricek)_

***************************************************************************************************

_(slide intentionally left blank)_

***************************************************************************************************

<h1 style="font-size:38pt">Domain specific languages</h1>

<h2 style="font-size:30pt"><em>Software engineering (Year 2)</em><h2>

<h4 style="margin-bottom:0px;margin-top:300px">Dr Tomas Petricek</h4>

_[http://tomasp.net](http://tomasp.net/academic) <span style="margin:0px 6px 0px 6px">|</span>
[tomas@tomasp.net](mailto:tomas@tomasp.net) <span style="margin:0px 6px 0px 6px">|</span>
[@tomaspetricek](http://twitter.com/tomaspetricek)_

***************************************************************************************************

<img src="images/dsls.png" style="width:700px;margin-left:100px" />

---------------------------------------------------------------------------------------------------

### Engineering problem
_Repeated problem with numerous variations_

<div class="narrow" style="padding:20px 0px 105px 0px;">
<p><i class="fa fa-desktop"></i> <em>User interfaces for form entry</em></p>
<p><i class="fa fa-table"></i> <em>Querying tabular data</em></p>
<p><i class="fa fa-money-bill-alt"></i> <em>Modelling insurance contracts</em></p>
</div>

---------------------------------------------------------------------------------------------------

# _Build a_ language _for a given_ domain!

---------------------------------------------------------------------------------------------------

### [<span style="color:black">DEMO</span>](http://fun3d.net)

_Language for composing 3D objects_

---------------------------------------------------------------------------------------------------

# DSL <em class="fragment">= SYNTAX + MODEL</em>

---------------------------------------------------------------------------------------------------

### Functional languages
_Custom operators and function composition_

    ( Fun.cone ) $
    ( Fun.cylinder |> Fun.move (0, -1, 0) ) 

<div class="fragment" style="margin-top:50px">

### Object-oriented languages
_Fluent interfaces and the builder pattern_

    [lang=csharp]
    fun.cone().combine
      (fun.cylinder().move(0, -1, 0) )

</div>

---------------------------------------------------------------------------------------------------

# DSL <em>= SYNTAX + MODEL</em>

---------------------------------------------------------------------------------------------------

### Object-oriented languages
_Modelling using class hierarchies_

<img src="images/hierarchy.png" style="width:500px;margin:60px 0px 0px 100px" />

---------------------------------------------------------------------------------------------------

### Functional languages
_Modelling using algebraic data types_

    type Shape = 
      | Cube 
      | Sphere
      | Combine of list<Shape>
      
---------------------------------------------------------------------------------------------------

<table><tr><td style="width:45%">

### Object-oriented

<div class="narrow" style="padding:20px 0px 105px 0px;">

_Easy to add new cases_

_Hard to add new operations_ 

_One class per file_

</div>
</td><td style="width:45%" class="fragment">

### Functional

<div class="narrow" style="padding:20px 0px 105px 0px;">

_Hard to add new cases_ 

_Easy to add operations_

_One file per domain_

</div>
</td></tr></table>

<div class="fragment" style="padding:0px 0px 0px 30px">
<p><i class="fa fa-lightbulb"></i> See also: <em>The expression problem</em></p>
</div>

***************************************************************************************************

### Domain specific languages

<div style="margin:60px 0px 100px 0px" class="narrow">

**When?** _Repeated problem with variations_

**How?** _Define model, provide readable syntax_

**What?** _Financial contracts, user interfaces, etc._
 
</div>

_Contacts for follow-up questions_<br />
_[tomas@tomasp.net](mailto:tomas@tomasp.net) <span style="margin:0px 6px 0px 6px">|</span>
[@tomaspetricek](http://twitter.com/tomaspetricek)_
