- title : Wrattler - Reproducible, live and polyglot notebooks
- description : 
- author : Tomas Petricek
- theme : white
- transition : none

****************************************************************************************************

# Wrattler: _Reproducible, live<br /> and polyglot notebooks_

<p style="margin-bottom:25px;margin-top:230px">Tomas Petricek<em>, James Geddes, Charles Sutton<br/>
University of Kent & The Alan Turing Institute</em></p>

_[http://tomasp.net](http://tomasp.net/academic) <span style="margin:0px 6px 0px 6px">|</span>
[tomas@tomasp.net](mailto:tomas@tomasp.net) <span style="margin:0px 6px 0px 6px">|</span>
[@tomaspetricek](http://twitter.com/tomaspetricek)_

****************************************************************************************************

### What makes data science hard?

<br />
<div class="fragment">

_<i class="fa fa-hand-spock"></i> Hard-to-find special cases_

_<i class="fa fa-calendar-alt"></i> Every data set is different_

_<i class="fa fa-sync-alt"></i> Can't say what works until we've done it_

_<i class="fa fa-align-justify"></i> Many tasks are repetitive_

</div>

----------------------------------------------------------------------------------------------------

### Data science 
_What tools do we need?_

<br />

_<i class="fa fa-comment"></i>_ Live – _give quick feedback_

_<i class="fa fa-retweet"></i>_ Reproducible – _be able to go back_

_<i class="fa fa-sign-language"></i>_ Polyglot – _mix tools that work_

_<i class="fa fa-flask"></i>_ Smart – _get help from the AI_

_<i class="fa fa-user"></i>_ Explainable – _no black boxes_

----------------------------------------------------------------------------------------------------

### DEMO
_Analysing broadband speed in Wrattler_

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
_<span class="circ"><span>2</span></span> Live development_  
_<span class="circ"><span>3</span></span> Platform for AI assistants_  
_<span class="circ"><span>4</span></span> Polyglot programming_  

****************************************************************************************************

# Wrattler 

__ <span class="circ"><span>1</span></span> reproducible <span class="circ"><span>2</span></span> live 
<span class="circ"><span>3</span></span> smart _and_ <span class="circ"><span>4</span></span> polyglot

----------------------------------------------------------------------------------------------------

### <span class="circ"><span>1</span></span> Reproducible
_Tracking provenance of computations_

<br />

_<i class="fa fa-code-branch"></i> Track dependencies within notebook_

_<i class="fa fa-sync"></i> Invalidate results on change_

_<i class="fa fa-table"></i> Attach results to unique hashes_

----------------------------------------------------------------------------------------------------

<img src="images/graph-simple.png" style="width:70%;margin-left:15%" />

----------------------------------------------------------------------------------------------------

### <span class="circ"><span>2</span></span> Live
_Tighter interaction feedback loop_

<br />

_<i class="fa fa-globe"></i> Browser-based language_

_<i class="fa fa-stopwatch"></i> Recalculated on-the-fly_

_<i class="fa fa-arrow-up"></i> Using dependency graph_

----------------------------------------------------------------------------------------------------

# <span class="circ"><span>2</span></span> DEMO
Live – _Exploring data in the browser_

----------------------------------------------------------------------------------------------------

### <span class="circ"><span>3</span></span> Smart
_Simplifying process with AI assistants_

<br />

_<i class="fa fa-database"></i> Full access to data store_

_<i class="fa fa-language"></i> Domain specific languages_

_<i class="fa fa-archive"></i> No black box magic_

----------------------------------------------------------------------------------------------------

# <span class="circ"><span>3</span></span> DEMO
Smart – _Cleaning data with the datadiff assistant_


----------------------------------------------------------------------------------------------------

### <span class="circ"><span>4</span></span> Polyglot
_Enabling platform for data science_

<br />

_<i class="fa fa-table"></i> Share data via data frames_

_<i class="fa fa-archive"></i> Computation graph for provenance_

_<i class="fa fa-comment-alt"></i> Semantic annotations_

----------------------------------------------------------------------------------------------------

# <span class="circ"><span>4</span></span> DEMO
Polyglot – _Sharing data between R and JavaScript_

****************************************************************************************************

# Summary
_Reproducible, live, smart and polyglot notebooks_

----------------------------------------------------------------------------------------------------

### Motivation and approach
_Improving the state of notebook systems_

<br />

_<i class="fa fa-book"></i> Based on main-stream data science tools_

_<i class="fa fa-wrench"></i> Change the things we need to change_

_<i class="fa fa-code-branch"></i> Use provenance for reproducibility and liveness_


----------------------------------------------------------------------------------------------------

### Wrattler
_Three key ideas behind the system_

<br />

_<i class="fa fa-database"></i> Separate state and language runtimes_

_<i class="fa fa-globe"></i> Provenance tracking in the browser_

_<i class="fa fa-magic"></i> Platform for AI assisted data science_

----------------------------------------------------------------------------------------------------

## Questions, answers & discussion

<br />

#### <i class="fa fa-question-circle"></i> _Granularity of provenance information_

#### <i class="fa fa-question-circle"></i> _What tools to integrate with_

#### <i class="fa fa-question-circle"></i> _What else provenance gives us_

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
