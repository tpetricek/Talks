- title : Popup from hell
- description : Popup from hell
- author : Tomas Petricek
- theme : simple
- transition : none

****************************************************************************************************
- class: front

# Popup from hell

<img class="fragment" src="images/ad.png" style="float:right;max-width:440px;margin-top:40px;margin-left:20px" />
<div style="height:40px"></div>

# _Reflections on the most annoying 1990s program_

<div style="height:200px"></div>

**Tomas Petricek**, _University of Kent_<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

****************************************************************************************************

# _Pop-ups in the 1990s_

<img src="images/popad.jpg" style="float:right;width:300px;" />

**Origins of pop-up ads**

- _Created at Tripod.com_
- _Hosting site like Geocities_
- _Disassociate ad from content_

<div class="fragment">

**Normal web design**

- _Custom dialog windows_
- _Manage window size & look_
- _Normal programs use windows!_

</div>

****************************************************************************************************

<img src="images/nn4.png" style="float:left;width:530px;margin:80px 40px 100px 0px" class="nb"/>

**Technical side**

JavaScript _first appeared in the Netscape Navigator browser in 1995_

window.open _and_ window.onunload _exist from version 1_

****************************************************************************************************

<img src="images/geocities.png" style="width:650px;margin:20px 40px 0px 0px;float:left" class="nb" />

**Social side**

Geocities

_Fairly well-documented creative online community of the 1990s_

****************************************************************************************************
- class: part

# _DEMO_
## Popup from hell

----------------------------------------------------------------------------------------------------

<img src="images/code.png" style="width:700px;margin-left:100px" class="nb"/>

----------------------------------------------------------------------------------------------------

<img src="images/popups.png" style="width:700px;margin-left:100px" class="nb"/>

****************************************************************************************************

<img src="images/block.png" style="float:left;width:500px;margin:20px 30px 20px 0px" class="nb"/>

**No more fun :-(**

Popup blocking commonplace in<br/> the early 2000s

_Blocks popups on page load, unload and timer events, but not on click_

****************************************************************************************************

<img src="images/sitepoint.png" style="float:left;width:540px;margin:20px 30px 20px 0px" />

**Well, actually...**

_Same user experience, recreated using harder to block technique_

****************************************************************************************************
- class: part

# _Reflections_
## What is a program

****************************************************************************************************

# _What is a program_
## Popup from hell that cannot be closed

_<i class="fa fa-school"></i> Source code from my highschool years?_

_<i class="fa fa-globe"></i> Other equivalent code from the web?_

_<i class="fa fa-lightbulb"></i> More general idea of an evil popup?_

_<i class="fa fa-window-restore"></i> System change prevents it from working!_

_<i class="fa fa-user-secret"></i> Environment determines if it is "bad"_

****************************************************************************************************
- class: part

# _REFLECTIONS_
## Two eras of the web

****************************************************************************************************

<div style="background:black;position:absolute;width:200%;right:51%;height:400%;top:-100%;z-index:-1000;"></div>
<style> .ts h3 { text-transform:none; } .tw p { color:white; font-size:1em; } .tw h3 { color:white; }</style>
<table class="ts" style="margin-top:40px"><tr><td style="width:45%;color:white" class="fragment tw">

### 1990s

<div class="narrow" style="padding:20px 0px 105px 0px;">
<p><i class="fa fa-code"></i> <em>View source</em></p>
<p><i class="fa fa-paste"></i> <em>Copy & paste</em></p>
<p><i class="fa fa-window-restore"></i> <em>Windows work!</em></p>
</div>

</td><td style="width:45%" class="fragment">

### 2010s

<div class="narrow" style="padding:20px 0px 105px 0px;">
<p><i class="fa fa-file"></i> <em>Compiled code</em></p>
<p><i class="fa fa-magic"></i> <em>Custom elements</em></p>
<p><i class="fa fa-cogs"></i> <em>WASM + Canvas</em></p>
</div>

</td></tr></table>

****************************************************************************************************

<img src="images/gdocs.png" style="max-width:520px;float:left;margin:30px 50px 100px 0px" />

**Google Docs**

Replace built-in editable element with custom code (May 2021)

<div class="fragment">

_Better performance_  
_Accessibility issues_  
_Affects extensions_

</div>

****************************************************************************************************

<img src="images/atom.png" style="max-width:520px;float:left;margin:30px 50px 100px 0px" />

**Atom vs Code**

Mini-editor war  
of the 2010s

<div class="fragment">

_Code enforces information hiding for performance, limits unexpected extensibility_

</div>

****************************************************************************************************
- class: part

# _REFLECTIONS_
## Evolution of systems

****************************************************************************************************

# _Growth of opacity_

<img src="images/js.png" style="float:right;width:270px" class="nb"/>

**JavaScript code**

- _From small clear code_
- _To compiled and minified_
- _And compiled assembly_

<div class="fragment">

**Browser element use**

- _From window.open to DOM_
- _And custom canvas element_
- _There are exceptions!_

</div>

****************************************************************************************************

# _Embedding_
## Use of system structure for program aspects

_<i class="fa fa-folder-open"></i> Program running in a system_

_<i class="fa fa-object-group"></i> Web page embedded in a browser_

_<i class="fa fa-desktop"></i> Smalltalk embedded in a host OS_

****************************************************************************************************

# _Deep vs. shallow_

<img src="images/smalltalk.jpg" style="float:right;width:350px" class="nb"/>

**Shallow embedding**

- Reuse system features
- Limited control
- Legible to the system
- Allows accessibility, blocking

<div class="fragment">

**Deep embedding**

- Redo everything from scratch
- Lose commonality, accessibility
- Gain control and flexibility

</div>

****************************************************************************************************

# _Deep embedding_
## Why and how did it happen?

_<i class="fa fa-window-restore"></i>_ Popups from hell  
_<i class="fa fa-xx"></i> Avoiding being understood by browser_

_<i class="fa fa-cogs"></i>_ Compilation to JavaScript  
_<i class="fa fa-xx"></i> Use "better" programming languages_

_<i class="fa fa-palette"></i>_ Replacing built-in features  
_<i class="fa fa-xx"></i> Programmers think they can do better_

****************************************************************************************************

# _Deep embedding_
## Is this an inevitable development?

_<i class="fa fa-puzzle-piece"></i> Attractive puzzle solving activity!_

_<i class="fa fa-users"></i> Community may be too small to do it_

_<i class="fa fa-recycle"></i> Community culture may favour reuse_

_<i class="fab fa-apple"></i> App Store may block doing this_

****************************************************************************************************
- class: part

# _Summary_
## Popup from hell

****************************************************************************************************

# _Summary_
## Popup from hell

_<i class="fa fa-city"></i>_ Laws of software system evolution?  
_<i class="fa fa-xx"></i> When is deep embedding inevitable?_

_<i class="fa fa-globe"></i>_ Embedding and information hiding  
_<i class="fa fa-xx"></i> Deep embedding hides information from system_

<br />

**Tomas Petricek**, _University of Kent_<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)
