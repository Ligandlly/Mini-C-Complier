#nullable enable
namespace MiniC
{
    public interface IIrBuilder
    {
        public string GenerateIr(string? operation = null, string? firstSrc = null, string? secondSrc = null,
            string? dist = null, string? label = null);
    }

    public class QuaternaryBuilder : IIrBuilder
    {
        public string GenerateIr(string? operation = null, string? firstSrc = null, string? secondSrc = null,
            string? dist = null, string? label = null)
        {
            return $"{label}: \t {operation ?? " "}; {firstSrc ?? " "}; {secondSrc ?? " "}; {dist ?? " "};";
        }
    }
}