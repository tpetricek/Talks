#load "data/gbpusd.fsx"

GbpUsd.gbpusd
|> Seq.pairwise
|> Seq.filter (fun (before, after) -> before.GbpUsd - after.GbpUsd > 0.02M)
|> Seq.map (fun (before, after) -> before.Date.ToShortDateString())


