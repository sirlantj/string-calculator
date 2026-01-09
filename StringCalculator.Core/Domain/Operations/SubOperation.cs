namespace StringCalculator.Core.Domain.Operations;
public class SubOperation : IOperation {
    public int Execute(IEnumerable<int> numbers) {
        var nums = numbers.ToList();
        return nums.Count == 0 ? 0 : nums.Skip(1).Aggregate(nums[0], (a, b) => a - b);
    }
    public string Symbol => "-";
}
