#r "packages/Suave/lib/net461/Suave.dll"
open Suave
open Suave.Writers
open Suave.Filters
open Suave.Operators
open System
open System.Net
open System.Collections.Concurrent

let root = "https://en.wikipedia.org"

let cache = ConcurrentDictionary<_, _>()

let app =
  setHeader  "Access-Control-Allow-Origin" "*"
  >=> setHeader "Access-Control-Allow-Methods" "GET,POST,OPTIONS,DELETE"
  >=> setHeader "Access-Control-Allow-Headers" "content-type"
  >=> pathScan "/%s" (fun p ctx -> async {
      if cache.ContainsKey(p) then
        return! Successful.OK (cache.[p]) ctx
      else
        let wc = new WebClient()
        let! html = wc.AsyncDownloadString(Uri(root + "/" + p))
        let html = html.Replace(root, "http://localhost:8011")
        cache.TryAdd(p, html) |> ignore
        return! Successful.OK html ctx })

let cfg = { defaultConfig with bindings = [ HttpBinding.createSimple HTTP "127.0.0.1" 8011 ] }
startWebServer cfg app
