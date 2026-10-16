- title: Document-Oriented Programming Systems

****************************************************************************************************
- template: title

# **Document-Oriented**<br> Programming Systems

---

**Tomas Petricek**, Charles University, Prague  

_<i class="fa fa-envelope"></i>_ [tomas@tomasp.net](mailto:tomas@tomasp.net)  
_<i class="fa fa-globe"></i>_ [https://tomasp.net](https://tomasp.net)  
_<i class="fa-brands fa-bluesky"></i>_ [@tomasp.net](https://bsky.app/profile/tomasp.net)    

----------------------------------------------------------------------------------------------------
- template: content
- class: three-column nologo
- style: .body div { margin-left:0px !important; } h1 { color:black; } img { width:70%; margin-left:20px; } h3 { line-height:1em; margin-bottom:30px !important; }

# Also talk to me about...

### History of programming

![](img/cover.jpg)

---

### Critical architecture

![](img/campo.jpg)

---

### Programming systems

![](img/datnicek.png)

****************************************************************************************************
- template: subtitle

# Essence
## Interesting programming systems

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller

![](img/boxer.png)

# Boxer (1986)

**Environment for creating interactive textbooks**

Spatial metaphor  
Naive realism

----------------------------------------------------------------------------------------------------
- template: image

![](img/subtext.png)

# Subtext (2005)

**Non-textual programming system based on copying**

First-class copy&paste  
Concrete programming

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller

![](img/jupyter.png)

# Jupyter/IPython (2001)

**Literate programming environment for data science**

Notebook with cells  
Code, outputs, text  

----------------------------------------------------------------------------------------------------
- template: image

![](img/hyper.png)

# Hypercard (1987)

**Hypermedia authoring system for end-users**

Stacks of cards  
User levels  
HyperTalk language

----------------------------------------------------------------------------------------------------
- template: subtitle

# Definition
## Document-oriented programming system

----------------------------------------------------------------------------------------------------
- template: largeicons

# Definition
## Document-oriented programming system

* *fa-file-code* **Primary notation is document**  
  Contains other data and code notations
* *fa-hand-pointer* **Interaction through document**  
  Authoring and using through the same interface
* *fa-rotate* **Editing running at the same time**  
  No separation between edit-time and run-time

----------------------------------------------------------------------------------------------------
- template: lists
- class: bigger
- style: h1 { letter-spacing:-2px; font-size:43pt; }

# What about document-oriented systems?

![](img/hyper1.png)

## Programming research
What is the formal model?  
Can we type check them?  
What are their design choices?

## Human-centric research
Better support non-programmers  
Support progression to expert  
More open and transparent!


****************************************************************************************************
- template: subtitle

# Denicek
## Substrate for end-user doc programming

----------------------------------------------------------------------------------------------------
- template: content

# Programming experiences

----------------------------------------------------------------------------------------------------
- template: content
- class: three-column nologo
- style: img { width:200px; border-style:none; }
    h3 { color:black; font-size:26pt }  p { font-size:24pt; }

# Programming experiences

### Collaboration

![](img/anim-collab.gif)

Merge edits made independently by different users

---

### Demonstration

![](img/anim-pbd.gif)

Specify program by showing concrete document action

---

### Schema change

![](img/anim-struct.gif)

Adapt references when document structure changes

----------------------------------------------------------------------------------------------------
- template: image
- class: noborder
- style: p { margin:5px 0px 0px 0px; } .body1 { margin-right:-30px; }

![](img/denicek1.png)

# Denicek

**Computational substrate for end-user document-oriented programming**

<br>

Makes implementing  
such systems easy!

----------------------------------------------------------------------------------------------------
- template: image
- class: noborder
- style: p { margin:5px 0px 0px 0px; }

![](img/denicek2.png)

# Webnicek

**Web-based end-user programming tool**

<br>

Co-developed with the Denicek substrate

Six formative examples

----------------------------------------------------------------------------------------------------
- template: image
- class: noborder
- style: p { margin:5px 0px 0px 0px; }

![](img/denicek3.png)

# Datnicek

**Simple data science notebook system**

<br>

Collaborative editing

Programming by demonstration

Incremental evaluation

----------------------------------------------------------------------------------------------------
- template: content
- class: noborder
- style: h1 { font-size:38pt; } .body img { max-width:550px; margin-left:80px }

# Denicek: **Document and Edit history**

----------------------------------------------------------------------------------------------------
- template: content
- class: noborder
- style: h1 { font-size:38pt; } .body img { max-width:550px; margin-left:80px }

# Denicek: **Document and Edit history**

![](img/doc/denicek1.png)

----------------------------------------------------------------------------------------------------
- template: content
- class: noborder
- style: h1 { font-size:38pt; } .body img { max-width:550px; margin-left:80px }

# Denicek: **Document and Edit history**

![](img/doc/denicek2.png)

----------------------------------------------------------------------------------------------------
- template: content
- class: noborder
- style: h1 { font-size:38pt; } .body img { max-width:550px; margin-left:80px }

# Denicek: **Document and Edit history**

![](img/doc/denicek3.png)

----------------------------------------------------------------------------------------------------
- template: content
- class: noborder
- style: h1 { font-size:38pt; } .body img { max-width:550px; margin-left:80px }

# Denicek: **Document and Edit history**

![](img/doc/denicek4.png)

----------------------------------------------------------------------------------------------------
- template: subtitle

# Demo
## Conference planning in Webnicek

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller noborder
- style: .body1 img { width:300px }

![](img/doc/branch1.png)

# Collaborative editing

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller noborder
- style: .body1 img { width:300px }

![](img/doc/branch2.png)

# Collaborative editing

One user changes the document structure

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller noborder
- style: .body1 img { width:300px }

![](img/doc/branch3.png)

# Collaborative editing

One user changes the document structure

Another user changes  
data in the document

---

**Can we merge the edits?**  

----------------------------------------------------------------------------------------------------
- template: image
- class: noborder
- style: .body1 img { width:500px }

![](img/doc/merged1.png)

# Merging edits

**Linearize like rebase**

----------------------------------------------------------------------------------------------------
- template: image
- class: noborder
- style: .body1 img { width:500px }

![](img/doc/merged2.png)

# Merging edits

**Linearize like rebase**

----------------------------------------------------------------------------------------------------
- template: image
- class: noborder
- style: .body1 img { width:500px }

![](img/doc/merged3.png)

# Merging edits

**Linearize like rebase**

Add data first, then change document structure is easy!

----------------------------------------------------------------------------------------------------
- template: image
- class: noborder
- style: .body1 img { width:500px }

![](img/doc/merged4.png)

# Merging edits

**Linearize like rebase**

Add data first, then change document structure is easy!

----------------------------------------------------------------------------------------------------
- template: image
- class: noborder
- style: .body1 img { width:500px }

![](img/doc/merged5.png)

# Merging edits

**Linearize like rebase**

Add data first, then change document structure is easy!

Change structure, then add new data is tricky!

----------------------------------------------------------------------------------------------------
- template: lists
- class: noborder nologo
- style: .body img { max-width:400px !important; max-height:600px !important; margin-top:0px !important; }

# Three primitive operations

![](img/edits.png)

## Apply edit history
Compute the current  
document state

## Merging of histories
Turn branching into  
a linear history

## Conflict detection
Report merge conflicts

----------------------------------------------------------------------------------------------------
- template: content
- style: h1 { font-size:38pt; } h3 { margin-bottom:0px; margin-top:10px;}

# Merging of edit histories

### Merging
Given $E, E_1$ and $E, E_2$ with a shared prefix $E$:

<blockquote>

$\mathcal{M}_E(E_1, E_2) = E, E_1, E_2'$  
$\mathcal{M}_E(E_2, E_1) = E, E_2, E_1'$

</blockquote>

---

### Edit reconciliation
Given $e_1$ and $e_2$, produce $e_2', e_2'', \ldots$ that can be applied after $e_1$

- Apply $e_1$ to nodes newly added by $e_2$
- Transform references in $e_2$ according to $e_1$

----------------------------------------------------------------------------------------------------
- template: subtitle

# Demo
## Programming by demonstration

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller noborder

![](img/doc/pbd1.png)

# The unreasonable effectiveness of merge

Simply replay edits on top of the current history!

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller noborder

![](img/doc/pbd2.png)

# The unreasonable effectiveness of merge

Simply replay edits on top of the current history!

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller noborder

![](img/doc/pbd3.png)

# The unreasonable effectiveness of merge

Simply replay edits on top of the current history!

**Does not work if the  
document evolves...**

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller noborder

![](img/doc/pbd4.png)

# The unreasonable effectiveness of merge

Branch off from the original document version & merge

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller noborder

![](img/doc/pbd5.png)

# The unreasonable effectiveness of merge

Branch off from the original document version & merge

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller noborder

![](img/doc/pbd6.png)

# The unreasonable effectiveness of merge

Branch off from the original document version & merge

**Merging logic adapts edits to the new schema**

----------------------------------------------------------------------------------------------------
- template: largeicons
- style: h1 { font-size:40pt; }

# The unreasonable effectiveness of merge

* *fa-users* **Collaborative-document editing**  
  Merge edits done independently
* *fa-arrow-pointer* **Programming by demonstration**  
  Replay edits correctly using merge
* *fa-arrows-rotate* **Incremental recomputation**  
  Remove conflicting evaluated edits
* *fa-clone* **Concrete programming**  
  Edit formula before it was copied

----------------------------------------------------------------------------------------------------
- template: subtitle

# Demo
## Denicek-based data exploration

****************************************************************************************************
- template: subtitle

# Timeline
## Adding time to spreadsheets

<script type="text/javascript">
  window.onload = function() {
    document.querySelectorAll("pre code.language-timeline").forEach(el => {
      const esc = s => s.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
      const re = new RegExp([
        /("[^"]*")/,                                   // strings
        /(?<![\w$.])(\$?[A-Z]{1,3}\$?\d+)(?!\w)/,      // cell refs
        /\b(IF|KEYPRESS)\b/,                           // functions
        /(?<![\w$.])(\d+(?:\.\d+)?)(?!\w)/             // numbers
      ].map(r => r.source).join("|"), "g");
      el.innerHTML = esc(el.textContent).replace(re, (m, str, ref, fn, num) =>
        str ? `<span class="str">${str}</span>` :
        ref ? `<span class="ref">${ref}</span>` :
        fn  ? `<span class="fn">${fn}</span>`   :
              `<span class="num">${num}</span>`);
    });
  }
</script>
<style type="text/css">
pre code { font-size:20pt !important; }
code .ref { color:FireBrick; }
code .num { color:DarkGreen; }
code .str { color:Goldenrod; }
code .fn { color:RebeccaPurple; }
</style>

----------------------------------------------------------------------------------------------------
- template: content
- class: nologo
- style: .body img { max-width:48%; } h1 { margin-bottom:20px; letter-spacing:-2px;font-size:44pt; } h1 em { font-style:normal; font-weight:200; }

# Dimensions: _Entities × Properties × Time_

![](img/demo-boids.png)
![](img/demo-planets.png)
![](img/demo-crossover.png)
![](img/demo-canon.png)

----------------------------------------------------------------------------------------------------
- template: image
- class: smaller
- style: h1 { font-size:28pt; } em { color:#d22d40; font-style:normal; }

![](img/dims.png)

# Third dimension in Excel

_Rows_ for time steps

_Columns_ left for entities and their properties

---

**Two ways of encoding attributes in a sheet**

Study participants recognize this as painful

----------------------------------------------------------------------------------------------------
- template: subtitle
- style: i { font-size:20pt; position:relative; left:10px; top:-10px; }

# Demo
## Flappy Bird in Timeline [<i class="fa fa-up-right-from-square"></i>](https://timelinesheets.com/spreadsheet/1187c0bd-fe59-4efd-9541-622f35587031)

----------------------------------------------------------------------------------------------------
- template: lists
- class: bigger
- style: ul { list-style-type:none; margin-top:20px; } pre { margin-top:20px; margin-bottom:20px; } p { margin-left:40px; margin-top:20px; margin-bottom:20px; } h2 { font-weight:600; }

# Adding time to spreadsheets

![](img/counter.gif)

## Implementing counter

```timeline
A1 = 0 => A1[-1] + 1
```

## Two language additions

`A1[-1]` to access past values  
`=>` to specify initial value  

## Synchronous dataflow

Compare formula with Lustre!  
`nat = 0 -> pre(nat) + 1;`

----------------------------------------------------------------------------------------------------
- template: lists
- class: bigger
- style: ul { list-style-type:none; margin-top:20px; } pre { margin-top:20px; margin-bottom:20px; } p { margin-left:40px; margin-top:20px; margin-bottom:20px; } h2 { font-weight:600; }

# Functional reactive programming

![](img/frp.gif)

## Counting key presses

```timeline
A1 = 0 => A1[-1] +
  IF(KEYPRESS("A"), 1, 0)
```

## Two reactive concepts

- **Events** for discrete events
- **Bahaviors** for time-varying values

## There is more to do

- No switching to dynamically  
  reconfigure dataflow graph

----------------------------------------------------------------------------------------------------
- template: lists
- class: bigger2x
- style: ul { margin-top:20px; } pre { margin-top:20px; margin-bottom:20px; } p { margin-left:40px; margin-top:20px; margin-bottom:20px; } h2 { font-weight:600; }

# Composable visualizations

![](img/compost.gif)

## Two problems

- Excel-like UI does not scale
- Need to see values over time

## Functional approach

- Domain-specific language
- **Primitives** like `COLUMN`
- **Combinators** like `OVERLAY`, `FILLCOLOR`
- Perfect fit for ranges & drag-down interaction

----------------------------------------------------------------------------------------------------
- template: content
- style: h2 { margin:50px 0px 20px 0px; padding:0px; font:600 30pt 'Roboto', sans-serif; }

# Coeffect-based optimization

## How many past values do we need to cache?

- Track the required number of past values
- `A1[-1]` creates $@\; 1$ requirement at `A1`

---

## Inspired by dataflow coeffects

<div style="padding:20px 50px; margin-top:20px">

$\dfrac{(x:\tau)\in\Gamma}{\Gamma \;@\; 0 \vdash x : \tau} \qquad \dfrac{\Gamma \;@\; n \vdash e : \tau}{\Gamma \;@\; (n+1) \vdash \textbf{prev}~e : \tau}$

</div>

----------------------------------------------------------------------------------------------------
- template: icons
- style: i { margin-right:10px; }

# Open question
## What makes spreadsheets work?

- *fa-table* **Closeness of mapping**
- *fa-square-root-variable* **Low abstraction support**
- *fa-arrow-rotate-right* **High degree of liveness**
- *fa-list-ol* **Concrete programming**

----------------------------------------------------------------------------------------------------
- template: icons
- style: i { margin-right:10px; } li:nth-child(1) { color:#d22d40; }
    li:nth-child(2) { color:#d22d40; } li:nth-child(3) { color:#d22d40; }

# Adding time
## Do we lose something?

- *fa-table* **Closeness of mapping**
- *fa-square-root-variable* **Low abstraction support**
- *fa-arrow-rotate-right* **High degree of liveness**
- *fa-list-ol* **Concrete programming**


****************************************************************************************************
- template: subtitle

# Design choices
## Document-oriented programming systems

----------------------------------------------------------------------------------------------------
- template: icons

# Design
## Four clusters of design choices

- *fa-file-lines* **Document structure** and representation
- *fa-code* **Programming model** and capabilities
- *fa-address-card* **Display and editing** mechanisms
- *fa-circle-play* **Evaluation mode** and state

----------------------------------------------------------------------------------------------------
- template: image
- style: em { color:#d22d40; font-style:normal; }
- class: smaller

![](img/blab.png)

# Display mechanism

---

**Naive realism**  
Boxer, Webstrates

---

**Renderer with widgets**  
Jupyter, Forms/3

---

**API calls**  
Ampleforth, Hypercard

---

**Memory-mapped**  
_BootstrapLab_, (BASIC :-)

----------------------------------------------------------------------------------------------------
- template: image
- style: em { color:#d22d40; font-style:normal; }

![](img/stepper.png)

# Evaluation state

---

**Ephemeral results**  
Spreadsheets, Jupyter

---

**Materialized results**  
Hypercard, Webstrates

---

**Internalized execution**  
_Boxer_, BootstrapLab

---

**Materialized execution**  
Denicek, Subtext

****************************************************************************************************
- template: subtitle

# Conclusions
## Document-oriented programming systems

----------------------------------------------------------------------------------------------------
- template: title
- style: h1 { font-size:40pt; color:#003657; } .items p { margin-top:0px; margin-bottom:5px; color:#d22d40; } i { margin-right:10px; }

# **Document-Oriented**<br /> Programming Systems

<div class="items">

_<i class="fa fa-laptop-code"></i>_ Interesting kind of **programming systems**

_<i class="fa fa-microscope"></i>_ Both **user-centric** and **theory** research

_<i class="fa fa-pen-nib"></i>_ Documents as **computational substrates**?

</div>

---

**Tomas Petricek**, Charles University, Prague

_<i class="fa fa-envelope"></i>_ [tomas@tomasp.net](mailto:tomas@tomasp.net)  
_<i class="fa fa-globe"></i>_ [https://tomasp.net](https://tomasp.net)  
_<i class="fa-brands fa-bluesky"></i>_ [@tomasp.net](https://bsky.app/profile/tomasp.net)    
