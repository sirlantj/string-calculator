using StringCalculator.Core.Domain.ValueObjects;

public interface INumberProcessor 
{
    ProcessedNumbers Process(string input, CalculatorOptions options);
}
