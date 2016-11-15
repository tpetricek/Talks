#r "packages/Selenium.WebDriver/lib/net40/WebDriver.dll"
#r "packages/canopy/lib/canopy.dll"

open canopy
open runner
open System

configuration.chromeDir <- __SOURCE_DIRECTORY__ 
start chrome

// DEMO: Getting Google search results
//
//  - go to "http://www.google.com"
//  - enter text into "#lst-ib"
//  - click ".lsb"
//  - get "a" elements in the "#search" element


// DEMO: Getting Bing search results
//
//  - go to "http://www.bing.com"
//  - enter text into ".b_searchbox"
//  - click "#sb_form_go"
//  - get "h2 a" elements in the "#b_results" element

quit()