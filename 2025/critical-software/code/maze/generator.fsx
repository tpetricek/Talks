#r "nuget:FSharp.Data"
open System.IO
open FSharp.Data
open System.Net

type Goodreads = HtmlProvider<const(__SOURCE_DIRECTORY__ + "/temp/goodreads.htm")>

let wc = new WebClient()
let root = __SOURCE_DIRECTORY__
let (@@) a b = System.IO.Path.Combine(a, b)

for i, b in Seq.indexed (Goodreads.GetSample().Html.CssSelect("#booksBody tr")) do
  let details = b.CssSelect(".cover a").Head.Attribute("href").Value()
  wc.DownloadFile("https://www.goodreads.com" + details, root @@ $"temp/book-{i}.html")

type Book = HtmlProvider<const(__SOURCE_DIRECTORY__ + "/temp/book-0.html")>
for i in 0 .. 29 do
  let b = Book.Load(root @@ $"temp/book-{i}.html")
  let cover = b.Html.CssSelect(".BookCover img").Head.Attribute("src").Value()
  let title = b.Html.CssSelect("h1").Head.InnerText()
  let authors = b.Html.CssSelect(".ContributorLinksList").Head.InnerText().Replace("more", "")
  let descr = b.Html.CssSelect(".BookPageMetadataSection__description").Head.InnerText()
//  wc.DownloadFile(cover, root @@ $"covers/{i}.jpg")
  printfn ""
  printfn "<div class='book book-%d'>" i
  printfn "<div class='cover'><img src='covers/%d.jpg'></div>" i
  printfn "<div class='info'><h2>%s</h2>" title
  printfn "<p class='author'>%s</p>" authors
  printfn "<p class='descr'>%s</p></div></div>" descr

//         0123456789
let gr=[| "xxxxx    x"
          "x        x"
          "x        x"
          "xxxxxxxxxx"
          "    x     "
          "    x     "
          " xxxxxxxx " |]

gr |> Seq.reduce (+) |> Seq.filter ((=) 'x') |> Seq.length

let locs = 
  [ for row, line in Seq.indexed gr do 
      for col, mark in Seq.indexed line do
        if mark = 'x' then yield row, col ]

for i, (row, col) in Seq.indexed locs do
  printfn ".book-%d { grid-column:%d; grid-row:%d; }" i (col+1) (row+1)


