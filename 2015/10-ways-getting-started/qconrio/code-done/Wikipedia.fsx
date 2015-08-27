#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open System
open FSharp.Data

let us = Globalization.CultureInfo.GetCultureInfo("en-US")

type States = HtmlProvider<"https://en.wikipedia.org/wiki/States_of_Brazil">

[ for state in States.GetSample().Tables.``List of Brazilian states``.Rows ->
    state.State, Double.Parse(state.``Population (2014)``.Split('♠').[1], us) ]
|> Seq.sortBy snd
|> Seq.iter (printfn "%A")

