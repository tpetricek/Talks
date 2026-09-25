# Beware

The talk slides generated mostly automatically using Claude from the notes below with some small amount of post-processing. The notes are intended as discussion points and not an accurate overview of the paper.

# Paper notes

**Structure of the paper**

-   Interesting as an example of HCI paper
-   Teaser figure in the paper
-   Up to 5 minute video

(watch: <https://dl.acm.org/doi/10.1145/3772318.3791666>)

-   Focus on studies and result analysis

(this paper is more extreme case!)

**Results**

-   What should we teach? Prompt writing or CS skills?
-   "CS achievement remains a significant predictor after\
    controlling for domain-general cognitive skills."
-   Are the results believable? Are there flaws?

-   Evaluated on existing university students!
-   Usual caveat - LLMs is a moving target

**What makes this a good paper**

-   Results are what one would probably expect
-   But tested with rigorous methodology for the first time

**Limitations**

-   Very specific kind of interaction (LLM chat)

-   c.f. GitHub repo assist

-   The writing is quite hard to get through!
-   Confounding factor (controlled for but...)

**Methodology**

-   Pre-registered study (serious!) - <https://aspredicted.org/wg3h-dx9m.pdf>
-   Threats to validity considerations

-   Custom programming platform to control for the accidental
-   There is even method for generating the task (expert elicitation)

-   Using "structured consensus-building process"
-   Two authentic expert-generated tasks
-   One explicitly decontextualized task

-   Measure four different factors independently

-   Writing achievement - write a short description of technical term

-   Marked by humans until agreement reached

-   CS achievement - use standardized benchmark
-   General reasoning skills - standardized ICAR test
-   Vibe coding platform - not sharing data; hides code; timer + Sonnet

-   Key problem - confounding factors?

**Statistical analysis**

-   This is way too sophisticated for me!
-   Summary of results

-   Written communication significant (but not with control)

-   *"CS achievement is the stronger predictor of aggregate vibe-coding*

*accuracy, but written-communication skills also show a reliable unique*

*contribution beyond CS achievement."*

-   Exploratory analysis

-   Negative correlation between LLM usage and CS achievement
-   Negative correlation between LLM usage and written skills
-   Higher human-marked writing correlates with human-marked prompt quality

**Discussion**

*We believe that the primary and most interesting finding from our study is that both written communication skills and CS achievement contribute positively, significantly, and independently to GUI-oriented vibe coding performance, and in the case of CS achievement, in a way that cannot be explained by domain-general cognitive ability alone.*

**Discussion about why:**

-   Better vocabulary for writing prompts?
-   More structured thinking?
-   Algorithmic thinking and decomposition?

**Notes**

-   "r" is "Pearson correlation coefficient" (says Claude, my stats skills are poor)
-   Claude thinks 0.1 is weak, 0.3 is medium, 0.5 is large in social sciences