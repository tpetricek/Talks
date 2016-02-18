- title : History and Philosophy of Types
- description : What are types? We can try to answer this question with a formal definition. 
   But there are numerous incompatible definitions and they fail to capture important aspects 
   of what types actually are - how are they used in practice, how we talk about them and how 
   we think about them. Why we often cannot even find a common language when talking about types? 
- author : Tomas Petricek
- theme : white
- transition : none

***************************************************************************************************

# History and Philosophy of Types

<div style="margin:40px 0px 40px 0px">
<img src="images/russell.jpg" style="height:300px" />
</div>

**Tomas Petricek**, University of Cambridge  
[http://tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

$$$
\definecolor{mc}{RGB}{0,32,172}
\definecolor{cc}{RGB}{172,0,32}

---------------------------------------------------------------------------------------------------

<div class="fragment">

# What is a _type_?

<br />
</div><div class="fragment">

# What are types _for_?

</div>

---------------------------------------------------------------------------------------------------

## Uses of types and type systems

1.	Detecting errors via type-checking
2.	Support for structuring large systems
3.	Documentation
4.	Efﬁciency
5.	Whole-language safety

<div class="reference">

Source: [Lecture notes from Types lecture](http://www.cl.cam.ac.uk/teaching/1314/Types/)

</div>

***************************************************************************************************

## History of types

### Archeology of computer science thought

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

### Theory of types and $\lambda$-calculus

<img src="images/russell.jpg" style="float:right; width:200px;margin-top:40px;" />
<div style="margin-right:300px">

<div class="fragment">

Bertrand Russell, 1908

> Class of all classes that do not contain themselves as elements

</div>
<div class="fragment">

Alonzo Church, 1940

$\lambda$-calculus and types as foundations of mathematics
(not programming!)

</div></div>

---------------------------------------------------------------------------------------------------

### Mathematical logic as based on the theory of types

<img src="images/russell.jpg" style="float:right; width:200px" />
<div style="margin-right:300px">

Base formulas have type $0$

Formula that uses a formula of <br />
type $n$ has a type $n+1$.

<div class="fragment" style="padding-top:20px;">

> We must interpret “I am lying” as “There is a proposition of order 
> $n$ which I affirm and which is false”. This is a proposition of order $n+1$.

</div>
</div>

---------------------------------------------------------------------------------------------------

### Meaning of types in logic

> It is unnecessary, in practice, to know what objects belong to the lowest type (...). 
> In practice, only the relative types of variables are relevant.

<div class="fragment">

> We refrain from making more definite the nature of the types $\omicron$ and $\iota$, 
> (...) the formal theory admits a variety of interpretations.

</div>
<div class="fragment" style="padding-top:20px;">

Types are not sets of values!

</div>

---------------------------------------------------------------------------------------------------

### Types appear in Algol 58 Zurich meeting

A data symbol falls in one of the following classes:<br />
a) Integer b) Boolean c) General

<div class="fragment" style="margin-top:20px">

> It is also remarkable that (...) there is no clue that in this process
> the technical term “type” from mathematical logic had any role.

<div class="reference">

