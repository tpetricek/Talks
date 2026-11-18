- title : FsReveal
- description : Introduction to FsReveal
- author : Tomas Petricek
- theme : night
- transition : none

****************************************************************************************************

# Types from data
## Making structured data first-class citizens in F#

<br /><br /><br /><br />

Tomas Petricek, University of Cambridge<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

Don Syme, Microsoft Research<br />
[donsyme@fastmail.fm](mailto:donsyme@fastmail.fm) | [@dsyme](http://twitter.com/dsyme)

Gustavo Guerra, Microsoft<br />
[@ovatsus](http://twitter.com/ovatsus)

****************************************************************************************************

## Unsafe dynamic access

    [lang=csharp]
    var url = "http://dvd.netflix.com/Top100RSS";
    var rss = XDocument.Load(topRssFeed);
    var channel = rss.Element("rss").Element("channel");

    foreach(var item in channel.Elements("item")) {    
      Console.WriteLine(item.Element("text").Value);
    }

<div class="fragment">
  <div class="tipbox" style="left:445px;top:-88px;width:84px;height:25px"></div>
  <div class="tiplbl" style="left:500px;top:-78px;width:150px;">Not found!</div>
</div>

****************************************************************************************************

## Unsafe dynamic access

    [lang=csharp]
    var url = "http://dvd.netflix.com/Top100RSS";
    var rss = XDocument.Load(topRssFeed);
    var channel = rss.Element("rss").Element("channel");

    foreach(var item in channel.Elements("item")) {    
      Console.WriteLine(item.Element("title").Value);
    }

<div>
  <div class="tipbox" style="left:445px;top:-88px;width:96px;height:25px"></div>
  <br />
</div>

****************************************************************************************************
 - data-transition:fade

<img src="images/api1.png" style="position:relative;top:-50px;" />

****************************************************************************************************
 - data-transition:fade

<img src="images/api2.png" style="position:relative;top:-50px;" />

****************************************************************************************************

## Tedious handwritten mappings

    [lang=csharp]
    public class SearchResults
    {  
      public int Page { get; set; }  
      public List<Result> Results { get; set; }
      public int TotalPages { get; set; }  
      public int TotalResults { get; set; }
    }
    public class Result
    {  
      public int ID { get; set; }  
      public List<KnownFor> KnownFor { get; set; }  
      public string Name { get; set; }  
      public double Popularity { get; set; }  
      public string ProfilePath { get; set; }
    }

<div class="fragment">
  <div class="tiplbl" style="left:770px;width:150px;">And more...</div>
</div>

****************************************************************************************************

## Tedious handwritten mappings

    [lang=csharp]
    public class KnownFor
    {  
      public bool Adult { get; set; }  
      public string BackdropPath { get; set; }  
      public List<int> GenreIDs { get; set; }  
      public int ID { get; set; }  
      public string Overview { get; set; }  
      public string ReleaseDate { get; set; }  
      public string PosterPath { get; set; }  
      public double Popularity { get; set; }  
      public string Title { get; set; }  
      public double VoteAverage { get; set; }  
      public int VoteCount { get; set; }  
      public string Name { get; set; }
    }

<div>
  <div class="tiplbl" style="left:770px;width:230px;">Much much more...</div>
</div>

****************************************************************************************************

# _DEMO_

## Reading news from RSS feed

****************************************************************************************************

<div class="diagram1">
<p>
  <span>{title&nbsp;:&nbsp;string,&nbsp;author&nbsp;:&nbsp;{age&nbsp;:&nbsp;int}}</span>
  <span style="margin-left:50px">{author&nbsp;:&nbsp;{age&nbsp;:&nbsp;float}}</span>
</p>  
<div class="fragment">
<p><span style="position:relative;top:70px;left:90px" class="arrow-down"></span></p>
<p>
  <span style="position:relative;top:30px;left:40px">{&nbsp;title&nbsp;:&nbsp;option&lt;string&gt;,
    &nbsp;author&nbsp;:&nbsp;{age&nbsp;:&nbsp;float}&nbsp;}</span>
</p>  
</div>
</div>

****************************************************************************************************

<div class="diagram2">
<p>
  <span>{&nbsp;location&nbsp;:&nbsp;{lng:num,&nbsp;lat:num}&nbsp;}</span>
  <span style="margin-left:50px">{&nbsp;location&nbsp;:&nbsp;string&nbsp;}</span>
</p>  
<div class="fragment">
<p><span style="position:relative;top:70px;left:90px" class="arrow-down"></span></p>
<p>
  <span style="position:relative;top:30px;left:40px">
  {&nbsp;location&nbsp;:&nbsp;{lng:num,&nbsp;lat:num}&nbsp;}&nbsp;+&nbsp;{&nbsp;location&nbsp;:&nbsp;string&nbsp;}</span>
</p>  
</div>
</div>

****************************************************************************************************

# _DEMO_

## Getting weather via REST service

****************************************************************************************************

# Behind the scenes

<br /><br />

<div class="fragment">
<div class="diagram1">
  <p><span style="padding:7px 30px 7px 30px">Structural shape inference</span></p>
</div>
</div>
<div class="fragment" style="padding:20px">
<div class="diagram2">
  <p><span style="padding:7px 30px 7px 30px">Language integration via type providers</span></p>
</div>
</div><div class="fragment">
<div class="diagram1">
  <p><span style="padding:7px 30px 7px 30px">Relative type safety</span></p>
</div>
</div>

<br /><br />

****************************************************************************************************

# Relative type safety

<div class="fragment" style="margin-top:70px">

Given _representative samples_ and _an input_ value

$S(d')\sqsubset S(d_1, \ldots, d_n)$

</div><div class="fragment" style="margin-top:70px">

Any _program_ written user _type provider_ reduces

$e_{user}[x\leftarrow e_{provided}(d')] \rightsquigarrow^* v$

</div>

****************************************************************************************************

# _DEMO_

## Error handling and schema change

****************************************************************************************************

# Schema change and stability

_Inferred type_ can change only in _limited ways_

<br />
<div class="fragment">

$C[e] \rightarrow C[e.M]$

$C[e] \rightarrow C[{\sf match}~e~{\sf with}~\ldots]$

$C[e] \rightarrow C[int(e)]$

</div>
<br />

****************************************************************************************************

# Addressing practical concerns

<br />

 * _Prefer records_ for tooling support
 * _Predictable and stable_ shape inference
 * _Open world_ assumption about top shapes

****************************************************************************************************

# Summary

****************************************************************************************************

### _Works in practice_

(148k downloads, 1900 commits, 47 contributors)

### _Type safety revisited_

Relative safety necessary for modern programs

<br /><br /><br />

Tomas Petricek, University of Cambridge<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)

Don Syme, Microsoft Research<br />
[donsyme@fastmail.fm](mailto:donsyme@fastmail.fm) | [@dsyme](http://twitter.com/dsyme)

Gustavo Guerra, Microsoft<br />
[@ovatsus](http://twitter.com/ovatsus)

****************************************************************************************************

# Bonus slides

****************************************************************************************************

<div style="width:1500px;margin-left:-250px">
<img src="http://www.pinksquirrellabs.com/image.axd?picture=2013%2f7%2fcyoa2.png" />
</div>

****************************************************************************************************

![Shape hierarchy](images/hierarchy.png)

****************************************************************************************************

# Labelled top types

<br />

Fundamental _open world_ assumption

    [lang=xml]
    <doc>
      <heading>Working with JSON</heading>
      <p>Type providers make this easy.</p>
      <heading>Working with XML</heading>
      <p>Processing XML is as easy as JSON.</p>
      <image source="xml.png" />
    </doc>

****************************************************************************************************

# Labelled top types

<br />

_Top type_ annotated with _possible cases_

${\sf any}\langle \sigma_1, \ldots, \sigma_n \rangle$

<br />
<div class="fragment">

Provides access to <em>$\sigma_1, \ldots, \sigma_n$</em>

Requires handling of _unknown case_

</div>
