using StringCalculator.Core.Contracts;
using StringCalculator.Core.Exceptions;

namespace StringCalculator.Core.Domain;

public class StringCalculatorEngine : IStringCalculatorEngine
{
    public int Add(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return 0;

        var (delimiters, numbersPart) = ParseDelimiters(input);

        var parts = numbersPart.Split(delimiters, StringSplitOptions.None);

        var sum = 0;
        var negatives = new List<int>();

        foreach (var part in parts)
        {
            if (!int.TryParse(part.Trim(), out var number))
                continue;

            if (number < 0)
            {
                negatives.Add(number);
                continue;
            }

            if (number > 1000)
                continue;

            sum += number;
        }

        if (negatives.Any())
            throw new NegativeNumbersNotAllowedException(negatives);

        return sum;
    }

    private static (string[] delimiters, string numbers) ParseDelimiters(string input)
    {
        var delimiters = new List<string> { ",", "\n" };

        if (!input.StartsWith("//"))
            return (delimiters.ToArray(), input);

        var newlineIndex = input.IndexOf('\n');

        if (newlineIndex == -1)
            return (delimiters.ToArray(), input);

        var header = input.Substring(2, newlineIndex - 2);
        var numbers = input[(newlineIndex + 1)..];

        if (header.StartsWith("[") && header.EndsWith("]"))
        {
            delimiters.Add(header.Substring(1, header.Length - 2));
        }
        else
        {
            delimiters.Add(header);
        }

        return (delimiters.ToArray(), numbers);
    }
}
