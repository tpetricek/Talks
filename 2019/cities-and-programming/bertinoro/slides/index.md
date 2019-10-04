- title : Programming languages as a design problem
- description : Programming languages as a design problem
- author : Tomas Petricek
- theme : simple
- transition : none

****************************************************************************************************

# Programming languages<br />as a design problem

<br /><br /><br /><br /><br /><br />

**Tomas Petricek**

University of Kent<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

----------------------------------------------------------------------------------------------------

<img src="images/lang.png" style="max-width:400px;float:left;margin:0px 40px 0px 0px" />

**Programming languages**

_As formal languages_

<div class="fragment" style="margin-top:60px">

Metaphor determines  
what questions we ask

_Syntax, semantics,  
formal properties_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/bridge.jpg" style="max-width:500px;float:left;margin:20px 40px 0px 0px" />

**Software engineering**

_Structured activity_

<div class="fragment" style="margin-top:60px">

Metaphor determines  
questions we ask

_Reliability, safety  
Development process_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/corbusier.jpg" style="max-width:450px;float:left;margin:20px 40px 0px 0px" />

**Architecture and  
urban planning**

_Design problem_

<div class="fragment" style="margin-top:60px">

Metaphor determines  
what questions we ask

_Dealing with complexity, evolution in time_

</div>

****************************************************************************************************
- class: part

# _Motivation_
## Is software like cities?

----------------------------------------------------------------------------------------------------

# _Kinds of complexity_

<img src="images/cities.jpg" style="float:right;max-width:260px;margin-top:20px;margin-left:50px" />

**Parnas on strategic defence**

 - _Analog systems_
 - _Repetitive digital systems_
 - _Non-repetitive digital systems_

<div class="fragment">

**Modelling via code and tests**

 - _Problems of simplicity_
 - _Unorganized complexity_
 - _Organized complexity_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/sdi.jpg" style="max-width:440px;float:left;margin:50px 30px 100px 0px" />

**Gradual development**

_Large computing systems are products of evolutio- nary development (...);
they became reliable through a process of slow testing and adaptation to an operational environment_

_(Weizenbaum on SDI)_

----------------------------------------------------------------------------------------------------

<img src="images/brand.jpg" style="max-width:440px;float:left;margin:50px 30px 100px 0px" />

**Christopher Alexander**

_Things that are good have a certain structure. You can't get that except dynamically._

_In nature you've got continuous very-small-feedback-loop (...), which   
is why things get to be harmonious._

----------------------------------------------------------------------------------------------------

# _Beautiful theories_

<img src="images/jacobs.jpg" style="float:right;max-width:320px;margin-top:20px;margin-left:50px" />

**Radiant garden city beautiful**

 - _Le Corbusier's Ville Radieuse_
 - _Garden Cities in the UK_
 - _American City Beautiful_

<div class="fragment">

**Programming languages**

 - _Simplified formal models_
 - _Object-oriented programming_
 - _Agile development methodology_

</div>

****************************************************************************************************
- class: part

# _Methodologies_
## Learning from urban planning and architecture

----------------------------------------------------------------------------------------------------

# _1. What actually works_

<img src="images/cities.jpg" style="float:right;max-width:260px;margin-top:20px;margin-left:50px" />

**The case of cities**

 - _Sidewalk life in Greenwich Village_
 - _Unslumming in North End_
 - _Why does it work?_

<div class="fragment">

**The case of programming**

 - _R and JavaScript languages_
 - _No information hiding in MIDI_
 - _Ethnography done in HCI_

</div>

----------------------------------------------------------------------------------------------------

# _1. Peter Naur_

<img src="images/naur.png" style="float:right;max-width:260px;margin-top:0px;margin-left:40px" />

_It is curious how authors, who in the formal aspects of their work require
painstaking demonstration and proof, in the informal aspects are satisfied
with subjective claims that have not the slightest support, neither in argument
nor in verifiable evidence._

The Place of Strictly Defined  
Notation in Human Insight

----------------------------------------------------------------------------------------------------

# _2. How buildings evolve_

<img src="images/brand.jpg" style="max-width:300px;float:right;margin:80px 0px 100px 20px" />

