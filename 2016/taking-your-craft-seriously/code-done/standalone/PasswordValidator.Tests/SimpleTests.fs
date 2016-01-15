#if INTERACTIVE
#r "../PasswordValidator/bin/Debug/PasswordValidator.dll"
#r "../packages/NUnit/lib/nunit.framework.dll"
#r "../packages/FsUnit/lib/net45/FsUnit.NUnit.dll"
#r "../packages/FsCheck/lib/net45/FsCheck.dll"
#else
module PasswordValidator.SimpleTests
#endif
open FsCheck
open FsUnit
open NUnit.Framework
open PasswordValidator

[<Test>]
let ``Power validator can combine length and upper-case`` () =
  let reqs = 
    [ Validators.LengthRequirement
      Validators.AsciiUpperRequirement ]
  let pow = PowerValidator(reqs)
  pow.IsSatisfied("TEST") |> should equal false
  pow.IsSatisfied("long test") |> should equal false
  pow.IsSatisfied("long TEST") |> should equal true


[<Test>]
let ``When a requirement is not satisfied, the password is rejected`` () =
  let password = "invalid"
  let requirement = 
    { new IRequirement with 
        member x.IsSatisfied pwd = false }
  let validator = PowerValidator([requirement])
  Assert.AreEqual(validator.IsSatisfied(password), false)


[<Test>]
let ``UpperCase correctly requires upper-case ASCII letter`` () = 
  let up = Validators.AsciiUpperRequirement
  let letters = set "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
  let config = { Config.QuickThrowOnFailure with MaxTest = 1000 }
  Check.One(config, fun (s:string) ->
    s <> null ==> (fun () ->
      let actual = up.IsSatisfied(s)
      let expected = s |> Seq.exists letters.Contains
      actual = expected
    ))
