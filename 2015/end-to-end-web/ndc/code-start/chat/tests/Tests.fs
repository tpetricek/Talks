module ChatTests

open System
open FsCheck
open Suave.Types
open NUnit.Framework

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
      // Send the message to the chat room
      let req =
        { HttpRequest.empty with
            url = Uri("http://localhost/post?name=Test")
            rawQuery = "name=Test"
            rawForm = System.Text.UTF8Encoding.UTF8.GetBytes(s)
            ``method`` = HttpMethod.POST }
      let ctx = { HttpContext.empty with request = req }
      App.app ctx |> Async.RunSynchronously |> ignore

      // Check that the message has been received
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
