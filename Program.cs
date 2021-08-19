/*
 * Vade, liber, verbisque meis loca grata saluta:              
 * contingam certe quo licet illa pede.
 * siquis, ut in populo, nostri non inmemor illi,
 * siquis, qui, quid agam, forte requirat, erit,
 * vivere me dices, salvum tamen esse negabis;
 * id quoque, quod vivam, munus habere dei.               
 * atque ita tu tacitus, quaerent si plura legentes,
 * ne, quae non opus est, forte loquare, cave!
 * protinus admonitus repetet mea crimina lector,
 * et peragar populi publicus ore reus.
 * tu cave defendas, quamvis mordebere dictis; 
 * causa patrocinio non bona maior erit.
 * invenies aliquem, qui me suspiret ademptum,
 * carmina nec siccis perlegat ista genis,
 * et tacitus secum, ne quis malus audiat, optet,
 * sit mea lenito Caesare poena levis.                              
 * nos quoque, quisquis erit, ne sit miser ille, precamur,
 * placatos miseris qui volet esse deos;
 * quaeque volet, rata sint, ablataque principis ira
 * sedibus in patriis det mihi posse mori.
 */

using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace MiniC
{
    class Program
    {
        static string str = @"
int a;
void interruptServer0(int a, int b) {
      int b;
      int c;
      b = a&0xf000;
      c = a + 1;
      c = c & 0x0fff;
      a = ~(~b&~c);
 }
 void interruptServer1(void)
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
      interruptServer1();
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