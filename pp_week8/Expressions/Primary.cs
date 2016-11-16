using System;
using System.Collections.Generic;

namespace pp_week13.Expressions
{
    public class Primary : Expression
    {
        public override string ToString()
        {
            return Value.ToString();
        }
        // this function should never called for primary expressions
        public override Dictionary<string, object> GetStringToOperationMap()
        {
            throw new NotImplementedException();
        }

        public override int Evaluate()
        {
            return Value;
        }
        public override string ToJson(string accSpaces = "", string delay = "   ")
        {
            return Value.ToString();
        }
        public Primary(int value)
        {
            Value = value;
        }
        public int Value { get; private set; }
    }

}