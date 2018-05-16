#nowarn "10001"
#load "packages/FsLab/FsLab.fsx"
open Deedle

let titanic = Frame.ReadCsv(__SOURCE_DIRECTORY__ + "/data/titanic.csv")
titanic

// TODO: Filter rows by gender and mean
// DEMO: Group by gender and aggregate
