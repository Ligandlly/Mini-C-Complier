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

        public readonly List<string> CodeSegment = new();

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
                CodeSegment.Add($"ORI $t{tmp}, $0, {l}");
                l = $"$t{tmp}";
            }
            else if (l[0] != '$')
            {
                int tmp = TmpReg++;
                usedTmpReg++;
                CodeSegment.Add($"LW  $t{tmp}, {_variables[l]}");
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
                        CodeSegment.Add($"SRL $t{tmp}, {src}, 1");
                        CodeSegment.Add($"ADD $t{tmp}, $t{tmp}, {src}");
                        CodeSegment.Add($"NOR $t{tmp}, $t{tmp}, $0");
                        CodeSegment.Add($"SRL {rlt}, $t{tmp}, 31");
                        break;
                    }
                case "~":
                    {
                        CodeSegment.Add($"NOR {rlt}, $0, {src}");
                        break;
                    }
                case "$":
                    {
                        CodeSegment.Add($"LW ${rlt}, 0(${src})");
                        break;
                    }
                case "=":
                    {
                        CodeSegment.Add($"OR {rlt}, {src}, $0");
                        break;
                    }

                default:
                    break;
            }

            if (originalRlt[0] != '$')
            {
                CodeSegment.Add($"SW {rlt}, {_variables[originalRlt]}");
            }

            TmpReg -= usedTmpReg;

            if (_hasComment)
            {
                CodeSegment.Add($"\t# {context.GetText()}");
            }
        }

        #endregion

        #region Program

        public override void EnterProgram(ProgramParser.ProgramContext context)
        {
            _dataSegment.Add(".DATA 0x100000");
            CodeSegment.AddRange(new[] { ".TEXT", "start:" });
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
            CodeSegment.Add(funcName + ':');
        }

        #endregion

        #region Binary

        private void Relop(string op, string lReg, string rReg, string rltReg)
        {
            switch (op)
            {
                case "==":
                    {
                        CodeSegment.Add($"XOR {rltReg}, {lReg}, {rReg}");
                        break;
                    }
                case "!=":
                    {
                        CodeSegment.Add($"SUB {rltReg}, {lReg}, {rReg}");
                        break;
                    }
                case "<":
                    {
                        CodeSegment.Add($"SLT {rltReg}, {lReg}, {rReg}");
                        break;
                    }
                case ">":
                    {
                        CodeSegment.Add($"SLT {rltReg}, {rReg}, {lReg}");
                        break;
                    }
                case "<=":
                    {
                        int tmp = TmpReg;
                        CodeSegment.Add($"ORI $t{tmp}, $0, 1");
                        CodeSegment.Add($"SLT {rltReg}, {rReg}, {lReg}");
                        CodeSegment.Add($"SUB {rltReg}, {rltReg}, $t{tmp}");
                        break;
                    }
                case ">=":
                    {
                        int tmp = TmpReg;
                        CodeSegment.Add($"ORI $t{tmp}, $0, 1");
                        CodeSegment.Add($"SLT {rltReg}, {lReg}, {rReg}");
                        CodeSegment.Add($"SUB {rltReg}, {rltReg}, $t{tmp}");
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

            CodeSegment.Add($"SLT $t{tmp1}, $0, {lReg}");
            CodeSegment.Add($"SLT $t{tmp2}, {lReg}, $0");
            CodeSegment.Add($"OR $t{tmp3}, {tmp1}, {tmp2}");

            CodeSegment.Add($"SLT $t{tmp1}, $0, {rReg}");
            CodeSegment.Add($"SLT $t{tmp2}, {rReg}, $0");
            CodeSegment.Add($"OR $t{tmp4}, {tmp1}, {tmp2}");

            string command = op == "||" ? "OR" : "AND";
            CodeSegment.Add($"{command} {rltReg}, $t{tmp3}, $t{tmp4}");

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
                CodeSegment.Add($"{command} {lReg}, {rReg}");
                CodeSegment.Add(op == "%" ? $"MFHI {rltReg}" : $"MFLO {rltReg}");
            }

            CodeSegment.Add($"{command} {rltReg}, {lReg}, {rReg}");
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
                CodeSegment.Add($"SW {rlt}, {_variables[originalRlt]}");

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
                CodeSegment.Add($"\t # {context.GetText()}");
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
        public override void ExitCall([NotNull] ProgramParser.CallContext context)
        {
            static string[] Push(string reg)
            {
                return new string[] {
                "ADDI $sp, $sp, -4", $"SW {reg}, 0($sp)"
            };
            }

            CodeSegment.AddRange(Push("$sp"));
            CodeSegment.AddRange(Push("$ra"));

            string funcName = context.Id().GetText();
            BackendFuncIdentity funcId = GetFuncId(funcName);

            Memory rlt = _variables[context.rlt.GetText()];

            CodeSegment.Add($"ADDI $sp, $sp, -{funcId.VariablesSize}");
            int tmp = TmpReg;
            CodeSegment.Add($"ORI $t{tmp}, $0, {funcId.VariablesSize >> 2}");
            CodeSegment.AddRange(Push($"$t{tmp}"));

            for (int i = 0; i < 8; ++i)
                CodeSegment.AddRange(Push($"$s{i}"));

            foreach (var item in context.param())
            {
                var text = item.GetText();
                if (char.IsDigit(text[0]))
                {
                    CodeSegment.Add($"ORI $t{tmp}, $0, {text}");
                    CodeSegment.AddRange(Push($"$t{tmp}"));
                }
                else
                {
                    var mem = _variables[text];

                    for (int i = 0; i < mem.Length; ++i)
                    {
                        CodeSegment.Add($"LW $t{tmp}, {mem.Offset + (i << 2)}({mem.Base})");
                        CodeSegment.AddRange(Push($"$t{tmp}"));
                    }

                }
            }

            CodeSegment.Add($"JAL {funcName}");
            CodeSegment.Add("OR $ra, $0, $v1");
            CodeSegment.Add($"SW $v0, {rlt}");
            if (_hasComment)
                CodeSegment.Add($"\t # call {funcName}");
        }



        void PublicReturnCode(string contextText)
        {
            static string[] Pop(string reg)
            => new[] {
                $"LW {reg}, 0($sp)",
                "ADDI $sp, $sp, 4"
            };

            for (int i = 0; i < 8; ++i)
                CodeSegment.AddRange(Pop($"$s{i}"));

            string funcName = _currentScopeName;
            BackendFuncIdentity funcId = GetFuncId(funcName);

            // Ignore the number of variables
            CodeSegment.Add("ADDI $sp, $sp, 4");
            // local variables and params
            CodeSegment.Add($"ADDI $sp, $sp, {funcId.VariablesSize}");

            CodeSegment.AddRange(Pop("$v1"));
            CodeSegment.AddRange(Pop("$sp"));
            if (_hasComment)
                CodeSegment.Add($"\t # {contextText}");
        }

        public override void ExitLiteralReturn([NotNull] ProgramParser.LiteralReturnContext context)
        {
            string rlt = context.Num().GetText();
            CodeSegment.Add($"ORI $v0, $0, {rlt}");

            PublicReturnCode(context.GetText());
        }

        public override void ExitVariableReturn([NotNull] ProgramParser.VariableReturnContext context)
        {
            var rlt = context.variable().GetText();
            CodeSegment.Add($"LW $v0, {rlt}");

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

        public override void ExitLabel([NotNull] ProgramParser.LabelContext context)
        {
            CodeSegment.Add(context.GetText());
        }

        public override void ExitJumpEqual([NotNull] ProgramParser.JumpEqualContext context)
        {
            var lOp = context.left.GetText();
            var rOp = context.right.GetText();
            var label = context.InlineLabel().GetText();
            var usedReg = 0;
            lOp = DealWithMemOrImme(lOp, ref usedReg);
            rOp = DealWithMemOrImme(rOp, ref usedReg);

            CodeSegment.Add($"BEQ ${lOp}, ${rOp}, {label}");

            TmpReg -= usedReg;
        }

        #endregion
        public override void ExitProgram(ProgramParser.ProgramContext context)
        {
            var stringBuilder = new StringBuilder();
            foreach (var line in _dataSegment)
                stringBuilder.AppendLine(line);
            foreach (var line in CodeSegment)
                stringBuilder.AppendLine(line);

            Result = stringBuilder.ToString();
        }
    }
}