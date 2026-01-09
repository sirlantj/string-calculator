using Microsoft.Extensions.DependencyInjection;
using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain;

var services = new ServiceCollection();
services.AddSingleton<IStringCalculatorEngine, StringCalculatorEngine>();

var provider = services.BuildServiceProvider();
var calculator = provider.GetRequiredService<IStringCalculatorEngine>();

Console.WriteLine("String Calculator");
Console.WriteLine(@"Enter values using ',' or '\n' as delimiters.");
Console.WriteLine("Empty line exits.\n");

while (true)
{
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        break;

    try
    {
        input = input.Replace("\\n", "\n");
        var result = calculator.Add(input);
        Console.WriteLine($"Formula: {result.Formula}");
        Console.WriteLine($"Result: {result.Result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
