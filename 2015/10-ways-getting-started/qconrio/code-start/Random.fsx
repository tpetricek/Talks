#r "packages/FsCheck/lib/net45/FsCheck.dll"
open FsCheck

let splitList l by = 
  l |> List.filter (fun n -> n < by),
  l |> List.filter (fun n -> n >= by)

splitList [1;2;3;4;5;6;7;8;9] 5

// DEMO: Length of two parts equals the original
// DEMO: No element from first is larger 
