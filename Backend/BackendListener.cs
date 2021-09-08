// FIXME: Symbol Tables is redundant
// FIXME: `_typeMap` is never used

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Misc;
using Frontend;
using SymbolTable = System.Collections.Generic.Dictionary<string, Frontend.Identity>;

namespace Backend
{
    public class BackendException : Exception
    {
        public BackendException(string message) : base(message)
        {
        }
    }

    public class BackendListener : ProgramBaseListener
    {
        public string Result { get; private set; }

        private readonly HashSet<string> _relopSet = new()
        {
            "<",
            ">",
            "==",
            "!=",
            "<=",
            ">="
        };

        private readonly HashSet<string> _multSet = new()
        {
            "*",
            "/",
            "%"
        };

        private readonly HashSet<string> _binaryLogicalSet = new()
        {
            "||",
            "&&"
        };

        private readonly bool _hasComment;

        private bool _isDeclFin;

        private readonly Dictionary<BackendFuncIdentity, int> _stackOffset = new();

        /// <summary>
        /// Name And Scope
        /// </summary>
        private readonly Dictionary<string, Memory> _variables = new();

        private readonly List<string> _dataSegment = new();

        private readonly List<string> _codeSegment = new();

        public const string Global = FrontEndListener.Global;

        private string _currentScopeName = Global;

        /// <summary>
        /// Naive Name
        /// </summary>
        private readonly Dictionary<string, SymbolTable> _tables = new();

        private readonly Dictionary<string, string> _typeMap = new()
        {
            { "int", "word" },
            { "short", "half" },
            { "char", "byte" }
        };

        private int _tmpReg;

        private int TmpReg
        {
            get => _tmpReg;
            set
            {
                if (value > 7)
                {
                    throw new BackendException("Temporary Register Run Out");
                }

                _tmpReg = value;
            }
        }

        public BackendListener(bool hasComment = true)
        {
            _hasComment = hasComment;
        }

        private BackendFuncIdentity GetFuncId(string funcName)
        {
            BackendFuncIdentity rlt = _tables[Global][funcName] as BackendFuncIdentity;

            Debug.Assert(rlt != null, nameof(rlt) + " != null");
            return rlt;
        }

        private string DealWithMemOrImme(string l, ref int usedTmpReg)
        {
            if (char.IsDigit(l[0]))
            {
                int tmp = TmpReg++;
                usedTmpReg++;
                _codeSegment.Add($"ORI $t{tmp}, $0, {l}");
                l = $"$t{tmp}";
            }
            else if (l[0] != '$')
            {
                int tmp = TmpReg++;
                usedTmpReg++;
                _codeSegment.Add($"LW  $t{tmp}, {_variables[l]}");
                l = $"$t{tmp}";
            }

            return l;
        }


        #region Unary

        public override void ExitUnary(ProgramParser.UnaryContext context)
        {
            string op = context.UnaryOp().GetText();
            string src = context.src.GetText();
            string rlt = context.rlt.GetText();

            int usedTmpReg = 0;
            src = DealWithMemOrImme(src, ref usedTmpReg);
            string originalRlt = rlt;
            rlt = DealWithMemOrImme(rlt, ref usedTmpReg);

            switch (op)
            {
                case "!":
                    // (~(x>>1)+x)>>31
                    {
                        int tmp = TmpReg;
                        _codeSegment.Add($"SRL $t{tmp}, {src}, 1");
                        _codeSegment.Add($"ADD $t{tmp}, $t{tmp}, {src}");
                        _codeSegment.Add($"NOR $t{tmp}, $t{tmp}, $0");
                        _codeSegment.Add($"SRL {rlt}, $t{tmp}, 31");
                        break;
                    }
                case "~":
                    {
                        _codeSegment.Add($"NOR {rlt}, $0, {src}");
                        break;
                    }
                case "$":
                    {
                        _codeSegment.Add($"LW ${rlt}, 0(${src})");
                        break;
                    }
                case "=":
                    {
                        _codeSegment.Add($"OR {rlt}, {src}, $0");
                        break;
                    }

                default:
                    break;
            }

            if (originalRlt[0] != '$')
            {
                _codeSegment.Add($"SW {rlt}, {_variables[originalRlt]}");
            }

            TmpReg -= usedTmpReg;

            if (_hasComment)
            {
                _codeSegment.Add($"\t# {context.GetText()}");
            }
        }

