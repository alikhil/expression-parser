# expression-parser
Modern Programming Paradigms course homework implementation on C#


## Example of usage

```cs
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
```
