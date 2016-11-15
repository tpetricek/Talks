#I @"packages/FSharp.Data.Toolbox.Twitter/lib/net40"
#I @"packages/FSharp.Data/lib/net40"
#r "packages/Suave/lib/net40/Suave.dll"
#r "FSharp.Data.Toolbox.Twitter.dll"
#r "FSharp.Data.dll"

#load "async.fs"
#load "config.fsx"

open System
open System.Web
open System.Text
open System.Collections.Generic
open FSharp.Data
open FSharp.Data.Toolbox.Twitter
open AsyncHelpers

open Suave
open Suave.Web
open Suave.Http
open Suave.Filters
open Suave.Operators
open Suave.Sockets
open Suave.Sockets.Control
open Suave.Sockets.AsyncSocket
open Suave.WebSocket
open Suave.Utils

// --------------------------------------------------------------------------------------
// STEP #1: Connecting to Twitter & streaming
// --------------------------------------------------------------------------------------

let ctx =
  { ConsumerKey = Config.TwitterKey; ConsumerSecret = Config.TwitterSecret;
     AccessToken = Config.TwitterAccessToken; AccessSecret = Config.TwitterAccessSecret }
let twitter = Twitter(UserContext(ctx))

let places = twitter.Trends.Available()
let ldn = places |> Seq.find (fun pl -> pl.Name = "London")

let trends = twitter.Trends.Place(ldn.Woeid).[0]
let phrases = [ for t in trends.Trends -> t.Name ]


type Tweet =
  { Tweeted : DateTime
    Text : string
    OriginalArea : string
    UserName : string
    UserScreenName : string
    PictureUrl : string
    OriginalLocation : option<decimal * decimal>
    Phrase : int
    IsRetweet : bool
    InferredLocation : option<decimal * decimal> }


let search = twitter.Streaming.FilterTweets(phrases)

let liveTweets =
  search.TweetReceived
  |> Observable.map (fun status ->
    // Parse the location, if the tweet has it...
    let origLocation =
      match status.Geo with
      | Some(geo) when geo.Type = "Point" && geo.Coordinates.Length = 2 ->
          Some(geo.Coordinates.[0], geo.Coordinates.[1])
      | _ -> None

    // Get user name, text of the tweet and location
    match status.User, status.Text with
    | Some user, Some text ->
        let phrase = phrases |> Seq.tryFindIndex (fun phrase ->
          text.ToLower().Contains(phrase))
        match user.Location.String, phrase with
        | Some userLocation, Some phrase ->
            let rt = text.StartsWith("RT") || status.Retweeted = Some true
            { Tweeted = DateTime.UtcNow; OriginalArea = userLocation; Text = text
              PictureUrl = user.ProfileImageUrl; UserScreenName = user.ScreenName
              OriginalLocation = origLocation; UserName = user.Name; Phrase = phrase
              InferredLocation = None; IsRetweet = rt; } |> Some
        | _ -> None
    | _ -> None )
  |> Observable.choose id

search.Start()

let cleanup() = search.Stop()


// DEMO: Connect to twitter
// TODO: Get trending phrases
// TODO: Print tweets, GEO codes and Locations
// DEMO: Define the type for tweets
// DEMO: Create 'liveTweets' observable & start it
// TODO: let cleanup() = search.Stop()

// --------------------------------------------------------------------------------------
// STEP #2: Creating Suave web site
// --------------------------------------------------------------------------------------

type JsonTypes = JsonProvider<"""{
    "socketFeedTweet":
      { "text":"hello", "originalArea":"World",
        "picture":"http://blah.jpg", "name":"sillyjoe", "screenName": "Silly Joe" },
    "socketMapTweet":
      { "latitude":50.07, "longitude":78.43, "text":"hello", "originalArea":"World",
        "picture":"http://blah.jpg", "name":"sillyjoe", "screenName": "Silly Joe" },
    "socketPhrase":
      [ {"phrase":"Happy new year", "count":10}, { "phrase":"manigong bagong taon", "count":5 } ],
    "timeZoneInfo":
      { "countries": [ {"country": "UK", "zone": "UTC" }, {"country": "UK", "zone": "UTC" } ],
        "zones": [ "UTC", "UTC+00:00" ] }
  }""">

