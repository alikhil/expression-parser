using pp_week13.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp_week13.Parsing
{
    public class Parser
    {
        private string InputString;

        private int Seek;

        public Parser(string input)
        {
            InputString = input.Replace(" ","");
            Seek = 0;
        }

       
        public Expression Parse()
        {
            var result = ParseLogical();
            if (Seek < InputString.Length)
                throw new ParsingException($"Unexpected character '{GetCurrentChar()}' at position {Seek}. Can not finish parsing.");
            return result;
        }

        private Expression ParseLogical()
        {

            Expression result = ParseRelation();

            while (true)
            {
                LogicalOperation relationOp = ParseLogOperator();
                if (relationOp != LogicalOperation.None)
                {
                    var right = ParseRelation();
                    if (right == null)
                        throw new ParsingException(ErrorKind.Logical, Seek, GetCurrentChar());
                    result = new Logical(relationOp, result, right);
                }
                else break;
            }
            return result;

        }

        private OperationType ParseOperator<OperationType, ExpressionType>() 
            where OperationType : IComparable, IConvertible 
            where ExpressionType : Expression, new()
        {
            int start = GetNextNoneEmptyPos(Seek);
            if (start == -1)
                return default(OperationType);

            var leftString = InputString.Substring(start);

            KeyValuePair<string, object> operatorCode = new ExpressionType()
               .GetStringToOperationMap()
               .Where(d => leftString.StartsWith(d.Key))
               .FirstOrDefault();


            if (operatorCode.Key != null && !operatorCode.Value.Equals(default(OperationType)))
            {
                Seek += operatorCode.Key.Length;
                return (OperationType)operatorCode.Value;
            }
            else return default(OperationType);
        }

        private LogicalOperation ParseLogOperator()
        {
            return ParseOperator<LogicalOperation, Logical>();
        }


        private Expression ParseRelation()
        {
            Expression term = ParseTerm();
            RelationOperator relOp = ParseRelationOperator();
            if (relOp != RelationOperator.None)
            {
                var right = ParseTerm();
                if (right == null)
                    throw new ParsingException(ErrorKind.Relation, Seek, GetNextChar());
                return new Relation(relOp, term, right);
            }
            return term;
        }

        private RelationOperator ParseRelationOperator()
        {
            return ParseOperator<RelationOperator, Relation>();
        }

        private Expression ParseTerm()
        {
            var factor = ParseFactor();
            while (true)
            {
                TermOperator termOp = ParseTermOperator();
                if (termOp != TermOperator.None)
                {
                    var right = ParseFactor();
                    if (right == null)
                        throw new ParsingException(ErrorKind.Term, Seek, GetNextChar());
                    factor = new Term(termOp, factor, right);
                }
                else break;
            }
            return factor;

        }

        private TermOperator ParseTermOperator()
        {
            return ParseOperator<TermOperator, Term>();
            
        }

        private FactorOperation ParseFactorOperation()
        {
            return ParseOperator<FactorOperation, Factor>();
        }

        private Expression ParseFactor()
        {
            var primary = ParsePrimary();
            while (true)
            {
                FactorOperation factOp = ParseFactorOperation();
                if (factOp != FactorOperation.None)
                {
                    var right = ParsePrimary();
                    if (right == null)
                        throw new ParsingException(ErrorKind.Factor, Seek, GetNextChar());
                    primary = new Factor(factOp, primary, right);
                }
                else break;
            }
            return primary;

        }

        private char GetCurrentChar()
        {
            if (Seek >= InputString.Length)
                return '\0';
            return InputString[Seek];
        }

        private Expression ParsePrimary()
        {
            Expression result = null;
            char nextChar = GetNextChar();
            if (char.IsDigit(nextChar))
                result = ParseIntereger();
            else if (nextChar == '(')
            {
                SkipNextChar();
                result = ParseLogical();
                nextChar = GetNextChar();
                if (nextChar != ')')
                    throw new ParsingException($"Syntax error: expected ')', but found '{nextChar}' at position {Seek}.");
                SkipNextChar();
            } 
            return result;
        }

        private void SkipNextChar()
        {
            int index = GetNextNoneEmptyPos(Seek);
            Seek = index + 1;
        }

        private Primary ParseIntereger()
        {
            try
            {
                int index = GetNextNoneEmptyPos(Seek);
                string val = "";
                for (Seek = index; Seek < InputString.Length; Seek++)
                {
                    if (char.IsDigit(InputString[Seek]))
                        val += InputString[Seek];
                    else break;
                }
                return new Primary(int.Parse(val));
            }
            catch(Exception e)
            {
                throw new ParsingException(ErrorKind.Integer, Seek, GetCurrentChar(), e);
            }
        }

        private int GetNextNoneEmptyPos(int seek)
        {
            for (int i = seek; i < InputString.Length; i++)
                if (InputString[i] != ' ')
                    return i;
            return -1;
            throw new ParsingException($"Can not get non empty pos starting from {seek}");
        }

        private char GetNextChar()
        {
            int index = GetNextNoneEmptyPos(Seek);
            if (index == -1)
                return '\0';
            return InputString[index];
        }

       
    }
}
