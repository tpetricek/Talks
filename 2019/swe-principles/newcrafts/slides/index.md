- title : The search for Fundamental Software Engineering Principles
- description : The search for Fundamental Software Engineering Principles
- author : Tomas Petricek
- theme : simple
- transition : none

****************************************************************************************************

# _<span style="font-weight:300">The search for</span> <br /> Fundamental Software Engineering Principles_

<br /><br /><br /><br /><br /><br />

**Tomas Petricek**

University of Kent and fsharpWorks<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

****************************************************************************************************
- class: part

# _Motivation_
## What is a fundamental principle?

----------------------------------------------------------------------------------------------------

<img src="images/opticks.jpg" style="max-width:400px;float:left;margin:30px 40px 100px 0px" />

**Fundamental knowledge**

<div class="fragment">

_What knowledge about software will remain  
relevant in 100 years?_

</div>
<div class="fragment">

_What should we teach students at university?_

</div>

----------------------------------------------------------------------------------------------------

# _The halting problem_
## Fundamental computer science knowledge

<img src="images/turing.gif" style="float:right;max-width:340px;margin-top:0px;margin-left:40px" class="nb"/>

It is impossible to give a program
$\Theta$ that, for any  
other program $p$ decides whether $p$ terminates or not.

----------------------------------------------------------------------------------------------------

# _Software engineering_
## What knowledge do we teach today

_<i class="fa fa-history"></i>_ Agile and Scrum methodologies

_<i class="fa fa-square"></i>_ UML or DDD modelling methods

_<i class="fa fa-project-diagram"></i>_ Object-oriented design patterns

_<i class="fa fa-wrench"></i>_ Tools like Git, Travis, Docker

----------------------------------------------------------------------------------------------------

# _Software engineering_
## The search for fundamental knowledge

_<i class="fa fa-church"></i>_ What is impossible to build?

_<i class="fa fa-trophy"></i>_ Can we guarantee cost or correctness?

_<i class="fa fa-chalkboard-teacher"></i>_ Not a formal mathematical problem

_<i class="fa fa-book"></i>_ Can we say anything certain at all?

----------------------------------------------------------------------------------------------------

<img src="images/sdi.jpg" style="max-width:500px;float:left;margin:30px 40px 100px 0px" />

**Historically situated Software Engineering**

_History and critical analysis of software development tools and practices_

****************************************************************************************************
- class: part

# _History_
## Historically situated Software Engineering

----------------------------------------------------------------------------------------------------

# _Programming in 1950_
## Hacker culture of programming

<img src="images/boys.jpg" style="float:right;max-width:230px;margin-top:0px;margin-left:60px" class="nb"/>

_Programming in the 1950s was a black art, a private
arcane matter (...), each problem required a unique beginning at square one,
and the success of a program depended primarily on the programmer's  
private techniques and inventions._

----------------------------------------------------------------------------------------------------

<img src="images/nato.jpg" style="max-width:450px;float:left;margin:60px 30px 100px 0px" />

**NATO 1968 Conference on Software Engineering**

_Programming started transition from a craft for a
long-haired priest-hood to
a real engineering discipline._

----------------------------------------------------------------------------------------------------

# _Software engineering_
## Agreement on problems, not on solutions

_<i class="fa fa-user-friends"></i>_ Follow-up NATO 1969 Conference failed

_<i class="fa fa-file"></i>_ Mathematical culture favours proofs

_<i class="fa fa-user-tie"></i>_ Business culture favours management

_<i class="fa fa-university"></i>_ Roots of a cultural divide?

----------------------------------------------------------------------------------------------------

<img src="images/office.jpg" style="max-width:440px;float:left;margin:50px 30px 100px 0px" />

**Apply industrial methods of production to software**

_Control over budgets, scheduling, workforce_

_Process should eliminate reliance on individuals_

<div class="fragment">

Heavy-weight processes like Waterfall make sense!

</div>

----------------------------------------------------------------------------------------------------

# _Paradigm Change (1987)_

<img src="images/floyd.jpg" style="float:right;max-width:220px;margin-top:50px;margin-left:40px" class="nb"/>

**Product oriented**

_Software is seen as a product  
serving some fixed use case_

<div class="fragment">

**Process oriented**

_Software emerges from the totality  
of interconnected processes of  
learning and communication in  
an evolving environment_

</div>

----------------------------------------------------------------------------------------------------

# _Fundamental knowledge_
## Agile methodology in historical context

_<i class="fa fa-sync"></i>_ Context, response and problems

