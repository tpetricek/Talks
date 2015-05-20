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
    code { font-family:consolas; } pre { margin-left:30px; } h1 { font:200% kreon; }
    li { margin-top:10px}</style><body>""" + html + "</body></html>"

// ------------------------------------------------------------------
// Demo: Using the F# Formatting library
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

#load "packages/FSharp.Formatting/FSharp.Formatting.fsx"
open FSharp.Markdown

Markdown.TransformHtml(sample) |> showHtml