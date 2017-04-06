- title : Miscomputation: Learning to live with errors
- description : Charles Babbage once said that 'if trials of three or four simple cases have been 
    made, it is scarcely possible that there can be any error'. We now know that errors are more 
    common and harder to eliminate. In this talk, I look at different strategies that 
    programmers use for dealing with errors. It turns out that there is a surprisingly wide 
    range of options! 
- author : Tomas Petricek
- theme : white
- transition : none

***************************************************************************************************

<h1 style="margin-bottom:-15px">Miscomputation in software</h1>

### _Learning to live with errors_

<br /><br /><br /><br />

### Tomas Petricek

The Alan Turing Institute, London<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net)

---------------------------------------------------------------------------------------------------

<img src="images/paper.png" style="height:700px;margin:-50px 0px 0px 200px" />

***************************************************************************************************

## Trivial problem or industry crisis?

---------------------------------------------------------------------------------------------------

> <img src="images/babbage.png" style="float:right;width:170px;margin:0px 0px 0px 10px"/>
> 
> If trials of three or four simple cases have been made, and are found to agree with the 
> results given by the engine, it is scarcely possible that there can be any error.
>
> <br />
>
> > Charles Babbage, On the mathematical  
> > powers of the calculating engine (1837)

---------------------------------------------------------------------------------------------------

> <img src="images/eniac.jpg" style="float:right;width:350px;margin:30px 0px 30px 20px"/>
>
> The ENIAC women would simply set up the machine to perform these predetermined plans (...)
>
> [T]hat this work would turn out to be difficult and require radically innovative thinking 
> was completely unanticipated.
>
> <br />
>
> > Nathan Ensmenger (2010)<br /> The Computer Boys Take Over

---------------------------------------------------------------------------------------------------

> By the end of [1960s] many were talking of a crisis (...). <br />
> For the next several decades, [managers, academics and governments]  would release 
> warnings about the desperate state of the software industry with 
> ritualistic regularity.
>
> <br />
>
> > Nathan Ensmenger (2010)<br /> The Computer Boys Take Over

---------------------------------------------------------------------------------------------------

<img src="images/still.jpg" style="margin-left:200px;height:650px" />

***************************************************************************************************

## Living with errors

<img src="images/hero3.png" />
<img src="images/hero2.png" />
<img src="images/hero4.png" />
<img src="images/hero5.png" />


***************************************************************************************************

<img src="images/hero3.png" style="float:right;margin:-60px 0px 0px 40px" />

## Errors as part of the process

---------------------------------------------------------------------------------------------------

# COBOL and data processing crisis

<div class="fragment">

## Eliminate the need for skilled programmers

> By late 1960s, the emerging software  <br />
> crisis became defined as managerial.

<br /><br />
</div>

---------------------------------------------------------------------------------------------------

## NATO Conference on Software Engineering

<img src="images/nato.png" style="float:right;width:350px;margin:50px 0px 30px 20px"/>

<p class="fragment" style="margin-right:400px;font-style:italic">
The black art of programming has to make way
for the science of software engineering. 
</p><p class="fragment" style="margin-right:400px;font-style:italic">
Software engineering completes the turn toward
managerial solutions to the software crisis.
</p>

---------------------------------------------------------------------------------------------------

## Software craftsmanship

> NATO conference (1968) started transition from computer 
> programming as a craft to an engineering discipline

<div class="fragment">

> Software craftsmanship (2001) emphasizes skills of developers
> _"individuals and interactions over processes and tools"_

</div>

---------------------------------------------------------------------------------------------------

# Test-driven development

## Error as part of the development process

1. Introduce controlled isolated error
2. Eliminate error by writing more code

---------------------------------------------------------------------------------------------------

# Test-driven development

## Error as a medium for information

Tests become the _specification_  
Tests as a honest _documentation_

***************************************************************************************************

<img src="images/hero2.png" style="float:right;margin:-60px 0px 0px 40px" />

## Errors as a contradiction

---------------------------------------------------------------------------------------------------

# Algol language

## "Remarkable computer science achievement"

<div class="fragment">

 - _"Object of beauty"_ and never widely (?) adopted
 - Defines academic programming agenda

<br /><br />
</div>


---------------------------------------------------------------------------------------------------

# Algol research programme (1960s)

## Formal language specification

> One of the goals (..) was to utilize the resources of logic to increase
> the confidence (..) in the correctness of a program (..) 
> "[Instead] of debugging a program, one should prove that it  
> meets its specifications (...)".
>
> > Mark Priestley (2011)<br />
> > Science of Operations 

---------------------------------------------------------------------------------------------------

# Algol research programme (2010s)

