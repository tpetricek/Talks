// ------------------------------------------------------------------
// Helper that displays HTML in a window
// ------------------------------------------------------------------

open System
open System.Windows.Forms
let wb = new WebBrowser(Dock=DockStyle.Fill)
let frm = new Form(Width=800, Height=600, Visible=true)
frm.Controls.Add(wb)
let showHtml html = 
  wb.DocumentText <- """<html><style>body {padding:30px;font:130% 'pt sans'}
    pre { font-family:consolas; margin-left:30px; } h1 { font:200% kreon; }
    li { margin-top:10px}</style><body>""" + html + "</body></html>"

// ------------------------------------------------------------------
// Markdown domain model
// ------------------------------------------------------------------

type MarkdownDocument = list<MarkdownBlock>

and MarkdownBlock = 
  | Heading of int * MarkdownSpans
  | Paragraph of MarkdownSpans
  | CodeBlock of list<string>

and MarkdownSpans = list<MarkdownSpan>

and MarkdownSpan =
  | Literal of string
  | InlineCode of string
  | Strong of MarkdownSpans
  | Emphasis of MarkdownSpans
  | HyperLink of MarkdownSpans * string
  | HardLineBreak

// ------------------------------------------------------------------
// Markdown spans parser
// ------------------------------------------------------------------

let toString chars =
  System.String(chars |> Array.ofList)

let (|StartsWith|_|) prefix list =
  let rec loop = function
    | [], rest -> Some(rest)
    | p::prefix, r::rest when p = r -> loop (prefix, rest)
    | _ -> None
  loop (prefix, list)

let rec parseBracketedBody closing acc = function
  | StartsWith closing (rest) -> Some(List.rev acc, rest)
  | c::chars -> parseBracketedBody closing (c::acc) chars
  | _ -> None
  
let (|Bracketed|_|) opening closing = function
  | StartsWith opening chars -> parseBracketedBody closing [] chars
  | _ -> None
let (|Delimited|_|) delim = (|Bracketed|_|) delim delim

let rec parseSpans acc chars = seq {
  let emitLiteral() = seq {
    if acc <> [] then 
      yield acc |> List.rev |> toString |> Literal }

  match chars with
  | StartsWith [' '; ' '; '\n'; '\r'] chars
  | StartsWith [' '; ' '; '\n' ] chars
  | StartsWith [' '; ' '; '\r' ] chars -> 
      yield! emitLiteral ()
      yield HardLineBreak
      yield! parseSpans [] chars
  | Delimited ['`'] (body, chars) ->
      yield! emitLiteral ()
      yield InlineCode(toString body)
      yield! parseSpans [] chars
  | Delimited ['*'; '*' ] (body, chars)
  | Delimited ['_'; '_' ] (body, chars) ->
      yield! emitLiteral ()
      yield Strong(parseSpans [] body |> List.ofSeq)
      yield! parseSpans [] chars
  | Delimited ['*' ] (body, chars)
  | Delimited ['_' ] (body, chars) ->
      yield! emitLiteral ()
      yield Emphasis(parseSpans [] body |> List.ofSeq)
      yield! parseSpans [] chars
  | Bracketed ['['] [']'] (body, Bracketed ['('] [')'] (url, chars)) ->
      yield! emitLiteral ()
      yield HyperLink(parseSpans [] body |> List.ofSeq, toString url)
      yield! parseSpans [] chars
  | c::chars ->
      yield! parseSpans (c::acc) chars
  | [] ->
      yield! emitLiteral () }

// Examples: Parsing single-paragraph Markdown texts
"hello  \nworld  \n!!!" |> List.ofSeq |> parseSpans [] |> List.ofSeq    
"**`hello` world** and _emph_" |> List.ofSeq |> parseSpans [] |> List.ofSeq

// ------------------------------------------------------------------
// Markdown paragraphs parser
// ------------------------------------------------------------------

