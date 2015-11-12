(*** hide ***)
#load "packages/FsLab/FsLab.fsx"
open FsLab
open Deedle
open FSharp.Data
open XPlot.GoogleCharts
(**
Understanding the World with F#
===============================

This journal demonstrates how to generate nice reports from your FsLab
data analysis. With FsLab journals, you can take your F# Script files, 
add inline comments using Markdown and turn them into HTML or PDF reports.
In this demo, we use WorldBank type provider to obtain various information 
about countries, then we analyze the data using Deedle and we create a 
number of charts using Foogle Charts. We look at two topics:

 - **Population growth** - Which countries have the largest population? 
   This is an easy question! But in which countries has the population
   have been growing the most over the last 10 years?
 - **Correlation of indicators** - Are there any interesting correlations
   between the indicators from the WorldBank? For example, does life
   expectancy depend on the GDP?

Population growth
-----------------

Let's start by looking at population and population growth. The following 
snippet reads population information in year 2000 and 2010 for all countries 
of the world using the WorldBank type provider. We store the results in two
series with the country names as keys:
*)
let wb = WorldBankData.GetDataContext()

let pop2000 = series [ for c in wb.Countries -> 
  c.Name, c.Indicators.``Population, total``.[2000] ]
let pop2010 = series [ for c in wb.Countries -> 
  c.Name, c.Indicators.``Population, total``.[2010] ]
(**
Now we can display the population in 2010 using a chart:
*)
(*** define-output: geo1 ***)
Chart.Geo(pop2010)
(*** include-it: geo1 ***)

(**
This shows the expected results. The countries with the largest population are
China and India. But what if we look at the growth? With Deedle series, we can
easily calculate the population growth between years 2000 and 2010. Deedle 
provides overloaded operators on series and it automatically aligns the two
series based on the keys (country names):
*)
(*** define-output: geo2 ***)
let growth = (pop2010 - pop2000) / pop2010 * 100.0
Chart.Geo(growth)
(*** include-it: geo2 ***)

(**
Indicator correlation
---------------------

Another interesting thing we can do is to look at correlation between 
different indicators that we can get from the WorldBank. The following
snippet creates a Deedle frame with a number of indicators. Feel free to
explore other options! Here, we download data on GDP, life expectancy,
access to electricity, CO2 emissions and population growth:
*)
let world = 
  [ for c in wb.Countries ->
      c.Name => series [ 
        "Electricity" => c.Indicators.``Access to electricity (% of population)``.[2010]
        "Life" => c.Indicators.``Life expectancy at birth, total (years)``.[2010]
        "GDP" => c.Indicators.``GDP per capita (current US$)``.[2010]
        "Growth" => c.Indicators.``GDP per capita growth (annual %)``.[2010]
        "CO2" => c.Indicators.``CO2 emissions (metric tons per capita)``.[2010]
        "Births" => c.Indicators.``Population growth (annual %)``.[2010] ] ]
  |> Frame.ofRows
(*** include-value:world***)
(**
With the `include-value` command, it is easy to embed charts, but also Deedle frames.
The above table gives us a quick summary of what the data look like and you can also
see where missing values appear most often. To display the correlation, we can use the 
`plot` function from R using the R type provider
*)
(*** define-output: cor ***)
open RProvider.graphics
R.plot(world)
(*** include-output: cor ***)
(**

Are GDP and life expectancy correlated?
---------------------------------------

If you look at the above picture carefuly, it looks like there might be some correlation
between life expectancy and GDP. The higher the GDP of a country is, the larger the life
expectancy. It also looks that the correlation is not linear, so let's try to plot
the life expectancy compared to a logarithm of GDP:
*)
(*** define-output: gdp ***)
let options = 
  Options
    ( pointSize=4, colors=[|"#3B8FCC"|], 
      trendlines=[|Trendline(opacity=0.5,lineWidth=10,color="#C0D9EA")|],
      hAxis=Axis(title="Log of GDP (per capita)"), 
      vAxis=Axis(title="Life expectancy (years)") )

Series.zipInner (log10 world?GDP) world?Life
|> Series.values
|> Chart.Scatter
|> Chart.WithOptions(options)
(*** include-it: gdp ***)
