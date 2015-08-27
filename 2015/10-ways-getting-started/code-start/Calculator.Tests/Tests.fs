namespace CalculatorTests

open CalculatorLib
open NUnit.Framework
open FsUnit

// DEMO: Using module and backticks
// DEMO: Using the FsUnit library

[<TestFixture>]
type CalculatorTests() =

  [<Test>]
  member x.AverageOfNoNumbersIsNAN() = 
    let calc = Calculator()
    Assert.AreEqual(nan, calc.Average)

  [<Test>]
  member x.AverageOfNumbersFromOneToTen() = 
    let calc = Calculator()
    for n in 1.0 .. 10.0 do 
      calc.Add(n)
    Assert.AreEqual(5.5, calc.Average)

