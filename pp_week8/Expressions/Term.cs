using System;
using System.Collections.Generic;
using System.Linq;

namespace pp_week13.Expressions
{
    public class Term: Expression
    {

        public Term() { }
        public Term(TermOperator termOp, Expression left, Expression right)
        {
            Left = left;
            Right = right;
            Operator = termOp;
        }

        public override int Evaluate()
        {
            var leftResult = Left.Evaluate();
            if (Operator == TermOperator.None)
                return leftResult;
            else return OperationActionMap[Operator](leftResult, Right.Evaluate());
        }

        private readonly Dictionary<TermOperator, Func<int, int, int>> OperationActionMap
            = new Dictionary<TermOperator, Func<int, int, int>>
            {
                [TermOperator.Add] = (a, b) => a + b,
                [TermOperator.Subtract] = (a, b) => a - b,
            };

        public TermOperator Operator { get; private set; }

        public override Dictionary<string, object> GetStringToOperationMap()
        {
            return new Dictionary<string, object>
            {
                ["+"] = TermOperator.Add,
                ["-"] = TermOperator.Subtract,
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

    public enum TermOperator
    {
        None, Add, Subtract
    }

}