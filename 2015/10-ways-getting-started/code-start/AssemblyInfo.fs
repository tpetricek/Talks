namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Demo")>]
[<assembly: AssemblyProductAttribute("Demo")>]
[<assembly: AssemblyDescriptionAttribute("This is just a demo")>]
[<assembly: AssemblyVersionAttribute("1.0.1")>]
[<assembly: AssemblyFileVersionAttribute("1.0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0.1"
