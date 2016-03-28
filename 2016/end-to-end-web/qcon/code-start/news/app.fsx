#r "System.Xml.Linq"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "packages/Suave/lib/net40/Suave.dll"
#r "packages/DotLiquid/lib/NET45/DotLiquid.dll"
#r "packages/Suave.DotLiquid/lib/net40/Suave.DotLiquid.dll"
#load "utils/dotliquid.fs"
open Suave
open Suave.Web
open System.IO
open System
open FSharp.Data
open Suave.Successful

// ----------------------------------------------------------------------------
// Hello world
// ----------------------------------------------------------------------------

let app = OK "Hello world!"









// ----------------------------------------------------------------------------

// DEMO: Define the domain model

// TODO: Get current news from BBC
//  - http://feeds.bbci.co.uk/news/rss.xml
//  - http://feeds.bbci.co.uk/news/world/latin_america/rss.xml
// TODO: Display using DotLiquid page

// TODO: Get current weather using JSON type provider
//  - http://api.openweathermap.org/data/2.5/forecast/daily?units=metric&q=Prague&APPID=cb63a1cf33894de710a1e3a64f036a27
// DEMO: Covert UNIX time stamps

// DEMO: Add async entry-point
// TODO: Make the data reading async

// TODO: Define NewsFilters.niceDate ('D') & add to template
// TODO: Register filters by name
