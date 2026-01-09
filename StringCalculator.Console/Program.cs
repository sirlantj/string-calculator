using Microsoft.Extensions.DependencyInjection;
using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain;
using StringCalculator.Core.Domain.ValueObjects;

var services = new ServiceCollection();
services.AddSingleton<IStringCalculatorEngine, StringCalculatorEngine>();

var provider = services.BuildServiceProvider();
var calculator = provider.GetRequiredService<IStringCalculatorEngine>();

var options = ParseArguments(args);

Console.WriteLine("String Calculator (Configurable)");
Console.WriteLine("Press Ctrl+C to exit.\n");

if (options.AlternateDelimiter.HasValue)
    Console.WriteLine($"Alternate delimiter: '{options.AlternateDelimiter}'");

Console.WriteLine($"Deny negatives: {options.DenyNegatives}");
Console.WriteLine($"Upper bound: {options.UpperBound}\n");

bool exiting = false;
Console.CancelKeyPress += (_, e) =>
{
    Console.WriteLine("\n\nExiting... Goodbye!");
    e.Cancel = true;
    exiting = true;
};

while (!exiting)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    if (exiting) break;

    try
    {
        input = input?.Replace("\\n", "\n") ?? string.Empty;
        var result = calculator.Add(input, options);

        Console.WriteLine($"Formula: {result.Formula}");
        Console.WriteLine($"Result: {result.Result}\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n");
    }
}

static CalculatorOptions ParseArguments(string[] args)
{
    var options = new CalculatorOptions();

    for (int i = 0; i < args.Length; i++)
    {
        switch (args[i].ToLower())
        {
            case "--delimiter":
            case "-d":
                if (i + 1 < args.Length && args[i + 1].Length == 1)
                {
                    options.AlternateDelimiter = args[++i][0];
                }
                else
                {
                    Console.WriteLine("Warning: --delimiter requires a single character.");
                }
                break;

            case "--allow-negatives":
            case "-n":
                options.DenyNegatives = false;
                break;

            case "--upper-bound":
            case "-u":
                if (i + 1 < args.Length && int.TryParse(args[i + 1], out var bound))
                    options.UpperBound = bound;
                i++;
                break;
        }
    }

    return options;
}