Source: [Several types of types in programming languages, Simone Martini](http://arxiv.org/abs/1510.03726)

</div>
</div>


---------------------------------------------------------------------------------------------------

### From expression types to computation types

<img src="images/hoare.jpg" style="float:right; width:200px" />
<div style="margin-right:300px">

John McCarthy, Tony Hoare, 1970s

<div class="fragment" style="padding-top:20px;">

> McCarthy’s theory was developed by Hoare, who proposed that 
> data types in PLs   could be understood as sets of data values.

</div>
<div class="reference fragment">

Source: [Science of Operations, Mark Priestley](http://www.springer.com/us/book/9781848825543)

</div>
</div>

---------------------------------------------------------------------------------------------------

### From expression types to computation types

$r \leftarrow s$

---------------------------------------------------------------------------------------------------

### From expression types to computation types

${\color{cc} r:{ref}_\rho, s:{ref}_\sigma} \vdash r \leftarrow s : \color{mc} unit~\&~\{ write~\rho, read~\sigma \} $

---------------------------------------------------------------------------------------------------

### From expression types to computation types

What is the set of a type: $\color{mc} unit~\&~\{ write~\rho, read~\sigma \}$?

<div class="reference fragment">

<h3 style="margin:50px 0px 20px 0px">Types are not sets but relations!</h3>

> Express meaning of high-level types as relational,  <br />
> extensional constraints on the behaviour of compiled code

Source: [Nick Benton: What we talk about when we talk about types](http://research.microsoft.com/en-us/um/people/nick)

</div>

---------------------------------------------------------------------------------------------------

## Types in modern programming

- **Imperative** - Effects and monads
- **Unsound** - Dart, TypeScript
- **Super sound** - Idris, Agda, F*
- **Relatively sound** - F# type providers


<div class="fragment">

We are not getting closer to one right definition!

</div>

***************************************************************************************************

## Philosophy of types

### Against a formal universal definition

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Francis Bacon and scientific method

<img src="images/bacon.jpg" style="float:right;margin-top:50px; width:200px" />
<div style="margin-right:220px" class="fragment">

Idols of the tribe

> The human Intellect, (...), easily supposes a 
> greater order and equality in things than it actually finds.

</div>
<div style="margin-right:220px" class="fragment">

Idols of the market

> The confusions of language: one and the same name being applied 
> indifferently to things that are not of the same nature. 

</div>

---------------------------------------------------------------------------------------------------

## Feyerabend's epistemological anarchism

<img src="images/feyerabend.jpg" style="float:right; width:200px" />
<div style="margin-right:300px" class="fragment">

Should there be a clear definition of type?

</div>
<div style="margin-right:300px" class="fragment">

> To 'clarify' the terms does not mean to study the additional properties of the domain in question,
> it means to fill them with existing notions from the entirely different domain of logic.

</div>

---------------------------------------------------------------------------------------------------

## Lakatos and research programmes

<img src="images/lakatos.jpg" style="float:right; width:200px" />
<div style="margin-right:300px" class="fragment">

Science consists of multiple competing mutually inconsistent research programmes.

</div>
<div style="margin-right:300px" class="fragment">

**Hard core** and protective <br/> belt of **auxiliary assumptions**

</div>
<div style="margin-right:300px" class="fragment">

> [Core assumptions] are not to be blamed for any apparent failure. 
> Rather, the blame is to be placed on the less fundamental components.

</div>

---------------------------------------------------------------------------------------------------

## Lakatos and concept stretching

<img src="images/lakatos.jpg" style="float:right; width:200px" />
<div style="margin-right:300px" class="fragment">

A new counterexample of a previously inconceivable form is discovered

</div>
<div style="margin-right:300px" class="fragment">

> In their critical zeal [the refutationists] stretched 
> the concept of polyhedron, to cover objects that were alien to the intended interpretation.

</div>
<div style="margin-right:300px" class="fragment">

We do not notice it & we cannot unthink it!

</div>

---------------------------------------------------------------------------------------------------

## Concept stretching and functions

<img src="images/hermite.jpg" style="float:right; width:200px" />
<div style="margin-right:300px" class="fragment">

<br />

> I turn aside with a shudder of horror from<br />
> this lamentable plague of functions<br />
> which have no derivatives.
>
> Charles Hermite, 1893

</div>

---------------------------------------------------------------------------------------------------

## Concept stretching and <span style="color:#a02030">types</span>

<img src="images/hermite.jpg" style="float:right; width:200px" />
<div style="margin-right:300px">

<br />

> I turn aside with a shudder of horror from<br />
> this lamentable plague of <span style="color:#a02030">type systems</span><br />
> which have no <span style="color:#a02030">soundness proof</span>.
>
> <p><span style="color:#a02030">Anonymous on Hacker News, 2015</span></p>

</div>

---------------------------------------------------------------------------------------------------

## Feyerabend's epistemological anarchism

<img src="images/feyerabend.jpg" style="float:right; width:200px" />
<div style="margin-right:300px" class="fragment">

What is the correct scientific method?

</div>
<div style="margin-right:300px" class="fragment">

> To those who look at the rich material provided by history, it will become clear that there 
> is only one principle that can be defended under all circumstances. It is the principle: anything goes.

</div>

---------------------------------------------------------------------------------------------------

## Living without clear notion of 'type'

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Living without clear notion of 'type'

<div class="fragment">

#### Meaning is use

</div><div class="fragment">

How we use types in language?

Construct interesting contexts for types!

</div><div class="fragment">

<div class="fragment">

#### Types as scientific entities

</div><div class="fragment">

What can we cause with types?

We can experiment without having theory!

</div>

***************************************************************************************************

## Summary

### There is one principle: Anything goes

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Summary

> Science is much more ‘sloppy’ and ‘irrational’ <br />
> than its methodological image.

<span></span>

> Precise answers “hamper the growth of knowledge” and <br />
> “deflect the course of investigation into narrow <br />
> channels of things already understood”.

<div class="reference">

Paul Feyerabend, Against Method<br />
Imre Lakatos, Proofs and Refutations

</div>

---------------------------------------------------------------------------------------------------

## Where to find more?

What can programming language research learn from the philosophy of science?<br />
[tomasp.net/blog/2014/philosophy-pl](http://tomasp.net/blog/2014/philosophy-pl)

Against the definition of types<br />
[tomasp.net/blog/2015/against-types](http://tomasp.net/blog/2015/against-types)

Philosophy of science books every computer scientist should read<br />
[tomasp.net/blog/2015/reading-list](http://tomasp.net/blog/2015/reading-list)

<br />

[http://tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)
