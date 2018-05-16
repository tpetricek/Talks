#nowarn "10001"
#load "packages/FsLab/FsLab.fsx"
open Deedle

let titanic = Frame.ReadCsv(__SOURCE_DIRECTORY__ + "/data/titanic.csv")

titanic
|> Frame.filterRows (fun _ row -> row.GetAs "Sex" = "female")
|> Stats.mean

titanic
|> Frame.groupRowsByString "Sex"
|> Frame.applyLevel fst Stats.mean
