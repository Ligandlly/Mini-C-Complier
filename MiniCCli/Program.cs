using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FrontEnd;

namespace MiniCCli
{
    static class Program
    {
        static string str = @"
int a;

void foo(int b) {
    if (a) {
        b = b + 1;
        if (b) {
            b = b + 2;
        } else {
            b = b + 3;
        }
    } else {
        b = b + 4;
    }
    b = b + 5; 
}
";

        static void Main(string[] args)
        {
            ICharStream stream = CharStreams.fromString(str);
            ITokenSource lexer = new ProgramLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            ProgramParser parser = new ProgramParser(tokens)
            {
                BuildParseTree = true
            };
            IParseTree tree = parser.program();

            var walker = new ParseTreeWalker();
            walker.Walk(new MyListener(), tree);
        }
    }
}