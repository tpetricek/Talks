using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator
{
  /// <summary>
  /// Represents a password requirement that can be evaluated
  /// using the `IsSatisfied` method
  /// </summary>
  public interface IRequirement
  {
    /// <summary>
    /// Returns `true` when the password satisfies the condition
    /// </summary>
    bool IsSatisfied(string password);
  }
}
