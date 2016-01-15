#r "PasswordValidator/bin/Debug/PasswordValidator.dll"
open PasswordValidator

let len = Validators.LengthRequirement
len.IsSatisfied "test"
len.IsSatisfied "very long test"


let reqs = 
  [ Validators.LengthRequirement
    Validators.AsciiUpperRequirement ]

let pow = PowerValidator(reqs)

pow.IsSatisfied("TEST")
pow.IsSatisfied("long test")
pow.IsSatisfied("long TEST")
