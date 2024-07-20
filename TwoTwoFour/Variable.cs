using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoTwoFour
{
    public class Variable
    {
        public char Data;
        public List<int>? Restrictions;
        public int DomainBeginning;
        public int DomainEnding;

        public bool ParentResultIsVar;
        public bool ParentCarryIsVar;
        public bool RootIsResult;
        public bool NoConstraint;

        public Variable(char data, List<int> restrictions, int domainBeginning, int domainEnding)
        {
            Data = data;
            Restrictions = restrictions;
            DomainBeginning = domainBeginning;
            DomainEnding = domainEnding;

            ParentResultIsVar = false;
            ParentCarryIsVar = false;
            RootIsResult = false;
            NoConstraint = false;
        }

        public Variable(char data, int domainBeginning, int domainEnding)
        {
            Data = data;
            Restrictions = null;
            DomainBeginning = domainBeginning;
            DomainEnding = domainEnding;

            ParentResultIsVar = false;
            ParentCarryIsVar = false;
            RootIsResult = false;
            NoConstraint = false;
        }
    }
}
