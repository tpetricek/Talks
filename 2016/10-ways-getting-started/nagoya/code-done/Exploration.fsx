open System.Text.RegularExpressions

let markdown = """
  [F#](http://www.fsharp.org) is a functional-first programming
  language that runs on [Windows](http://www.microsoft.com) using
  .NET and [Mac](http://www.apple.com) or [Linux](http://linux.org)
  using [Mono](http://www.mono-project.com)."""

let regLink = Regex("""\[([^\]]*)\]\(([^\)]*)\)""")

for m in regLink.Matches(markdown) do
  printfn "%s %s" m.Groups.[1].Value m.Groups.[2].Value
