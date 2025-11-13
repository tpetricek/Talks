- title: Simple programming tools for data exploration

*****************************************************************************************
- template: title

# Simple programming tools<br>_for data exploration_

---

**Tomas Petricek**  
Department of Distributed and Dependable Systems

_<i class="fa fa-envelope"></i>_ [petricek@d3s.mff.cuni.cz](mailto:petricek@d3s.mff.cuni.cz)  
_<i class="fa fa-globe"></i>_ [https://d3s.mff.cuni.cz/~petricek](https://d3s.mff.cuni.cz/~petricek)  
_<i class="fa-brands fa-bluesky"></i>_ [@tomasp.net](https://bsky.app/profile/tomasp.net)    

*****************************************************************************************
- template: icons

# Part 1
## Making programming with data easier

- *fa-school* PhD, University of Cambridge  
- *fa-industry* Microsoft Research Cambridge  
- *fa-city* The Alan Turing Institute, London  
- *fa-church* University of Kent, Canterbury  
- *fa-institution* Charles University, Prague  

*****************************************************************************************
- template: icons
- class: red234

# Part 1
## Making programming with data easier

- *fa-school* PhD, University of Cambridge  
- *fa-industry* **Microsoft Research Cambridge**  
- *fa-city* **The Alan Turing Institute, London**  
- *fa-church* **University of Kent, Canterbury**  
- *fa-institution* Charles University, Prague  

*****************************************************************************************
- template: image
- class: larger

![](img/owd.png)

# Data journalism

**Restoring trust in data in society?**

---

Correctness  
Understandability  
Reproducibility  

---

**Source code should give us all of these!**

*****************************************************************************************
- template: subtitle

# Use an AI to write the code for us?
## Analysing Olympic medalists

*****************************************************************************************
- template: image
- class: vibecode

![](img/vibe-code1.png)

# Understandability and correctness

<style>.vibecode .body1 img { max-width:1000px; max-height:1000px; width:580px; position:relative;left:-80px }
.vibecode h1 { color:black; font-weight:400; }
.vibecode p strong { color:#d22d40; font-weight:600; }</style>

*****************************************************************************************
- template: image
- class: vibecode

![](img/vibe-code2.png)

# Understandability and correctness

**Invalid columns?**  
Language does not understand the data

*****************************************************************************************
- template: image
- class: vibecode

![](img/vibe-code3.png)

# Understandability and correctness

**Invalid columns?**  
Language does not understand the data

**Complex abstractions**  
Designed for experts

*****************************************************************************************
- template: image
- class: vibecode

![](img/vibe-code4.png)

# Understandability and correctness

**Invalid columns?**  
Language does not understand the data

**Complex abstractions**  
Designed for experts

**Inconsistencies**  
Are we sure it is right?

*****************************************************************************************
- template: subtitle

# Demo
## Olympic medals in The Gamma

*****************************************************************************************
- template: content
- class: ccslide

# Completion based on a formal model

<img src="img/autocomplete.png" />

---

<p style="position:absolute;left:410px;top:190px" class="label"><strong>Correctness</strong> meaning all<br> recommendations valid</p>

---

<p style="position:absolute;left:450px;top:300px" class="label"><strong>Completeness</strong> meaning<br> all valid options listed</p>

<style>.content p.label { display:inline-block; font-weight:500; color:white; font-size:17pt; background:#003657; border-radius:7px; padding:8px 15px; margin-left:10px; }
.ccslide h1 { font-size:32pt; margin-bottom:20px }
.label strong {font-weight:600; }</style>

*****************************************************************************************
- template: lists
- class: condensedlists

# Research behind The Gamma

![](img/pldi.png)

## Language theory
Dot-driven development <span class="paper">ECOOP '17 (A)</span>  
Relative type safety <span class="paper">PLDI '16 (A*)</span>  

## User-centric design
Iterative prompting <span class="paper">VL/HCC '22</span>

## Data visualization
Composable library design <span class="paper">J. Funct. Program '21</span>

<style>
.condensedlists p { margin-top:2px; }
.reveal .lists.condensedlists .body img { max-width:240px; margin-top:-10px; }
.paper { display:inline-block; color:white; font-weight:600; font-size:15pt; background:#003657; border-radius:7px; padding:2px 10px; margin-left:10px; } </style>

*****************************************************************************************
- template:content
- class:blueh3 three-column

# Type checking and type safety

### Expressions

Minimal formal language model

$e\,:=\,x\,|\,v\,|\,e.N$

---

### Type system

Types in program are used correctly

$~~~~~~~\Gamma \vdash e : \tau$

---

### Evaluation

Formal model of how programs run

$~~~~~~~e\mapsto v$

*****************************************************************************************
- template:content
- class:nologo rulezz

![](img/foo-eval.png)

![](img/foo-types.png)

<style>.rulezz img { max-width:750px; margin-left:50px; }</style>

*****************************************************************************************
- template:content
- class:blueh3

# Type safety

### Well-typed programs do not go wrong

For any $e$ such that $\emptyset \vdash e : \tau$, the program evaluates to a value $e \mapsto v$
and the result has the right type $v \in \tau$

---

### But the initial environment is not empty!

It contains things from the real world! i.e. $\Gamma_0 \neq \emptyset$  
For some data $d$ and type provider $\pi$, let $\Gamma_0 = \pi(d)$

*****************************************************************************************
- template:content
- class:blueh3

# Relative type safety

### Well-typed programs do not go wrong

For any $e$ such that $\emptyset \vdash e : \tau$, the program evaluates to a value $e \mapsto v$
and the result has the right type $v \in \tau$

---

### As long as the world is well-behaved

Given a data sample $d$, type provider $\pi$ and an actual input<br> $d'$ such that
$\pi(d') \sqsubseteq \pi(d)$ then for any program $e$ such that $x:\pi(d) \vdash e : \tau$,
it evaluates $e[d'/x]\mapsto v$ and $v \in \tau$.


<style>.blueh3 h3 { color:#003657; margin-bottom:5px; }</style>

*****************************************************************************************
- template: content

# But does it make programming easier?

<div class="fragment">
<h2 style="margin:-35px 0px 30px 0px;font-size:32pt;">Gap between spreadsheets and Python...</h2>
<img src="img/substrates.png" style="border-style:none;max-width:700px"/>
</div>

*****************************************************************************************
- template: content

# Spreadsheets get many things right

<div>
<h2 style="margin:-35px 0px 30px 0px;font-size:32pt;">Uniformity of grid, transparency of formulas</h2>
<img src="img/substrates-thegamma.png" style="border-style:none;max-width:700px"/>
</div>

*****************************************************************************************
- template: lists
- class: h2red

# Keeping spreadsheet qualities?

![](img/cubetp.png)

## User-centric evaluation
- Three different data sources  
- Research lab business team  

## Research questions
- **RQ #1:** Can non-programmers use The Gamma?
- **RQ #2:** Can they learn from just code samples?  
- **RQ #3:** Can knowledge transfer between sources?

*****************************************************************************************
- template: content
- class: noborder bigloop

# Simple programming tools for data exploration

![](img/process1.png)

<style>.bigloop h1 { font-size:39pt; letter-spacing:-2px } .bigloop .body img { max-width:850px; margin-bottom:30px; max-height:1000px; }</style>

*****************************************************************************************
- template: content
- class: noborder bigloop

# Simple programming tools for data exploration

![](img/process2.png)

Two __Core A*__ conferences (POPL, PLDI) and   
one __D1__ (98 percentile) journal (IEEE TKDE)

*****************************************************************************************
- template: image

![](img/iccs.png)

# <span style="color:black;font-weight:500">Follow-up work<br></span> Institute of Computing for Climate Science

**Make the IPCC  
reports more understandable, transparent and reproducible?**

*****************************************************************************************
- template: icons
- class: red5

# Part 2
## From programming languages to systems

- *fa-school* PhD, University of Cambridge  
- *fa-industry* Microsoft Research Cambridge  
- *fa-city* The Alan Turing Institute, London  
- *fa-church* University of Kent, Canterbury  
- *fa-institution* **Charles University, Prague**  

*****************************************************************************************
- template: lists
- class: h2red bigger smallerfnt

# Research group @ D3S MFF

![](img/prgprg.jpg)

## Programming system thinking
- Visual programming languages
- Interactive proof assistants

<p style="position:absolute; font-weight:500; font-size:16pt; text-align:right; left:730px;top:310px">PRIMUS (2024-2027)</p>

## Team funded through PRIMUS
- **Jan Verter** - Ing from FIT CTU
- **Joel Jakubovic** - PhD from University of Kent
- **Pablo Donato** - PhD from École Polytechnique

## Related student theses
- **2 Masters,** 2 finished
- **4 Bachelor,** 13 finished

<style>.smallerfnt h1 { font-size:40pt; margin-bottom:10px;} .smallerfnt ul li { font-size:24pt; } .smallerfnt h2 { font-size:28pt; } </style>

*****************************************************************************************
- template: lists
- class: idp

# Interdisciplinary programming research

![](img/cultures.jpg)

## Programming language theory
- Powerful and general methodology
- Types for data science <span class="paper">PRIMUS 2024</span>  

## Human-computer interaction
- Programming substrates <span class="paper">UIST '25 (A*)</span>  

## History and philosophy
- Cultures of Programming <span class="paper">CUP 2025</span>  
- Donatio Chair application

<style>.idp h1 { font-size:40pt; letter-spacing:-1px }</style>

*****************************************************************************************
- template: title
- class: conclusion

# Thank you
## Programming is still very far<br> from being a solved problem!

---

**Tomas Petricek**  
Department of Distributed and Dependable Systems

_<i class="fa fa-envelope"></i>_ [petricek@d3s.mff.cuni.cz](mailto:petricek@d3s.mff.cuni.cz)  
_<i class="fa fa-globe"></i>_ [https://d3s.mff.cuni.cz/~petricek](https://d3s.mff.cuni.cz/~petricek)  
_<i class="fa-brands fa-bluesky"></i>_ [@tomasp.net](https://bsky.app/profile/tomasp.net)    

*****************************************************************************************
- template: content

v1

*****************************************************************************************
- template: largeicons

# Before returning to Matfyz

- *fa-school* **PhD, University of Cambridge**  
  Formal models of programming languages

- *fa-industry* **Microsoft Research Cambridge**  
  Applied functional programming and tools

- *fa-city* **The Alan Turing Institute, London**  
  Expert and non-expert tools for data science

- *fa-church* **University of Kent, Canterbury**  
  Principles of rich programming systems

*****************************************************************************************
- template: largeicons
- class: red234

# Before returning to Matfyz

- *fa-school* **PhD, University of Cambridge**  
  Formal models of programming languages

- *fa-industry* **Microsoft Research Cambridge**  
  Applied functional programming and tools

- *fa-city* **The Alan Turing Institute, London**  
  Expert and non-expert tools for data science

- *fa-church* **University of Kent, Canterbury**  
  Principles of rich programming systems

*****************************************************************************************
- template: image
- class: noborder movedown

![](img/v1/loop-crop.png)

# Data exploration is a half of the work!

Spreadsheets, data science notebooks, business intelligence

---

**I propose to view systems & tools for data exploration as programming tools**

<style>.movedown img { margin-top:50px; }</style>
<style>.red234 ul li:nth-child(2), .red5 ul li:nth-child(5),
  .red234 ul li:nth-child(3) { color:#d22d40; } .red234 ul li:nth-child(4) { color:#d22d40; }</style>

*****************************************************************************************
- template: imageanim
- class: image noborder movedown

![](img/v1/substrates.png)

# What is simple?

**Major gap in tooling<br> for data science!**

---

### Programming theory
A small number of composable primitives

---

### User-centric view
Non-programmers can complete more tasks

*****************************************************************************************
- template: subtitle

# Demo
## Weather info with F# Data

*****************************************************************************************
- template: image
- class: noborder

![](img/v1/substrates-fsdata.png)

# F# Data

**Makes accessing and exploring data easier for programmers**

*****************************************************************************************
- template: lists
- class: smallerh1 smaller

# Data as a programming language problem

![](img/v1/weather.png)

## Application of PL ideas

- Type systems for tooling and safety
- Composable libraries of primitives

## Rethink PL assumptions

- What types do we need for rich data?
- What does type safety guarantee?

<style>.smallerh1 h1 { font-size:43pt; letter-spacing:-2px }</style>

*****************************************************************************************
- template:content

<div class="bigeq">

$\emptyset \vdash e : \tau$

</div>
<style>
.reveal .bigeq p { text-align:center; margin-top:100px; }
.reveal .bigeq .math { font-size:3em !important; color:#d22d40; }
</style>

*****************************************************************************************
- template:content

<div class="bigeq bigeq2">

$\pi(~~~~~~) \vdash e : \tau$

<img src="img/v1/globe.png" style="position:absolute;border-style:none;left:217px;top:110px;width:220px" />
</div>

*****************************************************************************************
- template:content
- class:blueh3

# Relative type safety property

### Well-typed programs do not go wrong

For any $e$ such that $\emptyset \vdash e : \tau$, the program evaluates to a value $e \mapsto v$
and the result has the right type $v \in \tau$

---

### As long as the environment is well-behaved

Given sample inputs $d_1, \ldots, d_n$ and an actual input $d$ such that
$S(d) \sqsubseteq S(d1, \ldots, d_n)$ then for any program $e$ such that $x:S(d_1, \ldots, d_n) \vdash e : \tau$,
it evaluates $e[d/x]\mapsto v$ and $v \in \tau$.


<style>.blueh3 h3 { color:#003657; margin-bottom:5px; }</style>

*****************************************************************************************
- template: subtitle

# Demo
## Olympic medals in The Gamma

*****************************************************************************************
- template: image
- class: noborder movedown

![](img/v1/substrates-thegamma.png)

# The Gamma

**Lets non-programmers complete simple data programming tasks**

*****************************************************************************************
- template: lists
- class: smallerh1 noborder

# Programming tools for non-programmers

![](img/v1/study.png)

## Design principles

- Work with code for transparency
- Same interface for many sources

## Usability evaluation

- 13 participants from a business  
  team in non-technical roles
- "This is actually pretty simple to use"

*****************************************************************************************
- template: content
- class: noborder bigloop

# Simple programming tools for data exploration

![](img/v1/loop.png)

<style>.bigloop h1 { font-size:39pt; letter-spacing:-2px } .bigloop img { max-width:700px; }</style>

*****************************************************************************************
- template: content
- class: noborder bigloop

# Simple programming tools for data exploration

![](img/v1/loop-papers.png)

---

__Core A*__ (POPL, PLDI) and **Core A** (ECOOP)  
conferences and __journals__ (JFP, IEEE TKDE)

<style>.bigloop h1 { font-size:39pt; letter-spacing:-2px } .bigloop img { max-width:700px; }</style>

*****************************************************************************************
- template: largeicons
- class: red5

# After returning to Matfyz

- *fa-school* **PhD, University of Cambridge**  
  Formal models of programming languages

- *fa-industry* **Microsoft Research Cambridge**  
  Applied functional programming and tools

- *fa-city* **The Alan Turing Institute, London**  
  Expert and non-expert tools for data science

- *fa-church* **University of Kent, Canterbury**  
  Principles of rich programming systems

- *fa-institution* **Charles University, Prague**  
  Principles of rich programming systems

*****************************************************************************************
- template: image
- class: smaller2x

![](img/v1/prgprg.jpg)

# Research group @ D3S

<h3 style="line-height:1em;margin-bottom:10px">From programming languages to systems</h3>

Programming systems  
with **Joel Jakubovic**

User experience of proof assistants
with **Jan Verter**

Logics for visual program&shy;ming with **Pablo Donato**

<p style="position:relative; font-weight:500; font-size:16pt; left:-440px;top:-210px">PRIMUS (2024-2027) and collaboration<br> with prof. Jan Vitek and FIT CTU</p>

*****************************************************************************************
- template: lists
- class: idp

# Interdisciplinary programming research

![](img/v1/cultures.jpg)

## Programming language theory
- Applied to programming systems
- Rethink via programming systems

## Human-computer interaction
- Programming substrates <img src="img/v1/uist.png" style="border-style:none;float:none;width:100px;margin:0px;position:relative;top:3px;left:4px">

## History and philosophy
- Cultures of Programming <img src="img/v1/cup.png" style="border-style:none;float:none;width:100px;margin:0px;position:relative;top:3px;left:4px">
- Donatio Chair application

<style>.idp h1 { font-size:40pt; letter-spacing:-1px }</style>

*****************************************************************************************
- template: title
- class: conclusion

# Thank you
## Simple programming<br> tools for data exploration

---

**Tomas Petricek**   
Department of Distributed and Dependable Systems

_<i class="fa fa-envelope"></i>_ [petricek@d3s.mff.cuni.cz](mailto:petricek@d3s.mff.cuni.cz)  
_<i class="fa fa-globe"></i>_ [https://d3s.mff.cuni.cz/~petricek](https://d3s.mff.cuni.cz/~petricek)  
_<i class="fa-brands fa-bluesky"></i>_ [@tomasp.net](https://bsky.app/profile/tomasp.net)    
