- title : Data exploration through dot-driven development 
- description : 
- author : Tomas Petricek
- theme : white
- transition : none

****************************************************************************************************

# Wrattler: _Interactive, smart<br /> and polyglot notebooks_

<h4 style="margin-bottom:0px;margin-top:300px">Tomas Petricek, <em>The Alan Turing Institute</em></h4>

_[http://tomasp.net](http://tomasp.net/academic) <span style="margin:0px 6px 0px 6px">|</span>
[tomas@tomasp.net](mailto:tomas@tomasp.net) <span style="margin:0px 6px 0px 6px">|</span>
[@tomaspetricek](http://twitter.com/tomaspetricek)_

----------------------------------------------------------------------------------------------------

<div style="padding-left:100px">

**<span style="display:inline-block;width:60px">W</span>**  
**<span style="display:inline-block;width:60px">R</span>**  
**<span style="display:inline-block;width:60px">A</span>**  
**<span style="display:inline-block;width:60px">T</span>**  
**<span style="display:inline-block;width:60px">T</span>**  
**<span style="display:inline-block;width:60px">L</span>**  
**<span style="display:inline-block;width:60px">E</span>**  
**<span style="display:inline-block;width:60px">R</span>**  

</div>

----------------------------------------------------------------------------------------------------

<div style="padding-left:100px">

**<span style="display:inline-block;width:60px">W</span>** _Wrangle_  
**<span style="display:inline-block;width:60px">R</span>** _Reproduce_  
**<span style="display:inline-block;width:60px">A</span>** _Analyse_  
**<span style="display:inline-block;width:60px">T</span>** _Transform_  
**<span style="display:inline-block;width:60px">T</span>** _Troubleshoot_  
**<span style="display:inline-block;width:60px">L</span>** _Learn_  
**<span style="display:inline-block;width:60px">E</span>** _Explore_  
**<span style="display:inline-block;width:60px">R</span>** _Revise_  

</div>

****************************************************************************************************

### What makes data science hard?

<br />

_<i class="fa fa-hand-spock"></i>_ Big data is big  
<span style="margin-right:60px"></span> _Hard-to-find special cases_

_<i class="fa fa-calendar-alt"></i>_ The Double Anna Karenina principle  
<span style="margin-right:60px"></span> _Every data set is different_

_<i class="fa fa-sync-alt"></i>_ Feedback loops everywhere  
<span style="margin-right:60px"></span> _Can't say what works until we've done it_

_<i class="fa fa-align-justify"></i>_ Death by a thousand cuts  
<span style="margin-right:60px"></span> _Many tasks are repetitive_

----------------------------------------------------------------------------------------------------

### Data science 
_What tools do we need?_

<br />

_<i class="fa fa-comment"></i>_ Interactive – _give quick feedback_

_<i class="fa fa-retweet"></i>_ Reproducible – _be able to go back_

_<i class="fa fa-sign-language"></i>_ Polyglot – _mix tools that work_

_<i class="fa fa-flask"></i>_ Smart – _get help from the AI_

_<i class="fa fa-user"></i>_ Explainable – _no black boxes_

----------------------------------------------------------------------------------------------------

## DEMO

_Analysing broadband speed in Wrattler_  
_https://wrattler.github.io/wrattler/broadband.html_

----------------------------------------------------------------------------------------------------

## Traditional notebook architecture

<img src="images/jupyter.png" style="width:70%;margin-bottom:60px"/>

_<span class="circ"><span>1</span></span> Limited reproducibility_  
_<span class="circ"><span>2</span></span> No rollback of state_  
_<span class="circ"><span>3</span></span> Limited interaction model_  
_<span class="circ"><span>4</span></span> One language per kernel_  

----------------------------------------------------------------------------------------------------

## Wrattler system architecture

<img src="images/wrattler.png" style="width:70%"/>

----------------------------------------------------------------------------------------------------

## Wrattler system architecture
 
_<span class="circ"><span>1</span></span> Versioning and provenance_  
_<span class="circ"><span>2</span></span> Interactive development_  
_<span class="circ"><span>3</span></span> Platform for AI assistants_  
_<span class="circ"><span>4</span></span> Polyglot programming_  

****************************************************************************************************

# Wrattler 
_Noteboks that are_ <span class="circ"><span>1</span></span> interactive 
<span class="circ"><span>2</span></span> smart _and_ 
<span class="circ"><span>3</span></span> polyglot

----------------------------------------------------------------------------------------------------

### <span class="circ"><span>1</span></span> Interactive
_Tighter interaction feedback loop_

<br />

_<i class="fa fa-globe"></i> Browser-based language_

_<i class="fa fa-stopwatch"></i> Recalculated on-the-fly_

_<i class="fa fa-arrow-up"></i> Using dependency graph_

----------------------------------------------------------------------------------------------------

<img src="images/graph-simple.png" style="width:70%;margin-left:15%" />

----------------------------------------------------------------------------------------------------

# <span class="circ"><span>1</span></span> DEMO
Interactive – _Exploring data in the browser_

----------------------------------------------------------------------------------------------------

### <span class="circ"><span>2</span></span> Smart
_Simplifying process with AI assistants_

<br />

_<i class="fa fa-database"></i> Full access to data store_

_<i class="fa fa-language"></i> Domain specific languages_

_<i class="fa fa-archive"></i> No black box magic_

----------------------------------------------------------------------------------------------------

# <span class="circ"><span>2</span></span> DEMO
Smart – _Cleaning data with the datadiff assistant_


----------------------------------------------------------------------------------------------------

### <span class="circ"><span>3</span></span> Polyglot
_Enabling platform for data science_

<br />

_<i class="fa fa-table"></i> Share data via data frames_

_<i class="fa fa-archive"></i> Computation graph for provenance_

_<i class="fa fa-comment-alt"></i> Semantic annotations_

----------------------------------------------------------------------------------------------------

# <span class="circ"><span>3</span></span> DEMO
Polyglot – _Sharing data between R and JavaScript_

****************************************************************************************************

# Summary
_Interactive, smart and polyglot notebooks_

----------------------------------------------------------------------------------------------------

### Wrattler
_Three key ideas behind the system_

<br />

_<i class="fa fa-database"></i> Separate state and language runtimes_

_<i class="fa fa-globe"></i> Dependency graph in the browser_

_<i class="fa fa-magic"></i> Platform for AI assisted data science_

----------------------------------------------------------------------------------------------------

### Project status
_Wrattler prototype: [github.com/wrattler](http://github.com/wrattler)_

<br />

_<i class="fa fa-check-square"></i>_ Done – _Prototype with dependency graph running  
<span style="margin-right:60px"></span>R, JavaScript languages and datadiff assistant_

_<i class="fa fa-cog"></i>_ Progress – _Deployment as part of JupyterLab  
<span style="margin-right:60px"></span>Data store annotations and graph versioning_

_<i class="fa fa-eye"></i>_ Visionary – _Integration of further AI assistants  
<span style="margin-right:60px"></span>Provenance tracking, modes of interaction_

----------------------------------------------------------------------------------------------------

## Questions, answers & discussion

#### Data store – _Best data and annotation formats?_

#### Integration – _Languages? Jupyter integration?_

#### AI assistants – _What kinds of assistants?_

<br />
<br />

_[http://tomasp.net](http://tomasp.net/academic) <span style="margin:0px 6px 0px 6px">|</span>
[tomas@tomasp.net](mailto:tomas@tomasp.net) <span style="margin:0px 6px 0px 6px">|</span>
[@tomaspetricek](http://twitter.com/tomaspetricek)_

> To wrap up, I'll end with a slide that lists the three next papers that I plan to write.
> The first one is about implementing live programming environments, which is surprisingly
> tricky and the second one is extending the data aggregation work to cover data cleaning with
> AI assistants. Finally, I talked about one of the things that I'm interested in, but I also
> work on philosophy and history of programming and I got invited to submit a paper to an
> ACM HOPL conference, so that's my third. I have ideas about coeffects too, but I only wanted
> to list three.
