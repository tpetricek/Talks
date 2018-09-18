#r "packages/FSharp.Data/lib/net45/FSharp.Data.dll"
open FSharp.Data

type Tweet = JsonProvider<"data/timeline.json", SampleIsList=true>

let tweet = Tweet.Load("data/tweet1.json")
tweet.User.UtcOffset / 3600
