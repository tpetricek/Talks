module ManLang.Demo2

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open ManLang.Utils

let enigma = "https://api.enigma.io/v2/data/fmM23CALLVTvTe17HoQrUewAvccynq5ISqs7T21iLw3jztU1hOWSV"
let data = enigma + "/us.gov.whitehouse.salaries.2016"

let work () = 
  // TODO: request /sort/name+/limit/20
  // TODO: parse JSON and get result
  // DEMO: create Vegaspec
  // TODO: vega into #chart element
  ()

let whitehouse () = 
  if document.getElementById("chart") <> null then
    work ()
