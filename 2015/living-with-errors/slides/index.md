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

# Miscomputation

### Learning to live with errors

<div style="margin:120px 0px 60px 0px">
<img src="images/hapoc.jpg" style="height:150px" />
</div>

**Tomas Petricek**, University of Cambridge  
[http://tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

***************************************************************************************************

## Miscomputation

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

> <img src="images/babbage.png" style="float:right;width:170px;margin:0px 0px 0px 10px"/>
> 
> If trials of three or four simple cases have been made, and are found to agree with the 
> results given by the engine, it is scarcely possible that there can be any error (...).
>
> <br />
>
> > Charles Babbage, On the mathematical  
> > powers of the calculating engine (1837)

---------------------------------------------------------------------------------------------------

> Errors in coding were only gradually recognized to be a significant problem: a typical early 
> comment was that of Miller [circa 1949], who wrote that such errors, along with hardware faults, 
> could be "expected, in time, to become infrequent".
>
> > Mark Priestley, Science of Operations (2011)

---------------------------------------------------------------------------------------------------

## Examples of miscomputations

 - Invalid specification
 - Invalid implementation
 - No specification available
 - Physical error condition

---------------------------------------------------------------------------------------------------

<img src="images/error.png" style="height:650px" />

***************************************************************************************************

## Living with errors

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

<img src="images/hero1.png" />
<img src="images/hero2.png" />
<img src="images/hero3.png" />
<img src="images/hero4.png" />

***************************************************************************************************

## Errors as a curse

<br />
<img src="images/hero1.png" />

---------------------------------------------------------------------------------------------------

## Algol research programme (1960s)

> One of the goals (..) was to utilize the resources of logic to increase  
> the confidence (..) in the correctness of a program. As McCarthy had  
> put it, "[instead] of debugging a program, one should prove that it  
> meets its specifications (...)".
>
> > Mark Priestley, Science of Operations (2011)

---------------------------------------------------------------------------------------------------

## Dependently typed programming (2010s)

Tries to make proof a part of programming practice

<div class="fragment">

> [T]oday most people who write software (...) assume that the costs 
> of formal program verification outweigh the benefits. The purpose 
> of this book is to convince you that the technology of program 
> verification is mature enough today (...).
>
> > Adam Chlipala, Certified Programming  
> > with Dependent Types (2013)

</div>

---------------------------------------------------------------------------------------------------

## Error as a curse

<p class="fragment">Dream for the last 50 years</p>
<p class="fragment">Common point of view in academic<br />programming language community</p>
<p class="fragment">Mixed success in practice</p>

***************************************************************************************************

## Errors as progress

<br />
<img src="images/hero2.png" />

---------------------------------------------------------------------------------------------------

<img src="images/noproof.png" />

---------------------------------------------------------------------------------------------------

## Engineering approach

Solid engineering practices are often good enough.

Testing software is one such practice.

<ul>
 <li class="fragment"><b>Tests</b> to rule out errors</li>
 <li class="fragment"><b>Tests</b> to rule out regression errors</li>
 <li class="fragment"><b>Tests</b> as a specification</li>
</ul>

---------------------------------------------------------------------------------------------------

## Test-Driven Development (TDD)

> [In TDD] we drive development with automated tests (...). We
> 
> 1. write new code only if an automated test has failed
> 2. eliminate duplication. 
>
> These are two simple rules, but they generate complex  
> individual and group behavior (...).
>
> > Kent Beck, Test-Driven  
> > Development by Example (2003)

---------------------------------------------------------------------------------------------------

## Test-Driven Development (TDD)

1. Introduce controlled isolated miscomputation
2. Eliminate miscomputation by writing more code

<p class="fragment">TDD incorporates miscomputation<br />
as a part of the development cycle!</p>

<p class="fragment">Tests serve as specification and documentation</p>

***************************************************************************************************

## Errors as the unavoidable

<br />
<img src="images/hero3.png" />

---------------------------------------------------------------------------------------------------

## Erlang programming langauge

Created by Ericsson for telecomunications

Distributed long-running reliable systems

---------------------------------------------------------------------------------------------------

## Miscomputations in Erlang

> - **exceptions** occur when the run-time does not know what to do. 
> - **errors** occur when the programmer doesn’t know what to do. 
>
> > Joe Armstrong, Programming reliable systems (2003)

<p class="fragment" style="padding-top:35px">Errors are expected. Specification does not cover all cases.</p>

---------------------------------------------------------------------------------------------------

## Handling errors in Erlang

> What kind of code must the programmer write when they find an error? 
> The philosophy is let some other process fix the error, but what 
> does this mean for their code? The answer is **let it crash**.
>
> > Joe Armstrong, Programming reliable systems (2003)

---------------------------------------------------------------------------------------------------

## Errors as the unavoidable

Miscomputation is a normal part of execution.

<p class="fragment">(Should we still call it miscomputation?)</p>

***************************************************************************************************

## Errors as an inspiration

<br />
<img src="images/hero4.png" />

---------------------------------------------------------------------------------------------------

## Smalltalk ecosystem (1970s)

> [Smalltalk approach] to the design of programming languages [is] 
> quite different from what was familiar in the Algol [programme]. 
>
> Programming was not thought of as the task of constructing a 
> linguistic entity, but rather as a process of working interactively with the 
> semantic representation of the program, using text as one possible interface. 
>
> > Mark Priestley, Science of Operations (2011)

---------------------------------------------------------------------------------------------------

# Computation as interaction

Live coding environments for performing music

<br />

<div class="fragment">

> In musical genres that are not notated so closely (...), there are no wrong notes – 
> only notes that are more or less appropriate to the performance. 
>
> > Alan Blackwell and Nick Collins,
> > The Programming Language as a Musical Instrument (2005

</div>

---------------------------------------------------------------------------------------------------

# Miscomputation in live coding

> [Live coders] may well prefer to accept the results of an imperfect execution. 
> [They] might perhaps compensate for an unexpected result by manual intervention,
> or even accept the result as a serendipitous alternative to the original note. 
>
> > Alan Blackwell and Nick Collins,
> > The Programming Language as a Musical Instrument (2005)

---------------------------------------------------------------------------------------------------

## Errors as an inspiration

<p class="fragment">Make miscomputations more apparent.</p>

<p class="fragment">Enable quick human intervention.</p>

<p class="fragment">Not limited to live coded art performances!</p>

***************************************************************************************************

## Summary

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Summary

<div class="fragment">

**Not all miscomputation is bad**

(Is it still a miscomputation when it's expected?)

</div><div class="fragment" style="padding-top:20px">

**Different research programmes**

(No approach is better in general)

</div>
<br />

[http://tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)
