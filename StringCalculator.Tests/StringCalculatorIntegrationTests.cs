namespace StringCalculator.Tests;
public class StringCalculatorIntegrationTests : IClassFixture<StringCalculatorTestsFixture>
{
    private readonly StringCalculatorEngine _calculator;
    
    public StringCalculatorIntegrationTests(StringCalculatorTestsFixture fixture)
    {
        _calculator = fixture.Calculator;
    }

    [Fact]
    public void AddBackwardCompatibility_StillWorks()
    {
        var result = _calculator.Add("1,2,3");
        Assert.Equal(6, result.Result);
    }

    
}
