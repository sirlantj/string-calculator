# String Calculator

This project implements a **String Calculator**, following clean architecture principles and focusing on readability, testability, and incremental development by requirements.

---

## Goal

The goal of this project is to demonstrate:

- Incremental development by requirements
- Clear separation of concerns
- Readable, testable, and extensible code
- Proper use of Dependency Injection, even in a console application

This version implements **Requirements #1 and #2**.

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

Examples:

```
"20" -> 20
"1,5000" -> 5001
"4,-3" -> 1
"5,abc" -> 5
"1,2,3" -> Exception
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
