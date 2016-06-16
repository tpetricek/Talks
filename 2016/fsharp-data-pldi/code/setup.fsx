#r "System.Xml.Linq.dll"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open System.Text.RegularExpressions
let reg = Regex("\*([^\*]*)\*")

fsi.AddHtmlPrinter(fun (s:seq<string>) ->
  let lis =
    s
    |> Seq.map (fun s -> s.Replace("\n", "<br />"))
    |> Seq.map (fun s -> reg.Replace(s, fun m -> sprintf "<strong>%s</strong>" m.Groups.[1].Value))
    |> Seq.map (sprintf "<li>%s</li>") |> String.concat ""
  seq [ "style", """<style type="text/css">.bigu { font-size:16pt; }</style>"""],
  sprintf "<ul class='bigu'>%s</ul>" lis)
