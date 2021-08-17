using System;

namespace MiniC
{
    public class Identity
    {
        public string Name { get; }
        public string Type { get; }

        public Identity(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }

    public class ArrIdentity : Identity
    {
        public int Length { get; }

        public ArrIdentity(string name, string type, int length)
            : base(name, type)
        {
            Length = length;
        }
    }

    public class FuncIdentity : Identity
    {
        // private (string, string)[] _params;
        // public (string paramType, string paramName)[] Params
        // {
        //     get => _params;
        //     set
        //     {
        //         if (value.Length == 1 && value[0].Item1 == "void")
        //         {
        //             _params = Array.Empty<(string, string)>();
        //         }
        //         else
        //         {
        //             _params = value;
        //         }
        //     }
        // }
        //
        // public FuncIdentity(string name, string type, (string, string)[] @params) : base(name, type)
        // {
        //     Params = @params;
        // }

        public FuncIdentity(string name, string type) : base(name, type)
        {
        }
    }
}