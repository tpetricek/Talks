- title : TypeScript for F# Zealots
- description : TypeScript for F# Zealots
- author : Tomas Petricek
- theme : simple
- transition : none

****************************************************************************************************

# _**Incremental, Runtime and Extensible**: Type Checker<br /> for a Funny Language_

<br /><br /><br /><br /><br />

**Tomas Petricek**

University of Kent and fsharpWorks<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

****************************************************************************************************
- class: part

# _The Gamma_
## Simple language for data exploration

----------------------------------------------------------------------------------------------------

<img src="images/nyt.png" class="nb" style="max-width:600px;float:left;margin:30px 30px 100px 0px" />

**Data journalism**

<div class="fragment" style="margin-top:40px">

Making facts great again!

_Building more open, transparent and engaging data visualization_

</div>

----------------------------------------------------------------------------------------------------

# _The Gamma_
## Programming tools for data journalists

_<i class="fa fa-user-tie"></i> Can it be written by a_ non-expert?

_<i class="fa fa-redo"></i> Can the result be_ reproduced?

_<i class="fa fa-code"></i> Can the reader_ check the logic?

_<i class="fa fa-edit"></i> Can the reader_ explore variations?

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Exploring Olympic medals

----------------------------------------------------------------------------------------------------

# _Data exploration calculus_

**Capturing the essence of The Gamma**

<div style="transform:scale(0.6);margin:-40px 0px -40px -100px;">

$$$
\begin{array}{rlcl}
\textit{(programs)}\quad& p &::=& c_1; \ldots; c_n\\
\textit{(commands)}\quad& c &::=& \textbf{let}~x = t ~|~ t\\
\textit{(expressions)}\quad& e &::=& t ~|~ \lambda x\rightarrow e\\
\textit{(terms)}\quad& t &::=& o ~|~ x ~|~ t.m(e, \ldots, e)\\
\end{array}

</div>
<div class="fragment">

**What makes the language funny?**

- _Program is a list of declarations or values_
- _Lambdas allowed only as method arguments_
- _No abstraction mechanism (yet)_

</div>

****************************************************************************************************
- class: part

# _Liveness_
## Incremental evaluation and type checking

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Image manipulation without tricks

----------------------------------------------------------------------------------------------------

# _Incremental type checking_

<img src="images/ts-logo.png" style="float:right;width:180px" class="nb"/>

**Two-phase process**

- _Inspired by Roslyn & TypeScript_
- _Update a dependency graph_
- _Type-check graph, not expressions_

<div class="fragment">

**Graph construction**

- _Build graph for dependencies_
- _Variables used, arguments, etc._
- _Reuse nodes if dependencies unchanged_
- _Attach value and type to graph nodes_

</div>

----------------------------------------------------------------------------------------------------
- class: part

# _SKETCH_
## Incremental type checking & evaluation

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Image manipulation in The Gamma

****************************************************************************************************
- class: part

# _Fancy types_
## Generics, dependent types & type providers

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## COVID-19 cases in London

----------------------------------------------------------------------------------------------------

# _Fancy types_
## For accessing libraries and online data

_<i class="fa fa-globe-asia"></i>_ Type providers _generate types from data_

_<i class="fa fa-plug"></i>_ Extensible _to allow new kinds of types_

_<i class="fa fa-sync"></i>_ Dependent types _can depend on values_

_<i class="fa fa-angle-double-right"></i>_ Generics _for importing JS libraries_

----------------------------------------------------------------------------------------------------

# _Object and method types_

    [hide]
    type Type = X
    type Value = Y
    type Member = Z

Interface with object members & type equality

```
type ObjectType =
  abstract Members : Member list
  abstract TypeEquals : ObjectType -> bool
```

<div class="fragment">

Computes type from types & values of arguments

```
type MethodArgument =
  { Name: string; Static: bool; Type: Type }
type MethodType =
  { Arguments: MethodArgument list
    TypeFunc: ((Type * Value option) list -> Type option }
```
</div>

----------------------------------------------------------------------------------------------------

# _Implementing fancy types_

<img src="images/russell.jpg" style="float:right;width:250px" class="nb"/>

**Generics / polymorphism**  
_New ObjectType implementation_  
_Generic methods do resolution_

<div class="fragment">

**Type providers**  
_Methods inspect data source_  
_Generate simple object types_

</div>
<div class="fragment">

**"Dependent" types**  
_Method argument marked as static_  
_Type function gets value from the system_

</div>

****************************************************************************************************
- class: part

# _Summary_
## Lessons from the project

----------------------------------------------------------------------------------------------------
- class: part

# _CAVEAT_
## The Gamma is intentionally very primitive!

----------------------------------------------------------------------------------------------------

# _What makes things work?_

**Incremental type checking**

- _Tractable dependencies_
- _Also fine in TypeScript and C#_
- _Much harder to do for F#_

<div class="fragment">

**Fancy extensible types**

- _All logic in method types_
- _Abstract type equality testing_
- _No generalization on 'let'_
- _Integrated evaluation and type checking_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/thegamma.png" style="float:right;width:180px" class="nb"/>

Funny languages are fun  
_What is the use case?_  
_What assumptions still hold?_  

Live and extensible  
_Compilers must support editors_  
_Language design based on uses?_

Papers with more info  
[_Foundations of a live data exploration environment_](http://tomasp.net/academic/papers/live/)
[_Data exploration through dot-driven development_](http://tomasp.net/academic/papers/pivot/)

<br />

**Tomas Petricek**, _U. of Kent & fsharpWorks_<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)
