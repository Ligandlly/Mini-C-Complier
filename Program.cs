using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace MiniC
{
    class Program
    {
        static string str = @"
int a;
void interuptServer0(int a, int b) {
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
} }
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