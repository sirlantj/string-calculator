namespace StringCalculator.Core.Contracts
{
    public interface IDelimiterParser
    {
        (IReadOnlyList<string> CustomDelimiters, string NumbersPart) Parse(string input);
        IEnumerable<string> Split(string numbersPart, IReadOnlyList<string> allDelimiters);
    }
}