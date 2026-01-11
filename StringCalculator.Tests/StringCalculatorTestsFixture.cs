using Moq;
using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain;
using StringCalculator.Core.Domain.ValueObjects;
using StringCalculator.Core.Infrastructure.Numbers;
using StringCalculator.Core.Infrastructure.Parsing;

namespace StringCalculator.Tests;

public class StringCalculatorTestsFixture
{
    public IStringCalculatorEngine Calculator { get; }
    public StringCalculatorTestsFixture()
    {
        Calculator = new StringCalculatorEngine(
            new DelimiterParser(),
            new DefaultDelimiterProvider(),
            new IntNumberParser(),
            new NegativeNumberValidator(),
            new UpperBoundPolicy()
        );
    }
}
