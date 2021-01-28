- title : AI assistants: A framework for semi-automated, accountable, and tooling-rich data wrangling
- description : AI assistants: A framework for semi-automated, accountable, and tooling-rich data wrangling
- author : Tomas Petricek
- theme : simple
- transition : none

****************************************************************************************************
- class: front

# _**AI assistants**: A framework for semi-automated, accountable, and tooling-rich data wrangling_


<div style="margin:180px 0px 60px 0px">

**Tomas Petricek**  
_<span style="font-size:80%;line-height:30px">with Gerrit J.J. van den Burg, Alfredo Nazabal, Taha Ceritli, Ernesto Jimenez-Ruiz, Christopher K. I. Williams</span>_

</div>

_University of Kent & The Alan Turing Institute_<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)


****************************************************************************************************
- class: part

# _Motivation_
## AI tools for data wrangling

----------------------------------------------------------------------------------------------------

# _Data wrangling_

<img src="images/tidyverse.png" style="float:right;width:250px" class="nb"/>

**Getting data into usable form**

- _Merge data from multiple sources_
- _Fix errors and missing data_
- _Add semantic information_

<div class="fragment">

**Sure you want to be a data scientist?**

- _Takes 80% of data science project_
- _Tedious iterative manual process_

</div>
<div class="fragment">

**Can automatic AI tools help with this?**

</div>

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Merging real-world CSV files with datadiff

----------------------------------------------------------------------------------------------------

# _What do we want_
## Semi-automatic data wrangling tools that are

_<i class="fa fa-sync"></i>_ Interactive _- Let analyst guide and correct things_

_<i class="fa fa-plug"></i>_ Unified _- Share common structure_

_<i class="fa fa-search"></i>_ Accountable _- Not just opaquely transform data_

_<i class="fa fa-wrench"></i>_ Tooling-rich _- Integrate with notebook tools_

****************************************************************************************************
- class: part

# _Wrattler_
## A glimpse of the future

----------------------------------------------------------------------------------------------------

<img src="images/wrattler.png" class="nb" style="max-width:600px;float:left;margin:30px 30px 100px 0px" />

**Wrattler project**

<div class="fragment" style="margin-top:40px">

Research extension for JupyterLab

_Mix languages, build interactive tools, analyse code provenance_

</div>

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Wrattler and outlier detection

----------------------------------------------------------------------------------------------------

<img src="images/outliers.png" class="nb" style="max-width:400px;float:left;margin:80px 60px 100px 0px" />

**How AI assistants work**

<div class="fragment" style="margin-top:40px;">
<div style="padding-left:440px;">

0. _Start with no constraints_
1. _Guess a cleaning script_  
2. _Run it to show preview_  
3. _Offer constraints to add_
4. _Continue from 1_

</div>
</div>

****************************************************************************************************
- class: part

# _Theory_
## Formalizing AI assistants

----------------------------------------------------------------------------------------------------
- class:mathslide

# _Formal model_

**AI assistant works with**

- $e$ _– expressions representing cleaning scripts_
- $\mathcal{D}$ _– assistant-specific data representation_
- $H$ _– traces of human interactions_

<div class="fragment">

**AI assistant is defined by**

- $f(e, \mathcal{D})=\mathcal{D}'$ _– evaluation function_
- $\mathit{best}_{\mathcal{D}}(H)=e$ _– recommends best expression_
- $\mathit{choices}_{\mathcal{D}}(H)=(H_1, \ldots, H_n)$ _– offers constraints_
- $H_0$ _– empty human interaction trace_

</div>

----------------------------------------------------------------------------------------------------

# _Tooling support_
## Interacting with an AI assistant in Wrattler

<img src="images/flow.png" class="nb" style="max-width:800px;float:left;margin:-30px 30px 100px 0px" />

----------------------------------------------------------------------------------------------------
- class:mathslide

# _Machine learning model_

**Over in the machine learning world**

- $Q_H(\mathcal{D}, e)$ _– objective (scoring) function_
- $E_H$ _– set of expressions allowed under_ $H$

<div class="fragment" style="margin-top:80px">

**Optimization-based AI assistants**  
_Solve problem of finding best allowed expression_  

$\mathit{best}_{\mathcal{D}}(H) = \mathit{arg~max}_{e\in E_H}~Q_H(\mathcal{D}, e)$

</div>

----------------------------------------------------------------------------------------------------

# _Formal model_
## Why AI assistants need a formal model

_<i class="fa fa-magic"></i> Capture what an AI assistant really is_

_<i class="fa fa-puzzle-piece"></i> Formally define the interface_

_<i class="fa fa-wrench"></i> Explain how tools can use AI assistants_

_<i class="fa fa-clone"></i> Lets us easily capture many examples_

****************************************************************************************************
- class: part

# _Examples_
## Some useful AI assistants

----------------------------------------------------------------------------------------------------
- class: part

# _DEMO_
## Datadiff AI assistant

----------------------------------------------------------------------------------------------------
- class:mathslide

# _Datadiff AI assistant_

**Patches and constraints**

- Patches _– recode, permute, insert, delete, linear_
- Constraints _– nomatch, notransform, match_

<div class="fragment">

**Optimization-based datadiff assistant**

- $H_0$ _– empty set of constraints_
- $e$ _– list of patches to apply_
- $E_H$ _– expressions allowed under conditions_ $H$

</div>

----------------------------------------------------------------------------------------------------
- class:mathslide

# _Outlier detection AI assistant_

**Different kind of interactivity**

- _Human builds expression by choosing_
- Choices _suggests possible filters to add_
- Best _simply returns the selected filters_

<div class="fragment">

**Still fits with the formal model!**

- $\mathit{best}_{\mathcal{D}}(H)=H$ _– trace is an expression_
- $\mathit{choices}_{\mathcal{D}}(H)=(H\cup \{f_1\}, \ldots, H\cup\{ f_n \})$
- _Filters_ $f_1, \ldots, f_n$ _generated by clever AI_

</div>

****************************************************************************************************
- class: part

# _Evaluation_
## What can we do with this

----------------------------------------------------------------------------------------------------

# _Evaluation_
## What can we do with AI assistants

_<i class="fa fa-th"></i> Many existing tools fit this model!_

_<i class="fa fa-brain"></i> Can be extended for Bayesian framework_

_<i class="fa fa-user"></i> Qualitative evaluation using case studies_

_<i class="fa fa-stopwatch"></i> Count necessary human interactions_

----------------------------------------------------------------------------------------------------

# _Quantitative evaluation_

<img src="images/eval.png" class="nb" style="max-width:600px;margin:30px 30px 30px 50px" />

**Can wrangle more data with a few hints!**

- _But any evaluation is tricky_
- _About specific tools, not the framework_
- _Needs ad-hoc data set for each assistant_


****************************************************************************************************
- class: part

# _Conclusions_
## Framework of AI assistants

----------------------------------------------------------------------------------------------------

**AI assistants**: A framework for semi-automated, accountable, and tooling-rich data wrangling

<br />

- _Programming theory meets machine learning!_
- _Capture important class of ML tools!_
- _But where & how should we publish this??_

<br />

**Tomas Petricek**, _University of Kent_<br />
[tomas@tomasp.net](mailto:tomas@tomasp.net) | [@tomaspetricek](http://twitter.com/tomaspetricek)
