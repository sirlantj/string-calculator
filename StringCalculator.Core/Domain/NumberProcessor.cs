using StringCalculator.Core.Domain.ValueObjects;

public class NumberProcessor : INumberProcessor 
{
    private readonly IDelimiterParser _parser;
    
    public NumberProcessor(IDelimiterParser parser) => _parser = parser;
    
    public ProcessedNumbers Process(string input, CalculatorOptions options)
    {
        var (delimiters, numbersPart) = _parser.Parse(input);

        var allDelimiters = GetAllDelimiters(delimiters, options);
        var parts = _parser.Split(numbersPart, allDelimiters);
        
        return ProcessParts(parts, options);
    }
    
    private static List<string> GetAllDelimiters(List<string> customDelimiters, CalculatorOptions options)
    {
        var allDelimiters = new List<string> { "," };
        
        if (options.AlternateDelimiter.HasValue)
            allDelimiters.Add(options.AlternateDelimiter.Value.ToString());
        else
            allDelimiters.Add("\n");
            
        allDelimiters.AddRange(customDelimiters.Skip(1));
        return allDelimiters;
    }
    
    private ProcessedNumbers ProcessParts(IEnumerable<string> parts, CalculatorOptions options)
    {
        var usedValues = new List<int>();
        var negatives = new List<int>();
        
        foreach (var part in parts)
        {
            if (!int.TryParse(part.Trim(), out var number))
            {
                usedValues.Add(0); continue;
            }
            if (number < 0) { negatives.Add(number); if (options.DenyNegatives) continue; }
            if (number > options.UpperBound) continue;
            usedValues.Add(number);
        }
        
        return new ProcessedNumbers(usedValues, negatives);
    }
}

public record ProcessedNumbers(List<int> ValidNumbers, List<int> Negatives);
