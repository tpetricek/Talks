- title : Data exploration through dot-driven development 
- description : 
- author : Tomas Petricek
- theme : white
- transition : none

****************************************************************************************************

# Feedback

* nowhere tell us what the origin of Wrattler is
* "What makes data science hard" - Link to what Charles says
* DEMO - clarify: things we've done, things we know how to do, things that are visionary
  there is too much: principles of wrattler, implementation, broadband data set
* Traditional notebook - show some cells in the slide?
* Number buzzwords (1 interactive, 2 smart, 3 polyglot)
* DEMO - do all in one?
* GLM problem - sell better or replace with something better

****************************************************************************************************

# Wrattler: _Interactive, smart<br /> and polyglot notebooks_

<h4 style="margin-bottom:0px;margin-top:300px">Tomas Petricek, <em>The Alan Turing Institute</em></h4>

_[http://tomasp.net](http://tomasp.net/academic) <span style="margin:0px 6px 0px 6px">|</span>
[tomas@tomasp.net](mailto:tomas@tomasp.net) <span style="margin:0px 6px 0px 6px">|</span>
[@tomaspetricek](http://twitter.com/tomaspetricek)_

****************************************************************************************************

### Data science 
_What makes data science so hard?_

<br />

_<i class="fa fa-hand-spock"></i> Hard-to-find special cases_

_<i class="fa fa-calendar-alt"></i> Every data set is different_

_<i class="fa fa-sync-alt"></i> Can't say what works until we've done it_

_<i class="fa fa-align-justify"></i> Many tasks are repetitive_

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
_Interactive, smart and polyglot notebooks_

----------------------------------------------------------------------------------------------------

# DEMO
Interactive – _Exploring data in the browser_

----------------------------------------------------------------------------------------------------

### Interactive
_Tighter interaction feedback loop_

<br />

_<i class="fa fa-globe"></i> Browser-based language_

_<i class="fa fa-stopwatch"></i> Recalculated on-the-fly_

_<i class="fa fa-arrow-up"></i> Using dependency graph_

----------------------------------------------------------------------------------------------------

<img src="images/notebook.png" style="width:80%" />

----------------------------------------------------------------------------------------------------

<img src="images/graph.png" style="width:80%" />

----------------------------------------------------------------------------------------------------

# DEMO
Smart – _Cleaning data with the datadiff assistant_

----------------------------------------------------------------------------------------------------


### Smart
_Simplifying process with AI assistants_

<br />

_<i class="fa fa-database"></i> Full access to data store_

_<i class="fa fa-archive"></i> No black box magic_

_<i class="fa fa-language"></i> Domain specific languages_

----------------------------------------------------------------------------------------------------

# DEMO
Polyglot – _Sharing data between R and JavaScript_

----------------------------------------------------------------------------------------------------

### Polyglot
_Enabling platform for data science_

<br />

_<i class="fa fa-table"></i> Share data via data frames_

_<i class="fa fa-archive"></i> Computation graph for provenance_

_<i class="fa fa-comment-alt"></i> Semantic annotations_

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
