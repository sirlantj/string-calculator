using StringCalculator.Core.Domain;
using StringCalculator.Core.Exceptions;

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
    public void Add_NegativeNumbers_AreAllowedInThisStep()
    {
        var result = _calculator.Add("4,-3");
        Assert.Equal(1, result);
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
    public void Add_TwoEmptyParts_ReturnsZero()
    {
        var result = _calculator.Add(",");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_WhitespaceAroundNumbers_IsIgnored()
    {
        var result = _calculator.Add("  4  ,  -2 ");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Add_MoreThanTwoNumbers_ThrowsTooManyNumbersException()
    {
        Assert.Throws<TooManyNumbersException>(() => _calculator.Add("1,2,3"));
        Assert.Throws<TooManyNumbersException>(() => _calculator.Add("10,20,30,40"));
    }

    [Fact]
    public void Add_ThreeOrMoreParts_EvenWithEmpties_ThrowsException()
    {
        Assert.Throws<TooManyNumbersException>(() => _calculator.Add("1,,3"));
        Assert.Throws<TooManyNumbersException>(() => _calculator.Add(",,"));
        Assert.Throws<TooManyNumbersException>(() => _calculator.Add("1,2,"));
        Assert.Throws<TooManyNumbersException>(() => _calculator.Add(",1,2"));
    }

    [Fact]
    public void Add_MoreThanTwoNumbers_ThrowsException_WithCorrectDetails()
    {
        var exception = Assert.Throws<TooManyNumbersException>(() =>
            _calculator.Add("1,2,3,4,5"));

        Assert.Contains("5 provided", exception.Message);
        Assert.Contains("maximum of 2 allowed", exception.Message);
    }
}