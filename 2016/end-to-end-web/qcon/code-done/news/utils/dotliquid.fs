module Suave.DotLiquid

open System
open System.IO
open DotLiquid

let registerFiltersByName name =
  let asm = System.Reflection.Assembly.GetExecutingAssembly()
  let typ =
    [ for t in asm.GetTypes() do
        if t.FullName.EndsWith(name) && not(t.FullName.Contains("<StartupCode")) then yield t ]
    |> Seq.last
  Template.RegisterFilter(typ)
