# String Calculator

This project implements a **String Calculator**, following clean architecture principles and focusing on readability, testability, and incremental development by requirements.

---

## Goal

The goal of this project is to demonstrate:

- Incremental development by requirements
- Clear separation of concerns
- Readable, testable, and extensible code
- Proper use of Dependency Injection, even in a console application

This version implements **Requirements #1, #2, #3, #4, #5 #6, #7 and #8**.

---

## Requirement #1 (Implemented)

Rules:

- Support a maximum of **two numbers**
- Numbers are separated by a comma `,`
- Empty or missing numbers are treated as `0`
- Invalid numbers are treated as `0`
- Throws an exception when more than two numbers are provided

## Requirement #2 (Implemented)

Rules:

- Remove limit of 2 numbers
- Remove TooManyNumbersException (no longer needed)
- Update engine to handle any number of parts
- Add new tests for multiple numbers
- Keep all previous behavior (empty/invalid as 0, trim, etc.)

## Requirement #3 (Implemented)

Rules:

- Add support for '\n' as alternative delimiter alongside ','
- Split using both delimiters with StringSplitOptions.RemoveEmptyEntries
- Preserve all previous behavior (unlimited numbers, invalid/empty as 0, trim)
- Add comprehensive tests for newline and mixed delimiter scenarios

## Requirement #4 (Implemented)

Rules:

- Throw NegativeNumbersNotAllowedException when negative numbers are present
- Exception message lists all negative numbers found
- Add comprehensive tests covering single and multiple negatives
- Preserve all previous behavior (delimiters, invalid as 0, etc.)

## Requirement #5 (Implemented)

Rules:

- Numbers > 1000 are treated as 0 (ignored in the sum)
- All previous behavior preserved (delimiters, negatives rejection, invalid/empty as 0)
- Added comprehensive tests for values above, at, and below the threshold

## Requirement #6 (Implemented)

Rules:

- Parse //{delimiter}\n format
- Support single-character custom delimiter
- Fallback to default delimiters when not present
- Added tests for custom and default cases

## Requirement #7 (Implemented)

Rules:

- Support //[***]\n... format
- Extract delimiter between brackets
- Maintain compatibility with previous formats
- Added relevant tests

## Requirement #8 (Implemented)

Rules:

- Full support for //[{delim1}][{delim2}]...\n{numbers} format
- Multiple delimiters of any length parsed via regex
- Added comprehensive tests for multiple delimiters and edge cases
- All previous requirements preserved

Examples:

```
"20" -> 20
"1,5000" -> 5001
"4,-3" -> 1
"5,abc" -> 5
"1,2,3" -> 6
"1\n2,3" -> 6
"1,-2,3" -> Error: Negatives not allowed: -2
"2,1001,6" -> 8
"1\n2,3" -> 6
"//[***]\n11***22***33" -> 66
"//[*][!!][r9r]\n11r9r22*hh*33!!44" → 110
```

## Stretch goals

This version implements **Stretch #1, #2 and #3**.

## Stretch #1 (Implemented)

Rules:

- Return CalculationResult with sum and human-readable formula
- Formula includes only values used in the sum (invalid/empty as 0, negatives excluded, >1000 ignored)
- Example: "2,,4,rrrr,1001,6" → "2 + 0 + 4 + 0 + 6 = 12"
- Introduced CalculationResult value object for rich return type
- All previous requirements preserved

## Stretch #2 (Implemented)

Rules:

- Application now runs until Ctrl+C is pressed
- Empty lines are processed (return 0) instead of exiting
- Graceful shutdown with farewell message

## Stretch #3 (Implemented)

Rules:

- Add CalculatorOptions for runtime configuration
- Support:
  --delimiter (-d): change alternate delimiter
  --allow-negatives (-n): toggle negative rejection
  --upper-bound (-u): change maximum value threshold
- Defaults preserve original behavior
- Configuration parsed in console layer (Clean Architecture preserved)

```bash
# Default
dotnet run

# Alternate delimiter ';'
dotnet run --delimiter ";"

# Allow negatives
dotnet run --allow-negatives

# Limit 2000
dotnet run --upper-bound 2000

# All
dotnet run -d ";" -n -u 2000
```

## Project Structure

```
StringCalculator/
│
├── src/
│ ├── StringCalculator.Console/
│ │ ├── Program.cs
│ │ └── StringCalculator.Console.csproj
│ │
│ └── StringCalculator.Core/
│ ├── Contracts/
│ │ └── IStringCalculatorEngine.cs
│ ├── Domain/
│ │ └── StringCalculatorEngine.cs
  │── Exceptions/
│ │ └── NegativeNumbersNotAllowedException.cs
│ └── StringCalculator.Core.csproj
│
├── StringCalculator.Tests/
│ ├── StringCalculatorTests.cs
│ └── StringCalculator.Tests.csproj
│
├── .gitignore
├── README.md
└── StringCalculator.sln

```

## Requirements

- .NET SDK 8.0 or later
- Visual Studio Code (or any IDE)
- Git

## Running the Console Application

1. Clone the repository:

```bash
git clone https://github.com/sirlantj/string-calculator.git
```

2. Navigate to the console project:

```bash
cd StringCalculator/src/StringCalculator.Console
```

3. Run the application:

```bash
dotnet run
```

## Running the Tests

From the root of the solution:

```bash
dotnet test
```

## Architecture Decisions

- **Console Application**  
  Used to keep the focus on business logic.

- **Core Project**  
  Contains all domain rules and contracts.  
  Can be reused by:

  - Console applications
  - Web APIs
  - Background services

- **Dependency Injection**  
  Even in a console app, DI is used to keep the code loosely coupled and testable.
