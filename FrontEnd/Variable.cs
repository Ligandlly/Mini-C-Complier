using System;
using System.Collections.Generic;

namespace Frontend
{
    public abstract class Identity
    {
        public static readonly string Int = "int";
        public static readonly string Short = "short";
        public static readonly string Char = "char";

        public virtual string Name { get; }
        public string Type { get; }

        public string Scope { get; }

        protected Identity(string name, string type, string scope)
        {
            Name = name;
            Type = type;
            Scope = scope;
        }
    }

    public class VariableIdentity : Identity
    {
        public int Length { get; }

        /// <summary>
        /// t1 = t0 + 1 is immutable,
        /// while t0 = t0 + 1 is immutable
        /// </summary>
        public bool Mutable { get; }

        public override string Name => $"{base.Name}@{Scope}";

        public string GetNaiveName() => base.Name;


        public VariableIdentity(string name, string type, string scope, int length = 1, bool mutable = true)
            : base(name, type, scope)
        {
            if (length == 0)
                throw new Exception("Length of a Variable Can Not Be 0.");

            Length = length;
            Mutable = mutable;
        }

        public bool IsArr()
        {
            return Length != 1;
        }
    }

    public class FuncIdentity : Identity
    {
        public static readonly string Void = "void";

        public (string paramType, string paramName)[] Params { get; }

        public FuncIdentity(string name, string type, (string, string)[] @params) : base(name, type,
            FrontEndListener.Global)
        {
            if (@params.Length == 1 && @params[0].Item1 == "void")
            {
                Params = Array.Empty<(string, string)>();
            }
            else
            {
                Params = @params;
            }

            Params = @params;
        }
    }

    public class Literal : Identity
    {
        public static readonly string Lit = "literal";

        public Literal(string name) : base(name, "int", FrontEndListener.Global)
        {
        }
    }

    public class OffsetPair
    {
        public string Name { get; init; }
        public int Offset { get; init; }

        public OffsetPair(string name, int offset)
        {
            Name = name;
            Offset = offset;
        }
    }

    public class BackendFuncIdentity : Identity
    {
        public Dictionary<string, OffsetPair> OffsetPairs { get; } = new();

        public BackendFuncIdentity(string name, string type) : base(name, type, FrontEndListener.Global)
        {
        }

        public BackendFuncIdentity(string name, string type, List<OffsetPair> offsetPairs)
            : base(name, type, FrontEndListener.Global)
        {
            foreach (var offsetPair in offsetPairs)
            {
                OffsetPairs.Add(offsetPair.Name, offsetPair);
            }
        }
    }
}