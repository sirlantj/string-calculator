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

This version implements **Stretch #1, #2, #3, #4 and #5**.

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

## Stretch #5 (Implemented)

Rules:

- Inserted others operations
- Defaults preserve original behavior

```bash
# Sum (Default)
> 1,2,3
Formula: 1+2+3 = 6
Result: 6

> //[*]\n2*3*1001
Formula: 2+3 = 5
Result: 5

# Subtraction
> sub:10,3,2
Formula: 10-3-2 = 5
Result: 5

# Multiplication
> mul:2,3,4
Formula: 2×3×4 = 24
Result: 24

> mul:2,3,1001
Formula: 2×3 = 6
Result: 6

#Division
> div:100,5,2
Formula: 100÷5÷2 = 10
Result: 10

> div:20,4
Formula: 20÷4 = 5
Result: 5
```

## Project Structure

```
StringCalculator/
│
├── StringCalculator.Console/
│   ├── Program.cs
│   └── StringCalculator.Console.csproj
│
├── StringCalculator.Core/
│   ├── Contracts/
│   │   ├── IDefaultDelimiterProvider.cs
│   │   ├── IDelimiterParser.cs
│   │   ├── INumberInclusionPolicy.cs
│   │   ├── INumberParser.cs
│   │   ├── INumberValidator.cs
│   │   ├── IOperation.cs
│   │   └── IStringCalculatorEngine.cs
│   │
│   ├── Domain/
│   │   ├── Operations/
│   │   │   ├── AddOperation.cs
│   │   │   ├── DivOperation.cs
│   │   │   ├── MulOperation.cs
│   │   │   └── SubOperation.cs
│   │   ├── ValueObjects/
│   │   │   ├── CalculationResult.cs
│   │   │   └── CalculatorOptions.cs
│   │   ├── DelimiterParser.cs
│   │   └── StringCalculatorEngine.cs
│   │
│   ├── Exceptions/
│   │   └── NegativeNumbersNotAllowedException.cs
│   │
│   ├── Infrastructure/
│   │   ├── Numbers/
│   │   │   ├── IntNumberParser.cs
│   │   │   ├── NegativeNumberValidator.cs
│   │   │   └── UpperBoundPolicy.cs
│   │   └── Parsing/
│   │       └── DefaultDelimiterProvider.cs
│   │
│   └── StringCalculator.Core.csproj
│
├── StringCalculator.Tests/
│   ├── GlobalUsings.cs
│   ├── StringCalculator.Tests.csproj
│   ├── StringCalculatorIntegrationTests.cs
│   ├── StringCalculatorOperationsTests.cs
│   ├── StringCalculatorOptionsTests.cs
│   ├── StringCalculatorTests.cs
│   └── StringCalculatorTestsFixture.cs
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

### Core Project (Domain-Centric)

The `StringCalculator.Core` project contains all **business rules, contracts, and domain logic**, with no dependency on external frameworks.

This makes the core reusable by:

- Console applications
- Web APIs
- Background services
- Any other .NET host

---

### Clean Architecture & SOLID Principles

The solution follows Clean Architecture concepts and applies SOLID principles:

- **SRP (Single Responsibility Principle)**  
  Each class has a single, well-defined responsibility (parsing, validation, inclusion rules, operations).

- **OCP (Open/Closed Principle)**  
  New operations, delimiters, validation rules, or inclusion policies can be added **without modifying existing code**, only by introducing new implementations.

- **DIP (Dependency Inversion Principle)**  
  The core depends only on abstractions (`interfaces`), never on concrete implementations.

- **DRY (Don’t Repeat Yourself)**  
  All rules are centralized and reused across the engine and tests.

---

### Dependency Injection

Even though this is a console application, Dependency Injection is used to:

- Keep the code loosely coupled
- Improve testability
- Allow easy replacement of behaviors (e.g., parsing, validation, inclusion rules)

---

### Extensibility by Design

The calculator engine acts as an orchestration layer and is **policy-driven**:

- Operations are injected (`IOperation`)
- Validation rules are injected (`INumberValidator`)
- Inclusion rules are injected (`INumberInclusionPolicy`)
