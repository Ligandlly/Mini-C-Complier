// FIXME: Symbol Tables is redundant
// FIXME: `_typeMap` is never used

namespace Backend
{
    internal readonly struct Memory
    {
        public string Base { get; }
        public int Offset { get; }
        public bool IsArrHead { get; }

        public int Length { get; }

        public Memory(string @base = "$sp", int offset = 0, bool isArrHead = false, int length = 1)
        {
            Base = @base;
            Offset = offset;
            IsArrHead = isArrHead;
            Length = length;
        }

        public override string ToString()
        {
            return $"{Offset}({Base})";
        }
    }
}