using StringCalculator.Core.Contracts;

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

        int sum = 0;
        foreach (var part in parts)
        {
            sum += int.TryParse(part.Trim(), out var number) ? number : 0;
        }

        return sum;
    }
}
