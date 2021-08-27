using System;

namespace FrontEnd
{
    public class Identity
    {
        public static readonly string Int = "int";
        public static readonly string Short = "short";
        public static readonly string Char = "char";

        public string Name { get; }
        public string Type { get; }

        /// <summary>
        /// t1 = t0 + 1 is immutable,
        /// while t0 = t0 + 1 is immutable
        /// </summary>
        public bool Mutable { get; }

        public Identity(string name, string type, bool mutable=true)
        {
            Name = name;
            Type = type;
            Mutable = mutable;
        }
    }

    public class ArrIdentity : Identity
    {
        public int Length { get; }
        public int UnitSize { get; }

        public ArrIdentity(string name, string type, int length)
            : base(name, type)
        {
            Length = length;

            UnitSize = type switch
            {
                "intArr" => 4,
                "shortArr" => 2,
                "charArr" => 1,
                _ => throw new Exception("Invalid Type Specific")
            };
        }
    }

    public class FuncIdentity : Identity
    {
        public static readonly string Void = "void";

        private (string, string)[] _params;
        public (string paramType, string paramName)[] Params
        {
            get => _params;
            init
            {
                if (value.Length == 1 && value[0].paramType == "void")
                {
                    _params = Array.Empty<(string, string)>();
                }
                else
                {
                    _params = value;
                }
            }
        }

        public FuncIdentity(string name, string type, (string, string)[] @params) : base(name, type)
        {
            Params = @params;
        }
    }

    public class Literal : Identity
    {
        public static readonly string Lit = "literal";
        public Literal(string name) : base(name, "literal", false)
        {
        }
    }
}