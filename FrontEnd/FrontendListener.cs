﻿/*
 * Although she feeds me bread of bitterness, 
 * And sinks into my throat her tiger's tooth, 
 * Stealing my breath of life, 
 * I will confess I love this cultured hell that tests my youth! 
 * Her vigor flows like tides into my blood, 
 * Giving me strength erect against her hate. 
 * Her bigness sweeps my being like a flood. 
 * Yet as a rebel fronts a king in state, 
 * I stand within her walls with not a shred 
 * Of terror, malice, not a word of jeer. 
 * Darkly I gaze into the days ahead, 
 * And see her might and granite wonders there,
 * Beneath the touch of Time’s unerring hand, 
 * Like priceless treasures sinking in the sand.
 */

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FrontEnd
{
    using SymbolTable = Dictionary<string, Identity>;


    public class FrontEndException : Exception
    {
        public FrontEndException(string message) : base(message)
        {
        }
    }


    public class FrontEndListener
        : ProgramBaseListener
    {
        /// <summary>
        /// Intermediate Representation
        /// </summary>
        private readonly ParseTreeProperty<string> _ir = new();

        public string Result { get; private set; }

        /// <summary>
        /// Value of expressions
        /// </summary>
        private readonly ParseTreeProperty<Identity> _values = new();

        /// <summary>
        /// Symbol Tables
        /// </summary>
        public readonly Dictionary<string, SymbolTable> Tables = new();

        private readonly Stack<string> _currentScopeNameStack = new();

        private readonly IIrBuilder _irBuilder = new QuaternaryBuilder();

        public const string Global = "0_global";

        private int _labelNumber;

        private int LabelNumber => _labelNumber++;

        /// <summary>
        /// Template Variables List
        /// </summary>
        private readonly List<TmpIdentity> _tmpVariables = new();
        /// <summary>
        /// Create a new temporary variable
        /// </summary>
        /// <param name="type">The type of temporary variable</param>
        /// <param name="mutable"></param>
        /// <returns>Identity object</returns>
        private Identity NewTmpVar(string type, bool mutable = true)
        {
            var rlt = _tmpVariables.Count;
            var name = $"t{rlt}";
            _tmpVariables.Add(new TmpIdentity(name, type, _currentScopeNameStack.Peek()));

            return _tmpVariables.Last();
        }

        private void CopyIrAndValue(IParseTree context, IParseTree child)
        {
            _ir.Put(context, _ir.Get(child));
            _values.Put(context, _values.Get(child));
        }


        #region Program

        public override void EnterProgram(ProgramParser.ProgramContext context)
        {
            Tables.Add(Global, new SymbolTable());
            _currentScopeNameStack.Push(Global);
        }

        public override void ExitProgram(ProgramParser.ProgramContext context)
        {
            if (!Tables[Global].ContainsKey("main"))
                throw new FrontEndException("Main Function Undefined");

            // Temporary Variables
            StringBuilder stringBuilder = new();
            foreach (var tmpVariable in _tmpVariables)
                stringBuilder.AppendLine(_irBuilder.GenerateIr("decl_var", tmpVariable.Type, $"{tmpVariable.Name}@{tmpVariable.Scope}"));

            foreach (var declContext in context.decl())
                stringBuilder.AppendLine(_ir.Get(declContext));

            _ir.Put(context, stringBuilder.ToString());

            Result = stringBuilder.ToString();
            
            Result = Regex.Replace(Result, @"^\s*$[\r\n]*", string.Empty, RegexOptions.Multiline);

            Result += _irBuilder.GenerateIr("end") + "\r\n";
        }

        #endregion

        #region Primary_expr

        public override void ExitPrimary_exprHasExpr(ProgramParser.Primary_exprHasExprContext context)
        {
            CopyIrAndValue(context, context.expr());
        }

        public override void ExitPrimary_exprHasId(ProgramParser.Primary_exprHasIdContext context)
        {
            CopyIrAndValue(context, context.id());
        }

        public override void ExitPrimary_exprHasNum(ProgramParser.Primary_exprHasNumContext context)
        {
            CopyIrAndValue(context, context.num());
        }

        #endregion

        #region Decl

        /// <summary>
        /// Variable Declaration
        /// </summary>
        /// <example><c>int i;</c> => <c>decl_var; int; i;  </c></example>
        /// <exception cref="DuplicateNameException">Duplicate Variable Name</exception>
        /// <remarks>Local variables in function is declared by <c>decl_val</c>, while global variables are declared by <c>global</c>.</remarks>
        /// <param name="context"></param>
        public override void EnterVar_declHasId(ProgramParser.Var_declHasIdContext context)
        {
            var name = context.id().GetText();
            var type = context.type_spec().GetText();

            var table = Tables[_currentScopeNameStack.Peek()];

            // Check if the name has already in Symbol table
            if (table.ContainsKey(name))
            {
                throw new FrontEndException("Duplicate Variable Name");
            }

            table.Add(name, new Identity(name, type));

            var irCode = _irBuilder.GenerateIr( "decl_var", type,
                $"{name}@{_currentScopeNameStack.Peek()}");
            _ir.Put(context, irCode);
            _values.Put(context.id(), new Identity(name, type));
        }

        /// <summary>
        /// Function Definition, add function name to global symbol table, 
        /// push function name into name stack
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="DuplicateNameException">Duplicate Function Name</exception>
        public override void EnterFunc_def(ProgramParser.Func_defContext context)
        {
            var funcName = context.id().GetText();
            var rltType = context.type_spec() != null ? context.type_spec().GetText() : context.VOID().GetText();

            if (Tables[Global].Keys.Contains(funcName))
                throw new FrontEndException("Duplicate Function Name");

            _currentScopeNameStack.Push(funcName);
            var table = new SymbolTable();
            Tables.Add(funcName, table);

            // Add parameter list
            var tmpList = new List<(string, string)>();
            foreach (var child in context.param_list().children)
            {
                switch (child)
                {
                    case ProgramParser.ParamHasIntContext paramHasInt:
                    {
                        var type = paramHasInt.type_spec().GetText();
                        var name = paramHasInt.id().GetText();

                        table.Add(name, new Identity(name, type));
                        tmpList.Add((type, name));
                        break;
                    }
                    case ProgramParser.ParamHasArrContext paramHasArr:
                    {
                        var type = paramHasArr.type_spec().GetText();
                        var name = paramHasArr.id().GetText();

                        table.Add(name, new ArrIdentity(name, type + "Arr", int.Parse(paramHasArr.num().GetText())));
                        tmpList.Add((type + "Arr", name));
                        break;
                    }
                }
            }

            Tables[Global].Add(funcName, new FuncIdentity(funcName, rltType, tmpList.ToArray()));
        }

        /// <summary>
        /// Write function definition to IR
        /// And pop function name
        /// </summary>
        /// <example><c>int func1(int a, char b) { return a + b; } </c> =>
        /// <code>
        /// param_decl; int; a; ;
        /// param_decl; char; b; ;
        /// func; int; func1; 2;
        ///     +; a; b; 0_t;
        ///     return; 0_t; ; ;
        /// end_func; ; ; ;
        /// </code>
        /// </example>
        /// <param name="context"></param>
        /// <exception cref="DuplicateNameException"></exception>
        public override void ExitFunc_def(ProgramParser.Func_defContext context)
        {
            var rltBuilder = new StringBuilder();
            var funcName = context.id().GetText();
            var rltType = context.type_spec() != null ? context.type_spec().GetText() : context.VOID().GetText();

            var funcIdent = Tables[Global][funcName] as FuncIdentity;

            if (funcName != "main")
            {
                Debug.Assert(funcIdent != null, nameof(funcIdent) + " != null");
                foreach (var (paramType, paramName) in funcIdent.Params)
                {
                    if (paramType.Substring(paramType.Length - 3,3) == "Arr")
                    {
                        var arrId = Tables[_currentScopeNameStack.Peek()][paramName] as ArrIdentity;
                        Debug.Assert(arrId != null, nameof(arrId) + " != null");
                        rltBuilder.AppendLine(_irBuilder.GenerateIr("param_decl", paramType[..^3], arrId.Name,
                            $"{arrId.Length}"));
                    }
                    else
                        rltBuilder.AppendLine(_irBuilder.GenerateIr("param_decl", paramType, paramName));
                }
                rltBuilder.AppendLine(_irBuilder.GenerateIr("func", rltType, funcName, $"{funcIdent.Params.Length}"));
            }

            rltBuilder.AppendLine(_ir.Get(context.compound_stmt()));

            if (funcName != "main")
                rltBuilder.AppendLine(_irBuilder.GenerateIr("end_func"));

            var rlt = rltBuilder.ToString();
            _ir.Put(context, rlt);
            _currentScopeNameStack.Pop();
        }

        /// <summary>
        /// Array Declaration
        /// </summary>
        /// <remarks>Local variables in function is declared by <c>decl_arr</c>, while global variables are declared by <c>global_arr</c>.</remarks>
        /// <param name="context"></param>
        public override void EnterVar_declHasArr(ProgramParser.Var_declHasArrContext context)
        {
            var type = context.type_spec().GetText();
            var name = context.id().GetText();
            var length = int.Parse(context.num().GetText());
            var arr = new ArrIdentity(name, type + "Arr", length);

            Tables[Global].Add(name, arr);
            var irCode = _irBuilder.GenerateIr("decl_arr",
                type, $"{name}@{_currentScopeNameStack.Peek()}", length.ToString());

            _ir.Put(context, irCode);
        }

        public override void ExitDecl(ProgramParser.DeclContext context)
        {
            _ir.Put(context, _ir.Get(context.GetChild(0)));
        }

        #endregion

        #region Literal

        public override void ExitType_spec(ProgramParser.Type_specContext context)
        {
            _ir.Put(context, context.GetText());
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

            // _ir.Put(context, text);
            _values.Put(context, new Literal(text));
        }

        /// <summary>
        /// 變量
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="FormatException">Undefined Variable</exception>
        public override void ExitId(ProgramParser.IdContext context)
        {
            var name = context.GetText();
            var table = Tables[_currentScopeNameStack.Peek()];
            if (!table.ContainsKey(name))
            {
                if (Tables[Global].ContainsKey(name))
                    table = Tables[Global];
                else
                    throw new FrontEndException($"Undefined Identifier: {context.GetText()}");
            }

            var id = table[name];
            
            _values.Put(context, id);
        }

        #endregion

        #region Postfix_expr

        /// <summary>
        /// Function Call.
        /// This may introduce a temporary variable or may not
        /// </summary>
        /// <example>
        /// <c>int foo(int, int ,int); foo(a, 1, foo(2))</c> =>
        /// <code>
        /// param 2
        /// call foo 0_t 1
        /// param a
        /// param 1
        /// param 0_t
        /// call foo 1_t 3
        /// </code>
        /// </example>
        /// <param name="context"></param>
        public override void ExitPostfix_exprHasCall(ProgramParser.Postfix_exprHasCallContext context)
        {
            var funcName = context.id().GetText();

            var table = Tables[Global];

            // check whether the function is defined correctly
            if (!table.ContainsKey(funcName) || table[funcName] is not FuncIdentity func)
            {
                throw new FrontEndException("Function Undefined");
            }

            var paramsCount = 0;
            StringBuilder stringBuilder = new();
            foreach (var child in context.argument_expr_list().children)
            {
                if (child is not ProgramParser.ExprContext expr) continue;

                // Add expr to IR result
                stringBuilder.AppendLine(_ir.Get(child));

                var identity = _values.Get(expr);

                // Check Type
                if (identity.Type != "literal" && func.Params[paramsCount].paramType != identity.Type)
                    throw new FrontEndException("Arguments Type Not Match");

                paramsCount++;

                stringBuilder.AppendLine(_irBuilder.GenerateIr("param", identity.Name));
            }

            // Check Number
            if (paramsCount != func.Params.Length)
                throw new FrontEndException("Arguments Number Not Match");

            if (func.Type == FuncIdentity.Void)
            {
                stringBuilder.AppendLine(_irBuilder.GenerateIr("call", func.Name, $"{paramsCount}"));
                _values.Put(context, new Literal("0"));
            }
            else
            {
                var tmpVar = NewTmpVar(func.Type);
                stringBuilder.AppendLine(_irBuilder.GenerateIr("call", func.Name, $"{paramsCount}", tmpVar.Name));
                _values.Put(context, tmpVar);
            }

            _ir.Put(context, stringBuilder.ToString());
        }

        /// <summary>
        /// Empty Function Call.
        /// </summary>
        /// <example>
        /// <c>int foo(); x = foo();</c> =>
        /// call foo 0_t 0
        /// = 0_t ; x
        /// </example>
        /// <param name="context"></param>
        public override void ExitPostfix_exprHasEmptyCall(ProgramParser.Postfix_exprHasEmptyCallContext context)
        {
            var funcName = context.id().GetText();

            // If do not have function defined or the identity with the name is not defined as a function,
            var table = Tables[_currentScopeNameStack.Peek()];
            if (!table.ContainsKey(funcName) || table[funcName] is not FuncIdentity func)
            {
                throw new KeyNotFoundException("Function Undefined");
            }

            // If function has parameters,
            var paramsLength = func.Params.Length;
            if (paramsLength != 0)
                throw new ArgumentException($"Argument Error, Except {paramsLength}, found 0.");

            // All right
            if (func.Type == "void")
            {
                _ir.Put(context, _irBuilder.GenerateIr("call", funcName, "0"));
                _values.Put(context, new Literal("0"));
                return;
            }

            var type = func.Type;
            var tmpVar = NewTmpVar(type);
            _ir.Put(context, _irBuilder.GenerateIr("call", funcName, "0", tmpVar.Name));
            _values.Put(context, tmpVar);
        }

        /// <summary>
        /// postfix_expr: primary_expr
        /// </summary>
        /// <param name="context"></param>
        public override void ExitPostfix_exprHasPrimary_expr(ProgramParser.Postfix_exprHasPrimary_exprContext context)
        {
            CopyIrAndValue(context, context.primary_expr());
        }


        /// <summary>
        /// postfix_expr: primary_expr [ expr ]
        /// May add 1 or 2 temporary var
        /// </summary>
        /// 
        /// <example>
        /// 1
        /// <c>int arr[5]; arr[3];</c> =>
        /// <code>
        /// cp arr ; 0_t            # copy offset
        /// inc 12 ; 0_t            # get address of the 3rd item
        /// </code>
        /// </example>
        ///
        /// <example>
        /// <c>int arr[5]; arr[i];</c> =>
        /// <code>
        /// cp arr ; 0_t
        /// lsft i 2 1_t
        /// inc 1_t ; 0_t
        /// </code>
        /// </example>
        /// 
        /// <remarks>Only array identities are allowed.</remarks>
        /// <remarks>Offset is counted by Bytes</remarks>
        /// <param name="context"></param>
        public override void ExitPostfix_exprHasgetitem(ProgramParser.Postfix_exprHasgetitemContext context)
        {
            var stringBuilder = new StringBuilder();


            // Check identity first
            var identity = _values.Get(context.primary_expr());
            if (identity is not ArrIdentity arrIdentity)
                throw new FrontEndException("Get Item From Non-array Variable");

            var tmpRltId = NewTmpVar(arrIdentity.Type[..^3]);
            stringBuilder.AppendLine(_irBuilder.GenerateIr("cp", arrIdentity.Name, dist: tmpRltId.Name));

            // If expr is literal
            if (_values.Get(context.expr()) is Literal literal)
            {
                var offset = int.Parse(literal.Name) << 2;
                stringBuilder.AppendLine(_irBuilder.GenerateIr("inc", $"{offset}", dist: tmpRltId.Name));
            }
            else
            {
                var exprVal = _values.Get(context.expr());
                var tmpOffsetId = NewTmpVar(exprVal.Type[..^3]);
                stringBuilder.AppendLine(_irBuilder.GenerateIr("<<", exprVal.Name, "2", tmpOffsetId.Name));
                stringBuilder.AppendLine(_irBuilder.GenerateIr("inc", tmpOffsetId.Name, dist: tmpRltId.Name));
            }

            _ir.Put(context, stringBuilder.ToString());
            _values.Put(context, tmpRltId);
        }

        #endregion

        #region Unary_expr

        public override void ExitUnary_exprHasPostfix_expr(ProgramParser.Unary_exprHasPostfix_exprContext context)
        {
            CopyIrAndValue(context, context.postfix_expr());
        }

        /// <summary>
        /// Self Increment
        /// </summary>
        /// <example>
        /// <c>++i[3];</c> =>
        /// <code>
        /// cp i ; 0_t
        /// inc 12 ; 0_t
        /// + 0_t 1 0_t
        /// </code>
        /// </example>
        /// <param name="context"></param>
        /// <exception cref="FrontEndException">Increase a Immutable Value</exception>
        public override void ExitUnary_exprHasInc(ProgramParser.Unary_exprHasIncContext context)
        {
            var unaryExprVal = _values.Get(context.unary_expr());
            if (unaryExprVal.Mutable == false)
                throw new FrontEndException("Increase a Immutable Value");
            _ir.Put(context, _irBuilder.GenerateIr("+", unaryExprVal.Name, "1", unaryExprVal.Name));
            _values.Put(context, unaryExprVal);
        }

        /// <summary>
        /// Almost as same as inc
        /// </summary>
        /// <param name="context"></param>
        /// <seealso cref="ExitUnary_exprHasInc"/>
        /// <exception cref="FrontEndException">Decrease a Immutable Value</exception>
        public override void ExitUnary_exprHasDec(ProgramParser.Unary_exprHasDecContext context)
        {
            var unaryExprVal = _values.Get(context.unary_expr());
            if (unaryExprVal.Mutable == false)
                throw new FrontEndException("Decrease a Immutable Value");
            _ir.Put(context, _irBuilder.GenerateIr("-", unaryExprVal.Name, "1", unaryExprVal.Name));
            _values.Put(context, unaryExprVal);
        }

        /// <summary>
        /// $a, $0xff
        /// </summary>
        /// <example>
        /// <c>$a = 4</c> =>
        /// <code>
        /// $ a ; 0_t
        /// = 4 ; 0_t
        /// </code>
        /// </example>
        /// <param name="context"></param>
        public override void ExitUnary_exprHasDol([NotNull] ProgramParser.Unary_exprHasDolContext context)
        {
            var exprValue = _values.Get(context.unary_expr());
            var tmpRlt = NewTmpVar(Identity.Int);

            _ir.Put(context, _irBuilder.GenerateIr("$", exprValue.Name, dist: tmpRlt.Name));
            _values.Put(context, tmpRlt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// <c>a = !b</c> =>
        /// ! b ; 0_t
        /// = 0_t ; a
        /// </example>
        /// <param name="context"></param>
        public override void ExitUnary_exprHasLNot([NotNull] ProgramParser.Unary_exprHasLNotContext context)
        {
            var unaryId = _values.Get(context.unary_expr());
            var tmpRlt = NewTmpVar(unaryId.Type, false);

            _ir.Put(context, _irBuilder.GenerateIr("!", unaryId.Name, dist: tmpRlt.Name));
            _values.Put(context, tmpRlt);
        }

        public override void ExitUnary_exprHasNot([NotNull] ProgramParser.Unary_exprHasNotContext context)
        {
            var unaryId = _values.Get(context.unary_expr());
            var tmpRlt = NewTmpVar(unaryId.Type, false);

            _ir.Put(context, _irBuilder.GenerateIr("!", unaryId.Name, dist: tmpRlt.Name));
            _values.Put(context, tmpRlt);
        }

        #endregion

        #region Assignment_expr

        public override void ExitAssignmentExprHasBinary([NotNull] ProgramParser.AssignmentExprHasBinaryContext context)
            => CopyIrAndValue(context, context.binaryExpr());


        /// <summary>
        /// Assignment
        /// </summary>
        /// <param name="context"></param>
        public override void ExitAssignmentExprHasAssign([NotNull] ProgramParser.AssignmentExprHasAssignContext context)
        {
            StringBuilder stringBuilder = new();
            var unaryVal = _values.Get(context.unary_expr());
            var assVal = _values.Get(context.assignmentExpr());
            if (unaryVal.Mutable == false)
                throw new FrontEndException("Change Value of an Immutable Variable");

            stringBuilder.AppendLine(_ir.Get(context.unary_expr()));
            stringBuilder.AppendLine(_ir.Get(context.assignmentExpr()));
            stringBuilder.AppendLine(_irBuilder.GenerateIr("=", assVal.Name, dist: unaryVal.Name));

            _ir.Put(context, stringBuilder.ToString());
            _values.Put(context, assVal);
        }

        #endregion

        #region Binary_expr

        /// <summary>
        /// Binary Expression
        /// </summary>
        /// <example>
        /// <c>a + b</c> =>
        /// <code>
        /// + a b 0_t
        /// </code>
        /// </example>
        /// <param name="context"></param>
        /// <exception cref="FrontEndException">Divided By Zero</exception>
        public override void ExitBinaryExpr([NotNull] ProgramParser.BinaryExprContext context)
        {
            if (context.num() != null)
            {
                CopyIrAndValue(context, context.num());
            }
            else if (context.unary_expr() != null)
            {
                CopyIrAndValue(context, context.unary_expr());
            }
            else
            {
                StringBuilder stringBuilder = new();
                var leftVal = _values.Get(context.left);
                var rightVal = _values.Get(context.right);
                var tmpRlt = NewTmpVar(leftVal.Type);

                if (context.op.Text is "/" or "%")
                {
                    if (_ir.Get(context.right) == "0")
                        throw new FrontEndException("Divided By Zero");
                }
                
                stringBuilder.AppendLine(_ir.Get(context.left));
                stringBuilder.AppendLine(_ir.Get(context.right));
                stringBuilder.AppendLine(_irBuilder.GenerateIr(context.op.Text, leftVal.Name, rightVal.Name, tmpRlt.Name));
                _ir.Put(context, stringBuilder.ToString());
                _values.Put(context, tmpRlt);
            }
        }

        #endregion

        #region Expr

        public override void ExitExpr([NotNull] ProgramParser.ExprContext context)
        {
            CopyIrAndValue(context, context.assignmentExpr());
        }

        #endregion

        #region Selection_stmt

        /// <summary>
        /// if (expr) stmt
        /// </summary>
        /// <example>
        /// <code>
        /// if (a) {
        ///     b += 1;
        ///     if (c) {
        ///         b += 2;
        ///     }
        /// }
        /// b+= 3;
        /// </code>
        /// 
        /// =>
        /// 
        /// <code>
        ///     Je a 0 label0 -------------|
        ///     + b 1 b                     |
        ///     Je c 0 label1 ---------|   |
        ///     + b 2 b                 |   |
        /// label1:   -----------------|   |
        /// label0:    --------------------|
        ///     + b 3 b
        /// </code>
        /// </example>
        /// <param name="context"></param>
        public override void ExitSelection_stmtHasEmpty([NotNull] ProgramParser.Selection_stmtHasEmptyContext context)
        {
            var exprVal = _values.Get(context.expr());
            var endIf = LabelNumber;
            var head = _irBuilder.GenerateIr("Je", exprVal.Name, "0", $"label{endIf}");
            var tail = _irBuilder.GenerateIr(labelNumber: endIf);

            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine(_ir.Get(context.expr()));
            stringBuilder.AppendLine(head);
            stringBuilder.AppendLine(_ir.Get(context.stmt()));
            stringBuilder.AppendLine(tail);

            _ir.Put(context, stringBuilder.ToString());
        }

        /// <summary>
        /// IF '(' expr ')' ifStmt=stmt ELSE elseStmt=stmt
        /// </summary>
        /// <example>
        /// <code>
        /// if (a) {
        ///     b += 1;
        ///     if (c) {
        ///         b += 2;
        ///     } else {
        ///         b += 3;
        ///     }
        /// } else {
        ///     b += 4;
        /// }
        /// b += 5;
        /// </code>
        /// 
        /// =>
        /// 
        /// <code>
        ///         Je  a   0   label0  -------------|
        ///         +   b   1   b                     |
        ///         Je  b   0   label2               |
        ///         +   b   2   b                     |
        ///         J label3                         |
        /// label2:                                  |
        ///         +   b   3   b                     |
        /// label3:                                  |
        ///         J label1     --------------------|
        /// label0:    ------------------------------|
        ///         +   b   4   b                     |
        /// label1:  --------------------------------|
        ///         +   b   5   b
        /// </code>
        /// </example>
        /// <param name="context"></param>
        public override void ExitSelection_stmtHasElse([NotNull] ProgramParser.Selection_stmtHasElseContext context)
        {
            var exprVal = _values.Get(context.expr());
            var endIf = LabelNumber;
            var endElse = LabelNumber;

            var head = _irBuilder.GenerateIr("Je", exprVal.Name, "0", $"label{endIf}");
            var middle = _irBuilder.GenerateIr("J", $"label{endElse}") + "\n"
                                                                        + _irBuilder.GenerateIr(labelNumber: endIf);
            var tail = _irBuilder.GenerateIr(labelNumber: endElse);

            StringBuilder stringBuilder = new();

            var exprIr = _ir.Get(context.expr());

            stringBuilder.AppendLine(exprIr);
            stringBuilder.AppendLine(head);
            stringBuilder.AppendLine(_ir.Get(context.ifStmt));
            stringBuilder.AppendLine(middle);
            stringBuilder.AppendLine(_ir.Get(context.elseStmt));
            stringBuilder.AppendLine(tail);

            _ir.Put(context, stringBuilder.ToString());
        }

        #endregion

        #region Expr_stmt

        public override void ExitExpr_stmt(ProgramParser.Expr_stmtContext context)
        {
            _ir.Put(context, _ir.Get(context.expr()));
        }

        #endregion

        #region Iteration_stmt

        private readonly Stack<(int start, int end)> _labelStack = new();

        public override void EnterIteration_stmt(ProgramParser.Iteration_stmtContext context)
        {
            var startLabel = LabelNumber;
            var endLabel = LabelNumber;
            _labelStack.Push((startLabel, endLabel));
        }

        public override void ExitIteration_stmt(ProgramParser.Iteration_stmtContext context)
        {
            var (startLabel, endLabel) = _labelStack.Pop();

            var exprVal = _values.Get(context.expr());
            var head = _irBuilder.GenerateIr(labelNumber: startLabel) + "\n"
                                                                      + _irBuilder.GenerateIr("Je", exprVal.Name, "0",
                                                                          $"label{endLabel}");
            var tail = _irBuilder.GenerateIr("J", $"label{startLabel}") + "\n" +
                       _irBuilder.GenerateIr(labelNumber: endLabel);

            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine(_ir.Get(context.expr()));
            stringBuilder.AppendLine(head);
            stringBuilder.AppendLine(_ir.Get(context.stmt()));
            stringBuilder.AppendLine(tail);

            _ir.Put(context, stringBuilder.ToString());
        }

        #endregion

        #region Single_stmt
        
        public override void ExitContinue_stmt(ProgramParser.Continue_stmtContext context)
        {
            if (_labelStack.TryPeek(out (int, int) labels) == false)
                throw new FrontEndException("Use Continue Outside While Statement");
            _ir.Put(context, _irBuilder.GenerateIr("J", $"label{labels.Item1}"));
        }

        public override void ExitBreak_stmt(ProgramParser.Break_stmtContext context)
        {
            if (_labelStack.TryPeek(out (int, int) labels) == false)
                throw new FrontEndException("Use Break Outside While Statement");
            _ir.Put(context, _irBuilder.GenerateIr("J", $"label{labels.Item2}"));
        }

        public override void ExitReturn_stmt(ProgramParser.Return_stmtContext context)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine(_ir.Get(context.expr()));
            var exprVal = _values.Get(context.expr());
            stringBuilder.AppendLine(_irBuilder.GenerateIr("return", exprVal.Name));
            _ir.Put(context, stringBuilder.ToString());
        }

        #endregion

        #region Compound_stmt

        public override void ExitCompound_stmtHasBody(ProgramParser.Compound_stmtHasBodyContext context)
        {
            _ir.Put(context, _ir.Get(context.block_item_list()));
        }

        public override void ExitBlock_item_list(ProgramParser.Block_item_listContext context)
        {
            StringBuilder stringBuilder = new();
            foreach (var blockItem in context.block_item())
                stringBuilder.AppendLine(_ir.Get(blockItem));

            _ir.Put(context, stringBuilder.ToString());
        }

        public override void ExitBlock_item(ProgramParser.Block_itemContext context)
        {
            StringBuilder stringBuilder = new();
            foreach (var child in context.children)
                stringBuilder.AppendLine(_ir.Get(child));

            _ir.Put(context, stringBuilder.ToString());
        }

        #endregion

        #region Stmt

        public override void ExitStmt(ProgramParser.StmtContext context)
        {
            _ir.Put(context, _ir.Get(context.GetChild(0)));
        }

        #endregion
    }
}