_<i class="fa fa-comments"></i>_ Minority process oriented paradigm

_<i class="fa fa-user-tie"></i>_ Pushback against managerial culture

_<i class="fa fa-mobile-alt"></i>_ Application crisis of 1990s

----------------------------------------------------------------------------------------------------

# _Modelling in context_

<img src="images/uml.jpg" style="float:right;max-width:200px;margin-top:0px;margin-left:50px" />

**Modelling using UML**

 - _Fully specify system up-front_
 - _Managerial control over process_
 - _Becomes communication tool_

**Modelling via code and tests**

 - _Rise of engineering culture_
 - _Supports process-oriented approach_
 - _Keeping model up-to-date_

****************************************************************************************************
- class: part

# _Principles_
## Fundamental ideas from the past

----------------------------------------------------------------------------------------------------

**What is more complex software system to build?**

<table><tr>
<td>

<img src="images/stockfish.png" class="nb" style="width:390px"/>

Chess engine that can consistently win against grandmasters?

</td><td>

<img src="images/quickbooks.png" class="nb" style="width:420px"/>

Accounting system that calculates VAT in UK and France?

</td>
</tr></table>

----------------------------------------------------------------------------------------------------

<img src="images/system360.jpg" style="max-width:500px;float:left;margin:30px 40px 100px 0px" />

**IBM System/360**

Operating system  
for a wide range of  
IBM machines

_Massively delayed  
due to complexity  
and conflicting requirements_


----------------------------------------------------------------------------------------------------

# _Software complexity_
## No Silver Bullet (Fred Brooks, 1986)

<img src="images/brooks.jpg" style="float:right;max-width:260px;margin-top:-20px;margin-left:50px" />

_Much of the complexity [software engineer] must master is arbitrary
complexity, forced without rhyme  
or reason by the many human
institutions and systems to which his interfaces must confirm._

----------------------------------------------------------------------------------------------------

# _No silver bullet (1986)_

<img src="images/nosilver.png" style="max-width:220px;float:right;margin:30px 0px 0px 20px" />

**Essential complexity**

 - _Complexity of essential logic_
 - _Specification is as complex as program implementing it_

<div class="fragment">

**Accidental complexity**

 - _Imperfect programming tools_
 - _Unless this is more than 9/10, order  
   of magnitude improvement is impossible_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/bullet.png" style="max-width:400px;float:left;margin:30px 40px 100px 0px" />

**Fundamental Software Engineering principle**

_There is no single development, in either technology or management technique, which by
itself promises even one  
order-of-magnitude improvement within a decade in productivity,
in reliability, in simplicity._

----------------------------------------------------------------------------------------------------

<img src="images/starwars.jpg" style="max-width:540px;float:left;margin:30px 30px 100px 0px" />

**Star Wars (1980s)**

_Fully automatic software system to track and shoot down Soviet nuclear missiles_

Can reliable system to do this be built?

----------------------------------------------------------------------------------------------------

# _Sources of complexity_

**Analog systems**  
_Small change in input causes small change in output_  
_Analog computers of 1930s, audio synthesizers_

**Digital systems with repeated components**  
_Non-linear, but we can test components in isolation_  
_CPU units and much of modern hardware_

**Digital systems without repetition**  
_Non-linear and very hard to test_  
_Any modern software system!_

----------------------------------------------------------------------------------------------------

<img src="images/lick.jpg" style="max-width:460px;float:left;margin:40px 70px 100px 0px" />

**Fundamental Software  
Engineering Principle**

_Complex software can only be mastered if it is developed progressively, with the aid of
extensive testing, and then operated more or less continually in a somewhat lenient and forgiving environment._

----------------------------------------------------------------------------------------------------

# _Arguments that count (2013)_

<img src="images/arguments.png" style="max-width:260px;float:right;margin:40px 0px 0px 20px" />

**Defense would be unreliable**

_Since we have no spare planets  
on which to fight trial nuclear  
wars, testing of a global ABM  
system is impossible._

<div class="fragment">

**Enemy has it easier**

_Very expensive defenses could give the Soviet Union an incentive to invest in relatively
cheap offensive countermeasures, creating arms race instabilities._

</div>

----------------------------------------------------------------------------------------------------

<img src="images/patriot.jpg" style="float:left;max-width:560px;margin-top:0px;margin-right:40px" />

**Developed into the Patriot system**

<div class="fragment">

_Accidentally hit British fighter  
plane killing two_

_Software updated 9 times during  
the Gulf War_

