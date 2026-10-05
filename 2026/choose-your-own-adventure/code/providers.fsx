#r "nuget: FSharp.Data"
open System.IO
open System.Diagnostics
open FSharp.Data


// TODO: Search for cat art
"https://openaccess-api.clevelandart.org/api/artworks/?q=cats&has_image=1&limit=10"


// DEMO: Use the type provider to build a page
let html = 
  [ for a in Art.GetSample().Data do
      $"<h1>{a}</h1>" + 
      $"<img src='{a}'>" ]

File.WriteAllText("demo.html", String.concat "" html)
Process.Start(ProcessStartInfo("demo.html", UseShellExecute = true))
