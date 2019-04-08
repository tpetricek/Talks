module GbpUsd
#I __SOURCE_DIRECTORY__
#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data
type Gbpusd = CsvProvider<const(__SOURCE_DIRECTORY__ + "/gbpusd.csv")>
let gbpusd = Gbpusd.GetSample().Rows