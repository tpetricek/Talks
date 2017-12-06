open System.Net
#load "packages/FsLab/FsLab.fsx"
open FSharp.Data

Http.Request
  ( "http://localhost:8086/",httpMethod="POST",
    body=HttpRequestBody.TextRequest """
      { "Id":"", "Item":"Hello world 2" }"""  )

type Todo = JsonProvider<"http://localhost:8086">
let todo = Todo.GetSamples()

for it in todo do
  printfn " - %s" it.Item
