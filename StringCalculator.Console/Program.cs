using Microsoft.Extensions.DependencyInjection;
using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain;

var services = new ServiceCollection();

services.AddScoped<IStringCalculatorEngine, StringCalculatorEngine>();

var provider = services.BuildServiceProvider();
var calculator = provider.GetRequiredService<IStringCalculatorEngine>();

Console.WriteLine("String Calculator - Req #1");
Console.WriteLine("Enter values (comma separated):");

while (true)
{
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        break;

    try
    {
        var result = calculator.Add(input);
        Console.WriteLine($"Result: {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
