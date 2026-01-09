using StringCalculator.Core.Domain;

namespace StringCalculator.Tests;

public class StringCalculatorTests
{
    private readonly StringCalculatorEngine _calculator = new();

    [Fact]
    public void Add_EmptyString_ReturnsZero()
    {
        var result = _calculator.Add("");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_NullOrWhitespace_ReturnsZero()
    {
        Assert.Equal(0, _calculator.Add(null));
        Assert.Equal(0, _calculator.Add("   "));
    }

    [Fact]
    public void Add_SingleNumber_ReturnsThatNumber()
    {
        var result = _calculator.Add("20");
        Assert.Equal(20, result);
    }

    [Fact]
    public void Add_TwoNumbers_ReturnsSum()
    {
        var result = _calculator.Add("1,5000");
        Assert.Equal(5001, result);
    }

    [Fact]
    public void Add_MultipleNumbers_ReturnsSum()
    {
        var result = _calculator.Add("1,2,3,4,5");
        Assert.Equal(15, result);
    }

    [Fact]
    public void Add_MultipleNumbers_WithInvalidValues_IgnoresInvalids()
    {
        var result = _calculator.Add("1,abc,3,xyz,5");
        Assert.Equal(9, result);
    }

    [Fact]
    public void Add_NegativeNumbers_AreAllowedInThisStep()
    {
        var result = _calculator.Add("4,-3,-1");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_InvalidNumber_IsTreatedAsZero()
    {
        var result = _calculator.Add("5,tytyt");
        Assert.Equal(5, result);

        result = _calculator.Add("abc,8");
        Assert.Equal(8, result);
    }

    [Fact]
    public void Add_MissingNumberAtStart_IsTreatedAsZero()
    {
        var result = _calculator.Add(",5");
        Assert.Equal(5, result);
    }

    [Fact]
    public void Add_MissingNumberAtEnd_IsTreatedAsZero()
    {
        var result = _calculator.Add("5,");
        Assert.Equal(5, result);
    }

    [Fact]
    public void Add_MultipleEmptyParts_AreTreatedAsZero()
    {
        var result = _calculator.Add(",,");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_MixedEmptyAndValidParts_ReturnsCorrectSum()
    {
        var result = _calculator.Add("1,,2,,,3");
        Assert.Equal(6, result);
    }

    [Fact]
    public void Add_WhitespaceAroundNumbers_IsIgnored()
    {
        var result = _calculator.Add("  4  ,  -2 ,  3 ");
        Assert.Equal(5, result);
    }
}
