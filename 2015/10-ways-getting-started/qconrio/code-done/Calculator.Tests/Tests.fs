module ``Calculator tests``

open CalculatorLib
open NUnit.Framework
open FsUnit

[<Test>]
let ``Average of no numbers is not a number``() = 
  let calc = Calculator()
  calc.Average |> should equal nan

[<Test>]
let ``Average of numbers from 1 to 10``() = 
  let calc = Calculator()
  for n in 1.0 .. 10.0 do calc.Add(n)
  calc.Average |> should equal 5.5

