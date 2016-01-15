(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/PasswordValidator"
(**
Password validator tutorial
===========================

This is a demo library for validating passwords. You can check that password 
has a minimal _length_, contains an _upper case_ and a _lower case_ letter.

Referencing the library
-----------------------

If you're using F# Interactive, you can reference the library as follows:
*)
#r "PasswordValidator.dll"
open PasswordValidator
(**
Validating passwords
--------------------

There are two ways of validating passwords:

 * **Requirements** - You can use individual requirements via
   the `IRequirement` interface. The implementations can be found
   in the `Validators` module.

 * **Power validator** - Alternatively, you can use the 
   `PowerValidator` type, which takes a collection of requirements.

### Requirements

The following snippet demonstrates the length requirement:
*)
let len = Validators.LengthRequirement
len.IsSatisfied("hello")       // 'false'
len.IsSatisfied("hello world") // 'true'
(**

### Power validator

The following demonstrates how to use `PowerValidator` to 
check for the presence of an uppr case letter and sufficient length:
*)
let valids = 
  [ Validators.LengthRequirement
    Validators.AsciiUpperRequirement ]

let pow = PowerValidator(valids)
pow.IsSatisfied("Hello")  // 'false'
pow.IsSatisfied("1Hello") // 'true'