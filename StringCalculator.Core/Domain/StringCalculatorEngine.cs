using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain.ValueObjects;
using StringCalculator.Core.Exceptions;
using System.Text.RegularExpressions;

namespace StringCalculator.Core.Domain;

public class StringCalculatorEngine : IStringCalculatorEngine
{
    public CalculationResult Add(string input, CalculatorOptions? options = null)
    {
        options ??= new CalculatorOptions();

    if (string.IsNullOrWhiteSpace(input))
        return new CalculationResult(0, "0 = 0");

    var (delimiters, numbersPart) = ParseDelimiters(input);

    var allDelimiters = new List<string> { "," };
    if (options.AlternateDelimiter.HasValue)
    {
        allDelimiters.Add(options.AlternateDelimiter.Value.ToString());
    }
    else
    {
        allDelimiters.Add("\n"); 
    }

    allDelimiters.AddRange(delimiters.Skip(1)); 

    var parts = SplitNumbers(numbersPart, allDelimiters);

        var usedValues = new List<int>();
        var negatives = new List<int>();

        foreach (var part in parts)
        {
            if (!int.TryParse(part.Trim(), out var number))
            {
                usedValues.Add(0);
                continue;
            }

            if (number < 0)
            {
                negatives.Add(number);
                if (options.DenyNegatives)
                    continue; 
            }

            if (number > options.UpperBound)
                continue;

            usedValues.Add(number);
        }

        if (options.DenyNegatives && negatives.Any())
            throw new NegativeNumbersNotAllowedException(negatives);

        var sum = usedValues.Sum();
        var formula = string.Join("+", usedValues) + (usedValues.Any() ? $" = {sum}" : " = 0");

        return new CalculationResult(sum, formula);
    }
    private static (List<string> delimiters, string numbers) ParseDelimiters(string input)
    {
        var delimiters = new List<string> { ",", "\n" };

        if (!input.StartsWith("//"))
            return (delimiters, input);

        var newlineIndex = input.IndexOf('\n');
        if (newlineIndex == -1)
            return (delimiters, input);

        var delimiterSection = input.Substring(2, newlineIndex - 2);
        var numbers = input[(newlineIndex + 1)..];

        var matches = Regex.Matches(delimiterSection, @"\[(.*?)\]");
        if (matches.Count > 0)
        {
            foreach (Match match in matches)
                delimiters.Add(match.Groups[1].Value);
        }
        else
        {
            delimiters.Add(delimiterSection);
        }

        return (delimiters, numbers);
    }

    private static IEnumerable<string> SplitNumbers(string input, List<string> delimiters)
    {
        var pattern = string.Join("|", delimiters.Select(Regex.Escape));
        return Regex.Split(input, pattern);
    }
}