module List = 
  let partitionWhile f = 
    let rec loop acc = function
      | x::xs when f x -> loop (x::acc) xs
      | xs -> List.rev acc, xs
    loop [] 

let (|LineSeparated|) lines =
  let isWhite = System.String.IsNullOrWhiteSpace
  match lines |> List.partitionWhile (isWhite >> not) with
  | par, _::rest | par, ([] as rest) -> par, rest
    
let (|AsCharList|) (str:string) = 
  str |> List.ofSeq

let (|PrefixedLines|) prefix (lines:list<string>) = 
  let prefixed, other = lines |> List.partitionWhile (fun line -> line.StartsWith(prefix))
  [ for line in prefixed -> line.Substring(prefix.Length) ], other

let rec parseBlocks lines = seq {
  match lines with
  | AsCharList(StartsWith ['#'; ' '] heading)::rest ->
      yield Heading(1, parseSpans [] heading |> List.ofSeq)
      yield! parseBlocks rest
  | AsCharList(StartsWith ['#'; '#'; ' '] heading)::rest ->
      yield Heading(2, parseSpans [] heading |> List.ofSeq)
      yield! parseBlocks rest
  | PrefixedLines "    " (body, rest) when body <> [] ->
      yield CodeBlock(body)
      yield! parseBlocks rest
  | LineSeparated (body, rest) when body <> [] -> 
      let body = String.concat " " body |> List.ofSeq
      yield Paragraph(parseSpans [] body |> List.ofSeq)
      yield! parseBlocks rest 
  | line::rest when System.String.IsNullOrWhiteSpace(line) ->
      yield! parseBlocks rest 
  | _ -> () }

// ------------------------------------------------------------------
// Markdown to HTML convertor
// ------------------------------------------------------------------

open System.IO

// Helper function that generates a simple HTML element
let outputElement (output:TextWriter) (tag:string) attributes body =
  let attrString = String.concat " " [ for k, v in attributes -> k + "=\"" + v + "\"" ]
  output.Write("<" + tag + attrString + ">")
  body () 
  output.Write("</" + tag + ">")

let rec formatSpan (output:TextWriter) = function
  | Literal(str) -> output.Write(str)
  | InlineCode(code) -> output.Write("<code>" + code + "</code>")
  | Strong(spans) -> 
      outputElement output "strong" [] (fun () -> 
        spans |> List.iter (formatSpan output))
  | Emphasis(spans) ->
      outputElement output "em" [] (fun () ->
        spans |> List.iter (formatSpan output))
  | HyperLink(spans, url) ->
      outputElement output "a" ["href", url] (fun () ->
        spans |> List.iter (formatSpan output))
  | HardLineBreak -> 
      output.Write("<br />") // Exercise!

let rec formatBlock (output:TextWriter) = function
  | Heading(size, spans) ->
      outputElement output ("h" + size.ToString()) [] (fun () ->
        spans |> List.iter (formatSpan output))
  | Paragraph(spans) ->
      outputElement output "p" [] (fun () ->
        spans |> List.iter (formatSpan output))
  | CodeBlock(lines) ->
      outputElement output "pre" [] (fun () ->
        lines |> List.iter output.WriteLine )

// ------------------------------------------------------------------
// Example: Parsing a complete Markdown document!
// ------------------------------------------------------------------

let sample = """
# The F# language
  
F# is a **programming language** that supports _functional_, as       
well as _object-oriented_ and _imperative_ programming styles.        
Hello world can be written as follows:                                

    printfn "Hello world!"                                            

For more information, see the [F# home page](http://fsharp.org) or 
read [Real-World Functional Programming](http://manning.com/petricek) 
published by [Manning](http://manning.com).
"""

let doc = parseBlocks (sample.Split('\r', '\n') |> List.ofSeq) |> List.ofSeq

let sb = System.Text.StringBuilder()
let output = new StringWriter(sb)
doc |> Seq.iter (formatBlock output)
sb.ToString() |> showHtml

