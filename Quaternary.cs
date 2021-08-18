#nullable enable
namespace MiniC
{
    public interface IIrBuilder
    {
        public string GenerateIr(string? operation = null, string? firstSrc = null, string? secondSrc = null,
            string? dist = null);
    }
    

    public class QuaternaryBuilder : IIrBuilder
    {
        private int _no;

        public string GenerateIr(string? operation = null, string? firstSrc = null, string? secondSrc = null,
            string? dist = null)
            => $"{_no++ + ":"}\t{operation ?? " "}; {firstSrc ?? " "}; {secondSrc ?? " "}; {dist ?? " "};";

    }
}