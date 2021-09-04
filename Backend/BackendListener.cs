using Frontend;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Backend
{
    using SymbolTable = Dictionary<string, Identity>;

    internal record Memory
    {
        public string Base { get; init; }
        public int Offset { get; set; }

        public Memory(string @base = "$sp", int offset = 0)
        {
            Base = @base;
            Offset = offset;
        }

        public override string ToString()
        {
            return $"{Offset}({Base})";
        }
    }

    public class BackendException : Exception
    {
        public BackendException(string message) : base(message)
        {
        }
    }

    public class BackendListener : ProgramBaseListener
    {
        private const string RelationReg = "$s0";

        private HashSet<string> _relopSet = new()
        {
            "<", ">", "==", "!=", "<=", ">="
        };

        private HashSet<string> _multSet = new()
        {
            "*", "/", "%"
        };

        private bool _isDeclFin = false;

        private Dictionary<BackendFuncIdentity, int> _stackOffset = new();

        /// <summary>
        /// Name And Scope
        /// </summary>
        private readonly Dictionary<string, Memory> _variables = new();

        private readonly List<string> _functions = new();

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

        private int _tmpReg = 0;

        private int TmpReg
        {
            get => _tmpReg;
            set
            {
                if (value > 7)
                    throw new BackendException("Temporary Register Run Out");
                _tmpReg = value;
            }
        }

        private BackendFuncIdentity GetFuncId(string funcName)
        {
            var rlt = _tables[Global][funcName] as BackendFuncIdentity;

            Debug.Assert(rlt != null, nameof(rlt) + " != null");
            return rlt;
        }

        void Relop(string op, string lReg, string rReg)
        {
            switch (op)
            {
                case "==":
                {
                    _codeSegment.Add($"XOR {RelationReg}, {lReg}, {rReg}");
                    break;
                }
                case "!=":
                {
                    _codeSegment.Add($"SUB {RelationReg}, {lReg}, {rReg}");
                    break;
                }
                case "<":
                {
                    _codeSegment.Add($"SLT {RelationReg}, {lReg}, {rReg}");
                    break;
                }
                case ">":
                {
                    _codeSegment.Add($"SLT {RelationReg}, {rReg}, {lReg}");
                    break;
                }
                case "<=":
                {
                    var tmp = TmpReg;
                    _codeSegment.Add($"ORI $t{tmp}, $0, 1");
                    _codeSegment.Add($"SLT {RelationReg}, {rReg}, {lReg}");
                    _codeSegment.Add($"SUB {RelationReg}, {RelationReg}, $t{tmp}");
                    break;
                }
                case ">=":
                {
                    var tmp = TmpReg;
                    _codeSegment.Add($"ORI $t{tmp}, $0, 1");
                    _codeSegment.Add($"SLT {RelationReg}, {lReg}, {rReg}");
                    _codeSegment.Add($"SUB {RelationReg}, {RelationReg}, $t{tmp}");
                    break;
                }
            }
        }

        void BinOp(string op, string l, string r, string rlt)
        {
            var usedTmpReg = 0;
            l = DealWithMemOrImme(l, ref usedTmpReg);
            r = DealWithMemOrImme(r, ref usedTmpReg);
            rlt = DealWithMemOrImme(rlt, ref usedTmpReg);

            if (_relopSet.Contains(op))
            {
                Relop(op, l, r);
                TmpReg -= usedTmpReg;
                return;
            }

            var command = op switch
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
                "^" => "XOR"
            };

            if (_multSet.Contains(op))
            {
                _codeSegment.Add($"{command} {l}, {r}");
                _codeSegment.Add(op == "%" ? $"MFHI {rlt}" : $"MFLO {rlt}");
            }

            _codeSegment.Add($"{command} {rlt}, {l}, {r}");
            TmpReg -= usedTmpReg;
        }

        private string DealWithMemOrImme(string l, ref int usedTmpReg)
        {
            if (char.IsDigit(l[0]))
            {
                var tmp = TmpReg++;
                usedTmpReg++;
                _codeSegment.Add($"ORI $t{tmp}, $0, {l}");
                l = $"$t{tmp}";
            }
            else if (l[0] != '$')
            {
                var tmp = TmpReg++;
                usedTmpReg++;
                _codeSegment.Add($"LW  $t{tmp}, {l}");
                l = $"$t{tmp}";
            }

            return l;
        }

        #region Program

        public override void EnterProgram(ProgramParser.ProgramContext context)
        {
            _dataSegment.Add(".DATA 0x100000");
            _codeSegment.AddRange(new[] { ".TEXT", "start:" });
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
            var name = context.Id().GetText();
            var type = context.Type().GetText();

            var backendFuncIdentity = new BackendFuncIdentity(name, type);
            _tables[Global].Add(name, backendFuncIdentity);
            _tables.Add(name, new SymbolTable());
            _stackOffset.Add(backendFuncIdentity, 36);
        }

        public override void ExitVariableDecl(ProgramParser.VariableDeclContext context)
        {
            var type = context.Type().GetText();
            var name = context.variable().name.Text;
            var scope = context.variable().scope.Text;
            var num = context.Num() != null ? int.Parse(context.Num().GetText()) : 1;

            // Add to Symbol Table
            var table = _tables[scope];
            table.Add(name, new VariableIdentity(name, type, scope, num));

            // If Variable is in a function
            if (scope == Global)
            {
                _variables.Add($"{name}@{scope}", new Memory(@base: name));
                return;
            }

            var funcIdentity = _tables[Global][scope] as BackendFuncIdentity;
            var offset = _stackOffset[funcIdentity];
            _stackOffset[funcIdentity] += num << 2;
            funcIdentity.OffsetPairs.Add(name, new OffsetPair(name, offset));
            _variables.Add($"{name}@{scope}", new Memory(offset: offset));
        }

        public override void ExitParamDecl(ProgramParser.ParamDeclContext context)
        {
            var type = context.Type().GetText();
            var name = context.variable().name.Text;
            var scope = context.variable().scope.Text;
            var num = context.Num() != null ? int.Parse(context.Num().GetText()) : 1;

            // Add to Symbol Table
            var table = _tables[scope];
            table.Add(name, new VariableIdentity(name, type, scope, num));

            var funcIdentity = _tables[Global][scope] as BackendFuncIdentity;
            var offset = _stackOffset[funcIdentity];
            _stackOffset[funcIdentity] += num << 2;
            funcIdentity.OffsetPairs.Add(name, new OffsetPair(name, offset));
            _variables.Add($"{name}@{scope}", new Memory(offset: offset));
        }

        public override void EnterFuncDef(ProgramParser.FuncDefContext context)
        {
            _currentScopeName = context.funcHead().Id().GetText();
        }

        #endregion

        #region BinaryOp

        public override void ExitBinary(ProgramParser.BinaryContext context)
        {
            var op = context.BinaryOp().GetText();
            var left = context.left.GetText();
            var right = context.right.GetText();
            var rlt = context.rlt.GetText();
            
            BinOp(op, left, right, rlt);
        }

        #endregion
        
        #region Variable

        public override void ExitVariable(ProgramParser.VariableContext context)
        {
            if (_isDeclFin == false) return;
            if (context.Num() == null) return;

            var scope = context.scope.Text;
            var name = context.name.Text;
            var id = _tables[scope][name] as VariableIdentity;
            var num = int.Parse(context.Num().GetText());
            Debug.Assert(id != null, nameof(id) + " != null");
            var mem = _variables[id.Name];
            if (context.offset != null)
            {
                _variables.Add($"{id.Name}[{num}]@{scope}", new Memory(mem.Base, mem.Offset + (num << 2)));
            }
        }

        #endregion

        public override void ExitProgram(ProgramParser.ProgramContext context)
        {
            base.ExitProgram(context);
        }
    }
}