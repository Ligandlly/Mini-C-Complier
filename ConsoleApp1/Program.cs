using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FrontEnd;
using Backend;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = @"
int main() {
    int a;
    a = 1;
    a = a + 2;
}
";

            ICharStream stream = CharStreams.fromString(test);
            ITokenSource lexer = new ProgramLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            ProgramParser parser = new ProgramParser(tokens)
            {
                BuildParseTree = true,
                ErrorHandler = new FrontEndErrorStrategy()
            };
            IParseTree tree = parser.program();
            var walker = new ParseTreeWalker();
            var frontEndListener = new FrontEndListener();
            walker.Walk(frontEndListener, tree);
            Console.WriteLine(frontEndListener.Result);
            return;
            var backend = new Backend.Backend(frontEndListener.Result, frontEndListener.Tables);
            foreach (var q in backend.IrList)
            {
                Console.WriteLine(q);
            }
        }
    }
}