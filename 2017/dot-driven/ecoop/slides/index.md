- title : Data exploration through dot-driven development
- description : 
- author : Tomas Petricek
- theme : white
- transition : none

***************************************************************************************************

# The Gamma: _Data exploration<br /> through dot-driven development_

<br /><br /><br /><br /><br />

#### Tomas Petricek

_The Alan Turing Institute, London<br />
[http://tomasp.net](http://tomasp.net) |
[tomas@tomasp.net](mailto:tomas@tomasp.net) | 
[@tomaspetricek](http://twitter.com/tomaspetricek)_

***************************************************************************************************

<img src="images/nyt.png" />

***************************************************************************************************

<table style="width:90%;margin-bottom:100px"><tr><td style="width:50%">

### Spreadsheets

<div class="fragment" style="padding-top:20px">
<p><i class="fa fa-bug"></i> <em>Error-prone</em></p>
<p><i class="fa fa-gavel"></i> <em>Not reproducible</em></p>
<p><i class="fa fa-user"></i> <em>Easy to use</em></p>
</div>

</td><td style="width:50%">

### Programming

<div class="fragment" style="padding-top:20px">
<p><i class="fa fa-cog"></i> <em>Fully reproducible</em></p>
<p><i class="fa fa-check-circle"></i><em> Can be analyzed</em></p>
<p><i class="fa fa-mortar-board"></i><em> Even Python is hard</em></p>
</div>

</td></tr></table>

***************************************************************************************************

### Rio medalists in Python

_Athletes by number of gold medals from Rio 2016_

<div style="padding:10px 0px 60px 0px">

```
olympics = pd.read_csv("olympics.csv")
olympics[olympics["Games"] == "Rio (2016)"]
  .groupby("Athlete")
  .agg({"Gold": sum})
  .sort_values(by="Gold", ascending=False)
  .head(8)
```

</div>

***************************************************************************************************

### Rio medalists in Python

_Language and data source features you need to know_

<div class="fragment narrow" style="padding:20px 0px 105px 0px;">
<p><i class="fa fa-arrow-right"></i> <em>Python dictionaries <code>{"key": value}</code></em></p>
<p><i class="fa fa-eye"></i> <em>Generalised indexers <code>.[ condition ]</code></em></p>
<p><i class="fa fa-book"></i> <em>Operation names <code>sort_values</code></em></p>
<p><i class="fa fa-file"></i> <em>Data column names <code>"Athlete"</code></em></p>
</div>

***************************************************************************************************

<div style="padding:0px 0px 200px 100px">

# DEMO<br />_Dot-driven data exploration_

</div>

***************************************************************************************************

### Tooling complexity 

<img src="images/pl1.png" />

<div class="ref">

From: [Transcript: End-user programming of social apps](https://www.youtube.com/watch?v=XBpwysZtkkQ)<br />
Jonathan Edwards, YOW 2015

</div>

***************************************************************************************************

### Tooling complexity 

<img src="images/pl2.png" />

<div class="ref">

From: [Transcript: End-user programming of social apps](https://www.youtube.com/watch?v=XBpwysZtkkQ)<br />
Jonathan Edwards, YOW 2015

</div>

***************************************************************************************************

### Dot-driven development

_Encoding complex logic via simple member access_

<div class="fragment narrow" style="padding:20px 0px 105px 0px;">
<p><i class="fa fa-cog"></i> <em>Type providers for member generation</em></p>
<p><i class="fa fa-pagelines"></i> <em>Laziness for scaling to large hierarchies</em></p>
<p><i class="fa fa-rocket"></i> <em>Fancy types for the masses!</em></p>
</div>

***************************************************************************************************

### Pivot type provider

<br /><div class="fragment">

_Context $L$ maps names to definitions and nested contexts_

<div style="padding:10px 0px 50px 60px">

$
\definecolor{mc}{RGB}{0,32,172}
L(C) = {\color{mc}\text{type}}~C(x:\tau) = \overline{m}, L'
$

</div></div><div class="fragment">

_Pivot provider takes schema and provides a class with context_

<div style="padding:10px 0px 50px 60px">

$
\text{pivot}(F) = C, L
$

</div></div>

***************************************************************************************************

### Pivot type provider

_Generate class that constructs a relational algebra query_

<div style="padding:0px 0px 0px 0px">
<img src="images/rules.png" />
</div>

***************************************************************************************************

<div style="padding:0px 0px 200px 100px">

# DEMO<br />_Fancy types for the masses_

</div>

***************************************************************************************************

### Row types and phantom types

<br /><div class="fragment">

_Row types to track names and types of fields_

<div style="padding:10px 0px 50px 0px;transform:scale(0.8)">

$$$
\definecolor{cc}{RGB}{172,0,32}
\frac
  {\Gamma \vdash e : {\color{cc}[f_1:\tau_1, \ldots, f_n:\tau_n]}}
  {\Gamma \vdash e.\text{drop}~f_i : {\color{cc} [f_1:\tau_1, \ldots, f_{i-1}:\tau_{i-1}, f_{i+1}:\tau_{i+1}, \ldots, f_n:\tau_n]}}

</div></div><div class="fragment">

_Embed row types in provided nominal types_

<div style="padding:10px 0px 50px 0px;transform:scale(0.8)">

$$$
\frac
  {\Gamma \vdash e : {\color{mc} C_1}}
  {\Gamma \vdash e.\text{drop}~f_i : {\color{mc} C_2}}
\quad{\small \text{where}}

$$$
{fields({\color{mc} C_2}) = {\color{mc} \{f_1:\tau_1, \ldots, f_{i-1}:\tau_{i-1}, f_{i+1}:\tau_{i+1}, \ldots, f_n:\tau_n\}}}

</div></div>


***************************************************************************************************

### Fancy types for the masses!

_Powerful idea that works in other contexts_

<div class="fragment narrow" style="padding:20px 0px 105px 0px;">
<p><i class="fa fa-table"></i> <em>Row types and phantom types</em></p>
<p><i class="fa fa-phone"></i> <em>Session types for communication</em></p>
<p><i class="fa fa-question"></i> <em>Add your own fancy type here!</em></p>
</div>

***************************************************************************************************

<div style="padding:0px 0px 200px 00px">

# DEMO<br />_Unifying programming and spreadsheets_

</div>

***************************************************************************************************

# Thank you!
## _Data exploration through dot-driven development_

<style type="text/css">.final strong { width:230px; display:inline-block; } .final p { margin:0px 0px 5px 0px; }</style>
<div class="final">

**Data science** _Bridging spreadsheets and programming_

**Dot-driven** _Can express more than you'd think!_

**Fancy types** _Encoding row types via type providers_


</div> 
<br /><br />

Tomas Petricek<br />
_Questions and suggestions: [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek) 
Implementation and paper: [thegamma.net](http://thegamma.net) | [tomasp.net/academic](http://tomasp.net)_
