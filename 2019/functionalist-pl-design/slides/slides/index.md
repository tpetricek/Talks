- title : Functional-first programming with F#
- description : The word "function" in "functional programming" refers to mathematical concept of a
    function, but in this talk, I will pretend that that's not the case. Imagine that functional
    programming was instead inspired by functionalist architecture and the word "function" referred
    to the modernist design principle that "form follows function". How would this perspective
    transform our thinking about programming?
- author : Tomas Petricek
- theme : night
- transition : none

***************************************************************************************************
- data-background:images/prague2-dark.jpg

## _FUNCTIONAL-FIRST_<br />programming with F#

<br />
<br />
<br />
<br />

#### **Tomas Petricek**, University of Kent + fsharpWorks <br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) | [fsharpworks.com](http://fsharpworks.com)

---------------------------------------------------------------------------------------------------
- data-background:images/prague2-dark.jpg

F# is a mature, open source, cross-platform, **functional-first** programming language.
It empowers users to tackle complex problems with simple code.

---------------------------------------------------------------------------------------------------
- data-background:images/math-dark.jpg

# _FUNCTIONAL_

---------------------------------------------------------------------------------------------------
- data-background:images/math.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/math-dark.jpg

A _function_ from $A$ to $B$ is an object $f$  
such that every $a\in A$ is uniquely
associated with an object $f(a)\in B$.

---------------------------------------------------------------------------------------------------
- data-background:images/bauhaus2.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/bauhaus2-dark.jpg

A _function_ is the _purpose_ for which  
something is designed or exists.

---------------------------------------------------------------------------------------------------
- data-background:images/bauhaus2-dark.jpg

## **form** follows **function**

***************************************************************************************************
- data-background:images/bata2-dark.jpg

# _TRANSFORMATIONS_

---------------------------------------------------------------------------------------------------
- data-background:images/bata2-dark.jpg

_Transform a value_ into a new value  
using a series of _transformations_.

---------------------------------------------------------------------------------------------------
- data-background:images/bata2-dark.jpg

## DEMO<br/>_Working with data_

---------------------------------------------------------------------------------------------------
- data-background:images/bata2.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/zlin.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/zlin-dark.jpg

## DEMO<br/>_Transforming 3D shapes_

---------------------------------------------------------------------------------------------------
- data-background:images/horta-dark.jpg

The _elimination_ of _ornament_

---------------------------------------------------------------------------------------------------
- data-background:images/horta.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/tugendhat.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/tugendhat-dark.jpg

The _elimination_ of language _keywords_

---------------------------------------------------------------------------------------------------
- data-background:images/tugendhat-dark.jpg

<span></span>

    Fun.cylinder
    |> Fun.color Colors.yellow
    |> Fun.scale (1.0, 3.0, 1.0)
    |> Fun.move (0.0, -2.0, 0.0)

---------------------------------------------------------------------------------------------------
- data-background:images/prague2-dark.jpg

<div class="prehuge">

```
  let (|>) x f = f x
```

</div>

***************************************************************************************************
- data-background:images/expo-dark.jpg

# _TYPE PROVIDERS_

---------------------------------------------------------------------------------------------------
- data-background:images/expo-dark.jpg

_Navigating through_ and _extracting_
information from a data source.

---------------------------------------------------------------------------------------------------
- data-background:images/expo-dark.jpg

## DEMO<br/>_JSON type provider in F#_

---------------------------------------------------------------------------------------------------
- data-background:images/expo-dark.jpg

Design conveys the _message of simplicity_,
using _basic geometric forms_, simple materials.

---------------------------------------------------------------------------------------------------
- data-background:images/old-england.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/expo.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/expo-dark.jpg

<span></span>

    type W = JsonProvider<"http://...">

    let weather = W.GetSample()
    for w in weather.List do
      printTemp w.Temp.Day w.Temp.Night

---------------------------------------------------------------------------------------------------
- data-background:images/prague2-dark.jpg

How _far_ can we go  
with a _given material_?

---------------------------------------------------------------------------------------------------
- data-background:images/prague2.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/masp2.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/masp2-dark.jpg

## DEMO<br/>_Pivot provider in TheGamma_

---------------------------------------------------------------------------------------------------
- data-background:images/masp2-dark.jpg

If dot is _concrete_, then type providers are _reinforced concrete_.

***************************************************************************************************
- data-background:images/prosek-dark.jpg

# _COMPUTATIONS_

---------------------------------------------------------------------------------------------------
- data-background:images/prosek-dark.jpg

Give an _alternative meaning_<br />
to an ordinary _computation_.

---------------------------------------------------------------------------------------------------
- data-background:images/prosek.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/brasilia2.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/brasilia2-dark.jpg

<span></span>

    let tryGetPageLength url =
      let wc = WebClient()
      try
        let html = wc.Download(url)
        Some(html.Length)
      with e ->
        None

---------------------------------------------------------------------------------------------------
- data-background:images/brasilia2-dark.jpg

<span></span>

    let tryGetPageLength url = async {
      let wc = WebClient()
      try
        let! html = wc.AsyncDownload(url)
        return Some(html.Length)
      with e ->
        return None }

---------------------------------------------------------------------------------------------------
- data-background:images/brasilia2-dark.jpg

## DEMO<br/>_Asynchronous computations_

---------------------------------------------------------------------------------------------------
- data-background:images/niemayer-dark.jpg

Use _flexible material_ and  
design with _material in mind_.

---------------------------------------------------------------------------------------------------
- data-background:images/niemayer.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/abu.jpg

---------------------------------------------------------------------------------------------------
- data-background:images/niemayer-dark.jpg

Using the _right abstraction_ <br />
should be either _easy or impossible_.

---------------------------------------------------------------------------------------------------
- data-background:images/niemayer-dark.jpg

<span></span>

    type AsyncSeq<'T> =
      Async<AsyncSeqResult<'T>>

    and AsyncSeqResult<'T> =
      | Empty
      | Value of 'T * AsyncSeq<'T>

---------------------------------------------------------------------------------------------------
- data-background:images/niemayer-dark.jpg

## DEMO<br/>_Asynchronous sequences_

***************************************************************************************************
- data-background:images/bauhaus2-dark.jpg

# _SUMMARY_

---------------------------------------------------------------------------------------------------
- data-background:images/bauhaus2-dark.jpg

Modernism is associated with  

 - Analytical approach to the _function_
 - Rational use of _new materials_
 - Openness to _structural innovation_
 - The _elimination of ornament_

---------------------------------------------------------------------------------------------------
- data-background:images/expo-dark.jpg

_Function composition_  
elimination of ornament

_Type providers_  
structural innovation

_Computation expressions_  
leverage new materials

---------------------------------------------------------------------------------------------------
- data-background:images/tugendhat-dark.jpg

## FUNCTIONAL PROGRAMMING

<br />

**Function** as in purpose

**Form** follows **function**

<br />
<br />

#### **Tomas Petricek**, University of Kent + fsharpWorks <br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) | [fsharpworks.com](http://fsharpworks.com)

***************************************************************************************************
- data-background:images/abu-dark.jpg

<span></span>

    let whatDoesThisMean () = reactive {
      let trumpTweets = getTrumpTweets()
      for t in trumpTweets do
        if t.Text.Contains("great") then
          do! Async.Sleep(1000)
        yield t }
