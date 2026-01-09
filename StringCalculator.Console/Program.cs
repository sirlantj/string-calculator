using Microsoft.Extensions.DependencyInjection;
using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain;

var services = new ServiceCollection();
services.AddSingleton<IStringCalculatorEngine, StringCalculatorEngine>();

var provider = services.BuildServiceProvider();
var calculator = provider.GetRequiredService<IStringCalculatorEngine>();

Console.WriteLine("String Calculator");
Console.WriteLine(@"Enter values using ',' or '\n' as delimiters.");
Console.WriteLine("Press Ctrl+C to exit.\n");

bool exiting = false;

Console.CancelKeyPress += (_, e) =>
{
    Console.WriteLine("\n\nExiting gracefully... Goodbye!");
    e.Cancel = true; 
    exiting = true;
};

while (!exiting)
{
    var input = Console.ReadLine();

    if (exiting) break;

    try
    {
        input = input?.Replace("\\n", "\n") ?? string.Empty;
        var result = calculator.Add(input);

        Console.WriteLine($"Formula: {result.Formula}");
        Console.WriteLine($"Result: {result.Result}\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n");
    }
}