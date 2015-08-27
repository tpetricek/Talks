#r "packages/FsCheck/lib/net45/FsCheck.dll"
open FsCheck

let splitList l by = 
  l |> List.filter (fun n -> not (n > by)),
  l |> List.filter (fun n -> n > by)

splitList [1;2;3;4;5;6;7;8;9] 5
splitList [nan;5.0] 5.0

Check.Quick(fun l (v:float) ->
  let l1, l2 = splitList l v
  l1.Length + l2.Length = l.Length )

Check.Quick(fun l (v:float) ->
  let l1, l2 = splitList l v
  l1 |> List.forall (fun v1 ->
    l2 |> List.forall (fun v2 ->
      v1 <= v2 )))
