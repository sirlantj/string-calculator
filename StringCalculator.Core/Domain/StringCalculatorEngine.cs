using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain.ValueObjects;
using StringCalculator.Core.Exceptions;
using System.Text.RegularExpressions;

namespace StringCalculator.Core.Domain;

public class StringCalculatorEngine : IStringCalculatorEngine
{
    public CalculationResult Add(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new CalculationResult(0, "0 = 0");

        var (delimiters, numbersPart) = ParseDelimiters(input);

        var parts = SplitNumbers(numbersPart, delimiters);

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
                continue;
            }

            if (number > 1000)
                continue;

            usedValues.Add(number);
        }

        if (negatives.Any())
            throw new NegativeNumbersNotAllowedException(negatives);

        var sum = usedValues.Sum();
        var formula = string.Join("+", usedValues) + $" = {sum}";

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
