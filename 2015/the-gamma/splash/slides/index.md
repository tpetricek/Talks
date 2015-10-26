- title : The Gamma
- description : Data journalism
- author : Tomas Petricek
- theme : night
- transition : none

***************************************************************************************************
 - class: intro

# The Gamma

[www.thegamma.net](http://thegamma.net)

<br /><br /><br /><br />

**Tomas Petricek** <br />
[_@tomaspetricek_](http://twitter.com/tomaspetricek) <span class="sep">|</span>
[_tomas@tomasp.net_](mailto:tomas@tomasp.net)

---------------------------------------------------------------------------------------------------

<a href="http://www.theguardian.com/environment/2007/jun/19/china.usnews">
<img src="images/china.jpg" style="height:550px"/>
</a>

<div class="reference">

The Guardian: [China overtakes US as world's biggest CO2 emitter](http://www.theguardian.com/environment/2007/jun/19/china.usnews)<br />
The Economist: [The East is Grey](http://www.economist.com/news/briefing/21583245-china-worlds-worst-polluter-largest-investor-green-energy-its-rise-will-have?fsrc=scn/fb/wl/pe/eastisgrey)

</div>

---------------------------------------------------------------------------------------------------

### Transparency <span class="sep"></span> **Is it misleading?**

 - Is China the _biggest pulluter_?
 - _University enrollment_ in Czech Republic

<div class="fragment">

### Reproducibility <span class="sep"></span> **Is it correct?**

 - Critique of _Piketty's Capital_
 - _Reinhart-Rogoff_ Growth in Time of Debt

</div>
 
---------------------------------------------------------------------------------------------------

<h2 style="margin:0px">Data journalism</h2>

<img src="images/dj.png" style="height:400px" />

<div class="reference">

Illustration from: [Data journalism handbook](http://datajournalismhandbook.org/)

</div>

***************************************************************************************************

# The Gamma

---------------------------------------------------------------------------------------------------

<h3 style="padding-left:75px">Data driven article in The Gamma</h3>

<a href="http://thegamma.net/carbon">
<img src="images/gamma.png" style="height:400px"/>
</a>

<div class="reference">

The Gamma: [The world's biggest polluters](http://thegamma.net/carbon)  

</div>

---------------------------------------------------------------------------------------------------

## Data driven articles

<div class="fragment">

Not just text with visualizations, but...

</div><div class="fragment">

 - Can it be _modified and reproduced_?
 - Is the _source code_ available?
 - Are the _data sources_ referenced?

</div><div class="fragment">
<br />

> **Data driven article is really a program!**

<br /><br /></div>

---------------------------------------------------------------------------------------------------

## Article is a program view

<div class="fragment">

There is no magic. It is just **code and text**!

</div><div class="fragment">

 - We see the _journalist's story_ first
 - Readers can _modify the parameters_
 - Power users can _see and modify the code_

<br /><br /></div>

---------------------------------------------------------------------------------------------------

## Programming language research

<div class="fragment">

Typed functional language with type providers

</div><div class="fragment">

 - Simple code with _functional programming_
 - Editor support via **static types** 
 - Data access with _F# type providers_

<br /><br /></div>

---------------------------------------------------------------------------------------------------

<h3 style="padding-left:50px">Programming articles in The Gamma</h3>

<a href="https://github.com/tpetricek/TheGamma/blob/master/web/demos/carbon.md">
<img src="images/github.png" style="height:400px"/>
</a>

<div class="reference">

The Gamma: [The world's biggest polluters (source code)](https://github.com/tpetricek/TheGamma/blob/master/web/demos/carbon.md)  

</div>

***************************************************************************************************

# Theory of type providers

---------------------------------------------------------------------------------------------------

<img src="images/gamma-g.png" style="width:550px;position:relative;left:-50px" />

---------------------------------------------------------------------------------------------------

<img src="images/gamma-0.png" style="width:550px;position:relative;left:-50px" />

---------------------------------------------------------------------------------------------------

<img src="images/gamma-pi.png" style="width:550px;position:relative;left:-50px" />

***************************************************************************************************

# Technology behind

---------------------------------------------------------------------------------------------------

## World bank data source

<img src="images/wb1.png" style="height:450px;position:relative;top:-20px;left:-50px"/>

---------------------------------------------------------------------------------------------------

## World bank data source

<img src="images/wb2.png" style="height:450px;position:relative;top:-20px;left:-50px"/>

---------------------------------------------------------------------------------------------------

## World bank data source

<img src="images/wb3.png" style="height:450px;position:relative;top:-20px;left:-50px"/>

---------------------------------------------------------------------------------------------------

## World bank data source

<img src="images/wb4.png" style="height:450px;position:relative;top:-20px;left:-50px"/>

---------------------------------------------------------------------------------------------------

## World bank type provider

<div class="fragment">
<img src="images/diagram.png" style="height:450px;position:relative;top:-20px;left:-50px"/>
</div>

---------------------------------------------------------------------------------------------------

## Auto-generated options

<div class="fragment">

One of members of the same type

    let co2 =
      world.byYear.``2010``.``Climate Change``
        .``CO2 emissions (kt)``
        
</div><div class="fragment">

List with sub-set of properties

    let topCountries =
      [ world.byCountry.China
        world.byCountry.India
        world.byCountry.Japan ]    

</div>

***************************************************************************************************

# Future directions

---------------------------------------------------------------------------------------------------

## Future directions

<div class="fragment">

Programming experiences research

<div class="reference" style="position:absolute;top:450px;left:20px">

Programming experiences: [Sean McDirmid on Lambda the Ultimate](http://lambda-the-ultimate.org/node/5247)

</div>
</div><div class="fragment">

 - Usable _literate programming_
 - Use PL for **provenance, context** and more!
 - Mapping for _large-scale open government data_
 - Grammar of **interactive visualizations**
 
<br /><br /></div>



***************************************************************************************************

## Looking for early adopters :-)

<div class="fragment" style="padding-bottom:60px;padding-top:10px">

### Summary

 - Information _literacy_
 - Article is a _program_
 - Future _usable programming_

</div>
<div>

Prototype [http://thegamma.net](http://thegamma.net)  
Contact [@tomaspetricek](http://twitter.com/tomaspetricek) <span class="sep">|</span>
[tomas@tomasp.net](mailto:tomas@tomasp.net)

</div>










