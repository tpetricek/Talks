﻿#if INTERACTIVE
#r "../packages/NUnit/lib/nunit.framework.dll"
#r "../packages/Selenium.Support/lib/net40/WebDriver.Support.dll"
#r "../packages/Selenium.WebDriver/lib/net40/WebDriver.dll"
#r "../packages/canopy/lib/canopy.dll"
#else
module PasswordValidator.WebTests
#endif
open NUnit.Framework
open canopy

[<Test>]
let ``'Something' is valid and 'aa' is not valid`` () =
  configuration.chromeDir <- __SOURCE_DIRECTORY__ 
  start chrome  
  url "http://localhost:6581/"
  "#pwd" << "Something"
  element "form button" |> click
  element ".alert" |> read |> contains "Success"

  url "http://localhost:6581/"
  "#pwd" << "aa"
  element "form button" |> click
  element ".alert" |> read |> contains "Error"
  quit()