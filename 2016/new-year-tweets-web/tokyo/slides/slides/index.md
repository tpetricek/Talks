- title : Celebrating New Year's Eve with Suave and D3
- description : You would expect that the largest number of "Happy New Year" tweets would in every
    part of the world would appear around the midnight of December 31. But can we nicely visualize
    the live stream of tweets and see the tweets live as the old year comes to its end? I'll talk
    about a project I did for New Year's Eve 2015-2016 that does exactly this!
- author : Tomas Petricek
- theme : night
- transition : none

****************************************************************************************************
 - data-background-color: black

# Celebrating New Year's Eve<br /><small>with Suave and D3</small>

<img src="images/fireworks.png" style="margin-top:10px;height:350px" />

**Tomas Petricek**  
[@tomaspetricek](http://twitter.com/tomaspetricek) | [fsharpworks.com](http://fsharpworks.com)

---
 - data-background-color: white

<div style="background:url(images/advent.png) center/contain no-repeat;height:600px">
</div>

---

# What should I write about?

<img src="images/what.jpg" style="height:300px" />

---

<img src="images/demo.gif" style="height:700px" class="fragment" />

---

<img src="images/nada.png" style="height:700px" />

---

<img src="images/nepal.png" style="height:650px" />

---

<img src="images/nada.png" style="height:700px" />

****************************************************************************************************
- data-background-color: black

# **DEMO**
# Listening to Tweets

<img src="images/fireworks.png" style="margin-top:50px;height:350px" />

---
- data-background-color: white

<img src="images/fstoolbox.png" style="height:600px" />


---

# Adding Trends API

<br />

    type TrendsAvailable = JsonProvider<"json/trends_available.json">
    type TrendsPlace = JsonProvider<"json/trends_place.json">
    type TrendsClosest = JsonProvider<"json/trends_closest.json">

<br />

    type  Trends (context:TwitterContext) =
      member t.Available () =
        let res = TwitterRequest(context).RequestRawData
          ("https://api.twitter.com/1.1/trends/available.json", [])
        TwitterTypes.TrendsAvailable.Parse(res)

****************************************************************************************************
- data-background-color: black

# **DEMO**
# Processing Tweets

<img src="images/fireworks.png" style="margin-top:50px;height:350px" />

---

# Implementing Reactive Extensions

<br />
<div class="fragment">

    liveTweets
    |> Observable.limitRate 50

</div>
<div class="fragment">

    let event = Event<'T>()
    let agent = MailboxProcessor.Start(fun inbox ->
      let rec loop (lastTime:DateTime) = async {
        let! e = inbox.Receive()
        let now = DateTime.UtcNow
        if (now - lastTime).TotalMilliseconds > milliseconds then
          event.Trigger(e)
          return! loop now
        else
          return! loop lastMessageTime }
      loop DateTime.MinValue )

</div>

****************************************************************************************************
- data-background-color: black

# **DEMO**
# Geolocating Users

<img src="images/fireworks.png" style="margin-top:50px;height:350px" />

---
- data-background-color: white

<img src="images/star.png" class="fragment" />



****************************************************************************************************
- data-background-color: black

# SUMMARY

---
- data-background-color: black

# Have F#un! :-)

<img src="images/fireworks.png" style="margin-top:50px;height:350px" />

Tomas Petricek | [@tomaspetricek](http://twitter.com/tomaspetricek) | [fsharpworks.com](http://fsharpworks.com)
