using System;
using System.Linq;

namespace PasswordValidator
{
  public class Validators
  {
    static string asciiLower = "abcdefghijklmnopqrstuvwxyz";
    static string asciiUpper = asciiLower.ToUpperInvariant();

    public static IRequirement LengthRequirement
    {
      get { return new SimpleValidator(pass => pass.Length >= 8); }
    }

    public static IRequirement AsciiLowerRequirement
    {
      get { return new SimpleValidator(pass => 
        pass.Any(c => asciiLower.Contains(c))); }
    }

    public static IRequirement AsciiUpperRequirement
    {
      get { return new SimpleValidator(pass => 
        pass.Any(c => asciiUpper.Contains(c))); }
    }
  }
}
