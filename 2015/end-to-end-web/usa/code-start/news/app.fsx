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
// Hello world
// ----------------------------------------------------------------------------

let app = OK "Hello world!"


// ----------------------------------------------------------------------------
// Getting News from RSS feed and formatting it
// ----------------------------------------------------------------------------

// TODO: Get current news using XML type provider
// http://feeds.bbci.co.uk/news/rss.xml
// TODO: Read the news & add async


// ----------------------------------------------------------------------------
// Getting Weather information and formatting it
// ----------------------------------------------------------------------------

// TODO: Get current weather using JSON type provider
// http://api.openweathermap.org/data/2.5/forecast/daily?q=London,UK&mode=json&units=metric&cnt=10
// TODO: Read weather forecast
// TODO: Read weather forecast asynchronously & format


// ----------------------------------------------------------------------------
// Building asynchronous Suave server
// ----------------------------------------------------------------------------

// DEMO: Read HTML template
