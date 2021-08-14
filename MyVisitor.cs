using System;
using Antlr4.Runtime.Misc;
using System.Collections.Generic;

namespace minic_antlr
{
    using SymbolTabel = Dictionary<string, Variable>;

    /// <summary>
    /// Types allowed Mini C
    /// </summary>
    public enum MiniCType
    {
        Int,
        Short,
        Char,
        Void
    }

    /// <summary>
    /// Data for each variable
    /// </summary>
    public struct Variable
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public readonly int Offset { get; }
        public readonly MiniCType Type { get; }
    }

    internal class MyVisitor
        : ProgramBaseVisitor<LinkedList<string>>
    {
        public Dictionary<string, SymbolTabel> Memory { get; }


        /// <summary>
        /// Program
        /// Create global Symbol Table
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override LinkedList<string> VisitProgram([NotNull] ProgramParser.ProgramContext context)
        {
            Memory.Add("0_global", new SymbolTabel());
            return Visit(context);
        }

        /// <summary>
        /// 把十六进制换成十进制
        /// </summary>
        /// <param name="context"></param>
        /// <returns>只含一个字符串的链表</returns>
        public override LinkedList<string> VisitNum([NotNull] ProgramParser.NumContext context)
        {
            var rlt = new LinkedList<string>();
            string str = context.GetText();

            // hex
            if (str[1] == 'x' || str[1] == 'X')
            {
                rlt.AddLast(Convert.ToInt32(str, 16).ToString());
            }

            // dec
            else
            {
                rlt.AddLast(Convert.ToInt32(str, 10).ToString());
            }


            //Console.WriteLine(rlt.First);
            return rlt;

            //return base.VisitNum(context);
        }
    }
}
