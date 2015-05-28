- title : Scalable Machine Learning and Data Science with F#
- description : The F# programming language is a great fit for exploratory data science,
    implementing machine learning and for scaling your code to run on Big Data in the cloud.
- author : Tomas Petricek
- theme : night
- transition : none

***************************************************************************************************
 - data-background : images/bg-neurons.jpg

# Scalable Machine Learning and Data Science with F#

<br />

### **Tomas Petricek**, fsharpWorks <br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) | [fsharpworks.com](http://fsharpworks.com)

***************************************************************************************************
 - data-background : images/bg-neurons-dark.jpg
 - class : wordcloud

non-profit _books and tutorials_

_cross-platform_ **community** data science

## F# Software Foundation

commercial support **open-source** _contributions_

machine learning **[www.fsharp.org](http://www.fsharp.org)** web and cloud

consulting  _user groups_ research

***************************************************************************************************
 - data-background : images/bg-neurons-dark.jpg

<div style="padding-left:200px">

## <i style="color:#E3A396">❶</i> Data science

## <i style="color:#DE6F48">❷</i> Machine learning

## <i style="color:#C14F13">❸</i> Scaling to the cloud

</div>

---------------------------------------------------------------------------------------------------

<img src="images/loop.png" style="width:500px;border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

<style>
.vtop td { vertical-align:top; }
</style>
<table class="vtop"><tr><td>

# _DATA SCIENCE_

## Visualizing World Indicators

[Code](https://github.com/tpetricek/Talks/blob/master/2015/scalable-ml-ds-fsharp/code/world-cluster.fsx) |
[Result](images/charts/growth.html)

</td><td>
<img src="images/world.jpg" style="width:500px;border-style:none;background:transparent;" />
</td></tr></table>

---------------------------------------------------------------------------------------------------

<style>
.reveal .noborder td { border-style:none; vertical-align:top; }
</style>
<table class="noborder">
<tr><td><img src="images/logos/fslab.png" style="margin-top:30px;width:100px;border-style:none;"/></td><td>

### FsLab

Unified data-science package

</td></tr><tr><td><img src="images/logos/fsharpdata.png" style="margin-top:30px;width:100px;border-style:none;"/></td><td>

### F# Data

Type providers for data access

</td></tr><tr><td><img src="images/logos/deedle.png" style="margin-top:30px;width:100px;border-style:none;"/></td><td>

### Deedle

Data frame & time-series for .NET

</td>
</tr>
</table>

---------------------------------------------------------------------------------------------------

<table class="vtop"><tr><td>

# _MACHINE LEARNING_

## Clustering Countries

[Code](https://github.com/tpetricek/Talks/blob/master/2015/scalable-ml-ds-fsharp/code/world-cluster.fsx#L114) |
[Result](images/charts/cluster.html)

</td><td>
<img src="images/world.jpg" style="width:500px;border-style:none;background:transparent;" />
</td></tr></table>

---------------------------------------------------------------------------------------------------

<style>
td.lines { display:none; }
.pre pre.fssnip { font-size:115%; }
</style>

    distance  : ('T -> 'T -> float)  ->
    aggregate : ('T -> 'T[] -> 'T) ->
      count:int -> input:'T[] -> Clustering

---------------------------------------------------------------------------------------------------

<table class="vtop"><tr><td>

# _CLOUD_

## Scaling with M-Brace and Azure

[Code](https://github.com/tpetricek/Talks/blob/master/2015/scalable-ml-ds-fsharp/code/world-scale.fsx) |
[Result](images/charts/cluster.html)

</td><td>
<img src="images/world.jpg" style="width:500px;border-style:none;background:transparent;" />
</td></tr></table>

---------------------------------------------------------------------------------------------------

<pre style="font-size:250%; background:transparent;text-align:center;">
<span class="k">async</span> { … }
</pre>

---------------------------------------------------------------------------------------------------

<pre style="font-size:250%; background:transparent;text-align:center;">
<span class="k">cloud</span> { … }
</pre>

***************************************************************************************************
 - data-background : images/bg-neurons.jpg
 - class : summary

### Language for ML & DS[www.fsharp.org](http://www.fsharp.org)

### Data Science Package [www.fslab.org](http://www.fslab.org)

### Scales to the Cloud [www.m-brace.net](http://www.m-brace.net)

<div class="fragment" style="margin-top:50px">

## Learn more

Tomas Petricek ([@tomaspetricek](http://twitter.com/tomaspetricek) & [tomas@tomasp.net](mailto:tomas@tomasp.net))

Come for a Chat at the Poster Session!

F# Meetup on Web + Azure tonight

F# Friday Chat (on campus, Friday 5)

</div>
