using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain.ValueObjects;
namespace StringCalculator.Core.Infrastructure.Numbers
{
    public class UpperBoundPolicy : INumberInclusionPolicy
    {
        public bool ShouldInclude(int number, CalculatorOptions options)
            => number <= options.UpperBound;
    }
}
