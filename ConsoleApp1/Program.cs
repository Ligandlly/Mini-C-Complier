using System;
using System.Net.Http.Headers;
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
short foo(int a, char arr[3]) {
    return 1;
}
int main() {
    
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

            return;
             var rlt = MiddleWares.MergeLabel.Merge (frontEndListener.Result);

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
        }
    }
}