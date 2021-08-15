using System;
using Antlr4.Runtime.Misc;

namespace MiniC
{
    public class MyListener
        : ProgramBaseListener
    {
        public MyListener()
        {
        }

        public override void EnterNum([NotNull] ProgramParser.NumContext context)
        {
            base.EnterNum(context);
            System.Console.WriteLine("Num: {0}", context.GetText());
        }
    }
}