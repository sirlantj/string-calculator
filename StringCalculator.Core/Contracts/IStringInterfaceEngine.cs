using StringCalculator.Core.Domain.ValueObjects;

namespace StringCalculator.Core.Contracts;

public interface IStringCalculatorEngine
{
    CalculationResult Add(string input, CalculatorOptions? options = null);
    CalculationResult Calculate(string input, IOperation operation, CalculatorOptions? options = null);
}
