using System;
using System.Linq;
using System.Collections.Generic;

namespace PasswordValidator
{
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
    public bool IsSatisfied(string password)
    {
      return validators.All(req => req.IsSatisfied(password));
    }
  }
}
