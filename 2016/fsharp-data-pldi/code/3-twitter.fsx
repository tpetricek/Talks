#load "setup.fsx"
open FSharp.Data

type Tweet = JsonProvider<"data/timeline.json", SampleIsList=true>

let tweet = Tweet.Load("data/tweet2.json")
tweet.User.UtcOffset / 3600
