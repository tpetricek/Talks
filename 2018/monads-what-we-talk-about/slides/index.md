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

# What we talk about<br />when we talk about monads

<div style="margin:0px 0px 0px 0px">
<img src="images/monads.jpg" style="height:350px" />
</div>

 <span style="font-weight:bold">Tomas Petricek</span>, The Alan Turing Institute <br />
[tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

$$$
\definecolor{mc}{RGB}{137,64,96}
\newcommand{\mbnd}{>\!\hspace{-0.25em}>\!\hspace{-0.27em}=}

----------------------------------------------------------------------------------------------------

> A monad is just a monoid in the category<br /> 
> of endofunctors. What is the problem?

----------------------------------------------------------------------------------------------------

### What is a monad?

<div class="fragment" style="margin-top:-50px">

### _I will not give you an answer!_

</div>

----------------------------------------------------------------------------------------------------

### What do we say when we talk about monads?

<div class="fragment" style="margin-top:-50px">

### **I will add philosophical and cognitive context!**

</div>

****************************************************************************************************

# Internal history of monads

From category theory to programming

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

### Monads in category theory

> A _monad_ over a category $\mathcal{C}$ is a triple **$(T, \eta, \mu)$**
> where<br /> **$T : \mathcal{C} \rightarrow \mathcal{C}~$** is a functor, **$\eta : {Id}_{\mathcal{C}} \rightarrow T$** and<br />
> **$\mu : T^2 \rightarrow T$** are natural transformations such that:
> 
> $$$
> \begin{array}{l}
> \mu_{A} \circ T \mu_A = \mu_{A} \circ \mu_{T A} \\
> \mu_{A} \circ \eta_{T A} = \mathit{id}_{T A} = \mu_{A} \circ T \eta_{A}
> \end{array}

----------------------------------------------------------------------------------------------------

<h3 style="margin-bottom:10px"> Monads in programming</h3>

> A _monad_ is a triple **$(M, \mathit{unit}, \mbnd)$** consisting of a type<br /> 
> constructor **$M$** and two operations of the following types:
> 
> $$$
> \begin{array}{lcl}
> \mbnd &::& M x \rightarrow (x\rightarrow M y) \rightarrow M y\\ 
> \mathit{unit} &::& x\rightarrow M x\\
> \end{array}
> 
> These operations must satisfy the following laws:<br />
> 
> $$$
> \begin{array}{l}
> \mathit{unit}~a~\mbnd~f ~=~ f a\\
> m~\mbnd~\mathit{unit} ~=~ m\\
> (m~\mbnd~f)~\mbnd~g ~=~ m~\mbnd~(\lambda x.f x~\mbnd~g)
> \end{array}

----------------------------------------------------------------------------------------------------

## What has changed?

<div class="fragment">

#### **Purpose of monads**
Reasoning about effects _vs._ Introducing effects

</div><div class="fragment">
<br />

#### **Kind of entity**
A priori knowledge _vs._ A posteriori knowledge


</div>

****************************************************************************************************

# Explaining monads

Understanding monads using metaphors

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

# Monads as a formal entity

#### _Monad is a data type_

<div class="fragment">
<br />

> Data type `M a` with the two operations satisfying **monad laws**:
>
> Operation `return` has a type `a -> M a`<br />
> Operation `>>=` has a type `(a -> M b) -> (M a -> M b)`

<br />
<br />
<br />
</div>

----------------------------------------------------------------------------------------------------

# Monads as containers

#### _Monad is like a box of things_

<img src="images/box-1.png" style="width:90%;margin:0px;margin-top:20px" class="fragment"/>
<img src="images/box-2.png" style="width:90%;margin:0px" class="fragment"/>
<img src="images/box-3.png" style="width:90%;margin:0px" class="fragment"/>
<img src="images/box-4.png" style="width:90%;margin:0px" class="fragment"/>

----------------------------------------------------------------------------------------------------

# Monads as computations

#### _Monad is like a railway track_

<img src="images/rail-0.png" style="width:90%;margin:0px;margin-top:50px" class="fragment"/>
<br />
<br />

----------------------------------------------------------------------------------------------------

# Monads as computations

#### _Monad is like a railway track_

<img src="images/rail-1.png" style="width:90%;margin:0px;margin-top:50px" />
<br />
<br />

----------------------------------------------------------------------------------------------------

# Monads as computations

#### _Monad is like a railway track_

<img src="images/rail-2.png" style="width:90%;margin:0px;margin-top:50px" />
<br />
<br />

----------------------------------------------------------------------------------------------------

# Monads as computations

#### _Monad is like a railway track_

<img src="images/rail-3.png" style="width:90%;margin:0px;margin-top:50px" />
<br />
<br />

----------------------------------------------------------------------------------------------------

## Why metaphors matter

<div class="fragment">

> One of the principal results in cognitive science is that abstract concepts are typically
> understood, via metaphor, in terms of more concrete concepts.
>
> <p style="float:right">(Lakoff & Núñez, 2000)</p>

</div>

----------------------------------------------------------------------------------------------------

## Embodied cognition

Metaphors link _abstract concepts_ with _bodily experience_

 - **Movement** formal symbol manipulation
 - **Inside vs. outside** for containers and boxes
 - **Movement** for composing railway tracks
 
****************************************************************************************************

# Monads in research

Reasoning about programs with monads

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

> This paper is about logics for reasoning about programs,<br />
> in particular for proving equivalence of programs.
>
> <p style="text-align:right;width:100%">Moggi (1991)</p>

----------------------------------------------------------------------------------------------------

# Origins of algebraic program laws

#### **Intuition about programming constructs**

<br />

${\color{mc} \mathit{if}}~\,b~{\color{mc} \mathit{then}}~\,p~{\color{mc} \mathit{else}}~p = p$

----------------------------------------------------------------------------------------------------

# Origins of monad laws

#### **Composition of morphisms in category theory**

<br />

$(f^\ast \circ g^\ast) \circ h = f^\ast \circ (g^\ast \circ h)$

$\mathit{unit}^\ast \circ f = f = f^\ast \circ \mathit{unit}$

----------------------------------------------------------------------------------------------------

## Reasoning about programs with monads

 - Monadic query comprehensions _(Grust, 2004)_
 - Monad laws + concrete monads _(Gibbons, Hinze, 2011)_
 - Refactoring using monad laws _(To appear... never?)_
 
----------------------------------------------------------------------------------------------------

## Algol research paradigm

> One of the goals of the Algol programme was to utilize the resources of 
> logic to increase the confidence (...) in the correctness of a program.
>
> <p style="float:right">(Priestley, 2011)</p>

****************************************************************************************************

# Monads in programming

From abstractions to syntactic sugar

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

### Code reuse via monadic abstraction

    [lang=haskell]
    mapM :: Monad m => (a -> m b) -> [a] -> m [b]        

<div style="margin-top:-20px"></div>
    
    [lang=haskell]
    mapM f x = loop x []
      where loop [] acc = return (reverse acc)
            loop (x:xs) acc = f x >>= \y. loop xs (y:acc)

<br /><br />

----------------------------------------------------------------------------------------------------

### Sequencing of effects with monads

    [lang=haskell]
    main :: IO ()                

<div style="margin-top:-20px"></div>
    
    [lang=haskell]
    main = do
      putStr "What is your name?"
      n <- readLn
      putStr ("Hello " ++ n)

<br /><br />

----------------------------------------------------------------------------------------------------


### Non-standard computations

    let getLength url = async {
      try
        let! html = downloadAsync url
        return html.Length
      with e ->
        return 0 }

<br /><br />

----------------------------------------------------------------------------------------------------

### Useful syntactic sugar

    [lang=haskell]
    sayHello :: String -> Html 

<div style="margin-top:-20px"></div>
    
    [lang=haskell]
    sayHello name = H.body $ do
      H.header "Welcome"
      H.p ("Hello " ++ name)
      
<br /><br />

----------------------------------------------------------------------------------------------------

## How programming concepts evolve

<div class="fragment">

> Mathematics does not grow through **increase of the number of established theorems**, 
> but through _improvement_ by speculation and criticism, by the method of _proofs and 
> refutations_. 
>
> <p style="text-align:right;width:100%">Lakatos (1979)</p>

</div>

****************************************************************************************************

# How monads evolve

The nature of programming entities

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

## The nature of programming entities

#### _Metaphorical level_

<div style="margin:-15px 0px 20px 0px">

**Intuitively understanding** concepts

</div>

#### _Technical level_

<div style="margin:-15px 0px 20px 0px">

**Implementing things** in programs

</div>

#### _Formal level_

<div style="margin:-15px 0px 20px 0px">

**Reasoning and proving** about programs

</div>

----------------------------------------------------------------------------------------------------

## Shifts and adaptations

**Motivation at formal level**<br />
Monads are logic for _reasoning about effects_

**Used differently for implementation**<br />
Language abstraction for _encoding effects_

**Shift at implementation level**<br />
Abstraction and _notation_ for effects

**Causes adaptation at metaphorical level**<br />
Think of monads as _railway tracks_

****************************************************************************************************

# Sociology of monads

Monads as religious objects

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

<img src="images/shirt.jpg" style="width:60%" />

****************************************************************************************************

# Uses of monads

A case for wider understanding

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

### When monads are not the right tool

Monad is a _resource of logic_

<div class="fragment">

Monads are _cool and exciting_

</div><div class="fragment">

Monads are _discovered, not invented!_

</div>

----------------------------------------------------------------------------------------------------

### _Monad_ can be the uninteresting part

The `Par` monad for modelling parallel computations

<div class="fragment lispace">

 - Spawn a new process<br />`spawn : Par a -> Par (IVar a)`
 - Read and write shared variables<br />`get : IVar a -> Par a` and `put : IVar a -> a -> Par ()`

</div><div class="fragment" style="padding-top:30px">

Also supports monadic `return : a -> Par a`<br />
and `>>= : (a -> Par b) -> (Par a -> Par b)`

</div>

----------------------------------------------------------------------------------------------------

### _Monad_ can be the uninteresting part

<img src="images/par.png" style="width:80%;margin-top:-60px"/>

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
> The techniques [do not] extend to monadic parsers. [T]he monadic 
> formu-lation [causes] the evaluation of the parser construction over and over (...).
>
> <p style="text-align:right;width:100%;margin-top:60px">Swierstra & Duponcheel (1996)</p>

---------------------------------------------------------------------------------------------------

### _Monad_ as the wrong structure

> Wadge proposed that the semantics of the dataflow language Lucid (...), could be structured
> by a monad. 
> 
> Ten years later, Uustalu and Vene gave a semantics for Lucid in
> terms of a comonad, and stated that "dataflow cannot be structured with
> monads".
>
> <p style="text-align:right;width:100%;margin-top:60px">Orchard (2012)</p>


----------------------------------------------------------------------------------------------------

### _Monad_ as the wrong structure

 - Has the **right type** of join operation
 - Does not **provide plumbing**

<br/><br/>

    [lang=haskell]
    latest : Stream (Stream a) -> Stream a

****************************************************************************************************

# Conclusions

What we talk about when we talk about monads

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

### What we talk about when we talk about monads

Strong roots in the _Algol paradigm_

**Meaning evolves** at three levels

_Metaphors_ are a fundamental part

<br />
<br />
<br />

Tomas Petricek, The Alan Turing Institute<br />
[tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

