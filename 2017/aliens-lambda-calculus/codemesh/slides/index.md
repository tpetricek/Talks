- title : Would Aliens Understand Lambda Calculus?
- description : Elegant programming constructions and mathematical theories like LISP and lambda 
    calculus often look timeless and universal. They are not invented, but discovered! If there are 
    intelligent aliens, they will sooner or later run into formal logics and computation and, 
    shortly thereafter, discover lambda calculus and LISP. Or will they?
- author : Tomas Petricek
- theme : white
- transition : none

****************************************************************************************************

# Would Aliens Understand<br/> _Lambda Calculus?_

<br /><br /><br /><br />
<style>.normalbold strong { font-weight:bold !important; color:black !important; }</style>
<div class="normalbold">

**Tomas Petricek**, fsharpWorks & Alan Turing Institute<br />
[tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

</div>

****************************************************************************************************

### _"Are pure functions invented or discovered?"_

<div class="fragment">

### **"I wonder if there is a paper about that?"**

</div>
<div class="fragment">

### There is an entire discipline about that!

</div>

****************************************************************************************************

# _What is mathematics?_

Crash course in philosophy of mathematics

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

<img src="images/plato.jpg" style="float:right;margin-top:100px" />
<div style="margin-right:300px">

# _Platonism_

> The existence of mathematical objects is independent of us, our language, thoughts and practices.

<div class="fragment">
<h2 style="font-size:30pt;margin:60px 0px 10px 0px"><strong>The sad consequences</strong></h2>

> The Romance of Mathematics makes a wonderful story, but it intimidates, it helps
> to maintain an elite,<br />it rewards incomprehensibility.
>
> <p style="text-align:right;width:100%;margin-top:30px">Lakoff, Núñez (2000)</p>

</div>
</div>

----------------------------------------------------------------------------------------------------

<img src="images/lakatos.jpg" style="width:200px;float:right;margin-top:100px" />
<div style="margin-right:240px">

# _Social mathematics_

> Mathematics does not grow through increase of the number of established theorems,
> but through improvement by specu- lation and criticism, by the method
> of proofs and refutations.
>
> <p style="text-align:right;width:100%;margin-top:10px">Lakatos (1976)</p>

<div class="fragment">
<h2 style="font-size:30pt;margin:60px 0px 10px 0px"><strong>Counter-example causes refinement</strong></h2>

> "I turn aside with a shudder of horror from this lamentable plague of functions which have no derivatives."</p>

</div>
</div>

----------------------------------------------------------------------------------------------------

<img src="images/hilbert.jpg" style="width:200px;float:right;margin-top:100px" />
<div style="margin-right:300px">

# _Culture and mathematics_

> Culturally specific ideas often find their way into<br /> the very fabric of mathematics itself.
>
> <p style="text-align:right;width:100%;margin-top:30px">Lakoff, Núñez (2000)</p>

<div class="fragment">
<h2 style="font-size:30pt;margin:60px 0px 10px 0px"><strong>Ancient culture in maths</strong></h2>

1. The idea of essence
2. The idea that human reason is a form of logic
3. The idea of foundations for a subject matter</p>

</div>
</div>

----------------------------------------------------------------------------------------------------

<img src="images/lakoff.jpg" style="width:250px;float:right;margin-top:100px" />
<div style="margin-right:300px">

# _Embodied mathematics_

> The only mathematics we know or can know is  
> a brain-and-mind-based mathematics.
>
> <p style="text-align:right;width:100%;margin-top:30px">Lakoff, Núñez (2000)</p>

<div class="fragment">
<h2 style="font-size:30pt;margin:60px 0px 10px 0px"><strong>How to study mathematics?</strong></h2>

It is up to cognitive science to apply the science of mind to human mathematical ideas. 

</div>
</div>

****************************************************************************************************

# _Embodied mathematics_

Cognitive science of mathematics

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

## _Metaphors are central to thought_

> Cognitive science [showed that], **abstract concepts [are]<br />
> understood, via metaphor**, in terms of more concrete concepts.
>
> <div class="fragment">
>
> Many mathematical ideas are ways of **mathematicizing ordinary ideas**, <br /> 
> as when derivatives mathematicize the idea of instantaneous change.
>
> </div>
> <p style="text-align:right;width:100%;margin-top:60px">Lakoff, Núñez (2000)</p>

---------------------------------------------------------------------------------------------------

## Components of the analysis

***Innate arithmetic***  
Babies have some mathematical capacities

***Conceptual metaphors***  
Links concepts via neural conflations

***Layering metaphors***  
Explain more abstract mathematical concepts

---------------------------------------------------------------------------------------------------

## Innate arithmetic experiments

<img src="images/mickey.png" style="width:800px" />

---------------------------------------------------------------------------------------------------

<img src="images/arith1.png" />

---------------------------------------------------------------------------------------------------

## Arithmetic is object collection

**Linguistic examples**  
_Add_ onions and carrots to the soup  
Which is _bigger_, 5 or 7?

**Equational properties**  
Adding A to B gives the same result as  
adding B to A for _object collections_

**Limitations of the metaphor**  
Zero in terms of collections?

****************************************************************************************************

### _"Type theory and the $\lambda$-calculus are eternal"_

<div class="fragment">

### **"Libraries are ephemeral compared to maths"**

</div>
<div class="fragment">

### "$\lambda$-calculus is discovered, Angular is invented!"

</div>

****************************************************************************************************

# _What is computer science?_

Lambda calculus, category theory and functional programs
 
<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

<style>.examples h3 {margin-bottom:10px} .examples td { width:30%; }</style>
<table class="examples"><tr>
<td style="padding-right:20px">

#### Programs

<div class="fragment">

 * _type_
 * function
 * **tuple**

</div>
</td><td>

#### Proofs

<div class="fragment">

 * _formula_
 * implication
 * **conjunction**

</div>
</td><td style="padding-left:20px">

#### Categories

<div class="fragment">

 * _object_
 * arrow
 * **product**

</div>
</td></tr></table>

<div class="fragment">
<br />
<br />

### _Is this a deep truth about the universe?_

</div>

----------------------------------------------------------------------------------------------------

## _Philosopher's answer_
 
#### Category mistakes

 - **Program** refers to empirical, a posteriori knowledge
 - **Proof** refers to non-physical world of logic  

<br/>
<br/>
<div class="fragment">

#### Verification controversy

> The idea of program verification is what philosophers call "category mistake".
> Program verification is, literally, a form of nonsense.
>
> <p style="text-align:right;width:100%;margin-top:30px">Fetzer (1988)</p>

</div>

----------------------------------------------------------------------------------------------------

## _Sociologist's answer, Take 1_

<img src="images/lakatos.jpg" style="width:200px;float:right;margin-top:0px" />
<div style="margin-right:240px">

Carefully constructed to fit well via the<br /> method of _proofs and refutations_

<div class="fragment">

 - **Cartesian closed** category
 - **Intuitionalistic** logic
 - **Simply typed** lambda calculus
 
</div>
</div>

----------------------------------------------------------------------------------------------------

## _Sociologist's answer, Take 2_

<img src="images/hilbert.jpg" style="width:250px;float:right;margin-top:0px" />
<div style="margin-right:300px">

All three are product of the same network of mathematicians, solving the same problem.

<div class="fragment" style="padding-top:20px">

Searching for **foundations of mathematics**, formalising reasoning based
on **inference** that could be done mechanically. 
 
</div></div>

----------------------------------------------------------------------------------------------------

## _Cognitive scientists's answer_

<img src="images/lakoff.jpg" style="width:250px;float:right;margin-top:0px" />
<div style="margin-right:300px">

All three are derived from the same **embodied experience**
using a number of **conceptual** and **layering metaphors**. 

<div class="fragment" style="padding-top:20px">

_What is the embodied experience?_
 
</div></div>

****************************************************************************************************

### _"Would aliens understand $\lambda$ calculus?"_ 

<div class="fragment">

### "Any intelligent species is bound to have logic."

</div>
<div class="fragment">

### **"They'd also run into the program-proof duality."**

</div>

****************************************************************************************************

# _Where lambda calculus comes from?_

Cognitive science and lambda calculus
 
<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

# _Container schema_

<img src="images/containers.png" style="width:800px"/>

----------------------------------------------------------------------------------------------------

## _Metaphors behind reduction, Part 1_

<img src="images/implication.png" style="width:300px;margin-top:30px;float:right"/>
<div style="margin-right:320px">

**Modus Ponens** <br /> 
Given two Container schemas A and B and an <br />
object X, if A is in B and X is in A, then X is in B.

<div class="fragment" style="padding-top:20px">

**Function Application**<br />
Given two types $A$ and $B$ and a value $x$, <br />
if $f : A\rightarrow B$ and $x:A$ then $f(x):B$

</div>
</div>

----------------------------------------------------------------------------------------------------

## _Metaphors behind reduction, Part 2_

#### Evaluation using $\beta$-**reduction**

<div class="fragment">

**reduce**, verb (used with object), reduced, reducing.

 1. _to bring down_ to a smaller extent, size, amount
 2. _to lower in degree_, intensity, etc.
 3. _to bring down_ to a lower rank, dignity, etc.

</div>
<div class="fragment">
<br />

#### **Metaphor requires a sense of direction!**

</div>

****************************************************************************************************

### _"Would E.T. understand lambda calculus?"_ 

<div class="fragment">

### "How about the planet in Lem's Solaris?"

</div>
<div class="fragment">

### **"How about aliens from the Arrival movie?"**

</div>

****************************************************************************************************

# _Aliens and lambda calculus_

Cognitive science of extra-terrestrial beings
 
<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------
 - data-background:images/arrival.png

----------------------------------------------------------------------------------------------------
 - data-background:images/arrival-w.jpg

## Aliens from the Arrival movie

#### Circular language and time perception

No notion for _direction_

_Function application_ is directional!
  
Perhaps only _reversible_ computations?

----------------------------------------------------------------------------------------------------
 - data-background:images/solaris.jpg

----------------------------------------------------------------------------------------------------
 - data-background:images/solaris-w.jpg

## Stanislaw Lem's Solaris

#### The planet itself is a sentient being!

There is only _one_ being in the world

Would it have more numbers than _one_?

----------------------------------------------------------------------------------------------------
 - data-background:images/chaos.jpg

----------------------------------------------------------------------------------------------------
 - data-background:images/chaos-w.jpg

## Interstellar dust cloud

#### Aliens living in chaotic gaseous universe

There are no _boundaries_ in chaos!

There is no _inside_ and _outside_

No _container schema_ metaphors

****************************************************************************************************

# _Summary_

Would aliens understand lambda calculus?

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

----------------------------------------------------------------------------------------------------

## Would aliens understand lambda calculus?

_Is lambda calculus discovered or invented?_<br />
Platonism is just one (religious) belief

**Philosophy of mathematics and computer science**<br/>
Social, cultural enterprise, product of embodied mind

_So, would aliens understand lambda calculus?_<br />
Stretch your imagination! Boring aliens might...

<br />

[tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

----------------------------------------------------------------------------------------------------

## **Movies to watch & stories to read** 

#### _Arrival (2016)_ or Chiang's Story of your life
Aliens with circular language and time

<br />

#### _Solaris (2002)_ or Stanislaw's Lem Solaris
Not your grandma's sentient being 

----------------------------------------------------------------------------------------------------

## **Philosophy books to read** 

#### Imre Lakatos, _Proofs and refutations_<br />
How mathematics actually works

<br />

#### Lakoff & Núñez, _Where mathematics comes from_<br />
Cognitive account of mathematics via metaphors

<br />

#### Donald MacKenzie, _Mechanizing proof_<br />
Category mistakes and dissenting voices in the community 
 