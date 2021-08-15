using Antlr4.Runtime.Tree;
using Antlr4.Runtime;

namespace MiniC
{
    public class Runtime
    {
        IParseTree _tree;

        public Runtime(IParseTree tree)
        {
            _tree = tree;
        }

        public Runtime(string str)
        {
            ICharStream stream = CharStreams.fromString(str);
            ITokenSource lexer = new ProgramLexer(stream);
            ITokenStream token = new CommonTokenStream(lexer);
            ProgramParser parser = new ProgramParser(token);
            parser.BuildParseTree = true;
            _tree = parser.program();
        }
    }
}