using System;

namespace PasswordValidator
{
  public class SimpleValidator : IRequirement
  {
    private Func<string, bool> isValid;

    public SimpleValidator(Func<string, bool> isValid)
    {
      this.isValid = isValid;
    }

    public bool IsSatisfied(string password)
    {
      return isValid(password);
    }
  }
}
