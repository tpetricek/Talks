using System;
using System.Linq;

namespace PasswordValidator
{
  public class Validators
  {
    static string asciiLower = "abcdefghijklmnopqrstuvwxyz";
    static string asciiUpper = asciiLower.ToUpperInvariant();

    /// <summary>
    /// Requires 8 or more characters
    /// </summary>
    public static IRequirement LengthRequirement
    {
      get { return new SimpleValidator(pass => pass.Length >= 8); }
    }

    /// <summary>
    /// Requires lower-case ASCII character
    /// </summary>
    public static IRequirement AsciiLowerRequirement
    {
      get { return new SimpleValidator(pass => 
        pass.Any(c => asciiLower.Contains(c))); }
    }

    /// <summary>
    /// Requires upper-case ASCII character
    /// </summary>
    public static IRequirement AsciiUpperRequirement
    {
      get { return new SimpleValidator(pass => 
        pass.Any(c => asciiUpper.Contains(c))); }
    }
  }
}