        #endregion

        #region Program

        public override void EnterProgram(ProgramParser.ProgramContext context)
        {
            _dataSegment.Add(".DATA 0x100000");
            _codeSegment.AddRange(new[] { ".TEXT", "start:", "J main" });
            _tables[Global] = new SymbolTable();
        }

        public override void ExitDecls(ProgramParser.DeclsContext context)
        {
            _isDeclFin = true;
        }

        #endregion

        #region Decls

        public override void ExitFuncDecl(ProgramParser.FuncDeclContext context)
        {
            string name = context.Id().GetText();
            string type = context.Type().GetText();

            BackendFuncIdentity backendFuncIdentity = new BackendFuncIdentity(name, type);
            _tables[Global].Add(name, backendFuncIdentity);
            _tables.Add(name, new SymbolTable());
            _stackOffset.Add(backendFuncIdentity, 36);
        }

        public override void ExitVariableDecl(ProgramParser.VariableDeclContext context)
        {
            string type = context.Type().GetText();
            string name = context.variable().name.Text;
            string scope = context.variable().scope.Text;
            int num = context.Num() != null ? int.Parse(context.Num().GetText()) : 1;

            // Add to Symbol Table
            SymbolTable table = _tables[scope];
            table.Add(name, new VariableIdentity(name, type, scope, num));

            // If Variable is global
            if (scope == Global)
            {
                var zeros = string.Join(", ", Enumerable.Repeat("0", num));
                _dataSegment.Add($"{name}: .word {zeros}");
                _variables.Add($"{name}@{scope}", new Memory(@base: name));
                return;
            }

            BackendFuncIdentity funcIdentity = GetFuncId(scope);
            int offset = _stackOffset[funcIdentity];
            _stackOffset[funcIdentity] += num << 2;
            funcIdentity.VariablesSize += num << 2;
            funcIdentity.OffsetPairs.Add(name, new OffsetPair(name, offset));
            _variables.Add($"{name}@{scope}", new Memory(offset: offset, isArrHead: num != 1, length: num));
        }

        // public override void ExitParamDecl(ProgramParser.ParamDeclContext context)
        // {
        //     var type = context.Type().GetText();
        //     var name = context.variable().name.Text;
        //     var scope = context.variable().scope.Text;
        //     var num = context.Num() != null ? int.Parse(context.Num().GetText()) : 1;

        //     // Add to Symbol Table
        //     var table = _tables[scope];
        //     table.Add(name, new VariableIdentity(name, type, scope, num));

        //     var funcIdentity = _tables[Global][scope] as BackendFuncIdentity;
        //     var offset = _stackOffset[funcIdentity];
        //     _stackOffset[funcIdentity] += num << 2;
        //     funcIdentity.OffsetPairs.Add(name, new OffsetPair(name, offset));
        //     _variables.Add($"{name}@{scope}", new Memory(offset: offset));
        // }

        public override void EnterFuncDef(ProgramParser.FuncDefContext context)
        {
            var funcName = context.funcHead().Id().GetText();
            _currentScopeName = funcName;
            _codeSegment.Add(funcName + ':');
        }

        #endregion

        #region Binary

        private void Relop(string op, string lReg, string rReg, string rltReg)
        {
            switch (op)
            {
                case "==":
                    {
                        _codeSegment.Add($"XOR {rltReg}, {lReg}, {rReg}");
                        break;
                    }
                case "!=":
                    {
                        _codeSegment.Add($"SUB {rltReg}, {lReg}, {rReg}");
                        break;
                    }
                case "<":
                    {
                        _codeSegment.Add($"SLT {rltReg}, {lReg}, {rReg}");
                        break;
                    }
                case ">":
                    {
                        _codeSegment.Add($"SLT {rltReg}, {rReg}, {lReg}");
                        break;
                    }
                case "<=":
                    {
                        int tmp = TmpReg;
                        _codeSegment.Add($"ORI $t{tmp}, $0, 1");
                        _codeSegment.Add($"SLT {rltReg}, {rReg}, {lReg}");
                        _codeSegment.Add($"SUB {rltReg}, {rltReg}, $t{tmp}");
                        break;
                    }
                case ">=":
                    {
                        int tmp = TmpReg;
                        _codeSegment.Add($"ORI $t{tmp}, $0, 1");
                        _codeSegment.Add($"SLT {rltReg}, {lReg}, {rReg}");
                        _codeSegment.Add($"SUB {rltReg}, {rltReg}, $t{tmp}");
                        break;
                    }

                default:
                    break;
            }
        }

