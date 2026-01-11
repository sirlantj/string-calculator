using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain.ValueObjects;

namespace StringCalculator.Core.Infrastructure.Parsing
{
    public class DefaultDelimiterProvider : IDefaultDelimiterProvider
    {
        public IReadOnlyList<string> GetDefaultDelimiters(CalculatorOptions options)
        {
            var defaults = new List<string> { "," };
            defaults.Add(options.AlternateDelimiter?.ToString() ?? "\n");
            return defaults;
        }
    }
}
