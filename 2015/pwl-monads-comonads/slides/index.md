- title : The Origins of Monadic and Comonadic Computations
- description : The Origins of Monadic and Comonadic Computations
- author : Tomas Petricek
- theme : white
- transition : none

***************************************************************************************************

# The origins of Monadic and <br/> Comonadic Computations

<div style="margin:40px 0px 40px 0px">
<a href="http://www.cs.cmu.edu/afs/cs/user/crary/www/819-f09/Moggi91.pdf">
<img title="E. Moggi. Notions of computation and monads" src="images/monads.png" style="height:300px" />
</a>
&nbsp;&nbsp;
<a href="http://www.ioc.ee/~tarmo/papers/cmcs08.pdf">
<img title="T. Uustalu, V. Vene. Comonadic Notions of Computation" src="images/comonads.png" style="height:300px" />
</a>
</div>

**Tomas Petricek**, University of Cambridge  
[http://tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

---------------------------------------------------------------------------------------------------

## The deal with category theory

Just a collection of mathematical structures!

 - Very general and very rich...
 - But you don't always need category theory

---------------------------------------------------------------------------------------------------

![I don't always use mathematics](images/meme.jpg)

---------------------------------------------------------------------------------------------------

## Algebra and program design

A monoid $(S, \bullet, \epsilon)$ is a set $S$ with an element $\epsilon \in S$  
and an operation $\bullet : S\times S \rightarrow S$ satisfying:

 - **Unit element**  
   $\forall a \in S$ it holds that $a \bullet \epsilon = a = \epsilon \bullet a$

 - **Associativity**  
   $\forall a, b, c \in S$ it holds that $(a \bullet b) \bullet c = a \bullet (b \bullet c)$

---------------------------------------------------------------------------------------------------

## Monoids and programming

<div class="fragment">

### Standard - well-known structures

 - Numbers ($+$ with zero, $\ast$ with one)
 - Lists (concatenation, empty list)

</div><div class="fragment">

### Fancier - dictionaries with word counts

$$$
\forall w . (d_1 \bullet d_2)[w] = d_1[w] + d_2[w] \\
\forall w . \epsilon[w] = 0

</div>

---------------------------------------------------------------------------------------------------

## Why are monoids useful?

You get useful properties for free!<br />
Say you want to calculate:

$$$
r = a_1 \bullet \ldots \bullet a_n

<div class="fragment">

Monoids guarantee that $r = r_1 + r_2$ where

$$$
r_1 = \epsilon \bullet a_1 \bullet \ldots \bullet a_{n/2} \\
r_2 = \epsilon \bullet a_{n/2+1} \bullet \ldots \bullet a_{n}

</div><div class="fragment">

We just invented word count with **map-reduce**!

</div>

***************************************************************************************************

## Semantics and category theory

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Monads, comonads and category theory

> This paper is about logics for reasoning about programs,
> in particular for proving equivalence of programs.

This is not about programming, but about program semantics!

 - What programs are equivalent?
 - What does a program actually denote?
 - Do some useful program properties hold?

---------------------------------------------------------------------------------------------------

## Semantics of programming languages

### Operational semantics

Reduction relation: $e_1 \rightarrow e_2$

### Denotational semantics

Interpretation of terms: $[\![ e ]\!] = \,?$

---------------------------------------------------------------------------------------------------

## Category = objects + arrows

A category $C$ consists of objects $obj(C)$ and arrows $arr(C)$ between objects, i.e.
$f : A \rightarrow B$ where $A, B \in obj(C)$ with:

 - **Arrow composition**  
   Given $f : A \rightarrow B, g : B \rightarrow C$, there is $f\circ g:A \rightarrow C$.

 - **Associativity**  
   Given $f, g, h$, it holds that $(f\circ g)\circ h = f\circ (g\circ h)$

 - **Identity arrow for all** $B$  
   $id_B : B \rightarrow B$ such that $id_B \circ f = f$ and $g \circ id_B = g$

---------------------------------------------------------------------------------------------------

## Interesting categories

### Standard mathematical structures

 - Sets or spaces with functions
 - Numbers with arrows from smaller to larger

### Categories in programming langauges

 - Types and functions between them
 - Classes with inheritance arrows

---------------------------------------------------------------------------------------------------

## Category of types and functions

$$$
\begin{xy}
\xymatrix{
  \text{int}\times\text{int} \ar@/^/[r]^{~~~fst} \ar@/_/[r]_{~~~snd} & \text{int} \ar[r]_{zero}  & \text{bool}
}
\end{xy}

### This is a valid category

 - Identity arrows: $id_A = \lambda (x:A) \rightarrow A$
 - $\circ$ is (associative) function composition

---------------------------------------------------------------------------------------------------

## Functors between categories

Given categories $C$ and $D$ a functor $F$ from $C$ to $D$ associates:

 - Objects $A\in obj(C)$ with $F(A) \in obj(D)$
 - Arrows $f:A \rightarrow B$ with
   $F(f) : F(A) \rightarrow F(B)$

(also must preserve identities and composition)

---------------------------------------------------------------------------------------------------

## Functors between types

Turns category of types and functions into other types and functions between them

$$$
\begin{xy}
\xymatrix{
  \text{int}\times\text{int} \ar@/^/[r]^{~~~fst} \ar@/_/[r]_{~~~snd} \ar@{.>}[d]_{list} &
    \text{int} \ar@{.>}[d]_{list} \ar[r]_{zero}  & \text{bool} \ar@{.>}[d]_{list} \\
  (\text{int}\times\text{int})~\text{list} \ar@/^/[r]^{~~~map~fst} \ar@/_/[r]_{~~~map~snd} &
    \text{int}~\text{list} \ar[r]_{map~zero} & \text{bool}~\text{list}
}
\end{xy}

---------------------------------------------------------------------------------------------------

## Category theory wrap-up

### Just a very general algebraic structure!

 - Objects with arrows between them
 - Functors between categories

### Why people like and use cateogry theory?

 - Lots of explored structures we can steal
 - Close functional programming analogy
 - ...and it makes you sound smarter!

***************************************************************************************************

## Categorical approach to semantics

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Categorical approach to semantics

What is the meaning of `a + b`?

$$$
a:\text{int}, b:\text{int} \vdash a + b : \text{int}

Arrow or _morphism_ between two objects:

$$$
\text{int}\times\text{int} \rightarrow \text{int}

Yes, you can read this as a function too...

---------------------------------------------------------------------------------------------------

## Categorical approach to semantics

What is the meaning of `e`?

$$$
v_1:\tau_1, \ldots, v_n:\tau_n \vdash e : \tau

Arrow or _morphism_ between two objects:

$$$
\tau_1\times\ldots\times\tau_n \rightarrow \tau

Maps product of inputs to the output

---------------------------------------------------------------------------------------------------

## Simple categorical semantics

Variable access

$$$
[\![ v : \tau \vdash v : \tau  ]\!] = id_\tau

<div class="fragment">

Composition with let binding

$$$
[\![ v_0 : \tau_0 \vdash \text{let}~v_1=e_1~\text{in}~e_2 : \tau_2 ]\!] = \\
  [\![ v_1 : \tau_1 \vdash e_2 : \tau_2 ]\!] \circ [\![ v_0 : \tau_0 \vdash e_1 : \tau_1 ]\!]

</div><div class="fragment">

(Slightly cheating here - no variable scoping!)

</div>

***************************************************************************************************

## Monads and comonads

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Monadic and comonadic semantics

Interpret expression as morphism with some more structure:

$$$
\definecolor{mc}{RGB}{0,32,172}
\definecolor{cc}{RGB}{172,0,32}
\definecolor{mcl}{RGB}{128,150,250}
\definecolor{ccl}{RGB}{250,128,150}
v_1:\tau_1, \ldots, v_n:\tau_n \vdash e : \tau

Wrap output in monad ${\color{mc} M}$ or input in comonad ${\color{cc} C}$:

$$$
\tau_1\times\ldots\times\tau_n \rightarrow {\color{mc} M}(\tau) \\
{\color{cc} C}(\tau_1\times\ldots\times\tau_n) \rightarrow \tau

<div class="fragment">

(Tricky thing - the comonad is over the variable context!)

</div>

---------------------------------------------------------------------------------------------------

## Monadic notions of computation

Given a category $C$, a _monad_ is a functor ${\color{mc} M} : C \rightarrow C$  
together with mappings (or _natural transformations_):

 - $\eta_\tau : \tau \rightarrow {\color{mc} M}(\tau) $

 - $(-)^*$ which turns an arrow $f : \tau_1 \rightarrow {\color{mc} M}(\tau_2)$  
    into an arrow $f^* :  {\color{mc} M}(\tau_1) \rightarrow {\color{mc} M}(\tau_2)$

---------------------------------------------------------------------------------------------------

## What are monads good for?

> Given $\tau_1$ produce $\tau_2$ wrapped in some additional structure.
>
> $\tau_1 \rightarrow {\color{mcl} M}(\tau_2)$


 - Can produce no results (failure, exception)
 - Can produce multiple results (non-determinism)
 - Can produce results & something else (logging)

---------------------------------------------------------------------------------------------------

## The maybe monad

Models partial computations: ${\color{mc} M}~\alpha = \alpha + \{\bot\}$

    // A value 'a or a value representing failure
    type Maybe<'a> = | OK of 'a | Fail

    // Creates a successful computation
    let unit v = OK(v)
    // Propagates failure through a computation
    let bind f mv = match mv with
        | OK(v) -> f(v)
        | Fail -> Fail

---------------------------------------------------------------------------------------------------

## The list monad

Models non-deterministic computation: ${\color{mc} M}~\alpha = \mathcal{P}(\alpha)$

    // Using standard functional list..
    type List<'a> = 'a list

    // Creates result with just one value
    let unit v = [v]
    // Collects results for all possible inputs
    let bind f (mv:List<'T>)  =
      [ for v in mv do
          for r in f(v) do yield r ]

---------------------------------------------------------------------------------------------------

## Monadic categorical semantics

Uses monad operations to get: $\tau_1 \times \ldots \times \tau_n \rightarrow {\color{mc} M}(\tau)$

<div class="fragment">
<br />

Variable access uses `unit`:

$$$
[\![ v : \tau \vdash v : \tau  ]\!] = \eta_\tau

</div>

---------------------------------------------------------------------------------------------------

## Monadic categorical semantics

Composition uses `bind` written as $(-)^*$:

$$$
[\![ v_0 : \tau_0 \vdash \text{let}~v_1=e_1~\text{in}~e_2 : \tau_2 ]\!] = \\
  [\![ v_1 : \tau_1 \vdash e_2 : \tau_2 ]\!]^* \circ [\![ v_0 : \tau_0 \vdash e_1 : \tau_1 ]\!]

How does this work?

$$$
[\![ v_0 : \tau_0 \vdash e_1 : \tau_1 ]\!] : \tau_0 \rightarrow {\color{mc} M}(\tau_1) \\
[\![ v_1 : \tau_1 \vdash e_2 : \tau_2 ]\!] : \tau_1 \rightarrow {\color{mc} M}(\tau_2) \\
[\![ v_1 : \tau_1 \vdash e_2 : \tau_2 ]\!]^* : {\color{mc} M}(\tau_1) \rightarrow {\color{mc} M}(\tau_2)

---------------------------------------------------------------------------------------------------

## Monadic notions of computation

Given a category $C$, a _comonad_ is a functor ${\color{cc} C} : C \rightarrow C$  
together with mappings (or _natural transformations_):

 - $\mu_\tau : {\color{cc} C}(\tau) \rightarrow \tau $

 - $(-)^*$ which turns an arrow $f : {\color{cc} C}(\tau_1) \rightarrow \tau_2$  
    into an arrow $f^* : {\color{cc} C}(\tau_1) \rightarrow {\color{cc} M}(\tau_2)$

---------------------------------------------------------------------------------------------------

## Comonadic categorical semantics

Uses comonad operations to get: ${\color{cc} C}(\tau_1 \times \ldots \times \tau_n) \rightarrow \tau$

<div class="fragment">

Variable access uses `counit` written as $\mu_\tau$:

$$$
[\![ v : \tau \vdash v : \tau  ]\!] = \mu_\tau

</div><div class="fragment">

Composition uses `cobind` written as $(-)^*$:

$$$
[\![ v_0 : \tau_0 \vdash \text{let}~v_1=e_1~\text{in}~e_2 : \tau_2 ]\!] = \\
  [\![ v_1 : \tau_1 \vdash e_2 : \tau_2 ]\!]^* \circ [\![ v_0 : \tau_0 \vdash e_1 : \tau_1 ]\!]

</div>

---------------------------------------------------------------------------------------------------

## What are comonads good for?

> Given $\tau_1$ with some additional structure produce $\tau_2$.
>
> ${\color{ccl} C}(\tau_1) \rightarrow \tau_2$


 - Can take additional values (resources, parameters)
 - Can take multiple inputs (historical values)
 - Can take structured inputs (say, 2D grid)

---------------------------------------------------------------------------------------------------

## The product comonad

Models additional parameters: ${\color{cc} C}~\alpha = \phi \times \alpha$

    // Pair value with e.g. a "hidden dictionary"
    type Prod<'a> = Map<string, string> * 'a

    // Access the value and ignore the dictionary
    let counit (cv:Prod<'a>) = snd cv
    // Propagate the hidden dictionary without changing it
    let cobind f (cv:Prod<'a>) : Prod<'b> =
      let res = f cv
      (fst cv, res)

---------------------------------------------------------------------------------------------------

## The list comonad

Keeps a current value with a list of past values

Compute: $f(x) = x + \text{prev}(x)$

<table class="grid">
<tr><td class="cur">1.0</td><td>1.6</td><td>1.8</td><td>...</td></tr>
</table>

$f : {\color{cm} C}(\text{int}) \rightarrow \text{int}$

<table class="grid">
<tr><td class="cur">1.3</td></tr>
</table>

---------------------------------------------------------------------------------------------------

## The list comonad

Keeps a current value with a list of past values

Compute: $f(x) = x + \text{prev}(x)$

<table class="grid">
<tr><td class="cur">1.0</td><td>1.6</td><td>1.8</td><td>...</td></tr>
</table>

$f* : {\color{cm} C}(\text{int}) \rightarrow {\color{cm} C}(\text{int})$

<table class="grid">
<tr><td class="cur">1.3</td><td>1.7</td><td>...</td><td>...</td></tr>
</table>

---------------------------------------------------------------------------------------------------

## The list comonad

Value with zero or more past values: ${\color{cc} C}~\alpha = \alpha \times (\text{List}~\alpha)$

    // Current value with list of historical
    type Hist<'a> = 'a * list<'a>

    // Access the current value
    let counit (v:Hist<'a>) = fst v
    // Apply on current & previous elements (recursively)
    let rec cobind f (cv:Hist<'a>) : Hist<'b> =
      (f cv), ( match snd cv with
                | x::xs -> let r,rs = cobind f (x, xs) in r::rs
                | [] -> [] )

---------------------------------------------------------------------------------------------------

## Grid computations with comonads

Average value and its neighbours

<table>
<tr><td>

<table class="grid">
<tr><td>0.0</td><td class="nb">1.0</td><td>1.0</td></tr>
<tr><td class="nb">0.0</td><td class="cur">2.0</td><td class="nb">2.0</td></tr>
<tr><td>2.0</td><td class="nb">3.0</td><td>4.0</td></tr>
</table>

</td><td style="padding-left:100px;vertical-align:middle;">

<table class="grid">
<tr><td class="cur">1.6</td></tr>
</table>

</td></tr></table>

---------------------------------------------------------------------------------------------------

## Grid computations with comonads

Average value and its neighbours

<table>
<tr><td>

<table class="grid" id="grin">
<tr><td>0.0</td><td>1.0</td><td>1.0</td></tr>
<tr><td>0.0</td><td>2.0</td><td>2.0</td></tr>
<tr><td>2.0</td><td>3.0</td><td>4.0</td></tr>
</table>

</td><td>

<table class="grid" id="grout">
<tr><td>?</td><td>?</td><td>?</td></tr>
<tr><td>?</td><td>?</td><td>?</td></tr>
<tr><td>?</td><td>?</td><td>?</td></tr>
</table>

</td></tr></table>

<script>
function reset(el)
{
  var gr = document.getElementById(el);
  var rows = gr.getElementsByTagName("tr");
  for(var i=0; i<3; i++)
  {
    var row = rows[i].getElementsByTagName("td");
    for(var j=0; j<3; j++) row[j].className = "";
  }
}
function highlight(el,x,y,cls)
{
  if (x<0 || x>2 || y<0 || y>2) return;
  var gr = document.getElementById(el);
  var rows = gr.getElementsByTagName("tr");
  rows[x].getElementsByTagName("td")[y].className=cls;
}
function get(el,x,y)
{
  if (x<0 || x>2 || y<0 || y>2) return null;
  var gr = document.getElementById(el);
  var rows = gr.getElementsByTagName("tr");
  return 1.0 * rows[x].getElementsByTagName("td")[y].innerText;
}
function set(el,x,y,v)
{
  if (x<0 || x>2 || y<0 || y>2) return null;
  var gr = document.getElementById(el);
  var rows = gr.getElementsByTagName("tr");
  rows[x].getElementsByTagName("td")[y].innerText = Math.round(v*10)/10;
}
function calculate(x,y)
{
  reset("grin");
  reset("grout");
  highlight("grout", x, y, "cur");
  highlight("grin", x, y, "cur");
  highlight("grin", x-1, y, "nb");
  highlight("grin", x+1, y, "nb");
  highlight("grin", x, y-1, "nb");
  highlight("grin", x, y+1, "nb");
  var sum = 0;
  var count = 0;
  for(var i=-1; i<=1; i++)
  {
    for(var j=-1; j<=1; j++)
    {
      if (i != 0 && j != 0) continue;
      var v = get("grin", x+i, y+j);
      if (v != null) { sum += v; count++; }
    }
  }
  set("grout",x,y,sum/count);
}
function run(x,y)
{
  calculate(x,y);
  x++;
  if (x==3) { x = 0; y++; }
  if (y<3) {
    setTimeout(function () { run(x,y); }, 2000);
  } else {
    setTimeout(function () { reset("grin"); reset("grout"); }, 2000);
  }
}
</script>

<p><a href="#" onclick="run(0,0);return false;">Compute averages!</a></p>

***************************************************************************************************

## Summary

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Summary

### Monadic computations: $~\tau_1 \rightarrow {\color{mc} M}(\tau_2)$

 - Return value with some non-standard aspect
 - No value (failure), more values (non-determinism), logging

### Comonadic computations: $~{\color{cc} C}(\tau_1) \rightarrow \tau_2$

 - Take inputs with some non-standard aspect
 - Hidden parameters, history, neighborhood

---------------------------------------------------------------------------------------------------

## Summary

### Why is category theory useful?

 - Lots of structures we can steal
 - Like programming, focuses on composition
 - Not a sacred cow! Use it when it solves a problem!

### Why nobody knows about comonads?

 - Monads were first and are more useful
 - Creating `codo` notation is harder
