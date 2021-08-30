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
        public void TestDecl()
        {
            string test = @"
int x;
int foo() {
    char c; int d;
    c = 0; c = c + 1;
}
int main() {
    short a;
    int b;
    char cs[5];
}
";
            string rlt = @"    decl_var; char; t0@foo;  ;
    decl_var; int; x@0_global;  ;
    decl_var; char; c@foo;  ;
    decl_var; int; d@foo;  ;
    decl_var; short; a@main;  ;
    decl_var; int; b@main;  ;
    decl_arr; char; cs@main; 5;
    func; int; foo; 0;
    =; 0;  ; c;
    +; c; 1; t0;
    =; t0;  ; c;
    end_func;  ;  ;  ;
    end;  ;  ;  ;
";
            Compare(test, rlt);
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
    decl_var; int; t1@main;  ;
    decl_arr; int; arr@main; 5;
    param_decl; int; arr; 5;
    func; int; addThird; 1;
    +; arr[2]; 1; t0;
    return; t0;  ;  ;
    end_func;  ;  ;  ;
    =; 2;  ; arr[1];
    param; arr;  ;  ;
    call; addThird; 1; t1;
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