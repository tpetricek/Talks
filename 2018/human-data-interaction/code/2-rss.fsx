#r "System.Xml.Linq.dll"
#r "packages/FSharp.Data/lib/net45/FSharp.Data.dll"
open FSharp.Data

// http://feeds.bbci.co.uk/news/politics/rss.xml
// http://feeds.bbci.co.uk/news/world/rss.xml
// Format date using "D"

type Rss = XmlProvider<"http://feeds.bbci.co.uk/news/world/rss.xml">
let rss = Rss.Load("http://feeds.bbci.co.uk/news/politics/rss.xml")

for it in rss.Channel.Items do
  printfn " * %s (%s)" it.Title (it.PubDate.ToString "D")

