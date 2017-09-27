module ManLang.Utils

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser

// ----------------------------------------------------------------------------
// Wrapper for XMLHttpRequest
// ----------------------------------------------------------------------------

type Http =
  static member Request(meth, url, ?data) =
    Async.FromContinuations(fun (cont, econt, _) ->
      let xhr = XMLHttpRequest.Create()
      xhr.``open``(meth, url, true)
      xhr.onreadystatechange <- fun _ ->
        if xhr.readyState > 3. && xhr.status = 200. then
          cont(xhr.responseText)
        if xhr.readyState > 3. && xhr.status = 0. then
          econt(System.Exception(meth + " " + url + " failed: " + xhr.statusText))
        obj()
      xhr.send(defaultArg data "") )

// ----------------------------------------------------------------------------
// Type declarations for Vega binding
// ----------------------------------------------------------------------------

type VegaData = 
  { values : obj[] }

type VegaFieldEncoding = 
  { field : string
    ``type`` : string }

type VegaEncoding =
  { x : VegaFieldEncoding
    y : VegaFieldEncoding }

type VegaSpec = 
  { ``$schema`` : string
    width : int option
    height : int option
    mark : string
    data : VegaData
    encoding : VegaEncoding }

type VegaOptions = 
  { actions : bool }

type Vega =
  abstract embed : string * VegaSpec * VegaOptions -> unit

// ----------------------------------------------------------------------------
// Bindings for top-level JS values
// ----------------------------------------------------------------------------

[<Emit("vega")>]
let vega : Vega = failwith "JS only"

[<Emit("JSON.parse($0)")>]
let jsonParse<'R> (str:string) : 'R = failwith "JS Only"

// ----------------------------------------------------------------------------
// Complex numbers for Hokusai demo
// ----------------------------------------------------------------------------

type Complex =
  | Complex of float * float
  static member Abs(Complex(r, i)) =
    let num1, num2 = abs r, abs i
    if (num1 > num2) then
      let num3 = num2 / num1
      num1 * sqrt(1.0 + num3 * num3)
    elif num2 = 0.0 then
      num1
    else
      let num4 = num1 / num2
      num2 * sqrt(1.0 + num4 * num4)
  static member (+) (Complex(r1, i1), Complex(r2, i2)) =
    Complex(r1+r2, i1+i2)

module ComplexModule =
  let Pow(Complex(r, i), power) =
    let num = Complex.Abs(Complex(r, i))
    let num2 = atan2 i r
    let num3 = power * num2
    let num4 = num ** power
    Complex(num4 * cos(num3), num4 * sin(num3))

