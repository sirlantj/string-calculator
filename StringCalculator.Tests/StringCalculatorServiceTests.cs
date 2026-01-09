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
        var result = _calculator.Add("1,500");
        Assert.Equal(501, result);
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
    public void Add_NewLineDelimiter_ReturnsSum()
    {
        var result = _calculator.Add("1\n2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Add_MixedCommaAndNewLineDelimiters_ReturnsSum()
    {
        Assert.Equal(6, _calculator.Add("1\n2,3"));
        Assert.Equal(6, _calculator.Add("1,2\n3"));
        Assert.Equal(6, _calculator.Add("1\n2\n3"));
    }

    [Fact]
    public void Add_MultipleNewLines_ReturnsSum()
    {
        var result = _calculator.Add("1\n2\n3\n4");
        Assert.Equal(10, result);
    }

    [Fact]
    public void Add_NewLinesWithEmptyValues_AreTreatedAsZero()
    {
        var result = _calculator.Add("\n1\n\n2\n");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Add_NewLinesWithInvalidValues_IgnoresInvalids()
    {
        var result = _calculator.Add("1\nabc\n2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Add_MixedDelimiters_WithEmptyAndInvalid_ReturnsCorrectSum()
    {
        Assert.Equal(10, _calculator.Add("1\nabc,3\n\n4,tytyt\n2"));

    }

    [Fact]
    public void Add_NegativeNumbers_ThrowsExceptionWithAllNegatives()
    {
        var calculator = new StringCalculatorEngine();

        var exception = Assert.Throws<NegativeNumbersNotAllowedException>(() =>
            calculator.Add("1,-2,3,-5"));

        Assert.Contains("-2", exception.Message);
        Assert.Contains("-5", exception.Message);
    }

    [Fact]
    public void Add_WithNegativesAndDelimiters_StillThrowsCorrectly()
    {
        var ex = Assert.Throws<NegativeNumbersNotAllowedException>(() => _calculator.Add("1\n-2,3\n-4"));
        Assert.Equal("Negatives not allowed: -2, -4", ex.Message);
    }

    [Fact]
public void Add_NumberGreaterThan1000_IsIgnored()
{
    var result = _calculator.Add("2,1001,6");
    Assert.Equal(8, result);
}

[Fact]
public void Add_AllNumbersGreaterThan1000_ReturnsZero()
{
    var result = _calculator.Add("1001,2000,5000");
    Assert.Equal(0, result);
}

[Fact]
public void Add_MixedValidAndInvalidNumbers_IgnoresGreaterThan1000()
{
    var result = _calculator.Add("1,999,1000,1001,2");
    Assert.Equal(2002, result);
}
}