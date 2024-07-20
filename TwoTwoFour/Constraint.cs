using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TwoTwoFour
{
    public class Constraint
    {
        List<int> PartsOfProblem;
        Func<List<int>, bool> ConstraintFunction;

        public Constraint(int parentValue, int parentCarry, int currentValue, Func<List<int>, bool> constraintFunc) 
        {
            PartsOfProblem = new();

            PartsOfProblem.Add(parentValue);
            PartsOfProblem.Add(parentCarry);
            
            PartsOfProblem.Add(currentValue);

            ConstraintFunction = constraintFunc;
        }
    }
}
