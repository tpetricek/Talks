#nowarn "85" "10001"
#load "packages/FsLab/FsLab.fsx"
open Deedle
open XPlot.Plotly

System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

// TODO: Load clean/avia.csv
// TODO: Filter rows to get non aggregate KILs
// TODO: Sum and drop missing columns
// DEMO: Do the same for rail transport
// DEMO: Create a column chart
