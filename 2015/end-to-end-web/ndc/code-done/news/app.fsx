#r "System.Xml.Linq"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "packages/Suave/lib/net40/Suave.dll"
#r "packages/DotLiquid/lib/NET45/DotLiquid.dll"
#load "utils/dotliquid.fs"
open Suave
open Suave.Http.Successful
open Suave.Web
open Suave.Types
open System.IO
open System
open FSharp.Data

// ----------------------------------------------------------------------------
// Domain model for the F# Times homepage
// ----------------------------------------------------------------------------

type News =
  { ThumbUrl : string
    LinkUrl : string
    Title : string
    Description : string }

type Weather =
  { Date : DateTime
    Icon : string
    Day : int
    Night : int }

type Home =
  { News : seq<News>
    Weather : seq<Weather> }

// ----------------------------------------------------------------------------
// Getting Weather information and formatting it
// ----------------------------------------------------------------------------

type Forecast = JsonProvider<"http://api.openweathermap.org/data/2.5/forecast/daily?q=London,UK&mode=json&units=metric&cnt=10">

let toDateTime (timestamp:int) =
  let start = DateTime(1970,1,1,0,0,0,DateTimeKind.Utc)
  start.AddSeconds(float timestamp).ToLocalTime()

let getWeather() = async {
  let! res = Forecast.AsyncLoad("http://api.openweathermap.org/data/2.5/forecast/daily?q=Seattle&mode=json&units=metric&cnt=10")
  return
    [ for item in res.List ->
      { Date = toDateTime item.Dt
        Icon = item.Weather.[0].Icon
        Day = int item.Temp.Day
        Night = int item.Temp.Night } ] }

// ----------------------------------------------------------------------------
// Getting News from RSS feed and formatting it
// ----------------------------------------------------------------------------

type RSS = XmlProvider<"http://feeds.bbci.co.uk/news/rss.xml">

let getNews() = async {
  let! res = RSS.AsyncGetSample()
  return
    [ for item in res.Channel.Items |> Seq.take 15 do
      if item.Thumbnails |> Seq.length > 0 then
        let thumb = item.Thumbnails |> Seq.maxBy (fun t -> t.Width)
        yield
          { ThumbUrl = thumb.Url; LinkUrl = item.Link;
            Title = item.Title; Description = item.Description } ] }

// ----------------------------------------------------------------------------
// Building asynchronous Suave server
// ----------------------------------------------------------------------------

module NewsFilters =
  let niceDate (dt:DateTime) = dt.ToString("D")

DotLiquid.registerFiltersByName "NewsFilters"

// ----------------------------------------------------------------------------
// Building asynchronous Suave server
// ----------------------------------------------------------------------------

let app : WebPart = fun ctx -> async {
  let! news = getNews()
  let! weather = getWeather()
  return! DotLiquid.page "index.html" { News = news; Weather = weather } ctx }
