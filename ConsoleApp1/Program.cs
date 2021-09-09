using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Backend;
using Frontend;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = @"
int foo2(int i[2]) {
    if (i[0] == 0) {
        return i[1];
    } else {
        return i[0] + 1;
    }
}
void main() {
    int a[2];
    foo2(a);
    return;
}
";

            ICharStream stream = CharStreams.fromString(test);
            ITokenSource lexer = new Frontend.ProgramLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            Frontend.ProgramParser parser = new Frontend.ProgramParser(tokens)
            {
                BuildParseTree = true,
                ErrorHandler = new FrontEndErrorStrategy()
            };
            IParseTree tree = parser.program();
            var walker = new ParseTreeWalker();
            var frontEndListener = new FrontEndListener();
            walker.Walk(frontEndListener, tree);
            Console.WriteLine(frontEndListener.Result);


            var rlt = MiddleWares.MergeLabel.Merge(frontEndListener.Result);

            Console.WriteLine(rlt);
            ICharStream bstream = CharStreams.fromString(rlt);
            ITokenSource blexer = new Backend.ProgramLexer(bstream);
            ITokenStream btokens = new CommonTokenStream(blexer);
            Backend.ProgramParser bparser = new Backend.ProgramParser(btokens)
            {
                BuildParseTree = true,
                ErrorHandler = new FrontEndErrorStrategy()
            };

            IParseTree bTree = bparser.program();
            var bWalker = new ParseTreeWalker();
            var backendListener = new BackendListener();
            bWalker.Walk(backendListener, bTree);

            Console.WriteLine(string.Join("\n", backendListener.Result));
        }
    }
}