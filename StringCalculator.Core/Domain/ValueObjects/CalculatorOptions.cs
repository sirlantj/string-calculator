namespace StringCalculator.Core.Domain.ValueObjects;

public class CalculatorOptions
{
    public char? AlternateDelimiter { get; set; } = '\n';
    public bool DenyNegatives { get; set; } = true;
    public int UpperBound { get; set; } = 1000;
}