using System;
using System.Collections.Generic;
using System.Linq;

namespace pp_week13.Expressions
{
    public class Factor: Expression
    {
       
        public Factor()
        {

        }
        public Factor(FactorOperation factOp, Expression left, Expression right)
        {
            Operator = factOp;
            Left = left;
            Right = right;
        }

        public FactorOperation Operator { get; private set; }

        public override int Evaluate()
        {
            var leftResult = Left.Evaluate();
            if (Operator == FactorOperation.None)
                return leftResult;
            else return OperationActionMap[Operator](leftResult, Right.Evaluate());
        }

        private readonly Dictionary<FactorOperation, Func<int, int, int>> OperationActionMap
            = new Dictionary<FactorOperation, Func<int, int, int>>
            {
                [FactorOperation.Mult] = (a, b) => a * b,
                [FactorOperation.Devision] = (a, b) => a / b,
            };

        public override Dictionary<string, object> GetStringToOperationMap()
        {
            return new Dictionary<string, object>
            {
                ["*"] = FactorOperation.Mult,
                ["/"] = FactorOperation.Devision,
            };

        }
        public override string ToString()
        {
            return GetStringToOperationMap()
                .Where(p => OpcodeEquals(p.Value, Operator))
                .Select(p => p.Key)
                .FirstOrDefault();
        }
    }
    public enum FactorOperation
    {
        None, Mult, Devision
    }
}