- title : Programming as architecture, urban planning and design?
- description : Programming as architecture, urban planning and design?
- author : Tomas Petricek
- theme : simple
- transition : none

****************************************************************************************************
- class: front

# _**Cultures of programming**<br />An incomplete history of<br />getting programs to behave_

<div style="height:340px"></div>

**Tomas Petricek**, _University of Kent_<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

****************************************************************************************************

## How did software get reliable without proof?

<div style="float:right;text-align:center;"><img src="images/hoare.jpg" style="max-width:200px"  />
<p style="margin:-20px 0px 20px 10px;font-size:18pt">(Hoare, 1996)</p></div>

_"Twenty years ago it was reasonable to predict that the size of software
would be severely limited by [its] unreliability."_

_"Fortunately, the problem of program correctness has turned out to be far less serious than predicted."_

_"So [a question arises]: why have twenty years of pessimistic predictions been falsified?"_

****************************************************************************************************
- class: part

# _Origins_
## From black art to a discipline

----------------------------------------------------------------------------------------------------

<img src="images/eniac.jpg" style="float:left;max-width:440px;margin:30px 30px 100px 0px"/>

_"Programming in the early 1950s was a black art, a private arcane matter involving a programmer,
a problem, a computer, and perhaps a small library of subroutines and a primiti- ve assembly program"_

(Backus, 1976)

----------------------------------------------------------------------------------------------------

# _Establishing a discipline_

<img src="images/knuth.png" style="float:right;width:330px;margin-top:20px" class="nb"/>

**Hacker culture of programming**

- _Learning through practice_
- _Favours individual skills_
- _Knowledge not written down_

<div class="fragment">

**Establishing a discipline**

- _Mathematical computer science_
- _Programming as engineering discipline_
- _Programming as scientific management_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/babel.png" style="float:left;max-width:450px;margin:20px 50px 100px 0px" />

**Mathematical culture**

Programming as mathematics

_Notion of algorithm becomes central_

_Use formal methods to show correctness_

Worked well in academia!

----------------------------------------------------------------------------------------------------

<img src="images/swe.jpg" style="float:left;max-width:450px;margin:100px 50px 100px 0px" />

**Engineering culture**

_The black art of programming has to make a way for the science of software engineering._

NATO 1968 and 1969 Conferences on Software Engineering

----------------------------------------------------------------------------------------------------

<img src="images/mckinsey.png" style="float:left;max-width:450px;margin:100px 50px 100px 0px" />

**Managerial culture**

Unlocking Computer’s Profit Potential (McKinsey, 1968)

_Control over budgets, complexity, scheduling and the workforce._

_Focus on organisation and development proces_

****************************************************************************************************
- class: part

# _Debugging_
## Getting programs to work

----------------------------------------------------------------------------------------------------

# _Debugging Epoch Opens (1965)_

<img src="images/computers.png" style="float:right;width:250px;margin-top:30px" />

Limiting factors for computing

- _Hardware until mid-1950s_
- _Programming until mid-1960s_
- _What now? Now: debugging._

<div class="fragment">

Terminology in the 1960s

- _Program checkout_
- _Debugging and testing_
- _Error handling_

</div>

----------------------------------------------------------------------------------------------------

# _On-line debugging (1966)_

<img src="images/tt.jpg" style="float:right; max-width:270px;margin:30px 0px 0px 30px" />

_"With some care, it has been possible, for example, to find a bug while at a breakpoint in
running a test case, call the [structure] editor to make a correction, run the program on a
simpler test case to verify the correctness of the change, then resume execution of the original
test case from the breakpoint."_

****************************************************************************************************
- class: part

# _Error handling_
## Recovering from failures

----------------------------------------------------------------------------------------------------

<img src="images/errors.png" style="float:left;max-width:400px; margin:40px 70px 100px 0px" class="nb"/>

**ON in PL/1 (1964)**

_Allow writing  
robust programs_

