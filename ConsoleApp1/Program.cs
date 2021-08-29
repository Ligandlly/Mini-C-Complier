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
    global; int; t0;  ;
    decl_var; int; a;  ;
    Je; 1; 0; label_1;
    =; 1;  ; a;
    decl_var; int; b;  ;
    ==; a; 2; t0;
    Je; t0; 0; label_0;
    label_1; label_3; label_4; label_2; 
    =; 3;  ; b;
label_0:
label_1:
    end;  ;  ;  ;
label_2:
label_3:
label_4:
";

            //ICharStream stream = CharStreams.fromString(test);
            //ITokenSource lexer = new ProgramLexer(stream);
            //ITokenStream tokens = new CommonTokenStream(lexer);
            //ProgramParser parser = new ProgramParser(tokens)
            //{
            //    BuildParseTree = true,
            //    ErrorHandler = new FrontEndErrorStrategy()
            //};
            //IParseTree tree = parser.program();
            //var walker = new ParseTreeWalker();
            //var frontEndListener = new FrontEndListener();
            //walker.Walk(frontEndListener, tree);
            //Console.WriteLine(frontEndListener.Result);

            var _ = MiddleWares.MergeLabel.Merge (test);

            Console.WriteLine(_);

            return;
            //var backend = new Backend.Backend(frontEndListener.Result, frontEndListener.Tables);
            //foreach (var q in backend.IrList)
            //{
            //    Console.WriteLine(q);
            //}
        }
    }
}