#r "System.Xml.Linq.dll"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data

type Rss = XmlProvider<"http://feeds.bbci.co.uk/news/world/rss.xml">
let rss = Rss.GetSample()

for s in rss.Channel.Items do
  printfn "%s (%s)" s.Title (s.PubDate.ToString("D"))
