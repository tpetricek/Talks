// ------------------------------------------------------------------
// Helper that displays HTML in a window
// ------------------------------------------------------------------

open System
open System.Windows.Forms
let wb = new WebBrowser(Dock=DockStyle.Fill)
let frm = new Form(Width=800, Height=600, Visible=true)
frm.Controls.Add(wb)
let showHtml html = 
  wb.DocumentText <- """<html>
    <link rel="stylesheet" href="C:\Tomas\Materials\Talks\2015\functional-libraries\code\packages\FSharp.Formatting\styles\style.css" />
    <script src="C:\Tomas\Materials\Talks\2015\functional-libraries\code\packages\FSharp.Formatting\styles\tips.js"></script>
    <style>body {padding:30px;font:130% 'pt sans'}
    table.pre td { padding:10px; } table.pre .lines { padding-right: 0px; } table.pre .snippet { padding-left:10px; }
    pre, pre.fssnip { padding:10px; font-size:14pt; font-family:consolas; } code { font-family:consolas } h1 { font:200% kreon; }
    li { margin-top:10px}</style><body>""" + html + "</body></html>"

// ------------------------------------------------------------------
// Demo: Using the F# Formatting library
// ------------------------------------------------------------------

let sampleMd = """
# Computing factorials
  
Everybody loves factorials. In F#, we can write them in both
functional style (using recursion) and imperatively (using
mutable state):

    let rec factorialFun n = 
      if n = 0 then 1
      else n * (factorialFun (n-1))

    let factorialImp n = 
      let mutable r = 1
      for i in 1 .. n do r <- r * i
      r

Now, we need to check if the two return the same thing by calling
`factorialFun 10` and `factorialImp 10`...
"""

#load "packages/FSharp.Formatting/FSharp.Formatting.fsx"
open FSharp.Markdown
open FSharp.Literate

Markdown.TransformHtml(sampleMd) |> showHtml

let doc = Literate.ParseMarkdownString(sampleMd) 
Literate.WriteHtml(doc) |> showHtml

// ------------------------------------------------------------------
// Demo: Using the F# Formatting library
// ------------------------------------------------------------------

let sampleFsx = """
(**
# Computing factorials
  
Everybody loves factorials. In F#, we can write them in both
functional style (using recursion) and imperatively (using
mutable state):
*) 

let rec factorialFun n = 
  if n = 0 then 1
  else n * (factorialFun (n-1))

let factorialImp n = 
  let mutable r = 1
  for i in 1 .. n do r <- r * i
  r

(**
F# Formatting can also handle special comments for embedding values:
*)

(*** include-value:factorialFun 10 ***) 
(*** include-value:factorialImp 10 ***) 
"""

let docFs = Literate.ParseScriptString(sampleFsx, fsiEvaluator = FsiEvaluator()) 
let docFsEval = Literate.FormatLiterateNodes(docFs)
Literate.WriteHtml(docFsEval) |> showHtml