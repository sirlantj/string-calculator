using StringCalculator.Core.Domain.ValueObjects;

namespace StringCalculator.Core.Contracts
{
    public interface IDefaultDelimiterProvider
    {
        IReadOnlyList<string> GetDefaultDelimiters(CalculatorOptions options);
    }
}
