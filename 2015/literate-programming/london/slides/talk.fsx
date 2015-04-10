(**
- title : Literate programming with F#
- description : In this presentation, we look at the F# tools for literate 
  programming, including the F# Formatting library (which parses literate F# and 
  Markdown), ProjectScaffold (a template for projects that lets you write literate 
  documentation) and FsReveal (a tool for creating presentation using F#). You'll 
  learn useful things about documenting your (not just F#) code and about writing 
  understandable code and data analyses.
- author : Tomas Petricek
- theme : Night
- transition : none

*******************************************************************************

## Literate programming with F#

<br /><br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />
<br /><br /><br /><br />

**Tomas Petricek**, F# Works  
[@tomaspetricek](http://twitter.com/tomaspetricek) 
| [http://tomasp.net](http://tomasp.net) 
| [http://fsharpworks.com](http://fsharpworks.com)

*******************************************************************************

## What is literate programming?

<br /><br /><br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

-------------------------------------------------------------------------------

## What is literate programming?

<div class="fragment">
<br />

    [lang=csharp]
    public class Person {
      /// <summary>
      /// Gets or sets the name of the person
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// Gets or sets the age of the person
      /// </summary>
      public string Age { get; set; }
    }

<br />

This is *not* literate programming!

</div>

-------------------------------------------------------------------------------

## What is literate programming?

<br />

![Donald Knuth](images/knuth.jpg)

Professor Donald Knuth is not happy :-(

-------------------------------------------------------------------------------

<img src="images/book.jpg" style="width:400px" />

-------------------------------------------------------------------------------

## What is literate programming?

<br />

> Let us change our traditional attitude to the construction of programs: Instead 
> of imagining that our main task is to instruct a __computer__ what to do, let us 
> concentrate rather on explaining to __human beings__ what we want a computer to do.

-------------------------------------------------------------------------------

## The `WEB` system

> `WEB` is a combination of two other languages
>
>  1. a document formatting language and 
>  2. a programming language. 
>
> I chose the name `WEB` partly because it was one of the few three-letter 
> words of English that hadn’t already been applied to computers. 

-------------------------------------------------------------------------------

## The `WEB` system

    [lang=text]
    @* Printing primes: An example of \WEB. 
    \[The program text below specifies the ``expanded meaning'';
    notice that it involves the top-level descriptions of three 
    other sections. When those top-level descriptions are replaced 
    by their expanded meanings, a syntactically correct \PASCAL\ 
    program will be obtained.\] 
    
    @<Program to print...@>= 
      program print_primes(output); 
      const @!m=1000; 
      @<Other constants of the program@>@; 
      var @<Variables of the program@>@; 
      begin @<Print the first |m| prime numbers@>; 
      end.

*******************************************************************************

## Literate programming with F#

<br /><br /><br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

-------------------------------------------------------------------------------
*)
(*** hide ***)
#load "../tomas/svg.fsx"
open FsReveal.SmartArt
open System.Drawing

module Diagrams = 
  let mks = 
    Split(Vertical, None, [
      Align(BottomMiddle, tx "F# + Markdown"); 
      Align(TopMiddle, tx "Source")
    ])

  let diag = 
    (mks, color "#1F5B56", Color.White) ==>
      [ (tx "F# Code", color "#325E6B", Color.White) ==>
          [ nd(tx "F# Interactive", color "#437E8E", Color.White)
            nd(tx "F# Project", color "#437E8E", Color.White) ]
        (tx "Markdown", color "#31683F", Color.White) ==>
          [ nd(tx "FsReveal", color "#438E49", Color.White)
            nd(tx "PDF Reports", color "#438E49", Color.White) 
            nd(tx "Documentation", color "#438E49", Color.White) ] ]
  let d1 = WithSize(800, 400, Draw(diag))
(**
## Literate programming with F#

<br /><br />

*)
(*** include-value: Diagrams.d1 ***)
(**

-------------------------------------------------------------------------------

## Literate slides with FsReveal

<br />

No F# demo is complete without the `|>` operator...
*)
(*** define-output:hello ***)
[ "Hello"; " "; "Krakow"; "!" ]
|> String.concat ""
|> printfn "%s"
(**
<br />

FsReveal embeds the output automatically:
*)
(*** include-output:hello ***)
(**

-------------------------------------------------------------------------------

## Literate slides with FsReveal

<br />
*)
(*** hide ***)
#load "../FsiMock.fs"
#load "../packages/FsLab/FsLab.fsx"
open Foogle
open System.Drawing
open FSharp.Data
(**
Get School Enrollment data from WorldBank
*)
let wb = WorldBankData.GetDataContext()
let cz = wb.Countries.``Czech Republic``.Indicators
let eu = wb.Countries.``European Union``.Indicators
(**
Compare Czech Republic and EU stats
*)
(*** define-output:chart ***)
Chart.LineChart
 ([ for y in 1971 .. 2012 ->
     string y, 
       [ cz.``School enrollment, tertiary (% gross)``.[y] 
         eu.``School enrollment, tertiary (% gross)``.[y] ] ],
  Labels = ["CZ"; "EU"])
(**
-------------------------------------------------------------------------------

## Literate slides with FsReveal

<br />

FsReveal embeds the chart for us!
*)
(*** include-it:chart ***)
(**

*******************************************************************************

## Looking under the cover...

<br /><br /><br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

-------------------------------------------------------------------------------

## Looking under the cover...

Enabled by the great F# community!

<br />
<div class="fragment">

 - **[F# Formatting](http://tpetricek.github.io/FSharp.Formatting/)** to process F# scripts & Markdown
 - **[F# Compiler Service](http://fsharp.github.io/FSharp.Compiler.Service/)** for tool tips & evaluation
 - **[FsReveal](http://fsprojects.github.io/FsReveal/)** to generate reveal.js slides
 - **[FsLab](http://www.fslab.org)** data-science libraries for charts
 - **[FAKE](http://fsprojects.github.io/FAKE) & [Suave](http://suave.io/)** to put things together & host

</div>

-------------------------------------------------------------------------------

## Literate F#: Using F# Scripts

<br />

Write an F# script with special comments

<br />

    (** Use _Markdown_ in comments. *)
    (*** define-output:hello ***)
    printfn "Hello world!"
    (** write code as usual... *)
    (*** include-output:hello ***)

-------------------------------------------------------------------------------

## Literate F#: Markdown mode

<br />

Write Markdown document with F# code snippets

<br />

    [lang=text]
    Write standard _Markdown_ document 

        [lang=fsharp]
        printfn "Hello world!"
    
    With embedded F# snippets

-------------------------------------------------------------------------------
*)
(*** hide ***)
module D2 =
  let MakeBox(backColor, content) =
    [ Fill(HtmlColor backColor, RoundedRectangle(10G, 10G)); 
      Fill(HtmlColor "#FFFFFF", Graphics.Text content)]
    |> Combine
    |> WithMargin (10G, 10G, 10G, 10G)
  let svg = 
    Split(Horizontal, None, [
      MakeBox("#1F5B56", "Diagrams")
      MakeBox("#325E6B", "..are..")
      MakeBox("#31683F", "boring :-(")
    ])
  let d2 = WithSize(800, 300, Align(Center, svg))
(**
## Literate F#: Diagrams

<br />

We can embed results and even charts. How about...

<div class="fragment">
*)
(*** include-value: D2.d2 ***)
(**
</div>

-------------------------------------------------------------------------------

## Literate F#: Diagrams

Domain specific language for building diagrams!
*)
open FsReveal.SmartArt

let MakeBox(backColor, content) =
  [ Fill(HtmlColor backColor, RoundedRectangle(10G, 10G)); 
    Fill(HtmlColor "#FFFFFF", Text content)]
  |> Combine |> WithMargin (10G, 10G, 10G, 10G)

let svg = Split(Horizontal, None, [
    MakeBox("#1F5B56", "Diagrams")
    MakeBox("#325E6B", "..are..")
    MakeBox("#31683F", "fun!")
  ])
(**

*******************************************************************************

## Literate tools for F#

<br /><br /><br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

-------------------------------------------------------------------------------

## FsLab Journal

<a href="images/FsLab_Tutorial1.html" target="_blank">
<img src="images/journal.png" style="border-style:none;width:450px" />
</a>

Download the [FsLab Journal Template](https://visualstudiogallery.msdn.microsoft.com/45373b36-2a4c-4b6a-b427-93c7a8effddb)!

-------------------------------------------------------------------------------

## ProjectScaffold docs

<a href="http://fsharp.github.io/FSharp.Data/library/WorldBank.html" target="_blank">
<img src="images/scaffold.png" style="border-style:none;width:550px" />
</a>

See [ProjectScaffold](fsprojects.github.io/ProjectScaffold/) on GitHub for more!

*******************************************************************************

## Conclusions

<br /><br /><br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

-------------------------------------------------------------------------------

### Literate scripting

Journal or notebooks for data science  
Walkthrough tutorials in documentation

<br />

### Literate software engineering

This is still an interesting problem!

<br /><br />

**Tomas Petricek**, F# Works  
[@tomaspetricek](http://twitter.com/tomaspetricek) 
| [http://tomasp.net](http://tomasp.net) 
| [http://fsharpworks.com](http://fsharpworks.com)

Thanks to Karlkim Suwanmongkol ([@kimsk](https://twitter.com/kimsk)) for creating FsReveal!

*)
