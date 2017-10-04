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

## <em class="fragment">»object«</em> <span style="margin-right:150px"></span>  <em class="fragment">»lock«</em>

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

### Locks

 * _physical lock_
 * set of names
 * **mutual exclusion**

</td></tr></table>

---------------------------------------------------------------------------------------------------

## Multi-level nature of concepts

#### **All have three levels**
We always think, prove and implement!

<hr />

#### _One level dominates_
Common sense types, formal monads, implementation locks

****************************************************************************************************

# Scientific entities

Programming concepts as scientific entities

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

****************************************************************************************************

# Mathematical entities

Variations on proofs and refutations

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Where programming concepts come from


---------------------------------------------------------------------------------------------------

## How the concept of a monad evolves

_Originates at formal level_<br />
Pre-sheaves in category theory

**Metaphors evolve from applications**<br />
Containers like lists and sequencing of effects

_Implementations drift from theory_<br />
Artifacts such as do notation and LINQ
 
---------------------------------------------------------------------------------------------------

## How the concept of a type evolves

**Background combines all levels**<br />
Type theory, common sense and implementation in Algol

_Formalisms change to reflect use_<br />
Types as sets, types as relations, types as proofs

**Implementations stretches the theory**<br />
Unsound type systems for documentation and auto-complete

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
