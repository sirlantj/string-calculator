using StringCalculator.Core.Contracts;

namespace StringCalculator.Core.Infrastructure.Numbers
{
    public class NegativeNumberValidator : INumberValidator
    {
        public void Validate(int number, List<int> negativesCollector)
        {
            if (number < 0) negativesCollector.Add(number);
        }
    }
}
