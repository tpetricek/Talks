(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/PasswordValidator"

(**
PasswordValidator
======================

A demo project that implements a simple password validation library

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      The PasswordValidator library can be <a href="https://nuget.org/packages/PasswordValidator">installed from NuGet</a>:
      <pre>PM> Install-Package PasswordValidator</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>

Example
-------

The following shows how to reference PasswordValidator in an F# script file and
use the validators to check basic properties of a password:
*)
#r "PasswordValidator.dll"
open PasswordValidator

let len = Validators.LengthRequirement
let upper = Validators.AsciiUpperRequirement

len.IsSatisfied "short"
len.IsSatisfied "long enough"
upper.IsSatisfied "UPPER case"
(**
Samples & documentation
-----------------------

The library comes with comprehensible documentation. 
It can include tutorials automatically generated from `*.fsx` files in [the content folder][content]. 
The API reference is automatically generated from Markdown comments in the library implementation.

 * [Tutorial in F#](tutorial-fsharp.html) contains a further explanation of this sample library from F#.

 * [Tutorial in C#](tutorial-csharp.html) contains a further explanation of this sample library from C#.

 * [API Reference](reference/index.html) contains automatically generated documentation for all types, modules
   and functions in the library. This includes additional brief samples on using most of the
   functions.
 
Contributing and copyright
--------------------------

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests. If you're adding a new public API, please also 
consider adding [samples][content] that can be turned into a documentation. You might
also want to read the [library design notes][readme] to understand how it works.

The library is available under Public Domain license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 

  [content]: https://github.com/fsprojects/PasswordValidator/tree/master/docs/content
  [gh]: https://github.com/fsprojects/PasswordValidator
  [issues]: https://github.com/fsprojects/PasswordValidator/issues
  [readme]: https://github.com/fsprojects/PasswordValidator/blob/master/README.md
  [license]: https://github.com/fsprojects/PasswordValidator/blob/master/LICENSE.txt
*)
