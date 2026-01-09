using StringCalculator.Core.Contracts;
using StringCalculator.Core.Exceptions;

namespace StringCalculator.Core.Domain;

public class StringCalculatorEngine : IStringCalculatorEngine
{
    public int Add(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return 0;

        var delimiters = new[] { ',', '\n' };

        var parts = input
            .Replace("\\n", "\n")
            .Split(delimiters, StringSplitOptions.None);
        
        var negatives = new List<int>();

        int sum = 0;
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
}
