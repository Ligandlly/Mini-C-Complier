#nullable enable
using System;

namespace MiniC
{
    public interface IIrBuilder
    {
        public string GenerateIr(string? operation = null, string? firstSrc = null, string? secondSrc = null,
            string? dist = null, int? labelNumber = null);
    }


    public class QuaternaryBuilder : IIrBuilder
    {
        public string GenerateIr(string? operation = null, string? firstSrc = null, string? secondSrc = null,
            string? dist = null, int? labelNumber = null)
        {
            if (operation != null && labelNumber != null)
                throw new InvalidOperationException("Quaternary");

            return labelNumber != null
                ? $"label_{labelNumber}:"
                : $"    {operation ?? " "}; {firstSrc ?? " "}; {secondSrc ?? " "}; {dist ?? " "};";
        }
    }
}