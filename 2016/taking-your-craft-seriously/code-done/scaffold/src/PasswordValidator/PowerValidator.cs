using System;
using System.Linq;
using System.Collections.Generic;

namespace PasswordValidator
{
  /// <summary>
  /// A validator that lets you combine multiple different validators.
  /// For example:
  /// 
  ///     [lang=csharp]
  ///     var valids = new PowerValidator(new[] {
  ///       Validators.LengthRequirement,
  ///       Validators.AsciiUpperRequirement
  ///     });
  /// </summary>
  public class PowerValidator : IRequirement
  {
    IEnumerable<IRequirement> validators;

    /// <summary>
    /// Creates a power validator that combines the specified validators
    /// </summary>
    public PowerValidator(IEnumerable<IRequirement> validators)
    {
      this.validators = validators;
    }

    /// <summary>
    /// Evaluates the validator
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool IsSatisfied(string password)
    {
      return validators.All(req => req.IsSatisfied(password));
    }
  }
}