_[Almost all buildings are] designed not to adapt; also budgeted and financed not to,
constructed not to, administered not to, maintained not to, regulated and taxed not to,
even remodelled not to._

_But all buildings (...) adapt anyway, however poorly, because the usages in and around them are changing constantly._

----------------------------------------------------------------------------------------------------

# _2. How software evolves_

<img src="images/mode.jpg" style="max-width:400px;float:right;margin:40px 0px 100px 20px" />

**How software evolves?**

 - _Separation of concerns_
 - _Know all usages?_
 - _Usages will change_

<div class="fragment">

**Simondon's concretization**

 - _Parts with clear functions_
 - _Process of concretization_
 - _Functions get intermixed_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/image.jpg" style="max-width:400px;float:left;margin:10px 70px 0px 20px" />

**3. Navigating through a city**

A pleasant city is legible

_[A legible city is] one whose districts or landmarks or pathways are easily identifiable
and are easily grouped into an overall pattern._

----------------------------------------------------------------------------------------------------

# _3. Navigating through a city_

<img src="images/legible.png" style="max-width:400px;float:right;margin:40px 0px 0px 20px" />

**What makes city legible?**

 - Distinguishable districts
 - Visible landmarks
 - Paths and edges

<div class="fragment">

**What makes code legible?**

 - How people navigate?
 - Path of program execution
 - Linking definitions to usage

</div>

****************************************************************************************************
- class: part

# _Ideas_
## Building software like cities

----------------------------------------------------------------------------------------------------

# _1. Adaptable software_
## Teaching good maintenance habits

_<i class="fa fa-wrench"></i>_ New buildings teach bad maintenance habits

_<i class="fa fa-building"></i>_ Once built, owners stop paying attention

_<i class="fa fa-cloud"></i>_ Chaos engineering in cloud systems

_<i class="fa fa-toolbox"></i>_ Build parts that will have to be replaced soon?

----------------------------------------------------------------------------------------------------

<img src="images/vinyl.jpg" style="max-width:500px;float:left;margin:50px 30px 100px 0px" />

**1. Building materials**

Tale of vinyl siding

<div class="fragment">

_Materials that look bad before they act bad_

_Problems with traditional materials are well understood_

</div>

----------------------------------------------------------------------------------------------------

# _2. Vernacular design method_

<img src="images/hut.jpg" style="max-width:300px;float:right;margin:40px 0px 0px 20px" />

**Unself-conscious design**

 - _Christopher Alexander_
 - _Musgum mud huts_
 - _Develops by gradual adaptation_

**Self-conscious design**

 - _Design to satisfy requirements_
 - _Requires full understanding_
 - _Keeps reinventing form_

----------------------------------------------------------------------------------------------------

<img src="images/sap.png" style="max-width:400px;float:left;margin:30px 40px 0px 0px" />

**Vernacular design method**

Can we build software without reinventing form?

<div class="fragment">

_Buying and reconfiguring existing systems?_

</div>

----------------------------------------------------------------------------------------------------

# _3. Dealing with complexity_

<img src="images/cities.jpg" style="float:right;max-width:260px;margin-top:20px;margin-left:50px" />

**Jacobs on understanding cities**

 1. to think about processes
 2. to work inductively
 3. to seek for 'unaverage' clues

<div class="fragment">

**Understanding software**

 - Non-reductionist view?
 - Look at unexpected cases?
 - Not proofs but illustrations?

</div>


****************************************************************************************************
- class: part

# _Conclusions_
## Building software like cities

----------------------------------------------------------------------------------------------------

<img src="images/maps.jpg" style="max-width:400px;float:left;margin:30px 40px 100px 0px" />

<br />

**Programming** _as designing and intervening in complex systems that cannot be fully understood_

**Mathematical** _reductionism via statistics or logic cannot talk about all that matters_

----------------------------------------------------------------------------------------------------

# _Conclusions_

**Programming languages as a design problem**

 - Useful methodologies to follow    
   _What actually works? How software evolves?_
 - Concrete ideas about planning    
   _Design for adaptability? Avoid reinventing forms?_

<br /><br />

**Tomas Petricek**, University of Kent<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)