<div class="fragment">

## Dependently typed programming

> [T]oday most people who write software (...) assume that the costs 
> of formal program verification outweigh the benefits. The purpose 
> of this book is to convince you that the technology of program 
> verification is mature enough today (...).
>
> > Adam Chlipala, Certified Programming  
> > with Dependent Types (2013)

</div>

---------------------------------------------------------------------------------------------------

# Error as a contradiction

## Dream for the last 50 years

Good strategy for academic computer science  
Influences what questions we ask!

<br /><br /><br />

---------------------------------------------------------------------------------------------------

<img src="images/noproof.png" />

***************************************************************************************************

<img src="images/hero4.png" style="float:right;margin:-60px 0px 0px 40px" />

## Errors as the unavoidable

---------------------------------------------------------------------------------------------------

# Erlang language
## Distributed long-running systems

Created at Ericsson for telecommunications  
Errors will happen because of scale

---------------------------------------------------------------------------------------------------

## Handling errors in Erlang

> What kind of code must the programmer write when they find an error? 
> (..) let some other process fix the error, but what 
> does this mean for their code? The answer is *let it crash*.
>
> > Joe Armstrong (2003), <br />
> > Programming reliable systems

---------------------------------------------------------------------------------------------------

# Errors as the unavoidable
## Erlang error is the opposite of test error

Errors are the _lack of specification_  
They are expected - are they still errors?
 
***************************************************************************************************

<img src="images/hero5.png" style="float:right;margin:-60px 0px 0px 40px" />

## Errors as an inspiration

---------------------------------------------------------------------------------------------------

## Smalltalk ecosystem (1970s)

> [Smalltalk approach] to the design of languages [is] 
> quite different from what was familiar in the Algol [programme]. 
>
> Programming was not thought of as the task of constructing a 
> linguistic entity, but rather as a process of working interactively with the 
> semantic representation of the program.
>
> > Mark Priestley (2011)<br />Science of Operations

---------------------------------------------------------------------------------------------------

# Live coding
## Environments for music

<div class="fragment">

> In musical genres that are not notated so closely (...), there are no wrong notes – 
> only notes that are more or less appropriate to the performance. 
>
> > Alan Blackwell and Nick Collins (2005)<br />
> > The Programming Language as a Musical Instrument

</div>

---------------------------------------------------------------------------------------------------

# Live coding
## Errors in live coding

> [Live coders] may well prefer to accept the results of an imperfect execution. 
> [They] might perhaps compensate for an unexpected result by manual intervention,
> or even accept the result as a serendipitous alternative to the original note. 
>
> > Alan Blackwell and Nick Collins (2005), <br />
> > The Programming Language as a Musical Instrument

---------------------------------------------------------------------------------------------------

# Errors as an inspiration
## Enable quick human intervention

Make errors easier to _hear_ or _see_  
Not just live coded art performances

***************************************************************************************************

## What are errors... 

## ...and what are programs?

---------------------------------------------------------------------------------------------------

<img src="images/error.png" style="height:650px; margin-left:150px" />

---------------------------------------------------------------------------------------------------

# Socio-technological entities

---------------------------------------------------------------------------------------------------

<img src="images/knight.png" style="height:650px; margin-left:150px" />

---------------------------------------------------------------------------------------------------

### _Live coder answer_
It took _45 minutes_ to shut it down!

### _Erlang answer_
New message, old server _should crash_!

### _Logician answer_
Critical systems must be _proved correct_

### _Craftsman answer_
What are the _tests_ for preventing errors?

***************************************************************************************************

## Summary

<img src="images/hero3.png" />
<img src="images/hero2.png" />
<img src="images/hero4.png" />
<img src="images/hero5.png" />

---------------------------------------------------------------------------------------------------

# Summary
## Crisis narrative

> The continued four-decades-long crisis in one of the largest
> and fastest-growing sectors of the US economy suggests an interesting
> dichotomy (..)
>
> > Nathan Ensmenger (2010)<br /> The Computer Boys Take Over

---------------------------------------------------------------------------------------------------

# Summary
## What is a program?

Understanding errors in software  
Understanding software in society

Alternative look at programming paradigms
 
<br /><br /><br />

---------------------------------------------------------------------------------------------------

# Summary
## How to write programs?

Write precise specification as types  
vs. Write precise implementation as code

Write tests, theorems or interact?

<br /><br /><br />

---------------------------------------------------------------------------------------------------

# Thank you!
## Learning to live with errors

 - Errors teach us about _the industry_
 - Errors teach us about _paradigms_
 - Errors teach us about _writing programs_
 
<br /><br /><br />

Questions & answers?  
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net)
