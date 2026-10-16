/// <summary>
/// Calculator class that implements the ICalculator interface.
/// Provides basic arithmetic operations and advanced mathematical functions.
/// </summary>
public class Calculator : ICalculator
{
    /// <summary>
    /// Adds two numbers.
    /// </summary>
    /// <param name="a">The first addend.</param>
    /// <param name="b">The second addend.</param>
    /// <returns>The sum of a + b.</returns>
    public double Add(double a, double b)
    {
        return a + b;
    }

    /// <summary>
    /// Subtracts the second operand from the first.
    /// </summary>
    /// <param name="a">The minuend.</param>
    /// <param name="b">The subtrahend.</param>
    /// <returns>The difference a - b.</returns>
    public double Subtract(double a, double b)
    {
        return a - b;
    }

    /// <summary>
    /// Multiplies two numbers.
    /// </summary>
    /// <param name="a">The first multiplicand.</param>
    /// <param name="b">The second multiplicand.</param>
    /// <returns>The product a * b.</returns>
    public double Multiply(double a, double b)
    {
        return a * b;
    }

    /// <summary>
    /// Divides the numerator by the denominator.
    /// </summary>
    /// <param name="a">The numerator (dividend).</param>
    /// <param name="b">The denominator (divisor).</param>
    /// <returns>The quotient a / b.</returns>
    /// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero");
        }
        return a / b;
    }

    /// <summary>
    /// Raises a number to the specified power.
    /// </summary>
    /// <param name="a">The base value.</param>
    /// <param name="b">The exponent.</param>
    /// <returns>The result of a raised to the power of b.</returns>
    public double Power(double a, double b)
    {
        return Math.Pow(a, b);
    }

    /// <summary>
    /// Computes the square root of a non-negative number.
    /// </summary>
    /// <param name="a">The value to compute the square root of.</param>
    /// <returns>The square root of a.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when a is negative.</exception>
    public double Sqrt(double a)
    {
        if (a < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(a), "Cannot take square root of negative number");
        }
        return Math.Sqrt(a);
    }

    /// <summary>
    /// Calculates the percentage of a value.
    /// </summary>
    /// <param name="a">The base value.</param>
    /// <param name="percent">The percentage to apply (e.g., 20 for 20%).</param>
    /// <returns>The computed percentage of a.</returns>
    public double Percentage(double a, double percent)
    {
        return a * percent / 100.0;
    }
}
