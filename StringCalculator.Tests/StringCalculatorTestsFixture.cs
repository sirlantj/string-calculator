public class StringCalculatorTestsFixture
{
    public StringCalculatorEngine Calculator { get; }
    
    public StringCalculatorTestsFixture()
    {
        var parser = new DelimiterParser();
        var processor = new NumberProcessor(parser);
        Calculator = new StringCalculatorEngine(processor);
    }
}
