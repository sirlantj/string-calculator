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
        Assert.Equal(0, result.Result);
    }
    
    [Fact]
    public void Add_NullOrWhitespace_ReturnsZero()
    {
        Assert.Equal(0, _calculator.Add(null).Result);
        Assert.Equal(0, _calculator.Add("   ").Result);
    }

    [Fact]
    public void Add_SingleNumber_ReturnsThatNumber()
    {
        var result = _calculator.Add("20");
        Assert.Equal(20, result.Result);
        Assert.Equal("20 = 20", result.Formula);
    }

    [Fact]
    public void Add_TwoNumbers_ReturnsSum()
    {
        var result = _calculator.Add("1,500");
        Assert.Equal(501, result.Result);
        Assert.Equal("1+500 = 501", result.Formula);
    }

    [Fact]
    public void Add_MultipleNumbers_ReturnsSum()
    {
        var result = _calculator.Add("1,2,3,4,5");
        Assert.Equal(15, result.Result);
        Assert.Equal("1+2+3+4+5 = 15", result.Formula);
    }

    [Fact]
    public void Add_MultipleNumbers_WithInvalidValues_IgnoresInvalids()
    {
        var result = _calculator.Add("1,abc,3,xyz,5");
        Assert.Equal(9, result.Result);
        Assert.Equal("1+0+3+0+5 = 9", result.Formula);
    }

    [Fact]
    public void Add_InvalidNumber_IsTreatedAsZero()
    {
        var result = _calculator.Add("5,tytyt");
        Assert.Equal(5, result.Result);

        result = _calculator.Add("abc,8");
        Assert.Equal(8, result.Result);
    }

    [Fact]
    public void Add_MissingNumberAtStart_IsTreatedAsZero()
    {
        var result = _calculator.Add(",5");
        Assert.Equal(5, result.Result);
    }

    [Fact]
    public void Add_MissingNumberAtEnd_IsTreatedAsZero()
    {
        var result = _calculator.Add("5,");
        Assert.Equal(5, result.Result);
    }

    [Fact]
    public void Add_MultipleEmptyParts_AreTreatedAsZero()
    {
        var result = _calculator.Add(",,");
        Assert.Equal(0, result.Result);
    }

    [Fact]
    public void Add_MixedEmptyAndValidParts_ReturnsCorrectSum()
    {
        var result = _calculator.Add("1,,2,,,3");
        Assert.Equal(6, result.Result);
        Assert.Equal("1+0+2+0+0+3 = 6", result.Formula);
    }

    [Fact]
    public void Add_NewLineDelimiter_ReturnsSum()
    {
        var result = _calculator.Add("1\n2");
        Assert.Equal(3, result.Result);
        Assert.Equal("1+2 = 3", result.Formula);
    }

    [Fact]
    public void Add_MixedCommaAndNewLineDelimiters_ReturnsSum()
    {
        Assert.Equal(6, _calculator.Add("1\n2,3").Result);
        Assert.Equal(6, _calculator.Add("1,2\n3").Result);
        Assert.Equal(6, _calculator.Add("1\n2\n3").Result);
    }

    [Fact]
    public void Add_MultipleNewLines_ReturnsSum()
    {
        var result = _calculator.Add("1\n2\n3\n4");
        Assert.Equal(10, result.Result);
        Assert.Equal("1+2+3+4 = 10", result.Formula);
    }

    [Fact]
    public void Add_NewLinesWithEmptyValues_AreTreatedAsZero()
    {
        var result = _calculator.Add("\n1\n\n2\n");
        Assert.Equal(3, result.Result);
        Assert.Equal("0+1+0+2+0 = 3", result.Formula);
    }

    [Fact]
    public void Add_NewLinesWithInvalidValues_IgnoresInvalids()
    {
        var result = _calculator.Add("1\nabc\n2");
        Assert.Equal(3, result.Result);
        Assert.Equal("1+0+2 = 3", result.Formula);
    }

    [Fact]
    public void Add_MixedDelimiters_WithEmptyAndInvalid_ReturnsCorrectSum()
    {
        Assert.Equal(10, _calculator.Add("1\nabc,3\n\n4,tytyt\n2").Result);
        Assert.Equal("1+0+3+0+4+0+2 = 10", _calculator.Add("1\nabc,3\n\n4,tytyt\n2").Formula);
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
        Assert.Equal(8, result.Result);
        Assert.Equal("2+6 = 8", result.Formula);
    }

    [Fact]
    public void Add_AllNumbersGreaterThan1000_ReturnsZero()
    {
        var result = _calculator.Add("1001,2000,5000");
        Assert.Equal(0, result.Result);
    }

    [Fact]
    public void Add_MixedValidAndInvalidNumbers_IgnoresGreaterThan1000()
    {
        var result = _calculator.Add("1,999,1000,1001,2");
        Assert.Equal(2002, result.Result);
        Assert.Equal("1+999+1000+2 = 2002", result.Formula);
    }

    [Fact]
    public void Add_CustomDelimiter_SingleCharacter_IsSupported()
    {
        var result = _calculator.Add("//#\n2#5");
        Assert.Equal(7, result.Result);
        Assert.Equal("2+5 = 7", result.Formula);
    }

    [Fact]
    public void Add_CustomDelimiter_Comma_IsStillSupported()
    {
        var result = _calculator.Add("//,\n2,ff,100");
        Assert.Equal(102, result.Result);
        Assert.Equal("2+0+100 = 102", result.Formula);
    }

    [Fact]
    public void Add_CustomDelimiter_WithNewlineStillWorks()
    {
        var result = _calculator.Add("//;\n1;2\n3");
        Assert.Equal(6, result.Result);
        Assert.Equal("1+2+3 = 6", result.Formula);
    }

    [Fact]
    public void Add_CustomDelimiterWithAnyLength_ReturnsSum()
    {
        var result = _calculator.Add("//[***]\n11***22***33");
        Assert.Equal(66, result.Result);
        Assert.Equal("11+22+33 = 66", result.Formula);
    }

    [Fact]
    public void Add_CustomDelimiterWithAnyLength_AllowsN_Numbers()
    {
        var result = _calculator.Add("//[---]\n1---2---3---4---5");
        Assert.Equal(15, result.Result);
        Assert.Equal("1+2+3+4+5 = 15", result.Formula);
    }

    [Fact]
    public void Add_CustomDelimiterWithAnyLength_IgnoresInvalidNumbers()
    {
        var result = _calculator.Add("//[***]\n2***ff***100");
        Assert.Equal(102, result.Result);
        Assert.Equal("2+0+100 = 102", result.Formula);
    }

    [Fact]
    public void Add_CustomDelimiterWithAnyLength_SupportsCommaAndNewline()
    {
        var result = _calculator.Add("//[***]\n1***2\n3,4");
        Assert.Equal(10, result.Result);
        Assert.Equal("1+2+3+4 = 10", result.Formula);
    }

    [Fact]
    public void Add_CustomDelimiterWithAnyLength_ThrowsOnNegativeNumbers()
    {
        var exception = Assert.Throws<NegativeNumbersNotAllowedException>(() =>
            _calculator.Add("//[***]\n1***-2***-5"));

        Assert.Contains("-2", exception.Message);
        Assert.Contains("-5", exception.Message);
    }

    [Fact]
    public void Add_CustomDelimiterFormat_Malformed_NoNewline_FallsBackToDefault()
    {
        var result = _calculator.Add("//[***]1***2***3");

        Assert.Equal(0, result.Result);
    }

    [Fact]
    public void Add_MultipleDelimitersOfAnyLength_ReturnsSum()
    {
        var result = _calculator.Add("//[*][!!][r9r]\n11r9r22*hh*33!!44");
        Assert.Equal(110, result.Result);
        Assert.Equal("11+22+0+33+44 = 110", result.Formula);
    }

    [Fact]
    public void Add_MultipleDelimiters_CoexistsWithCommaAndNewline()
    {
        var result = _calculator.Add("//[***][#]\n1***2#3\n4,5");
        Assert.Equal(15, result.Result);
        Assert.Equal("1+2+3+4+5 = 15", result.Formula);
    }

    [Fact]
    public void Add_MultipleDelimiters_ThrowsOnNegativeNumbers()
    {
        var exception = Assert.Throws<NegativeNumbersNotAllowedException>(() =>
            _calculator.Add("//[*][!!]\n1*-2!!-3"));

        Assert.Contains("-2", exception.Message);
        Assert.Contains("-3", exception.Message);
    }

    [Fact]
    public void Add_MultipleDelimiters_IgnoresValuesGreaterThan1000()
    {
        var result = _calculator.Add("//[***][%%]\n2***1001%%6");
        Assert.Equal(8, result.Result);
        Assert.Equal("2+6 = 8", result.Formula);
    }

    [Fact]
    public void Add_WithIgnoredValues_ReturnsCorrectFormula()
    {
        var result = _calculator.Add("2,,4,rrrr,1001,6");
        Assert.Equal(12, result.Result);
        Assert.Equal("2+0+4+0+6 = 12", result.Formula);
    }
    
}