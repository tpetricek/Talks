using System;
using System.Linq;

namespace PasswordValidator
{
  /// <summary>
  /// Exposes `IRequirement` implementations for common password rules:
  /// 
  ///  * `LengthRequirement` requires password to have 8 or more characters
  ///  * `AsciiLowerRequirement` requires password to contain a 
  ///     lower-case ascii character
  ///  * `AsciiUppperRequirement` requires password to contain an
  ///     upper-case ascii character
  /// 
  /// </summary>
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
