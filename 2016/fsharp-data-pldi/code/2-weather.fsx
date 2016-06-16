#load "setup.fsx"
open FSharp.Data

type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/forecast/daily?units=metric&q=Prague&APPID=cb63a1cf33894de710a1e3a64f036a27">

let w = Weather.Load("http://api.openweathermap.org/data/2.5/forecast/daily?units=metric&q=Santa+Barbara&APPID=cb63a1cf33894de710a1e3a64f036a27")

let getTemp (it:Weather.List) =
  sprintf "*Day* %A / *Night*: %A" it.Temp.Day it.Temp.Night

let getRain (it:Weather.List) =
  match it.Rain with
  | Some rain -> " / *Rain*: " + string rain
  | _ -> ""

[ for d in w.List ->
    getTemp d + getRain d ]
