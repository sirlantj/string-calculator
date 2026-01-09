using StringCalculator.Core.Exceptions;
using StringCalculator.Core.Contracts;

namespace StringCalculator.Core.Domain;

public class StringCalculatorEngine : IStringCalculatorEngine
{
    public int Add(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return 0;

        var parts = input.Split(',')
                         .Select(p => p.Trim());

        if (parts.Count() > 2)
            throw new TooManyNumbersException(parts.Count());

        int sum = 0;
        foreach (var part in parts)
        {
            sum += int.TryParse(part, out var number) ? number : 0;
        }

        return sum;
    }
}
