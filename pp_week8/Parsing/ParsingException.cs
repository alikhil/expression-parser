using System;
using System.Runtime.Serialization;

namespace pp_week13.Parsing
{
    [Serializable]
    public  class ParsingException : Exception
    {

        public ParsingException()
        {
        }

        public ParsingException(ErrorKind kind, int position, char ch, Exception innererException = null) :
            base($"Error on parsing {kind} operand. Can not recognize char '{ch}' at position {position}.", innererException)
        {

        }
        public ParsingException(string message) : base(message)
        {
        }

        public ParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public enum ErrorKind
    {
        Integer,
        Primary,
        Factor,
        Term,
        Relation,
        Logical,
        UnkownOperator
    }

}