using System;
using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FrontEnd;

namespace MiniCCli
{
    static class Program
    {
        static string str = @"
int a;
void interuptServer0(void)
{
int b;
int c;
b = a&0xf000;
c = a + 1;
c = c & 0x0fff;
a = ~(~b&~c);
}
void interuptServer1(void)
{
int b;
int c;
b=a&0x0fff;
c = $0xfffffc10;
c = c<<12;
a = ~(~b&~c);
}
void main(void)
{
$0xfffffc00 = 2;
$0xff22 = 0x000fffff;
a = 0;
while(1)
{
$0xff00 = a;
}
}

";

        static void Main(string[] args)
        {
            ICharStream stream = CharStreams.fromString(str);
            ITokenSource lexer = new ProgramLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            ProgramParser parser = new ProgramParser(tokens)
            {
                BuildParseTree = true,
                ErrorHandler = new FrontEndErrorStrategy()
            };

            // try
            // {
                IParseTree tree = parser.program();
                var walker = new ParseTreeWalker();
                var frontEndListener = new FrontEndListener();
                walker.Walk(frontEndListener, tree);
                Console.WriteLine(frontEndListener.Result);
            // }
            // catch (FrontEndException e)
            // {
            //     Console.ForegroundColor = ConsoleColor.Red;
            //     Console.WriteLine("Error: {0}", e.Message);
            //     System.Environment.Exit(1);
            // }
            // finally
            // {
            //     Console.ResetColor();
            // }
                       
        }
    }
}