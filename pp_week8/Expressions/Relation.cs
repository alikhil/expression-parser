using System;
using System.Collections.Generic;
using System.Linq;

namespace pp_week13.Expressions
{
    public class Relation : Expression
    {
        public Relation()
        {

        }

        public Relation(RelationOperator termOp, Expression left, Expression right)
        {
            Operator = termOp;
            Left = left;
            Right = right;
        }

        public RelationOperator Operator { get; private set; }

        public override int Evaluate()
        {
            var leftResult = Left.Evaluate();
            if (Operator == RelationOperator.None)
                return leftResult;
            else return OperationActionMap[Operator](leftResult, Right.Evaluate()) ? 1 : 0;
        }

        private readonly Dictionary<RelationOperator, Func<int, int, bool>> OperationActionMap
            = new Dictionary<RelationOperator, Func<int, int, bool>>
            {
                [RelationOperator.Equal] = (a, b) => a == b,
                [RelationOperator.Greater] = (a, b) => a > b,
                [RelationOperator.GreaterOrEqual] = (a, b) => a >= b,
                [RelationOperator.Less] = (a, b) => a < b,
                [RelationOperator.LessOrEqual] = (a, b) => a <= b,
                [RelationOperator.NotEqual] = (a, b) => a != b
            };

        public override Dictionary<string, object> GetStringToOperationMap()
        {
            return new Dictionary<string, object>
            {
                [">="] = RelationOperator.GreaterOrEqual,
                ["="] = RelationOperator.Equal,
                ["<="] = RelationOperator.LessOrEqual,
                ["<"] = RelationOperator.Less,
                [">"] = RelationOperator.Greater,
                ["/="] = RelationOperator.NotEqual
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
    public enum RelationOperator
    {
        None, Less, LessOrEqual, Greater, GreaterOrEqual, Equal, NotEqual
    }
}