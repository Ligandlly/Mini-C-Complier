using System;
using Antlr4.Runtime;

namespace Frontend
{
    public class FrontEndErrorStrategy : DefaultErrorStrategy
    {
        public override void ReportError(Parser recognizer, RecognitionException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            base.ReportError(recognizer, e);
            Console.ResetColor();
        }

        protected override void ReportMissingToken(Parser recognizer)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            base.ReportMissingToken(recognizer);
            Console.ResetColor();
            throw new FrontEndException("Missing Token");
        }
    }
}