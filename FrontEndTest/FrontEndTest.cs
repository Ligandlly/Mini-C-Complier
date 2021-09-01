using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Frontend;
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
            string rlt = @"    
    decl_var; char; t0@foo;  ;
    decl_var; int; x@global0;  ;
    decl_var; char; c@foo;  ;
    decl_var; int; d@foo;  ;
    decl_var; short; a@main;  ;
    decl_var; int; b@main;  ;
    decl_arr; char; cs@main; 5;
    func; int; foo; 0;
    =; 0;  ; c@foo;
    +; c@foo; 1; t0@foo;
    =; t0@foo;  ; c@foo;
    end_func;  ;  ;  ;
    func; int; main; 0;
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
            string rlt = @"    
    decl_var; int; t0@main;  ;
    decl_var; int; a@main;  ;
    func; int; main; 0;
    =; 1;  ; a@main;
    +; a@main; 2; t0@main;
    =; t0@main;  ; a@main;
    end_func;  ;  ;  ;
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
            string rlt = @"    
    decl_var; int; t0@addThird;  ;
    decl_var; int; t1@main;  ;
    decl_arr; int; arr@main; 5;
    param_decl; int; arr@addThird; 5;
    func; int; addThird; 1;
    +; arr@addThird[2]@addThird; 1; t0@addThird;
    return; t0@addThird;  ;  ;
    end_func;  ;  ;  ;
    func; int; main; 0;
    =; 2;  ; arr@main[1]@main;
    param; arr@main;  ;  ;
    call; addThird; 1; t1@main;
    end_func;  ;  ;  ;
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
            var rlt = @"
    decl_arr; short; arr@global0; 3;
    decl_arr; short; localArr@main; 4;
    func; int; main; 0;
    end_func;  ;  ;  ;
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
            var rlt = @"    
    param_decl; int; a@foo; 1;
    param_decl; char; arr@foo; 3;
    func; short; foo; 2;
    return; 1;  ;  ;
    end_func;  ;  ;  ;
    func; int; main; 0;
    end_func;  ;  ;  ;
    end;  ;  ;  ;
";
            Compare(test, rlt);

        }
    }
}