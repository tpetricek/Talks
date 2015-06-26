(**
- title : Coeffects: Theory of context-aware programming languages
- description : Modern computer programs do not execute in void. They run in rich environments
    that provide access to resources (GPS sensor, etc.), information (Facebook data) and other
    context. Coeffects provide a way for tracking the context and making sure it is used
    correctly.
- author : Tomas Petricek
- theme : white
- transition : transform

***************************************************************************************************

# **Coeffects**: Theory of context-aware programming languages

<div style="margin:50px 0px 50px 0px">
<a href="http://tomasp.net/academic/papers/coeffects/">
<img title="Coeffects: Unified static analysis of context-dependence" src="images/coeffects.png" style="height:280px" />
</a>
&nbsp;&nbsp;&nbsp;
<a href="http://tomasp.net/academic/papers/structural/">
<img title="Coeffects: A calculus of context-dependent computation" src="images/structural.png" style="height:280px" />
</a>
</div>

**Tomas Petricek**, University of Cambridge <br/>
[http://tomasp.net](http://tomasp.net) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)


$$$
\definecolor{cw}{RGB}{255,255,255}
\definecolor{mc}{RGB}{0,32,172}
\definecolor{cc}{RGB}{172,0,32}

<style>
.wide-p-spaces p { margin:50px 0px 30px 0px; }
.reveal em { font-style:normal; font-weight:bold; color:#2C7DA0; }
.reveal strong { font-style:normal; font-weight:bold; color:#9B2B40; }
.reveal h3 { font-weight:normal; }
.reveal .h3-left h3 { position:relative; left:-100px; }
</style>

*)
(*** hide ***)
#nowarn "86"

type RulePart =
  | Expr of string
  | Text of string
  | Op of string * RulePart * RulePart
  | Color of string * RulePart
  | Frac of RulePart * RulePart
  | Space of RulePart
  member x.AsLatex() =
    let rec format = function
      | Expr s -> s
      | Space r -> "~" + (format r) + "~"
      | Text s -> "\\text{" + s + "}"
      | Frac(t, b) -> "\\dfrac{ " + format t + " }{ " + format b + " }"
      | Color(c, r) -> "{\\color{" + c + "} " + (format r) + "}"
      | Op(op, l, Color(c, r)) ->
          (format l) + "{\\color{" + c + "} " + op + " " + (format r) + "}"
      | Op(":", Expr l, Expr r) -> // Remove spaces when both sides are simple
          l + "\\!:\\!" + r
      | Op(op, l, r) ->
          (format l) + op + (format r)
    format x
  static member (+)(l, r) = Op("", l, r)

let e = Expr "e"
let e1 = Expr "e_1"
let e2 = Expr "e_2"
let t = Expr "\\tau"
let t1 = Expr "\\tau_1"
let t2 = Expr "\\tau_2"
let tn = Expr "\\tau_n"
let v = Expr "v"
let v1 = Expr "v_1"
let v2 = Expr "v_2"
let vn = Expr "v_n"
let x = Expr "x"
let s = Expr "\\sigma"
let s1 = Expr "\\sigma_1"
let s2 = Expr "\\sigma_2"

let r = Expr "r"
let Gamma = Expr "\Gamma"
let clr1 e = Color("cc", e)
let clr2 e = Color("mc", e)
let white e = Color("cw", e)
let ($) v t = Op(":", v, t)
let (++) l r = Op(", ", l, r)
let (@) g r = Op(" @~ ", g, r)
let (&) x e = Op("~\&~", x, e)
let (&&) l r = Op("~~~~", l, r)
let (|-) g e = Op(" \\vdash ", g, e)
let (---) a b = Frac(a, b)
let kvd s = Space(Color("mc", Text s))
let ident s = Space(Text s)
let (!) s = Expr s

// Equations that involve mutable references
let ref = function Expr s -> Expr("\\text{ref}_{" + s + "}") | _ -> failwith "Not a variable!"
let effs l = Expr "\\{" + (List.reduce (++) l) + Expr "\\}"
let rref = function Expr s -> Expr("~! " + s) | _ -> failwith "Not a variable!"
let wref l r = match l, r with Expr l, Expr r -> Expr(l + "\leftarrow " + r) | _ -> failwith "Not a variable!"

// Function type and value
let fn x f t = f + !"\\xrightarrow{" + x + !"}" + t
let abs v e = kvd "fun" + v + !"\\Rightarrow " + e

// Semantics
let sem e = !"[\![" + e + !"]\!]"
(**

***************************************************************************************************

## Programming languages and context

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

# Computing in rich environments

<table><tr><td style="text-align:center;padding:40px 40px 0px 0px">
<i class="fa fa-windows" style="font-size:200%;color:#2C7DA0;position:relative;top:40px"></i>
<br /><br /><br />
<i class="fa fa-mobile-phone" style="font-size:250%;color:#9B2B40;position:relative;top:40px"></i>
</td><td style="text-align:center">

<div class="fragment">

### Execution environments are **rich** <br /> but gradually more **diverse**

</div><div class="fragment" style="padding-top:30px">

### Programming _context-aware <br /> applications_ is hard!

</div>
</td><td style="text-align:center;padding:40px 0px 0px 40px">
<i class="fa fa-apple" style="font-size:200%;color:#2C7DA0;position:relative;top:40px"></i>
<br /><br /><br />
<i class="fa fa-coffee" style="font-size:200%;color:#9B2B40;position:relative;top:40px"></i>
</td></tr></table>

---------------------------------------------------------------------------------------------------

# Programming in rich contexts

<div style="padding:30px">

    let getNews() =
      let news = query(extern(DB), "News")
      filterByLocation(news, extern(GPS))

</div><div class="fragment">

### This is becoming **important problem!**

 - What _resources_ are available?
 - What features does a _device_ support?
 - Is the source _secure_ and _trustworthy_?

</div><div class="fragment">

### Coeffects provide **unified abstraction.**

</div>

---------------------------------------------------------------------------------------------------

# Checking program properties with types

<div class="wide-p-spaces fragment">

*)
(*** hide ***)
let let_jdg =
  ( Gamma |- kvd "let" + v1 + !"=" + e1 + kvd "in" + e2 $ t2 )

let let_drv =
  ( ( Gamma |- e1 $ t1 ) &&
    ( Gamma ++ v1 $ t1 |- e2 $ t2 ) )
  ---
  let_jdg
(**

_Typing judgements_ specify program type
*)
(*** include-value: let_jdg ***)
(**

<div class="fragment">

_Typing rules_ define checking procedure
*)
(*** include-value: let_drv ***)
(**
</div></div>

***************************************************************************************************

## Checking effects and coeffects

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

# Checking memory accesses with effects

<div style="padding-top:20px">
*)
(*** hide ***)
let ef_gam =
  Gamma + !"=" + v1 $ ref s1 + !"\\text{int}" ++ v2 $ ref s2 + !"\\text{int}"
let ef_simple =
  Gamma |- kvd "let" + x + !"=" + rref v1 + kvd "in" + wref v2 x $ ident "unit"
let ef_full =
  ef_simple & clr1 (effs [ident "read" + s1; ident "write" + s2])
(**
Language with _mutable references_ on the heap:
*)
(*** include-value:ef_gam ***)
(**
</div><div class="fragment" style="margin-top:40px">

Simple _type system_ does not tell us much:
*)
(*** include-value:ef_simple ***)
(**
</div><div class="fragment" style="margin-top:40px">

_Effect systems_ check memory locations!
*)
(*** include-value:ef_full ***)
(**
</div>

---------------------------------------------------------------------------------------------------

<table><tr><td style="padding-right:40px">

## Effect systems

What **program does** <br /> (to the environment)
*)
(*** include-value: Gamma |- e $ t & clr1 s ***)
(**

 - Memory access
 - Input, output
 - Non-determinism

</td><td style="padding-left:40px" class="fragment">

## Coeffect systems

What _program needs_ <br /> (from the environment)
*)
(*** include-value: Gamma @ clr2 r |- e $ t ***)
(**

 - Variable access
 - Resources and data
 - History, neighbours

</tr></table>


---------------------------------------------------------------------------------------------------

# The fun part: Typing rules

*)
(*** hide ***)
let abs_write = Gamma |- abs x (wref v !"42") $ !"~ ?"
let abs_access = Gamma |- abs x (kvd "extern" + ident "gps") $ !"~ ?"
let abs_eff =
  ( Gamma ++ v $ t1 |- e $ t2 & (clr1 s) )
  ---
  ( Gamma |- abs v e $ fn (clr1 s) t1 t2 )
let abs_coeff =
  ( Gamma ++ v $ t1 @ clr1 (s1 + !"\cup" + s2) |- e $ t2 )
  ---
  ( Gamma @ clr1 s1 |- abs v e $ fn (clr1 s2) t1 t2 )
(**

What happens when we create a **function value**?

<br />

*)
(*** include-value: abs_write ***)
(*** include-value: abs_access ***)
(**

---------------------------------------------------------------------------------------------------

# Effectful and coeffectful functions

_Effect systems_ delay all effects
*)
(*** include-value: abs_eff ***)
(**
<div class="fragment" style="padding-top:30px">

_Coeffect systems_ split context requirements
*)
(*** include-value: abs_coeff ***)
(**
</div>

---------------------------------------------------------------------------------------------------

# Why coeffect systems matter?

<div class="h3-left">

### Programming language **theory**

 - _Unify_ systems explored before
 - _Variable_ usage, _data-flow_ and _resources_
 - Interesting and _differnet shape_
 - Semantics using _comonad structure_

### Programming language **practice**

 - Foundations for _context-aware_ languages

</div>

***************************************************************************************************

## Categorical approach to semantics

<br />
<img src="images/literate.png" style="width:300px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Categorical approach to semantics

What is the _meaning_ of an expression $e$?

$$$
v_1:\tau_1, \ldots, v_n:\tau_n \vdash e : \tau

<div class="fragment">

Function between sets or _morphism between objects_:

$$$
\tau_1\times\ldots\times\tau_n \rightarrow \tau

Maps product of input variables to the output

</div>

---------------------------------------------------------------------------------------------------

# Semantics of context-aware langauges
*)
(*** hide ***)
let sem_plain = sem (v1 $ t1 ++ !"\ldots" ++ vn $ tn |- e $ t)
let sem_coeff = sem (v1 $ t1 ++ !"\ldots" ++ vn $ tn @ (clr1 r) |- e $ t)
(**
Coeffects sdd some _additional structure_:
*)
(*** include-value: sem_plain ***)
(**

Wrap input in a comonad ${\color{cc} C}$:

$$$
{\color{cc} C}(\tau_1\times\ldots\times\tau_n) \rightarrow \tau

---------------------------------------------------------------------------------------------------

# Semantics of context-aware langauges

Coeffects sdd some _additional structure_:
*)
(*** include-value: sem_coeff ***)
(**

Wrap input in an _indexed_ comonad ${\color{cc} C^r}$:

$$$
{\color{cc} C^r}(\tau_1\times\ldots\times\tau_n) \rightarrow \tau

---------------------------------------------------------------------------------------------------

# Comonadic semantics of coeffects

$$$
{\color{cc} C}(\tau_1\times\ldots\times\tau_n) \rightarrow \tau

 - Context provides _additional resources_ <br/>
   $~~~{\color{cc} C}\alpha = \alpha \times \Psi$

 - Context _may be missing_ (dead variables) <br />
   $~~~{\color{cc} C}\alpha = \alpha_\bot$

 - Context provides _historical values_ <br />
   $~~~{\color{cc} C}\alpha = \alpha_1 \times \ldots \times \alpha_n ~~(n \geq 1)$

---------------------------------------------------------------------------------------------------

# Comonadic semantics of coeffects

$$$
{\color{cc} C^r}(\tau_1\times\ldots\times\tau_n) \rightarrow \tau

 - Context provides _additional resources_ <br/>
   $~~~{\color{cc} C^r}\alpha = \alpha \times \psi^{\color{cc} r}$

 - Context _may be missing_ (dead variables) <br />
   $~~~{\color{cc} C^1}\alpha = \alpha~~~~{\color{cc} C^0}\alpha = \bot$

 - Context provides _historical values_ <br />
   $~~~{\color{cc} C^n}\alpha = \alpha_{\color{cc} 1} \times \ldots \times \alpha_{\color{cc} n}$

***************************************************************************************************

# Summary

### **Programming language** theory problems

 - Features in _small calculus_
 - _Type system_ properties
 - Programming language _semantics_

### **Context-aware** programming langauges

 - _Unifying_ languages with context
 - Interesting _theory structure_

*)
