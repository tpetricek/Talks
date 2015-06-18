#r "System.Xml.Linq"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "packages/Suave/lib/net40/Suave.dll"
open Suave
open Suave.Http.Successful
open Suave.Web
open Suave.Types
open System.IO
open System
open FSharp.Data

// ----------------------------------------------------------------------------
// Getting Weather information and formatting it
// ----------------------------------------------------------------------------

type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/forecast/daily?q=London,UK&mode=json&units=metric&cnt=10">

let formatWeather =
  sprintf "<li><h3>%s</h3><img src=\"http://openweathermap.org/img/w/%s.png\"><p>Day: %.0f°C</p><p>Night: %.0f°C</p></li>"

let toDateTime (timestamp:int) =
  let start = DateTime(1970,1,1,0,0,0,DateTimeKind.Utc)
  start.AddSeconds(float timestamp).ToLocalTime()

let getWeather() = async {
  let! res = Weather.AsyncLoad("http://api.openweathermap.org/data/2.5/forecast/daily?q=Seattle&mode=json&units=metric&cnt=10")
  return
    [ for item in res.List ->
      formatWeather ((toDateTime item.Dt).ToString("D"))
        item.Weather.[0].Icon item.Temp.Day item.Temp.Night ]
    |> String.concat "" }

// ----------------------------------------------------------------------------
// Getting News from RSS feed and formatting it
// ----------------------------------------------------------------------------

type RSS = XmlProvider<"http://feeds.bbci.co.uk/news/rss.xml">

let formatNews =
  sprintf "<li><img src=\"%s\" /><h3><a href=\"%s\">%s</a></h3><p>%s</p></li>"

let getNews() = async {
  let! res = RSS.AsyncGetSample()
  return
    [ for item in res.Channel.Items |> Seq.take 15 do
      let thumb = item.Thumbnails |> Seq.maxBy (fun t -> t.Width)
      yield formatNews thumb.Url item.Link item.Title item.Description ]
    |> String.concat "\n" }

// ----------------------------------------------------------------------------
// Building asynchronous Suave server
// ----------------------------------------------------------------------------

let app : WebPart = fun ctx -> async {
  let! news = getNews()
  let! weather = getWeather()
  let html = File.ReadAllText(__SOURCE_DIRECTORY__ + "/web/index.html")
  return! ctx |> OK(html.Replace("#1", news).Replace("#2", weather)) }
