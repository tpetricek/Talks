using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class Calculator
    {
      private List<double> numbers = new List<double>();

      public void Add(double number)
      {
        numbers.Add(number);
      }

      public double Average
      {
        get { return numbers.Sum() / numbers.Count; }
      }
    }
}
