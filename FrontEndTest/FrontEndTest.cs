using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FrontEnd;
using NUnit.Framework;
namespace FrontEndTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAdd()
        {
            string test = @"
int main() {
    int a;
    a = 1;
    a = a + 2;
}
";
            string rlt = @"    global; int; t0;  ;
    decl_var; int; a;  ;
    =; 1;  ; a;
    +; a; 2; t0;
    =; t0;  ; a;
    end;  ;  ;  ;
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

            Assert.AreEqual(rlt.Trim(), frontEndListener.Result.Trim());
        }
        
        [Test]
        public void TestArray()
        {
            string test = @"
int addThird(int arr[5]) {
    return arr[2] + 1;
}

int main() {
    int arr[5];
    arr[1] = 2;
    addThird(arr);
}
";
            string rlt = @"    global; int; t0;  ;
    global; int; t1;  ;
    global; int; t2;  ;
    global; int; t3;  ;
    param_decl; intArr; arr;  ;
    func; int; addThird; 1;
    cp; arr;  ; t0;
    inc; 8;  ; t0;
    +; t0; 1; t1;
    return; t1;  ;  ;
    end_func;  ;  ;  ;
    decl_arr; int; arr; 5;
    cp; arr;  ; t2;
    inc; 4;  ; t2;
    =; 2;  ; t2;
    param; arr;  ;  ;
    call; addThird; 1; t3;
    end;  ;  ;  ;
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

            Assert.AreEqual(rlt.Trim(), frontEndListener.Result.Trim());
        }
    }
}