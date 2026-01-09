using StringCalculator.Core.Domain.Operations;
using StringCalculator.Core.Domain.ValueObjects;

namespace StringCalculator.Tests;

public class StringCalculatorOptionsTests : IClassFixture<StringCalculatorTestsFixture>
{
    private readonly StringCalculatorEngine _calculator;
    
    public StringCalculatorOptionsTests(StringCalculatorTestsFixture fixture)
    {
        _calculator = fixture.Calculator;
    }

    [Fact]
    public void Add_WithAlternateDelimiter_UsesCustomDelimiter()
    {
        var options = new CalculatorOptions { AlternateDelimiter = ';' };
        var result = _calculator.Calculate("1;2;3", new AddOperation(), options);
        Assert.Equal(6, result.Result);
        Assert.Equal("1+2+3 = 6", result.Formula);
    }

    [Fact]
    public void Add_AllowNegativesOption_IncludesNegativesInCalculation()
    {
        var options = new CalculatorOptions { DenyNegatives = false };
        var result = _calculator.Calculate("-1,2,-3", new AddOperation(), options);
        Assert.Equal(-2, result.Result);
        Assert.Equal("-1+2+-3 = -2", result.Formula);
    }

    [Fact]
    public void Add_CustomUpperBound_UsesNewThreshold()
    {
        var options = new CalculatorOptions { UpperBound = 5 };
        var result = _calculator.Calculate("2,6,3", new AddOperation(), options);
        Assert.Equal(5, result.Result);
        Assert.Equal("2+3 = 5", result.Formula);
    }
}