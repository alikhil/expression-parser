using pp_week13.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp_week13
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter expression(keep in mind that all spaces ' ' will be removed):");
            var expression = Console.ReadLine();
            Parser parser = new Parser(expression);

            try
            {
                var ex = parser.Parse();
                ex.PrintPretty();
                Console.WriteLine("Evaluated: " + ex.Evaluate());
                Console.WriteLine("Json:");
                Console.WriteLine(ex.ToJson());
            }
            catch (ParsingException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
