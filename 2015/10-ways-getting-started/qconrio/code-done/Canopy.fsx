#r "packages/Selenium.Support/lib/net40/WebDriver.Support.dll"
#r "packages/Selenium.WebDriver/lib/net40/WebDriver.dll"
#r "packages/canopy/lib/canopy.dll"

open canopy
open runner
open System

configuration.chromeDir <- __SOURCE_DIRECTORY__ 

start chrome

url "http://www.google.com"

"#lst-ib" << "rio de janeiro"
click ".lsb"

for a in element "#search" |> elementsWithin "a" do
  if a.Text <> "" then
    printfn " - %s\n    %s" a.Text (a.GetAttribute("href"))

quit()

url "http://www.bing.com"

".b_searchbox" << "rio de janeiro"
click "#sb_form_go"

for a in element "#b_results" |> elementsWithin "h2 a" do
  if a.Text <> "" then
    printfn " - %s\n    %s" a.Text (a.GetAttribute("href"))

quit()