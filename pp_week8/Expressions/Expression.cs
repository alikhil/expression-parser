using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp_week13.Expressions
{
    public abstract class Expression
    {

        public Expression Left { get; protected set; }

        public Expression Right { get; protected set; }

        public abstract Dictionary<string, object> GetStringToOperationMap();

        public void PrintPretty(string indent = "", bool last = true)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "   ";
            }
            else
            {
                Console.Write("|-");
                indent += "|  ";
            }
            Console.WriteLine('(' + ToString() + ')');

            Left?.PrintPretty(indent, Right == null);
            Right?.PrintPretty(indent, true);
        }

        protected bool OpcodeEquals<T>(object obj, T opcode)
        {
            T enumVal = (T)Enum.Parse(typeof(T), obj.ToString());
            return enumVal.Equals(opcode);
        }

        public abstract int Evaluate();

        public virtual string ToJson(string accSpaces = "", string delay = "   ")
        {
            return "{"
                + "\n" + accSpaces + delay + $"Operator : '{ToString()}',"
                + "\n" + accSpaces + delay + $"Left : {Left.ToJson(accSpaces + delay)},"
                + "\n" + accSpaces + delay + $"Right : {Right.ToJson(accSpaces + delay)}"
                + "\n" + accSpaces + "}";
        }
    }
}
