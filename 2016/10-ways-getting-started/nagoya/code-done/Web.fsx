#r "packages/Suave/lib/net40/Suave.dll"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data
open Suave
open Suave.Web
open Suave.Http
open Suave.Filters
open Suave.Operators

type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/forecast/daily?units=metric&q=Prague">

let getTemp place = 
  let w = Weather.Load("http://api.openweathermap.org/data/2.5/forecast/daily?units=metric&q=" + place)
  let body = 
    [ for t in w.List ->
        sprintf "<li>%f</li>" t.Temp.Day ]
    |> String.concat ""
  Successful.OK(w.City.Name + ", " + w.City.Country + "<ul>" + body + "</ul>")

startWebServer defaultConfig (choose [ pathScan "/city/%s" getTemp; getTemp "Rio+de+Janeiro"])