using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain.Operations;
using StringCalculator.Core.Domain.ValueObjects;
using StringCalculator.Core.Exceptions;

public class StringCalculatorEngine : IStringCalculatorEngine 
{
    private readonly INumberProcessor _processor;
    
    public StringCalculatorEngine(INumberProcessor processor) 
    {
        _processor = processor;
    }
    
    public CalculationResult Add(string input, CalculatorOptions? options = null)
    {
        return Calculate(input, new AddOperation(), options);
    }

    public CalculationResult Calculate(string input, IOperation operation, CalculatorOptions? options = null)
    {
        options ??= new CalculatorOptions();
        
        if (string.IsNullOrWhiteSpace(input)) 
            return new CalculationResult(0, "0 = 0");

        var processed = _processor.Process(input, options);
        
        if (options.DenyNegatives && processed.Negatives.Any())
            throw new NegativeNumbersNotAllowedException(processed.Negatives);

        var result = operation.Execute(processed.ValidNumbers);
        var formula = string.Join(operation.Symbol, processed.ValidNumbers) + 
                     (processed.ValidNumbers.Any() ? $" = {result}" : " = 0");
        
        return new CalculationResult(result, formula);
    }
}