_Handle 23 known errors_

**ERRORSET in LISP (1962)**

_More control to programmers_

----------------------------------------------------------------------------------------------------

# _Exceptions_

<img src="images/goodenough.png" style="float:right;width:350px"/>

**John B. Goodenough (1975)**

- _Introduces "exceptions"_
- _Language feature_
- _Theory of exceptions_

<div class="fragment">

**Exception as boundary object**

- _Studied formally_
- _Implemented in compilers_
- _Handled in reliable ways_

</div>

----------------------------------------------------------------------------------------------------

<img src="images/axe.jpg" style="float:left;max-width:420px; margin:40px 40px 100px 0px" />

**Exceptions in telecom**

_One machine is not enough_

_When something goes wrong, kill the process_

_Supervisor process restars_

Exceptions as a basis for programming style

****************************************************************************************************
- class: part

# _Testing_
## Getting programs to solve the problem

----------------------------------------------------------------------------------------------------

<img src="images/test.jpg" style="float:left;max-width:420px; margin:40px 70px 100px 20px" />

**Computer Program Test Methods Symposium (1972)**

_Testing distinguished  
from debugging_

_Companies hire_ test managers _and_ test technicians

_Test automation establishes a_ test _as an artifact_

----------------------------------------------------------------------------------------------------

# _Growth of software testing_

<div style="float:right;text-align:center;"><img src="images/gelperin.jpg" style="max-width:200px"  />
<p style="margin:-20px 0px 20px 10px;font-size:18pt">(Gelperin & Hetzel, 1988)</p></div>

**Demonstration period (1957-78)**

 - _Test to show that programs  
   work as required_
 - _Alternative to proofs_

**Destruction period (1978-82)**

  - _Test to find errors_
  - _Enables new tool developments_
  - _Mutation testing, random testing_

----------------------------------------------------------------------------------------------------

<img src="images/waterfall.png" style="max-width:510px;float:left;margin:100px 30px 100px 0px" class="nb"/>

**Testing as part of the development process**

_Testing after coding in Waterfall (1970)_

Unit testing _separate from_ acceptance tests

----------------------------------------------------------------------------------------------------

# _Extreme programming_

<img src="images/tdd.jpg" style="float:right;width:350px" class="nb"/>

**Test-Driven Development**

- _Programmers & customers_
- _Write tests first_
- _Then write code and improve_

**Development methodology**

- _From black art to engineering_
- _Engineering take on  
  managerial methods_

****************************************************************************************************
- class: part

# _Conclusions_
## Cultures of programming

----------------------------------------------------------------------------------------------------

<img src="images/debug.gif" style="max-width:400px;float:left;margin:30px 60px 100px 0px" />

**What happened  
to debugging?**

_Conceptually similar to '60s_  
_Learned through practice_  
_Hacker culture only_

No boundary object to exchange with other cultures?

----------------------------------------------------------------------------------------------------

# _Cultures of programming_
## Ways of trusting software systems

_<i class="fa fa-keyboard"></i>_ Hacker _- Trust person making it_

_<i class="fa fa-wrench"></i>_ Engineering _- Rely on tools_

_<i class="fa fa-project-diagram"></i>_ Managerial _- Rely on processes_

_<i class="fa fa-infinity"></i>_ Mathematical _- Trust formal proofs_

----------------------------------------------------------------------------------------------------
- class: front

# _**Cultures of programming**_
## A useful perspective for history of programming

_<i class="fa fa-subscript"></i> Mathematization of programming_

_<i class="fa fa-wrench"></i> Rise of software engineering_

_<i class="fa fa-terminal"></i> Interactive programming_

_<i class="fa fa-tape"></i> Programming with types_  

<br />

Papers: [http://tomasp.net/academic](http://tomasp.net/academic)<br />
Contact: [t.petricek@kent.ac.uk](mailto:t.petricek@kent.ac.uk)
