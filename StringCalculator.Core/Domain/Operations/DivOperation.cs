namespace StringCalculator.Core.Domain.Operations;

public class DivOperation : IOperation
{
    public int Execute(IEnumerable<int> numbers)
    {
        var numList = numbers.ToList();
        if (numList.Count < 2)
            return 0;

        int result = numList[0];
        for (int i = 1; i < numList.Count; i++)
        {
            if (numList[i] == 0)
                throw new InvalidOperationException("Division by zero is not allowed");

            result /= numList[i];
        }
        return result;
    }

    public string Symbol => "÷";
}
