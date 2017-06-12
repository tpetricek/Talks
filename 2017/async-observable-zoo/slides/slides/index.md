- title : Observables, Events, Asynchronous Sequences and Other Wild Animals 
- description : 
- author : Tomas Petricek
- theme : white
- transition : none

****************************************************************************************************

## Observables, Events, Asynchronous Sequences and Other Wild Animals

<br /><br /><br /><br /><br />

**Tomas Petricek**, fsharpWorks & Alan Turing Institute <br />
[fsharpworks.com](http://fsharpworks.com) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

****************************************************************************************************

# Asyncs and tasks

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

# Asynchronous workflows

<br />
 
    type Async<'T> = 
      abstract Start : ('T -> unit) -> unit

 - Computation, not a task
 - Calls continuation once

---------------------------------------------------------------------------------------------------

# Tasks 

<br />
 
    type Task<'T> = 
      abstract Value : option<'T> 
      abstract OnCompleted : (unit -> unit) -> unit

 - Running task, not a computation
 - Inherently mutable
 
****************************************************************************************************

# Events and observables

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

# Observable

<br />
 
    type IObservable<'T> = 
      abstract AddHandler : ('T -> unit) -> (unit -> unit)

 - Calls continuation repeatedly
 - Returns function for cancelling subscription
 
****************************************************************************************************

# Async sequences

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

# Async sequences

<br />
 
    type AsyncSeq<'T> = Async<AsyncSeqRes<'T>>
    and AsyncSeqRes<'T> = 
      | Nil
      | Cons of 'T * AsyncSeq<'T>

 - Asynchronously ask for next value
 - Pull-based, not push-based
 
****************************************************************************************************

# Summary

Events, observables and async sequences

<br />
<img src="images/literate.png" style="width:200px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

<h2 class="fragment">If it is not easy, it is impossible!</h2>

_**Async**_ for asynchronous computations<br />
_Tasks_ do not compose well

_**Observables**_ for push-based streams<br />
or _Events_ if you want state sharing

_**Async sequences**_ for pull-based streams

<br /><br />

[fsharpworks.com](http://fsharpworks.com) | [tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)
