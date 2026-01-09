namespace StringCalculator.Core.Domain.Operations;

public class MulOperation : IOperation
{
    public int Execute(IEnumerable<int> numbers)
    {
        return numbers.Aggregate(1, (a, b) => a * b);
    }

    public string Symbol => "×";
}
