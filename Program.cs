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