- title : End-to-end functional web development
- description : One of the myths about F# is that it is only good for complex
    mathematics. This could not be further from truth. In this talk, I'’'m going to walk through a
    complete web development story with F#. We’ll start with a script file to test a couple of ideas,
    wrap the code into a web server and we’ll finish by deploying our system to Azure and Heroku.
- author : Tomas Petricek
- theme : night
- transition : none

***************************************************************************************************

# End-to-end functional web development

<img src="images/suave.png" style="width:150px;margin:60px;border-color:#F26822" />

**Tomas Petricek**, fsharpWorks  
[@tomaspetricek](http://twitter.com/tomaspetricek)
| [tomasp.net](http://tomasp.net)
| [fsharpworks.com](http://fsharpworks.com)

***************************************************************************************************

# The web is _functional_!

<br /><br />

<pre style="font:175% consolas; background:transparent;text-align:center;" class="fragment">
<span class="p">Request <span class="o">-&gt;</span> Response</span>
</pre>

---------------------------------------------------------------------------------------------------

# The web is _asynchronous_!

<br /><br />

<pre style="font:175% consolas; background:transparent;text-align:center;" class="fragment">
<span class="p">Request <span class="o">-&gt;</span> <span class="t">Async</span><span class="o">&lt;</span>Response<span class="o">&gt;</span></span>
</pre>

***************************************************************************************************

<table><tr><td style="padding-right:50px;padding-top:60px">

# DEMO

Hello world and <br />_type providers_

</td><td style="padding-left:50px">
<img src="images/news.jpg" style="width:400px;border-style:none;" />
</td></tr></table>

---------------------------------------------------------------------------------------------------

# Demo summary

<br />
<div class="large-icon-list">

 - <i class="fa clr fa-rocket"></i> _Lightweight_ and _asynchronous_
 - <i class="fa clr fa-database"></i> Great with _type providers_
 - <i class="fa clr fa-cog"></i> Build and package _infrastructure_

</div>

---------------------------------------------------------------------------------------------------

# Package management with Paket

Specify dependencies in `paket.dependencies`

    [lang=text]
    source https://nuget.org/api/v2
    nuget Suave

Locked version numbers in `paket.lock`

    [lang=text]
    NUGET
      remote: https://nuget.org/api/v2
      specs:
        FSharp.Core (3.1.2.1)
        FsPickler (1.2.1)
        Suave (0.28.1)
          FSharp.Core (>= 3.1.2.1)
          FsPickler (>= 1.0.7)


---------------------------------------------------------------------------------------------------

# Building and live reloading script

_FAKE_ for building and _F# Compiler Service_ for reloading

    let reloadAppServer () =
      currentApp.Value <- reloadScript()

    Target "run" (fun _ ->
      let app ctx = currentApp.Value ctx
      let _, server = startWebServerAsync serverConfig app
      Async.Start(server)

      use watcher =
        !! (__SOURCE_DIRECTORY__ @@ "*.*")
        |> WatchChanges (fun _ -> reloadAppServer())
      Console.ReadLine()
    )


---------------------------------------------------------------------------------------------------

<table><tr><td style="padding-right:60px;padding-top:100px">

# DEMO

Building _chat app_<br /> with agents

</td><td style="padding-left:60px;">
<img src="images/telegraph.jpg" style="width:400px;border-style:none;" />
</td></tr></table>

---------------------------------------------------------------------------------------------------

# Demo summary

<br />
<div class="large-icon-list">

 - <i class="fa clr fa-arrow-circle-right"></i> _Composable library_ for REST APIs
 - <i class="fa clr fa-building-o"></i> Works with _agent-based_ architecture
 - <i class="fa clr fa-cloud"></i> Scale with _Akka.net_, _Cricket_ and _M-Brace_

</div>

---------------------------------------------------------------------------------------------------

# Routing and handling headers

Using F# power for _domain-specific languages_

    choose
      [ path "/" >>= setMimeType "text/html" >>= index
        path "/chat" >>= GET >>= noCache >>= getMessages
        path "/post" >>= POST >>= noCache >>= postMessage
        NOT_FOUND "Found no handlers" ]

---------------------------------------------------------------------------------------------------

<table><tr><td style="padding-right:60px;padding-top:100px">

# DEMO

Testing _chat app_<br /> with FsCheck

</td><td style="padding-left:60px;">
<img src="images/telegraph.jpg" style="width:400px;border-style:none;" />
</td></tr></table>

---------------------------------------------------------------------------------------------------

# Demo summary

<br />
<div class="large-icon-list">

 - <i class="fa clr fa-check-square-o"></i> Nice _unit testing_ with _simple syntax_
 - <i class="fa clr fa-trophy"></i> Powerful random testing with _FsCheck_
 - <i class="fa clr fa-institution"></i> Integrated with _FAKE_ and _CI servers_

</div>

***************************************************************************************************

# Summary

---------------------------------------------------------------------------------------------------

# Summary

### Many more interesting F# web stacks!

<br />

 - _Suave_ for lightweight web development
 - _Freya_ hosting on IIS & elsewhere using _OWIN_
 - _WebSharper_ integrates client and server-side
 - But also: _ASP.NET MVC_, _Nancy_ & _ServiceStack_

---------------------------------------------------------------------------------------------------

# Thank you!

<br />

<table><tr><td style="vertical-align:middle;padding-right:30px;">
<img src="images/ad-fworks.png" style="width:250px;background:transparent;border:none;" />
</td><td style="padding-left:30px;">
<img src="images/ad-qsh.png" style="width:190px;background:transparent;border:none;" />
</td></tr></table>

<br />

**Tomas Petricek**, fsharpWorks  

See [functional-programming.net](http://functional-programming.net/) for books & trainings!  
[@tomaspetricek](http://twitter.com/tomaspetricek)
| [tomasp.net](http://tomasp.net)
| [fsharpworks.com](http://fsharpworks.com)  
