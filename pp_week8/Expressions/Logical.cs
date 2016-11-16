using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp_week13.Expressions
{
    public class Logical : Expression
    {

        public Logical()
        {

        }
        public Logical(LogicalOperation relationOp, Expression left, Expression right)
        {
            Left = left;
            Right = right;
            Operator = relationOp;

        }

        public LogicalOperation Operator { get; private set; }

        public override int Evaluate()
        {
            var leftResult = Left.Evaluate();
            if (Operator == LogicalOperation.None)
                return leftResult;
            else return OperationActionMap[Operator](leftResult, Right.Evaluate());
        }

        private readonly Dictionary<LogicalOperation, Func<int, int, int>> OperationActionMap
            = new Dictionary<LogicalOperation, Func<int, int, int>>
            {
                [LogicalOperation.And] = (a, b) => a & b,
                [LogicalOperation.Or] = (a, b) => a | b,
                [LogicalOperation.Xor] = (a, b) => a ^ b
            };

        public override Dictionary<string, object> GetStringToOperationMap()
        {
            return new Dictionary<string, object>
            {
                ["and"] = LogicalOperation.And,
                ["xor"] = LogicalOperation.Xor,
                ["or"] = LogicalOperation.Or
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

    public enum LogicalOperation
    {
        None, And, Or, Xor
    }
}
