namespace StringCalculator.Core.Domain.Operations;
public class AddOperation : IOperation {
    public int Execute(IEnumerable<int> numbers) => numbers.Sum();
    public string Symbol => "+";
}