        private void BinaryLogicalOp(string op, string lReg, string rReg, string rltReg)
        {
            int tmp1 = TmpReg++;
            int tmp2 = TmpReg++;
            int tmp3 = TmpReg++;
            int tmp4 = TmpReg++;

            _codeSegment.Add($"SLT $t{tmp1}, $0, {lReg}");
            _codeSegment.Add($"SLT $t{tmp2}, {lReg}, $0");
            _codeSegment.Add($"OR $t{tmp3}, {tmp1}, {tmp2}");

            _codeSegment.Add($"SLT $t{tmp1}, $0, {rReg}");
            _codeSegment.Add($"SLT $t{tmp2}, {rReg}, $0");
            _codeSegment.Add($"OR $t{tmp4}, {tmp1}, {tmp2}");

            string command = op == "||" ? "OR" : "AND";
            _codeSegment.Add($"{command} {rltReg}, $t{tmp3}, $t{tmp4}");

            TmpReg -= 4;

        }

        private void BinaryAlgebraOp(string op, string lReg, string rReg, string rltReg)
        {
            string command = op switch
            {
                "+" => "ADD",
                "-" => "SUB",
                "*" => "MULT",
                "/" => "DIV",
                "%" => "DIV",
                "<<" => "SLLV",
                ">>" => "SRAV",
                "&" => "AND",
                "|" => "OR",
                "^" => "XOR",
                _ => throw new NotImplementedException()
            };

            if (_multSet.Contains(op))
            {
                _codeSegment.Add($"{command} {lReg}, {rReg}");
                _codeSegment.Add(op == "%" ? $"MFHI {rltReg}" : $"MFLO {rltReg}");
            }

            _codeSegment.Add($"{command} {rltReg}, {lReg}, {rReg}");
        }

        private void BinOp(string op, string l, string r, string rlt)
        {
            int usedTmpReg = 0;
            l = DealWithMemOrImme(l, ref usedTmpReg);
            r = DealWithMemOrImme(r, ref usedTmpReg);
            string originalRlt = rlt;
            rlt = DealWithMemOrImme(rlt, ref usedTmpReg);

            if (_relopSet.Contains(op))
            {
                Relop(op, l, r, rlt);
            }
            else if (_binaryLogicalSet.Contains(op))
            {
                BinaryLogicalOp(op, l, r, rlt);
            }
            else
            {
                BinaryAlgebraOp(op, l, r, rlt);
            }

            if (originalRlt[0] != '$')
                _codeSegment.Add($"SW {rlt}, {_variables[originalRlt]}");

            TmpReg -= usedTmpReg;
        }

        public override void ExitBinary(ProgramParser.BinaryContext context)
        {
            string op = context.BinaryOp().GetText();
            string left = context.left.GetText();
            string right = context.right.GetText();
            string rlt = context.rlt.GetText();

            BinOp(op, left, right, rlt);
            if (_hasComment)
                _codeSegment.Add($"\t # {context.GetText()}");
        }

        #endregion

        #region CallAndReturn

