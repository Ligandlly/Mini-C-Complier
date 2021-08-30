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

        private static void Compare(string test, string rlt)
        {
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
        public void TestAdd()
        {
            string test = @"
int main() {
    int a;
    a = 1;
    a = a + 2;
}
";
            string rlt = @"    decl_var; int; t0@main;  ;
    decl_var; int; a@main;  ;
    =; 1;  ; a;
    +; a; 2; t0;
    =; t0;  ; a;
    end;  ;  ;  ;
";
            Compare(test, rlt);
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
            string rlt = @"    decl_var; int; t0@addThird;  ;
    decl_var; int; t1@addThird;  ;
    decl_var; int; t2@main;  ;
    decl_var; int; t3@main;  ;
    param_decl; int; arr; 5;
    func; int; addThird; 1;
    cp; arr;  ; t0;
    inc; 8;  ; t0;
    +; t0; 1; t1;
    return; t1;  ;  ;
    end_func;  ;  ;  ;
    decl_arr; int; arr@main; 5;
    cp; arr;  ; t2;
    inc; 4;  ; t2;
    =; 2;  ; t2;
    param; arr;  ;  ;
    call; addThird; 1; t3;
    end;  ;  ;  ;
";
            Compare(test, rlt);
        }



        [Test]
        public void TestArrDecl()
        {
            var test = @"
short arr[3];
int main() {
    short localArr[4];
}";
            var rlt = @"    decl_arr; short; arr@0_global; 3;
    decl_arr; short; localArr@main; 4;
    end;  ;  ;  ;
";
            Compare(test, rlt);

        }

        [Test]
        public void TestFuncDef()
        {
            var test = @"
short foo(int a, char arr[3]) {
    return 1;
}
int main() {
    
}";
            var rlt = @"    param_decl; int; a;  ;
    param_decl; char; arr; 3;
    func; short; foo; 2;
    return; 1;  ;  ;
    end_func;  ;  ;  ;
    end;  ;  ;  ;
";
            Compare(test, rlt);

        }
    }
}