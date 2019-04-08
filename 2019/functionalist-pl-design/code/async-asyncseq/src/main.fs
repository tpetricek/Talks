module AsyncReactive.Main

open System
open Fable.Core
open Fable.Import
open Fable.Import.Browser
open System.Collections.Generic
open AsyncReactive.Helpers

// TODO: Async loop to control traffic lights
// TODO: Async.AwaitGuiEvent Section1.next.addEventListener_click
// TODO: Using `for` and `while` loops

show "section1"
Section1.light.style.backgroundColor <- "green"

// DEMO: Async price reading
// TODO: Refactoring using AsyncSeq
// TODO: Pairwise and change color