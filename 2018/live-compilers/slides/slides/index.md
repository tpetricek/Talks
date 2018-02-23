- title : Rethinking compilers with live coding
- description : Live coding environments run your program on-the-fly as you write it. This makes 
    development easier, but it is a challenge for compiler writers - textbook compiler architecture 
    does not work for live coding systems. I’ll show interesting aspects of a live coding 
    environment for The Gamma, its functional F# implementation and explain how the compiler 
    differs from textbook examples. Along the way, you'll see practical code samples interesting 
    for both web developers and programming language enthusiasts.
- author : Tomas Petricek
- theme : night
- transition : none

***************************************************************************************************

# _Rethinking compilers_<br/> with live coding

<br />
<br />
<br />
<br />

#### _Tomas Petricek_, Alan Turing Institute + fsharpWorks <br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) | [fsharpworks.com](http://fsharpworks.com)


***************************************************************************************************
 - data-background : images/kiss.jpg
 - class : withbackground

<div class="fragment">

# Welcome to the **post-fact** world

</div>

***************************************************************************************************

<img src="images/nyt.png" style="background:transparent;width:900px;" />

***************************************************************************************************

### Can the result be _reproduced_?
   
### Is the visualization _misleading_?
 
### Can the reader _explore further_?

***************************************************************************************************

<h1>
 <img src="images/gamma-logo.png" style="width:100px;position:relative;top:37px;margin-right:30px"/>
 The Gamma
</h1>  

***************************************************************************************************

# Making _data science_ easier

***************************************************************************************************

<img src="images/pl1.png"/>

***************************************************************************************************

<img src="images/pl2.png"/>

***************************************************************************************************

<img src="images/excel1.gif" />

***************************************************************************************************

<img src="images/excel2.gif"/>

***************************************************************************************************

# _DEMO_

## Live previews in TheGamma

***************************************************************************************************

## _TEXTBOOK COMPILERS_

***************************************************************************************************

#### Textbook compiler _is batch based_

#### Textbook compiler _builds & transforms trees_

#### Textbook compiler _rejects bad programs_

***************************************************************************************************

## **Example** - live image processing

<br />

    let pope = image.load("pope.png")
    let shadow = image.load("shadow.png")
    
    shadow.greyScale().blur(5)
      .combine(pope, 50)


***************************************************************************************************

# _SKETCH_

### Transforming abstract syntax trees

***************************************************************************************************

# _DEMO_

### Writing a batch evaluator

***************************************************************************************************

## _LIVE COMPILERS_

***************************************************************************************************

#### Live compiler _caches earlier results_

#### Live compiler _builds dependency graphs_

#### Live compiler _accepts bad programs_
 
***************************************************************************************************

# _SKETCH_

### Building a dependency graph

***************************************************************************************************

# _DEMO_

### Writing a graph-based evaluator


***************************************************************************************************

## _SUMMARY_

***************************************************************************************************

## Check out **TheGamma** project

And help make facts great again!

<br />

[www.thegamma.net](http://www.thegamma.net)

[gamma.turing.ac.uk](http://gamma.turing.ac.uk)

***************************************************************************************************

<table><tr><td style="text-align:center">

## _F# language_

[www.fsharp.org](http://www.fsharp.org)

<br />

Pragmatic functional-first programming 

</td><td style="text-align:center">

## _Fable compiler_

[www.fable.io](http://www.fable.io)

<br />

F# to JavaScript compiler for the 21st century

</td></tr></table>

***************************************************************************************************

### _Dependency graphs_ over syntax trees

### _Reusing nodes_ enables caching

<div class="fragment" style="padding-top:20px">

### Good luck writing a decent parser :-)

</div>

***************************************************************************************************

### _Now you know how to write<br />your own live coding environment!_

<br />
<br />

## Questions?

<br />
<br />

#### _Tomas Petricek_, Alan Turing Institute + fsharpWorks <br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) | [fsharpworks.com](http://fsharpworks.com)
