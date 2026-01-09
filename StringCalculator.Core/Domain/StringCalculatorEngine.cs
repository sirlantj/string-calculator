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

    private static (char[] delimiters, string numbers) ParseDelimiters(string input)
    {
        if (!input.StartsWith("//"))
            return (new[] { ',', '\n' }, input);

        var newlineIndex = input.IndexOf('\n');
        var delimiter = input[2];
        var numbers = input[(newlineIndex + 1)..];

        return (new[] { ',', '\n', delimiter }, numbers);
    }
}
