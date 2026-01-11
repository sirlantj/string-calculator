using StringCalculator.Core.Domain.ValueObjects;

namespace StringCalculator.Core.Contracts
{
    public interface INumberInclusionPolicy
    {
        bool ShouldInclude(int number, CalculatorOptions options);
    }
}

