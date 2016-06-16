#load "setup.fsx"
open FSharp.Data

type Rss = XmlProvider<"http://feeds.bbci.co.uk/news/world/rss.xml">
let rss = Rss.GetSample()

for s in rss.Channel.Items do
  printfn "%s (%s)" s.Title (s.PubDate.ToString("D"))
