using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace MiniC
{
    using SymbolTable = Dictionary<string, Identity>;

    public class MyListener
        : ProgramBaseListener
    {
        public ParseTreeProperty<string> Ir { get; } = new ParseTreeProperty<string>();

        private readonly Dictionary<string, SymbolTable> _tables = new Dictionary<string, SymbolTable>();

        private Stack<string> _currentScopeNameStack = new Stack<string>();

        private IIrBuilder _irBuilder = new QuaternaryBuilder();

        private const string Global = "0_global";

        public override void EnterProgram(ProgramParser.ProgramContext context)
        {
            _tables.Add(Global, new SymbolTable());
            _currentScopeNameStack.Push(Global);
        }

        #region Primary_expr

        public override void ExitPrimary_exprHasExpr(ProgramParser.Primary_exprHasExprContext context)
        {
            Ir.Put(context, Ir.Get(context.expr()));
        }

        public override void ExitPrimary_exprHasId(ProgramParser.Primary_exprHasIdContext context)
        {
            Ir.Put(context, Ir.Get(context.id()));
        }

        public override void ExitPrimary_exprHasNum(ProgramParser.Primary_exprHasNumContext context)
        {
            Ir.Put(context, Ir.Get(context.num()));
        }

        #endregion

        #region Decl

        /// <summary>
        /// Variable Declaration
        /// </summary>
        /// <example><c>int i;</c> => <c>decl_var; int; i;  </c></example>
        /// <exception cref="DuplicateNameException">Duplicate Variable Name</exception>
        /// <param name="context"></param>
        public override void ExitVar_declHasId(ProgramParser.Var_declHasIdContext context)
        {
            var name = context.id().GetText();
            var type = context.type_spec().GetText();

            var table = _tables[_currentScopeNameStack.Peek()];

            // Check if the name has already in Symbol table
            if (table.Keys.Contains(name))
            {
                throw new DuplicateNameException("Duplicate Variable Name");
            }


            Ir.Put(context, _irBuilder.GenerateIr("decl_var", type, name));
        }

        /// <summary>
        /// Function Definition, add function name to global symbol table, 
        /// push function name into name stack
        /// </summary>
        /// <param name="context"></param>
        /// 
        /// <exception cref="DuplicateNameException">Duplicate Function Name</exception>
        public override void EnterFunc_def(ProgramParser.Func_defContext context)
        {
            var funcName = context.id().GetText();
            var rltType = context.type_spec() == null ? context.type_spec().GetText() : context.VOID().GetText();

            if (_tables[Global].Keys.Contains(funcName))
                throw new DuplicateNameException("Duplicate Function Name");

            _tables[Global].Add(funcName, new FuncIdentity(funcName, rltType));

            _currentScopeNameStack.Push(funcName);
            _tables.Add(funcName, new SymbolTable());
        }

        /// <summary>
        /// Write function definition to IR
        /// And pop function name
        /// </summary>
        /// <example><c>int func1(int a, char b) { return a + b; } </c> =>
        /// <code>
        /// param; int; a; ;
        /// param; char; b; ;
        /// func; int; func1; ;
        ///     local; int; rlt; ;
        ///     +; a; b; rlt;
        ///     return; rlt; ; ;
        /// end_func; ; ; ;
        /// </code>
        /// </example>
        /// <param name="context"></param>
        /// <exception cref="DuplicateNameException"></exception>
        public override void ExitFunc_def(ProgramParser.Func_defContext context)
        {
            var rltBuilder = new StringBuilder();
            var funcName = context.id().GetText();
            var rltType = context.type_spec() == null ? context.type_spec().GetText() : context.VOID().GetText();

            var paramList = context.param_list();

            foreach (var child in paramList.children)
            {
                if (child is not ProgramParser.ParamContext tmpParam) continue;
                // 1st: Add param to Symbol Table
                var name = tmpParam.id().GetText();
                var type = tmpParam.type_spec().GetText();
                var table = _tables[_currentScopeNameStack.Peek()];
                // Check if the name has already in Symbol table
                if (table.Keys.Contains(name))
                {
                    throw new DuplicateNameException("Duplicate Parameter Name");
                }

                // 2nd: Add to IR code
                rltBuilder.AppendLine(_irBuilder.GenerateIr("param", type, name));
            }

            rltBuilder.AppendLine(_irBuilder.GenerateIr("func", rltType, funcName));

            rltBuilder.AppendLine(Ir.Get(context.compound_stmt()));

            rltBuilder.AppendLine(_irBuilder.GenerateIr("end_func"));
            var rlt = rltBuilder.ToString();
            Ir.Put(context, rlt);
            _currentScopeNameStack.Pop();
        }

        #endregion


        #region Literal

        public override void ExitType_spec(ProgramParser.Type_specContext context)
        {
            Ir.Put(context, context.GetText());
        }


        /// <summary>
        /// 數字
        /// 如果是十六進制就要轉換成十進制
        /// 如果溢出保留int的最大值
        /// </summary>
        /// <param name="context"></param>
        public override void ExitNum([NotNull] ProgramParser.NumContext context)
        {
            var text = context.GetText();
            if (text.Length > 1 && (text[1] == 'x' || text[1] == 'X'))
            {
                try
                {
                    text = $"{Convert.ToInt32(text[3..], 16)}";
                }
                catch (OverflowException)
                {
                    text = $"{int.MaxValue}";
                }
            }

            else
            {
                try
                {
                    var _ = int.Parse(text);
                }
                catch (OverflowException)
                {
                    text = $"{int.MaxValue}";
                }
            }

            Ir.Put(context, text);
        }

        /// <summary>
        /// 變量
        /// 檢查變量名
        /// <remarks>不允許有label加數字的形式</remarks>
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="FormatException">Start With 'label'</exception>
        public override void ExitId(ProgramParser.IdContext context)
        {
            var name = context.GetText();
            if (!name.StartsWith("label"))
            {
                Ir.Put(context, name);
            }
            else
            {
                throw new FormatException("Identifiers should not start with 'label'");
            }
        }

        #endregion
    }
}