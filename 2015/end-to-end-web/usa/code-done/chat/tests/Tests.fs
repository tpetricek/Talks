#if INTERACTIVE
#r "../packages/FsCheck/lib/net45/FsCheck.dll"
#r "../packages/Suave/lib/net40/Suave.dll"
#r "../packages/NUnit/lib/nunit.framework.dll"
#load "../app.fsx"
#else
module ChatTests
#endif
open System
open NUnit.Framework
open FsCheck
open App
open Suave.Types

[<Test>]
let ``Requesting "/" returns HTTP 200``() =
  let ctx = HttpContext.empty
  let resp = App.app ctx |> Async.RunSynchronously
  Assert.IsTrue(resp.IsSome)
  Assert.AreEqual(HttpCode.HTTP_200, resp.Value.response.status)

[<Test>]
let ``Requesting "/chat" via GET returns HTTP 200``() =
  let req = { HttpRequest.empty with url = Uri("http://localhost/chat"); ``method`` = HttpMethod.GET }
  let ctx = { HttpContext.empty with request = req }
  let resp = App.app ctx |> Async.RunSynchronously
  Assert.IsTrue(resp.IsSome)
  Assert.AreEqual(HttpCode.HTTP_200, resp.Value.response.status)

[<Test>]
let ``Requesting "/chat" via POST returns HTTP 404``() =
  let req = { HttpRequest.empty with url = Uri("http://localhost/chat"); ``method`` = HttpMethod.POST }
  let ctx = { HttpContext.empty with request = req }
  let resp = App.app ctx |> Async.RunSynchronously
  Assert.IsTrue(resp.IsSome)
  Assert.AreEqual(HttpCode.HTTP_404, resp.Value.response.status)

[<Test>]
let ``Roundtripping does not lose messages`` () =
  Check.Quick(fun (s:string) ->
    s <> null ==> lazy
      let req = 
        { HttpRequest.empty with 
            url = Uri("http://localhost/post?name=Test")
            rawQuery = "name=Test"
            rawForm = System.Text.UTF8Encoding.UTF8.GetBytes(s)
            ``method`` = HttpMethod.POST }
      let ctx = { HttpContext.empty with request = req }
      App.app ctx |> Async.RunSynchronously |> ignore

      let req = { HttpRequest.empty with url = Uri("http://localhost/chat"); ``method`` = HttpMethod.GET }
      let ctx = { HttpContext.empty with request = req }
      let resp = App.app ctx |> Async.RunSynchronously
      Assert.IsTrue(resp.IsSome)
      Assert.AreEqual(HttpCode.HTTP_200, resp.Value.response.status)
      let html = 
        match resp.Value.response.content with
        | HttpContent.Bytes(bytes) -> System.Text.UTF8Encoding.UTF8.GetString(bytes)
        | _ -> ""
      Assert.IsTrue(html.Contains(": " + s + "</li>")) 
  )