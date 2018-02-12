- title : The inner life of programming concepts
- description : Mathematical and scientific entities such as
    polyhedra and electrons are well studied by philosophers.
    In theoretical programming language research and practical
    software development, a similarly fundamental role is
    played by programming concepts such as monads, types
    and processes, which are much less well-explored.
    Like computer programs, programming concepts
    have a dual nature and exist as an abstract form and as a
    physical form. In this paper, we explore the rich structure,
    or inner life, of the abstract form of programming concepts
    that is used when reasoning about programs, constructing
    programs and proving program properties.
    Programming concepts exist at a number of levels:
    an intuitive metaphorical level, a mathematical level used
    for formal reasoning and an implementation level. In this
    paper, we contrast each level with a different approach to
    thinking about mathematical and scientific entities.
- author : Tomas Petricek
- theme : white
- transition : none

****************************************************************************************************

# The inner life of programming concepts

<div style="margin:80px 0px 80px 0px">
<img src="images/engine.jpg" style="height:250px" />
</div>

**Tomas Petricek**, The Alan Turing Institute <br />
[tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

****************************************************************************************************

# Motivation

What is this talk about?

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------
 
## <strong class="fragment">»type«</strong>

## <em class="fragment">»object«</em> <span style="margin-right:150px"></span>  <em class="fragment">»function«</em>

## <strong class="fragment">»monad«</strong>

---------------------------------------------------------------------------------------------------

# Programming concepts

 - Where do they _come from_?
 - How do **they evolve**?
 - How are they _used in practice_?
 - What is **their nature**?
 
****************************************************************************************************

# Programming concepts

Multi-level nature of programming concepts

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

### _1. Metaphorical common sense_

### 2. Formal models

### **3. Computer implementation**

---------------------------------------------------------------------------------------------------

<style>.examples h3 {margin-bottom:10px}</style>
<table class="examples"><tr>
<td style="padding-right:20px">

### Types

 * _category or kind_
 * sets or relations
 * **ML or Java types**

</td><td class="fragment">

### Monads

 * _box or sequencing_
 * category theory
 * **Haskell or LINQ**

</td><td style="padding-left:20px" class="fragment">

### Functions

 * _reusable block_
 * math function
 * **FORTRAN or ML**

</td></tr></table>

---------------------------------------------------------------------------------------------------

## Multi-level nature of concepts

#### **All have three levels**
We always think, prove and implement!

<hr />

#### _One level sometimes dominates_
Common sense types, formal monads, implementation functions

****************************************************************************************************

# Programming concepts as entities

Learning from mathematical and scientific entities

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Realism and anti-realism debate

#### **More flexible than electrons**
Paper on gravity does not change how apples fall

<hr />

#### _Not just abstraction_
Types across languages don't share the abstract


---------------------------------------------------------------------------------------------------

## Experimentalist view

#### _Causing effects_
Types are used to provide auto-complete

<hr />

#### **Scientific progress**
We accumulate what we can implement

---------------------------------------------------------------------------------------------------

## Learning from mathematical entities


#### _How programming concepts evolve_
Proofs and refutations (Imre Lakatos)

<hr />

#### **Where programming concepts come from**
Conceptual metaphors (George Lakoff & Rafael Núñez)


****************************************************************************************************

# Monads as mathematical entities

Variations on proofs and refutations

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

#### _Mathematical layer_

Monad is a monoid in the category of endofunctors

<hr />

<div class="fragment">

#### **Implementation layer**
Data type `M a` satisfying monad laws with operations:

 * `return` of type `a -> M a`
 * `>>=` of type `(a -> M b) -> (M a -> M b)`

</div>

---------------------------------------------------------------------------------------------------

### **Metaphorical layer:** Monads as boxes

<div class="fragment">

`return` wraps thing in a _box_

<img src="images/return.png" />

</div>
<div class="fragment">

`>>=` applies operation on all things in a box

<img src="images/bind1.png" class="fragment" style="margin-right:15px;" />
<img src="images/bind2.png" class="fragment" style="margin-left:15px;" />
</div>

---------------------------------------------------------------------------------------------------

### **Metaphorical layer:** Monads as computations

Plumbing for composing computations with _side-effects_

<img src="images/compose1.png" style="width:30%;opacity:0" />
<img src="images/compose2.png" style="width:30%;opacity:0" />
<img src="images/compose3.png" style="width:30%;opacity:0" />

---------------------------------------------------------------------------------------------------

### **Metaphorical layer:** Monads as computations

Plumbing for composing computations with _side-effects_

<img src="images/compose1.png" style="width:30%;opacity:1" />
<img src="images/compose2.png" style="width:30%;opacity:0" />
<img src="images/compose3.png" style="width:30%;opacity:0" />

---------------------------------------------------------------------------------------------------

### **Metaphorical layer:** Monads as computations

Plumbing for composing computations with _side-effects_

<img src="images/compose1.png" style="width:30%;opacity:0.3" />
<img src="images/compose2.png" style="width:30%;opacity:1" />
<img src="images/compose3.png" style="width:30%;opacity:0" />

---------------------------------------------------------------------------------------------------

### **Metaphorical layer:** Monads as computations

Plumbing for composing computations with _side-effects_

<img src="images/compose1.png" style="width:30%;opacity:0.3" />
<img src="images/compose2.png" style="width:30%;opacity:0.3" />
<img src="images/compose3.png" style="width:30%;opacity:1" />

---------------------------------------------------------------------------------------------------

## Monad at multiple levels

#### _Formal level_
Pre-sheaves in category theory

#### **Metaphorical layer**
Containers like lists and sequencing of effects

#### _Implementation layer_
Artifacts such as do notation and LINQ
 
---------------------------------------------------------------------------------------------------

## Shifts and adaptations

**Motivation at formal level**<br />
Monads are logic for _reasoning about effects_

**Used differently for implementation**<br />
Language abstraction for _encoding effects_

**Shift at implementation level**<br />
Abstraction and _notation_ for effects

**Causes adaptation at formal level**<br />
Algebraic _reasoning about syntactic_ structures

****************************************************************************************************

# Types, functions and more

Variations on proofs and refutations

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />


---------------------------------------------------------------------------------------------------

## Types in programming languages

#### _Formal layer_
Types as sets, types as relations, types as proofs

#### **Implementation layer**
Types for error checking, assisting developers

#### _Metaphorical layer_
Category of a value, property specification

---------------------------------------------------------------------------------------------------

## How the concept of a type evolves

**Rebirth at the implementation layer**<br/>
No clue that "type" from logic had role in early Algol

_Modelled at the formal layer_<br />
Type as a set of things (except for pointers)

**Shifts at the application layer**<br />
Types used for effect tracking, tooling, proving

_Adaptation at the formal layer_<br /> 
Types as relations, types as proofs

---------------------------------------------------------------------------------------------------

## Functions in programming languages

#### **Implementation layer**
Gluing tapes, compiling sub-routines

#### _Formal layer_
Mapping from inputs to outputs

#### **Metaphorical layer**
Mathematical lookup tables, math functions 

---------------------------------------------------------------------------------------------------

## How the concept of a function evolves

**Formal and implementation appearance**<br/>
Mathematics vs. gluing sequences of instructions

_Getting closer at implementation layer_<br />
Functions and procedures in Pascal

**Implementation and metaphorical shifts**<br />
Sending a message to an object in Smalltalk

_Adaptation at the formal layer_<br /> 
Unit-returning function with side-effects

****************************************************************************************************

# Summary

Why understanding programming concepts matters

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Computer science method questions

 - Metaphors appears _only in textbooks_
 - How to discuss **conceptual metaphors**
 - What to study about _implementation level_

---------------------------------------------------------------------------------------------------

## History and philosophy questions

 - Origins of _programming concepts_
 - Link between **formal and implementation**
 - _Proofs and refutations_ like developments

---------------------------------------------------------------------------------------------------

## Shifts and adaptations?

 - _Shift_ at one layer, followed by _adaptation_ at another
 - **Joining** two concepts, **splitting** one concept
 - _Reversal_ of a shift caused by another layer
 
---------------------------------------------------------------------------------------------------


## The inner life of programming concepts

_Multi-level nature_<br />
Conceptual, formal, implementation

**Useful philosophy of science perspective**<br/>
Where programs come from, scientific progress

_Variations on proofs and refutations_<br />
Evolve across all three levels

<br />

[tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)



****************************************************************************************************

#### BACKUP SLIDES

---------------------------------------------------------------------------------------------------

## Concepts as technical artifacts

#### Dual nature of technical artifacts
**Function or specification** vs. _physical implementation_

<hr />

#### Function of programming concepts
_Changes and evolves_  and **stretched by implementations**

---------------------------------------------------------------------------------------------------

## Physical reality

**Hard to cover all philosophy of science positions!**

<hr />

_Scientific entities_<br />
There is some independent physical reality

_Programming concepts_<br />
The physical is interlinked with the theoretical
