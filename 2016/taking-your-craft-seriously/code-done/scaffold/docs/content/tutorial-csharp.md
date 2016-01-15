Password validator tutorial
===========================

This is a demo library for validating passwords. You can check that password 
has a minimal _length_, contains an _upper case_ and a _lower case_ letter.

Referencing the library
-----------------------

When using the library in C#, just add a reference to `PasswordValidator.dll`
and open the `PasswordValidator` namespace.

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

    [lang=csharp]
    var len = Validators.LengthRequirement;
    len.IsSatisfied("hello");       // 'false'
    len.IsSatisfied("hello world"); // 'true'

### Power validator

The following demonstrates how to use `PowerValidator` to 
check for the presence of an uppr case letter and sufficient length:

    [lang=csharp]
    let valids = 
      new[] { 
        Validators.LengthRequirement
        Validators.AsciiUpperRequirement };

    var pow = new PowerValidator(valids);
    pow.IsSatisfied("hello world");  // 'false'
    pow.IsSatisfied("Hello world");  // 'true'