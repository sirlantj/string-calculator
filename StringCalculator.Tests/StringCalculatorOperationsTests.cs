using StringCalculator.Core.Domain.Operations;
using StringCalculator.Core.Domain.ValueObjects;

namespace StringCalculator.Tests;

public class StringCalculatorOperationsTests : IClassFixture<StringCalculatorTestsFixture>
{
    private readonly StringCalculatorEngine _calculator;
    
    public StringCalculatorOperationsTests(StringCalculatorTestsFixture fixture)
    {
        _calculator = fixture.Calculator;
    }
    private readonly CalculatorOptions _defaultOptions = new();

    [Fact]
    public void Subtract_TwoNumbers_ReturnsDifference()
    {
        var result = _calculator.Calculate("10,3", new SubOperation(), _defaultOptions);
        Assert.Equal(7, result.Result);
        Assert.Equal("10-3 = 7", result.Formula);
    }

    [Fact]
    public void Subtract_MultipleNumbers_ReturnsCorrectResult()
    {
        var result = _calculator.Calculate("10,3,2", new SubOperation(), _defaultOptions);
        Assert.Equal(5, result.Result);
        Assert.Equal("10-3-2 = 5", result.Formula);
    }

    [Fact]
    public void Multiply_TwoNumbers_ReturnsProduct()
    {
        var result = _calculator.Calculate("3,4", new MulOperation(), _defaultOptions);
        Assert.Equal(12, result.Result);
        Assert.Equal("3×4 = 12", result.Formula);
    }

    [Theory]
    [InlineData("2,3,4", 24)]
    [InlineData("5,2", 10)]
    [InlineData("1,1000", 1000)]
    public void Multiply_MultipleNumbers_ReturnsProduct(string input, int expected)
    {
        var result = _calculator.Calculate(input, new MulOperation(), _defaultOptions);
        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void Divide_TwoNumbers_ReturnsQuotient()
    {
        var result = _calculator.Calculate("100,5", new DivOperation(), _defaultOptions);
        Assert.Equal(20, result.Result);
        Assert.Equal("100÷5 = 20", result.Formula);
    }

    [Theory]
    [InlineData("100,5,2", 10)] 
    [InlineData("20,4", 5)]
    public void Divide_MultipleNumbers_ReturnsCorrectResult(string input, int expected)
    {
        var result = _calculator.Calculate(input, new DivOperation(), _defaultOptions);
        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() => 
            _calculator.Calculate("10,0", new DivOperation(), _defaultOptions));
    }

    [Fact]
    public void Operations_IgnoreNumbersOverUpperBound()
    {
        var options = new CalculatorOptions { UpperBound = 10 };
        
        var mulResult = _calculator.Calculate("2,3,1001", new MulOperation(), options);
        Assert.Equal(6, mulResult.Result);
        
        var subResult = _calculator.Calculate("10,1001,2", new SubOperation(), options);
        Assert.Equal(8, subResult.Result);
    }

    [Fact]
    public void Operations_CustomDelimiter_WorksCorrectly()
    {
        var result = _calculator.Calculate("//[*]\n2*3*4", new MulOperation(), _defaultOptions);
        Assert.Equal(24, result.Result);
        Assert.Equal("2×3×4 = 24", result.Formula);
    }

    [Fact]
    public void Operations_WithNegativesAndAllowOption_Works()
    {
        var options = new CalculatorOptions { DenyNegatives = false };
        var result = _calculator.Calculate("-2,3,-1", new MulOperation(), options);
        Assert.Equal(6, result.Result); 
    }
}