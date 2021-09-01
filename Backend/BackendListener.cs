using System;
using Frontend;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Antlr4.Runtime;

namespace Backend
{
    using SymbolTable = Dictionary<string, Identity>;

    public class BackendException : Exception
    {
        public BackendException(string message) : base(message)
        {
        }
    }

    public class BackendListener : ProgramBaseListener
    {
        private readonly List<string> _functions = new();

        private readonly List<string> _dataSegment = new();

        private readonly List<string> _codeSegment = new();

        public const string Global = FrontEndListener.Global;

        private string _currentScopeName = Global;

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

        #region Program

        public override void EnterProgram(ProgramParser.ProgramContext context)
        {
            _dataSegment.Add(".DATA 0x100000");
            _codeSegment.AddRange(new[] { ".TEXT", "start:" });

        }

        #endregion


        #region Decls

        public override void ExitLocalVarDecl(ProgramParser.LocalVarDeclContext context)
        {
            var type = context.Type().GetText();
            var name = context.name.Text;
            var scope = context.scope.Text;

            if (_tables.TryGetValue(scope, out var table))
            {
                table.Add(name, new VariableIdentity(name, type, scope));
            }
            else
            {
                _tables.Add(scope, new SymbolTable
                {
                    {name, new VariableIdentity(name, type, scope)}
                });
            }

        }

        public override void ExitLocalArrDecl(ProgramParser.LocalArrDeclContext context)
        {
            var type = context.Type().GetText();
            var name = context.name.Text;
            var scope = context.scope.Text;
            var length = int.Parse(context.Num().GetText());

            //_tables[scope].Add(name, new ArrIdentity(name, type, length));
            if (_tables.TryGetValue(scope, out var table))
            {
                table.Add(name, new VariableIdentity(name, type, scope, length));
            }
            else
            {
                _tables.Add(scope, new SymbolTable
                {
                    {name, new VariableIdentity(name, type, scope ,length)}
                });
            }

            var zeros = string.Join(", ", (Enumerable.Repeat("0", length)));
            _dataSegment.Add($"{name}:\t.{_typeMap[type]} {zeros} \t #{context.GetText()}");
        }

        public override void EnterFuncDef(ProgramParser.FuncDefContext context)
        {
            var type = context.Type().GetText();
            var name = context.Id().GetText();
            var @params = new List<Identity>();
            var offsetPairs = new List<OffsetPair>();

            // 0-31 寄存器
            // 32-35 局部变量个数
            var currentOffset = 36;
            foreach (var (localVarName, localVarIdentity) in _tables[name])
            {
                var v = localVarIdentity as VariableIdentity;
                Debug.Assert(v != null, nameof(v) + " != null");
                offsetPairs.Add(new OffsetPair(localVarName, currentOffset));
                currentOffset += v.Length << 2;
            }

            // 参数个数 4byte
            currentOffset += 4;

            foreach (var paramDeclContext in context.paramDecl())
            {
                switch (paramDeclContext)
                {
                    case ProgramParser.VariableParamContext variable:
                        @params.Add(new VariableIdentity(variable.Id().GetText(), variable.Type().GetText(), name, mutable: false));
                        offsetPairs.Add(new OffsetPair(variable.Id().GetText(), currentOffset));
                        currentOffset += 4;
                        break;
                    case ProgramParser.ArrayParamContext arr:
                        @params.Add(new VariableIdentity(arr.Id().GetText(), arr.Type().GetText(), name,
                            int.Parse(arr.Num().GetText())));
                        offsetPairs.Add(new OffsetPair(arr.Id().GetText(), currentOffset));
                        currentOffset += int.Parse(arr.Num().GetText()) << 2;
                        break;
                }
            }

            var funcId = new BackendFuncIdentity(name, type, @params, offsetPairs);
            _tables[Global].Add(name, funcId);
            _currentScopeName = name;
        }

        #endregion

        #region Add

        public override void ExitDigitAddOrMinus(ProgramParser.DigitAddOrMinusContext context)
        {
            var rltName = context.rlt.Text;

            Identity rltId;
            foreach (var (_, table) in _tables)
            {
                table.TryGetValue(rltName, out rltId);
            }
            switch (context.AdditaveOp().GetText())
            {
                case "+":
                    var tmp = TmpReg++;
                    break;
                case "-":
                    break;
                case "<<":
                    break;
                case ">>":
                    break;
                case "&":
                    break;
                case "|":
                    break;
                case "^":
                    break;
            }
        }

        #endregion
    }
}
