- title : Monads are not what they seem
- description : The notion of monad (in the mathematical sense) and its use in functional 
    programming has popularized category theory (a branch of abstract algebra) in programming 
    community. Many programmers believe that they now need to understand category theory in order 
    to become better functional programmers. But how do computer scientists and programmers 
    actually use the monad structure in their research narratives and in practice? 
    In this talk, I will briefly introduce monads and then discuss the different metaphors 
    that programmers use when explaining monads. I will look at the kinds of questions that 
    researchers attempt to answer with monads, but also what questions become valid (and 
    ill-formed) once we start looking at programming through the perspective of category theory. 
    Monads are an interesting topic not just because of their popularity, but because they serve 
    as a prime example of abstraction in programming - and can illustrate the strengths and 
    weaknesses of the theoretical programming language research paradigm.
- author : Tomas Petricek
- theme : white
- transition : none

****************************************************************************************************

# Monads are not what they seem

<div style="margin:40px 0px 40px 0px">
<img src="images/monads.jpg" style="height:350px" />
</div>

**Tomas Petricek**, The Alan Turing Institute <br />
[tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

****************************************************************************************************

# Monad metaphors

How monads are taught

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

$$$
\definecolor{mc}{RGB}{32,64,172}

---------------------------------------------------------------------------------------------------

### Formal _category theory_ definition

A _monad_ is a functor ${\color{mc} M} : C \rightarrow C$ together with _mappings_:

 - $\eta_\alpha : \alpha \rightarrow {\color{mc} M}\alpha $
 - $(-)^*$ turns an arrow $f : \alpha \rightarrow {\color{mc} M}\beta$  
    into an arrow $f^* :  {\color{mc} M}\alpha \rightarrow {\color{mc} M}\beta$

The mappings are required to satisfy the three _monad laws_:

 - $\eta_\alpha^* = id_{M\alpha}$
 - $f^* \circ \eta_\alpha = f$ 
 - $f^* \circ g^* = (f^* \circ g)^* $
 
---------------------------------------------------------------------------------------------------

<img src="images/shirt.jpg" style="width:60%" />

---------------------------------------------------------------------------------------------------
    
## _Monads_ as generic types

The structure ${\color{mc}M} \alpha$ represents a _data type_

<div class="fragment">

 * A collection of things - `List int` or `List string`
 * A computation - `Lazy int` or `Lazy float`
 * You can nest them too - `Lazy (List int)`

</div>
 
---------------------------------------------------------------------------------------------------

### _Formally:_ Monads as symbol manipulation

<div class="fragment">

Data type `M a` satisfying _monad laws_ with operations:

 * `return` of type `a -> M a`
 * `>>=` of type `(a -> M b) -> (M a -> M b)`

</div>

---------------------------------------------------------------------------------------------------

### _Containers:_ Monads as boxes

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

### _Containers:_ Monads as burritos

<br />

<img src="images/burrito.jpg" class="fragment" />

---------------------------------------------------------------------------------------------------

### _Sequencing:_ Monads as computations

Plumbing for composing computations with _side-effects_

<img src="images/compose1.png" style="width:30%;opacity:0" />
<img src="images/compose2.png" style="width:30%;opacity:0" />
<img src="images/compose3.png" style="width:30%;opacity:0" />

---------------------------------------------------------------------------------------------------

### _Sequencing:_ Monads as computations

Plumbing for composing computations with _side-effects_

<img src="images/compose1.png" style="width:30%;opacity:1" />
<img src="images/compose2.png" style="width:30%;opacity:0" />
<img src="images/compose3.png" style="width:30%;opacity:0" />

---------------------------------------------------------------------------------------------------

### _Sequencing:_ Monads as computations

Plumbing for composing computations with _side-effects_

<img src="images/compose1.png" style="width:30%;opacity:0.3" />
<img src="images/compose2.png" style="width:30%;opacity:1" />
<img src="images/compose3.png" style="width:30%;opacity:0" />

---------------------------------------------------------------------------------------------------

### _Sequencing:_ Monads as computations

Plumbing for composing computations with _side-effects_

<img src="images/compose1.png" style="width:30%;opacity:0.3" />
<img src="images/compose2.png" style="width:30%;opacity:0.3" />
<img src="images/compose3.png" style="width:30%;opacity:1" />

---------------------------------------------------------------------------------------------------

### _Metaphors_ for understanding monads

Neuroscientist's perspective on mathematical thinking

 - _Movement_ formal symbol manipulation
 - _Inside vs. outside_ for containers and boxes
 - _Movement_ for composing comptuations

****************************************************************************************************

# Monads in practice

How and why people use monads

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

### _Syntax_ for side-effectful comptuations

```
let addr = findAddress "Tomas"
let news = findNews (getCity addr)
return getTop10 news
```

What if `findAddress` and `findNews` can fail?

<div class="fragment">

```
let addr = tryFindAddress "Tomas"
if addr = null then return null
else
  let news = tryFindNews (getCity addr)
  if news = null then return null
  else
    return Success (getTop10 news)
```

</div>

---------------------------------------------------------------------------------------------------

### _Syntax_ for side-effectful comptuations

```
let addr = findAddress "Tomas"
let news = findNews (getCity addr)
return getTop10 news
```

_Monadic notation_ to remove nesting & repetition:

<div style="padding-bottom:60px">

```
maybe {
  let! addr = tryFindAddress "Tomas"
  let! news = tryFindNews (getCity addr)
  return getTop10 news 
}
```

</div>

---------------------------------------------------------------------------------------------------

### _Reasoning_ about monadic code

<div class="fragment">

Get address and compose message with city:

```
maybe {
  let! addr = findAddress "Tomas"
  return "Tomas lives in " + (getCity addr) }
```    

</div>
<div class="fragment">

Does extracting `getHome` function change meaning?

```
let getHome name = maybe {
  let! addr = findAddress mame
  return name + " lives in " + getCity addr }
```

```
maybe {
  let! msg = getHome "Tomas"
  return msg }
```    

</div>

---------------------------------------------------------------------------------------------------

### _Code reuse_ using monad abstraction

<div class="fragment">

Write useful functions that work for _any monad_

 - Transform the thing inside the monad<br />`mapM : (a -> b) -> (M a -> M b)`
 
 - Sum list and aggregate side-effects<br />`sumM : List (M int) -> M int`

</div>

  
****************************************************************************************************

# Misuses of monads

When monads are not the right thing

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

### _Monad_ can be the uninteresting part

The `Par` monad for modelling parallel computations

<div class="fragment">

 - Run things in parallel<br />`parallel : Par a -> Par b -> Par (a * b)`
 - Return the first of two results<br />`choose : Par a -> Par a -> Par a`

</div><div class="fragment" style="padding-top:30px">

Also supports monadic `return : a -> Par a`<br />
and `>>= : (a -> Par b) -> (Par a -> Par b)`

</div>

---------------------------------------------------------------------------------------------------

### _Monad_ as tempting harmful abstraction

`Parser a` reads input string and produces value `a`

<div class="fragment">

 - Parse one thing and then another thing<br />
   `Parser a -> Parser b -> Parser (a * b)`

 - Try parsing in two ways, use the first success<br />
   `Parser a -> Parser a -> Parser a`
   
</div><div class="fragment" style="padding-top:30px">

Parsers can be extended to support monadic `>>=` and `return`.

</div>

---------------------------------------------------------------------------------------------------

### _Monad_ as tempting harmful abstraction 

> The normal disadvantages of conventional [monadic] parsers, <br />such as
> their lack of speed and their poor error reporting are remedied.
>
> The techniques [do not] extend to monad-based parsers. [T]he monadic 
> formulation [causes] the evaluation of the parser construction over and over (...).
>
> <p style="text-align:right;width:100%;margin-top:60px">Deterministic, Error-Correcting
> Combinator Parsers,<br />Swierstra & Duponcheel (1996)</p>

---------------------------------------------------------------------------------------------------

### _Monadic syntax_ without 'true' monads

<div class="fragment">

Notation _originally_ intended just for monads 

Can be used for things that are _not_ monadic

```
H.html {
  H.head {
    H.title "Sample web page"
  }
  H.body {
    H.h1 "Sample web page"
    H.p "This is a sample page..."
  }
}
```

Do we need _syntactic flexibility_ instead of _monads_?

</div>

****************************************************************************************************

# Philosophy of monads

What monads really are

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

### _Concept stretching_ and changing meaning

<div class="fragment">

> Then came the refutationists. In their critical zeal they
> stretched the concept of polyhedron, to cover objects that
> were alien to the intended interpretation. 
>
> <p style="text-align:right;width:100%;margin-top:60px">Proofs and refutations,
> <br />Lakatos (1979)</p>

</div>

---------------------------------------------------------------------------------------------------

### _Concept stretching_ and changing meaning

<ol>
<li>Monads are logic for <em>reasoning about effects</em></li>
<li class="fragment">Language abstraction for <em>encoding effects</em></li>
<li class="fragment"><em>Abstraction and notation</em> for effects</li>
<li class="fragment">Abstraction and <em>notation (not just) for effects</em></li>
</ol>

---------------------------------------------------------------------------------------------------

### Monads rooted in _research paradigm_

> One of the goals of the Algol research programme was to utilize the resources of
> logic to increase the confidence (...) in the correctness of a
> program.
>
> <p style="text-align:right;width:100%;margin-top:60px">Science of Operations,
> <br />Priestley (2011)</p>

---------------------------------------------------------------------------------------------------

### Monads and _research paradigms_

> This paper is about logics for reasoning about programs, <br />
> in particular for proving equivalence of programs.
>
> <p style="text-align:right;width:100%;margin-top:60px">Notions of computations
> and monads,<br />Moggi (1991)</p>

---------------------------------------------------------------------------------------------------

### Monads and _research paradigms_

_Reasoning about programs_ often appears in papers

In practice monads are just _programming tool_

The _cost of abstraction_ is rarely debated

Gentlemen do not talk about _syntax_

---------------------------------------------------------------------------------------------------

### Monads in _theory and practice_

<div class="fragment">

> Before thinking about the philosophy of experiments we
> should record a certain class or caste difference between
> the theorizer and the experimenter. It has little to do with
> philosophy. We find prejudices in favour of theory, as far
> back as there is institutionalized science.    
>
> <p style="text-align:right;width:100%;margin-top:60px">Representing and intervening,
> <br />Hacking (1983)</p>

</div>

---------------------------------------------------------------------------------------------------

### _Anything_ including monads _goes_

> Galileo replaces one natural interpretation by a very different
> and as yet at least partly unnatural interpretation. (...) Galileo
> uses propaganda; he uses psychological tricks, in addition to whatever
> intellectual reasons he has to offer.
>
> <p style="text-align:right;width:100%;margin-top:60px">Against method,
> <br />Feyerabend (1975)</p>

---------------------------------------------------------------------------------------------------

### _Anything_ including monads _goes_

Community finds monads an _attractive topic_

They are _unnatural_ until you _get them_

Popularity means _not all uses are justified_
    
<br /><br />
    
****************************************************************************************************

# Summary

Monads are not what they seem

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Monads are not what they seem

**Metaphors** guide our understanding

**Concepts** often change their meaning

**Paradigms** and **propaganda** matter

<p class="fragment"><em>Applies to other concepts. Bigger picture?</em></p>

<br />

[tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

****************************************************************************************************

### Backup notes

The `Par` monad is a joinad, which captures the more interesting aspects

Also write about dataflow (is it monad or a comonad?)

Monads were supposed to be used for reasoning about effects, but instead
they are used to introduce effects. Where does the "reasoning" meme 
come from? How much it is actually done in practice? (Algol?)