let feedTweets =
  liveTweets
  |> Observable.filter (fun t -> not t.IsRetweet)
  |> Observable.limitRate 850
  |> Observable.map (fun tweet ->
    JsonTypes.SocketFeedTweet(tweet.Text, tweet.OriginalArea, tweet.PictureUrl,
      tweet.UserName, tweet.UserScreenName).JsonValue.ToString())
  |> Observable.start

let socketOfObservable (updates:IObservable<string>) (webSocket:WebSocket) cx = socket {
  while true do
    let! update = updates |> Async.AwaitObservable |> Suave.Sockets.SocketOp.ofAsync
    do! webSocket.send Text (Encoding.UTF8.GetBytes update) true }

// TODO: Browse 'web' directory and the 'index.html' file (Files module)
// DEMO: Define JSON types for the results using JSON TP
// TODO: feedTweets skips RTs, limits rate (850ms) & DEMO: returns JSON type & Observable.start (!)
// DEMO: Socket of observable helper
// TODO: Use 'handShake' to expose sockets at '/feedtweets'

// --------------------------------------------------------------------------------------
// STEP #3: Geolocting and adding map
// --------------------------------------------------------------------------------------

let [<Literal>] Sample = "http://www.mapquestapi.com/geocoding/v1/address?location=Prague&key=" + Config.MapQuestKey
type Mapquest = JsonProvider<Sample>

let locate place = async {
  let! locs = Mapquest.AsyncLoad("http://www.mapquestapi.com/geocoding/v1/address?location=" + place + "&key=" + Config.MapQuestKey)
  return
    locs.Results
    |> Seq.collect (fun r -> r.Locations)
    |> Seq.map (fun l -> l.LatLng.Lat, l.LatLng.Lng)
    |> Seq.tryHead }

let replay = SchedulerAgent<Tweet>()

// Handle tweets that come with a geolocation information (yay!)
liveTweets
|> Observable.choose (fun tw ->
    match tw.OriginalLocation with
    | Some loc ->
        ( tw.Tweeted.AddSeconds(5.0),
          { tw with InferredLocation = Some loc }) |> Some
    | _ -> None)
|> Observable.add replay.AddEvent

// Geolocating tweets using MapQuest occasionally...
liveTweets
|> Observable.filter (fun tw -> tw.OriginalArea |> Seq.exists Char.IsLetter)
|> Observable.limitRate 1000
|> Observable.mapAsyncIgnoreErrors (fun tw -> async {
    let! located = locate tw.OriginalArea
    return located |> Option.map (fun loc ->
      tw.Tweeted.AddSeconds(10.0),
      { tw with InferredLocation = Some loc }) })
|> Observable.choose id
|> Observable.add replay.AddEvent

// TODO: MapQuest geo location (locate : string -> Async<option<decimal * decimal>>)
// ("http://www.mapquestapi.com/geocoding/v1/address?location=Prague&key=" + Config.MapQuestKey)
// TODO: let replay = SchedulerAgent<Tweet>()
// TODO: Use tweet loc (delay by 5 seconds set InferredLocation)
// TODO: Geolocate (filter IsLetter, limitRate, set InferredLocation & delay & replay)
// TODO: Use 'handShake' to expose sockets at '/maptweets'

let mapTweets =
  replay.EventOccurred
  |> Observable.map (fun tweet ->
      JsonTypes.SocketMapTweet(fst tweet.InferredLocation.Value,
        snd tweet.InferredLocation.Value, tweet.Text, tweet.OriginalArea,
        tweet.PictureUrl, tweet.UserName, tweet.UserScreenName).JsonValue.ToString())

let root = IO.Path.Combine(__SOURCE_DIRECTORY__, "web")
let app =
  choose
    [ path "/" >=> Files.browseFile root "index.html"
      path "/feedtweets" >=> handShake (socketOfObservable feedTweets)
      path "/maptweets" >=> handShake (socketOfObservable mapTweets)
      Files.browse root ]

















//