</div>

****************************************************************************************************
- class: part

# _Metaphors_
## Fundamental ideas from other disciplines

----------------------------------------------------------------------------------------------------

# _Metaphors for programming_

<img src="images/bridge.jpg" style="float:right;max-width:280px;margin-top:20px;margin-left:20px" />

**Engineering metaphor**

 - _Managerial response to crisis_
 - _Focus on planning and control_
 - _Each metaphor has misfits_

<div class="fragment">

**Many other metaphors**

 - _Architecture and urban planning_
 - _Programming is writing code_
 - _Programming is growing a system_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/garden.jpg" style="max-width:500px;float:left;margin:20px 30px 100px 0px" />

**Why metaphors for programming matter?**

New way of thinking and shift of emphasis

<div class="fragment">

_Goal of writing is communication_

_Growing needs long-term maintainability_

</div>

----------------------------------------------------------------------------------------------------

# _Urban planning_

<img src="images/corbusier.jpg" style="float:right;max-width:340px;margin-top:0px;margin-left:40px" />

**Complexity of software**

 - _Analog systems_
 - _Repetitive digital systems_
 - _Non-repetitive digital_

**Complexity of cities**

 - _Problems of simplicity_
 - _Problems of unorganized complexity_
 - _Problems of organized complexity_

----------------------------------------------------------------------------------------------------

<img src="images/life.jpg" style="float:left;max-width:400px;margin:20px 60px 0px 40px" />

**Dealing with problems  
of organized complexity**

_City processes are too complex to be abstracted. Explore them in full complexity._

_One unaverage value tells us more than statistic._

----------------------------------------------------------------------------------------------------

# _Adaptable buildings_

<img src="images/learn.jpg" style="max-width:300px;float:right;margin:30px 0px 0px 20px" />

**Evolve and need maintenance**

 - _But none are designed to evolve_
 - _Different layers evolve differently_
 - _How to teach maintenance?_

**Choosing a material**

 - _Traditional materials are well understood_
 - _It should look bad before it acts bad_

****************************************************************************************************
- class: part

# _Conclusions_
## What is Software Engineering?

----------------------------------------------------------------------------------------------------

# _It's own discipline_

<img src="images/newton.jpg" style="max-width:400px;float:right;margin:30px 0px 0px 20px" />

**Meeting of cultures**

 - _Managerial culture_
 - _Engineering culture_
 - _Mathematical culture_

<div class="fragment">

**What are the foundations**

 - _Cannot be from just one culture_
 - _Past tools and practices in context_
 - _Historically situated_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/latour.jpg" style="max-width:400px;float:left;margin:30px 30px 100px 0px" />

**We Have Never  
Been Modern**

_We must rework our thinking to conceive of a "Parliament of Things"
where natural and social phenomena, and the discourse about them are not seen as
separate objects, but as hybrids made by the interaction of people, things and concepts._

----------------------------------------------------------------------------------------------------

# _Summary_

**Fundamental Software Engineering principles**

- _History and critical analysis of software development tools and practices_
- _Interaction between cultures:  
  managerial, mathematical, engineering_
- _"Parliament of Things" made by interaction_

<br /><br />

**Tomas Petricek**, University of Kent and fsharpWorks<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

----------------------------------------------------------------------------------------------------

**Reading list**

<div class="sm"><style> .reveal .sm li { font-size:26pt; margin-bottom:10px; } </style>

 - Nathan Ensmenger (2012). [Computer Boys Take Over](https://amzn.to/2YCeTeF)
 - Rebecca Slayton (2013). [Arguments that Count](https://amzn.to/2Ho41uW)
 - Jane Jacobs (1961). [The Death and Life of  
   Great American Cities](https://amzn.to/2LR4El7)
 - Bruno Latour (1993). [We Have Never Been Modern](https://amzn.to/2Eg5UIb)
 - Stewart Brand (1994). [How Buildings Learn](https://amzn.to/2JJQlfv)
 - Brian Randell (1968). [The 1968/69 NATO  
   Software Engineering Reports](http://homepages.cs.ncl.ac.uk/brian.randell/NATO/NATOReports/)
 - Christiane Floyd (1993). [Outline of a Paradigm  
   Change in Software Engineering](https://link.springer.com/chapter/10.1007/978-94-011-1793-7_11)
 - Fred Brooks (1986). [No Silver Bullet](http://worrydream.com/refs/Brooks-NoSilverBullet.pdf)

</div>

****************************************************************************************************
