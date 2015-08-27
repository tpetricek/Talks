#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data

type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/forecast/daily?units=metric&q=Prague">

let w = Weather.Load("http://api.openweathermap.org/data/2.5/forecast/daily?units=metric&q=Rio+de+Janeiro")
for t in w.List do
  printfn "%A" t.Temp.Day
