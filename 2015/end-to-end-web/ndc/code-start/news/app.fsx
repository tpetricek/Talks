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
// Hello world
// ----------------------------------------------------------------------------

let app = OK "Hello world!"



















// ----------------------------------------------------------------------------

// DEMO: Define the domain model

// TODO: Get current news from BBC
// (http://feeds.bbci.co.uk/news/rss.xml)
// TODO: Display using DotLiquid page

// TODO: Get current weather using JSON type provider
// (http://api.openweathermap.org/data/2.5/forecast/daily?q=London,UK&mode=json&units=metric&cnt=10)
// DEMO: Covert UNIX time stamps

// DEMO: Add async entry-point
// TODO: Make the data reading async

// TODO: Define NewsFilters.niceDate ('D') & add to template
// TODO: Register filters by name
