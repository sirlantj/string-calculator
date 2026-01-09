namespace StringCalculator.Core.Exceptions;

public class TooManyNumbersException : Exception
{
    public int NumbersProvided { get; }

    public TooManyNumbersException(int numbersProvided)
        : base($"Too many numbers: {numbersProvided} provided, maximum of 2 allowed.")
    {
        NumbersProvided = numbersProvided;
    }
}