        /// <summary>
        /// <ol>
        /// <li>numbers</li>
        /// <li>local variables</li>
        /// <li>params</li>
        /// <li>old sp</li>
        /// </ol>
        /// </summary>
        /// <param name="context"></param>
        public override void ExitCall(ProgramParser.CallContext context)
        {
            static string[] Push(string reg)
            {
                return new string[] {
                "ADDI $sp, $sp, -4", $"SW {reg}, 0($sp)"
            };
            }

            var originalSp = TmpReg++;
            var tmp = TmpReg++;
            
            _codeSegment.Add($"OR $t{originalSp}, $0, $sp");

            _codeSegment.AddRange(Push("$sp"));
            _codeSegment.AddRange(Push("$ra"));

            var funcName = context.Id().GetText();
            var funcId = GetFuncId(funcName);

            var rlt = _variables[context.rlt.GetText()];
            
            foreach (var item in context.param())
            {
                var text = item.GetChild(2).GetText();
                if (char.IsDigit(text[0]))
                {
                    _codeSegment.Add($"ORI $t{tmp}, $0, {text}");
                    _codeSegment.AddRange(Push($"$t{tmp}"));
                }
                else
                {
                    var mem = _variables[text];

                    for (var i = 0; i < mem.Length; ++i)
                    {
                        _codeSegment.Add($"LW $t{tmp}, {mem.Offset + (i << 2)}($t{originalSp})");
                        _codeSegment.AddRange(Push($"$t{tmp}"));
                    }

                }
            }

            _codeSegment.Add($"ORI $t{tmp}, $0, {funcId.VariablesSize >> 2}");
            _codeSegment.AddRange(Push($"$t{tmp}"));

            for (var i = 0; i < 8; ++i)
                _codeSegment.AddRange(Push($"$s{i}"));

            _codeSegment.Add($"JAL {funcName}");
            _codeSegment.Add("OR $ra, $0, $v1");
            _codeSegment.Add($"SW $v0, {rlt}");
            if (_hasComment)
                _codeSegment.Add($"\t # call {funcName}");

            TmpReg -= 2;
        }



        void PublicReturnCode(string contextText)
        {
            static string[] Pop(string reg)
            => new[] {
                $"LW {reg}, 0($sp)",
                "ADDI $sp, $sp, 4"
            };

            for (int i = 0; i < 8; ++i)
                _codeSegment.AddRange(Pop($"$s{i}"));

            string funcName = _currentScopeName;
            BackendFuncIdentity funcId = GetFuncId(funcName);

            // Ignore the number of variables
            _codeSegment.Add("ADDI $sp, $sp, 4");
            // local variables and params
            _codeSegment.Add($"ADDI $sp, $sp, {funcId.VariablesSize}");

            // Pop $ra into $v1
            // ra in stack should not cover $ra due to jr will use it.
            _codeSegment.AddRange(Pop("$v1"));
            _codeSegment.AddRange(Pop("$sp"));
            _codeSegment.Add($"JR");
            if (_hasComment)
                _codeSegment.Add($"\t # {contextText}");
        }

        public override void ExitLiteralReturn(ProgramParser.LiteralReturnContext context)
        {
            string rlt = context.Num().GetText();
            _codeSegment.Add($"ORI $v0, $0, {rlt}");

            PublicReturnCode(context.GetText());
        }

        public override void ExitVariableReturn(ProgramParser.VariableReturnContext context)
        {
            var rlt = context.variable().GetText();
            _codeSegment.Add($"LW $v0, {rlt}");

            PublicReturnCode(context.GetText());
        }

        #endregion

        #region Variable

        public override void ExitVariable(ProgramParser.VariableContext context)
        {
            if (_isDeclFin == false) return;
            if (context.Num() == null) return;

            string scope = context.scope.Text;
            string name = context.name.Text;
            VariableIdentity id = _tables[scope][name] as VariableIdentity;
            int num = int.Parse(context.Num().GetText());
            Debug.Assert(id != null, nameof(id) + " != null");
            Memory mem = _variables[id.Name];
            if (context.offset != null)
            {
                _variables.Add(context.GetText(), new Memory(mem.Base, mem.Offset + (num << 2)));
            }
        }

        #endregion

        #region Jump

        public override void ExitLabel(ProgramParser.LabelContext context)
        {
            _codeSegment.Add(context.GetText());
        }

        public override void ExitJumpEqual(ProgramParser.JumpEqualContext context)
        {
            var lOp = context.left.GetText();
            var rOp = context.right.GetText();
            var label = context.InlineLabel().GetText();
            var usedReg = 0;
            lOp = DealWithMemOrImme(lOp, ref usedReg);
            rOp = DealWithMemOrImme(rOp, ref usedReg);

            _codeSegment.Add($"BEQ ${lOp}, ${rOp}, {label}");

            TmpReg -= usedReg;
        }

        #endregion
        public override void ExitProgram(ProgramParser.ProgramContext context)
        {
            var stringBuilder = new StringBuilder();
            foreach (var line in _dataSegment)
                stringBuilder.AppendLine(line);
            foreach (var line in _codeSegment)
                stringBuilder.AppendLine(line);

            Result = stringBuilder.ToString();
        }
    }
}