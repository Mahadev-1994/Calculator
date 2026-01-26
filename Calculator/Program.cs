/// <summary>
/// Simple command-line calculator demonstrating basic arithmetic operations.
/// This file contains top-level statements and local functions used by the interactive loop.
/// </summary>
double Add(double a, double b) => a + b;

/// <summary>
/// Subtracts the second operand from the first.
/// </summary>
/// <param name="a">The minuend.</param>
/// <param name="b">The subtrahend.</param>
/// <returns>The difference <c>a - b</c>.</returns>
double Subtract(double a, double b) => a - b;

/// <summary>
/// Multiplies two numbers.
/// </summary>
/// <param name="a">The first multiplicand.</param>
/// <param name="b">The second multiplicand.</param>
/// <returns>The product <c>a * b</c>.</returns>
double Multiply(double a, double b) => a * b;

/// <summary>
/// Divides the numerator by the denominator.
/// </summary>
/// <param name="a">The numerator (dividend).</param>
/// <param name="b">The denominator (divisor).</param>
/// <returns>The quotient <c>a / b</c>.</returns>
/// <exception cref="DivideByZeroException">Thrown when <paramref name="b"/> is zero.</exception>
double Divide(double a, double b)
{
    if (b == 0) throw new DivideByZeroException();
    return a / b;
}

/// <summary>
/// Raises a number to the specified power.
/// </summary>
/// <param name="a">The base value.</param>
/// <param name="b">The exponent.</param>
/// <returns>The result of <c>Math.Pow(a, b)</c>.</returns>
double Power(double a, double b) => Math.Pow(a, b);

/// <summary>
/// Computes the square root of a non-negative number.
/// </summary>
/// <param name="a">The value to compute the square root of.</param>
/// <returns>The square root of <paramref name="a"/>.</returns>
/// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="a"/> is negative.</exception>
double Sqrt(double a)
{
    if (a < 0) throw new ArgumentOutOfRangeException(nameof(a), "Cannot take square root of negative number");
    return Math.Sqrt(a);
}

/// <summary>
/// Calculates the percentage of a value.
/// </summary>
/// <param name="a">The base value.</param>
/// <param name="percent">The percentage to apply (e.g., 20 for 20%).</param>
/// <returns>The computed percentage of <paramref name="a"/>: <c>a * percent / 100.0</c>.</returns>
double Percentage(double a, double percent) => a * percent / 100.0;

/// <summary>
/// Prompts the user and attempts to parse a <see cref="double"/> from the console input.
/// </summary>
/// <param name="prompt">The message to display to the user before reading input.</param>
/// <param name="value">When this method returns, contains the parsed <see cref="double"/> if successful; otherwise 0.</param>
/// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
bool TryReadDouble(string prompt, out double value)                 
{
    Console.Write(prompt);
    var input = Console.ReadLine();
    return double.TryParse(input, out value);
}

/// <summary>
/// Writes the main menu for the calculator to the console.
/// </summary>
void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("Standard Calculator");
    Console.WriteLine("-------------------");
    Console.WriteLine("1) Add");
    Console.WriteLine("2) Subtract");
    Console.WriteLine("3) Multiply");
    Console.WriteLine("4) Divide");
    Console.WriteLine("5) Power (a^b)");
    Console.WriteLine("6) Square root");
    Console.WriteLine("7) Percentage (a of b%)");
    Console.WriteLine("C) Clear");
    Console.WriteLine("E) Exit");
    Console.WriteLine();
}

// Memory holds the last computed result (persistent across operations in the session).
double memory = 0;
bool running = true;
while (running)
{
    ShowMenu();
    Console.Write("Select option: ");
    var choice = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(choice)) continue;
    choice = choice.Trim().ToUpperInvariant();

    try
    {
        switch (choice)
        {
            case "1":
            case "ADD":
                if (!TryReadDouble("Enter first number: ", out var a1)) { Console.WriteLine("Invalid input"); break; }
                if (!TryReadDouble("Enter second number: ", out var b1)) { Console.WriteLine("Invalid input"); break; }
                memory = Add(a1, b1);
                Console.WriteLine($"Result: {memory}");
                break;
            case "2":
            case "SUBTRACT":
            case "SUB":
                if (!TryReadDouble("Enter first number: ", out var a2)) { Console.WriteLine("Invalid input"); break; }
                if (!TryReadDouble("Enter second number: ", out var b2)) { Console.WriteLine("Invalid input"); break; }
                memory = Subtract(a2, b2);
                Console.WriteLine($"Result: {memory}");
                break;
            case "3":
            case "MULTIPLY":
            case "MUL":
                if (!TryReadDouble("Enter first number: ", out var a3)) { Console.WriteLine("Invalid input"); break; }
                if (!TryReadDouble("Enter second number: ", out var b3)) { Console.WriteLine("Invalid input"); break; }
                memory = Multiply(a3, b3);
                Console.WriteLine($"Result: {memory}");
                break;
            case "4":
            case "DIVIDE":
            case "DIV":
                if (!TryReadDouble("Enter numerator: ", out var a4)) { Console.WriteLine("Invalid input"); break; }
                if (!TryReadDouble("Enter denominator: ", out var b4)) { Console.WriteLine("Invalid input"); break; }
                try
                {
                    memory = Divide(a4, b4);
                    Console.WriteLine($"Result: {memory}");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Error: Division by zero");
                }
                break;
            case "5":
            case "POWER":
                if (!TryReadDouble("Enter base: ", out var a5)) { Console.WriteLine("Invalid input"); break; }
                if (!TryReadDouble("Enter exponent: ", out var b5)) { Console.WriteLine("Invalid input"); break; }
                memory = Power(a5, b5);
                Console.WriteLine($"Result: {memory}");
                break;
            case "6":
            case "SQRT":
                if (!TryReadDouble("Enter number: ", out var a6)) { Console.WriteLine("Invalid input"); break; }
                try
                {
                    memory = Sqrt(a6);
                    Console.WriteLine($"Result: {memory}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                break;
            case "7":
            case "PERCENTAGE":
                if (!TryReadDouble("Enter value: ", out var a7)) { Console.WriteLine("Invalid input"); break; }
                if (!TryReadDouble("Enter percent: ", out var p7)) { Console.WriteLine("Invalid input"); break; }
                memory = Percentage(a7, p7);
                Console.WriteLine($"Result: {memory}");
                break;
            case "C":
            case "CLEAR":
                memory = 0;
                Console.Clear();
                break;
            case "E":
            case "EXIT":
                running = false;
                break;
            default:
                Console.WriteLine("Unknown option");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
