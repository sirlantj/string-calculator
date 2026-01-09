namespace StringCalculator.Core.Exceptions;

public class NegativeNumbersNotAllowedException : Exception
{
    public IReadOnlyCollection<int> Negatives { get; }

    public NegativeNumbersNotAllowedException(IEnumerable<int> negatives)
        : base($"Negatives not allowed: {string.Join(", ", negatives)}")
    {
        Negatives = negatives.ToList().AsReadOnly();
    }
}