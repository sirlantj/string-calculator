using StringCalculator.Core.Domain.ValueObjects;

namespace StringCalculator.Core.Contracts;

public interface IStringCalculatorEngine
{
    CalculationResult Add(string input);
}
