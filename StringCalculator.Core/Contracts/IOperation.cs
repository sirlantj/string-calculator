namespace StringCalculator.Core.Contracts
{
    public interface IOperation
    {
        int Execute(IEnumerable<int> numbers);
        string Symbol { get; }
    }
}