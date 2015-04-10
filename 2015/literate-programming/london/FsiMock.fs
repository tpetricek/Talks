namespace global
module fsi =
  let AddPrinter(a) = ()

[<AutoOpen>]
module RecoverBangOperator = 
  let (!) (a:ref<_>) = a.Value 

(*
This does not help with the error in FsLab.fsx

namespace FSharp.Charting
type Chart =
  static member Bar(data:seq<'K * 'V>, ?Name, ?Title, ?Labels, ?Color, ?XTitle, ?YTitle) = ()
*)