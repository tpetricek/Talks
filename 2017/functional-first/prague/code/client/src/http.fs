module Http

open Fable.Core
open Fable.Import
open Fable.Import.Browser

[<Emit("JSON.stringify($0)")>]
let jsonStringify json : string = failwith "JS Only"

[<Emit("JSON.parse($0)")>]
let jsonParse<'R> (str:string) : 'R = failwith "JS Only"

type Http =
  static member Request(meth, url, ?data, ?cookies) =
    Async.FromContinuations(fun (cont, econt, _) ->
      let xhr = XMLHttpRequest.Create()
      xhr.``open``(meth, url, true)
      match cookies with 
      | Some cookies when cookies <> "" -> xhr.setRequestHeader("X-Cookie", cookies)
      | _ -> ()
      xhr.onreadystatechange <- fun _ ->
        if xhr.readyState > 3. && xhr.status = 200. then
          cont(xhr.responseText)
        if xhr.readyState > 3. && xhr.status = 0. then
          econt(System.Exception(meth + " " + url + " failed: " + xhr.statusText))
        obj()
      xhr.send(defaultArg data "") )
