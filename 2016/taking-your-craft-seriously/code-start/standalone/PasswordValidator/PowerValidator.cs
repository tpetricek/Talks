using System;
using System.Linq;
using System.Collections.Generic;

namespace PasswordValidator
{
  public class PowerValidator : IRequirement
  {
    IEnumerable<IRequirement> validators;

    public PowerValidator(IEnumerable<IRequirement> validators)
    {
      this.validators = validators;
    }

    public bool IsSatisfied(string password)
    {
      return validators.All(req => req.IsSatisfied(password));
    }
  }
}
