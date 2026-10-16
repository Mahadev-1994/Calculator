/// <summary>
/// Interface for calculator operations providing basic arithmetic and advanced mathematical functions.
/// </summary>
public interface ICalculator
{
    /// <summary>
    /// Adds two numbers.
    /// </summary>
    /// <param name="a">The first addend.</param>
    /// <param name="b">The second addend.</param>
    /// <returns>The sum of <c>a + b</c>.</returns>
    double Add(double a, double b);

    /// <summary>
    /// Subtracts the second operand from the first.
    /// </summary>
    /// <param name="a">The minuend.</param>
    /// <param name="b">The subtrahend.</param>
    /// <returns>The difference <c>a - b</c>.</returns>
    double Subtract(double a, double b);

    /// <summary>
    /// Multiplies two numbers.
    /// </summary>
    /// <param name="a">The first multiplicand.</param>
    /// <param name="b">The second multiplicand.</param>
    /// <returns>The product <c>a * b</c>.</returns>
    double Multiply(double a, double b);

    /// <summary>
    /// Divides the numerator by the denominator.
    /// </summary>
    /// <param name="a">The numerator (dividend).</param>
    /// <param name="b">The denominator (divisor).</param>
    /// <returns>The quotient <c>a / b</c>.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="b"/> is zero.</exception>
    double Divide(double a, double b);

    /// <summary>
    /// Raises a number to the specified power.
    /// </summary>
    /// <param name="a">The base value.</param>
    /// <param name="b">The exponent.</param>
    /// <returns>The result of <c>Math.Pow(a, b)</c>.</returns>
    double Power(double a, double b);

    /// <summary>
    /// Computes the square root of a non-negative number.
    /// </summary>
    /// <param name="a">The value to compute the square root of.</param>
    /// <returns>The square root of <paramref name="a"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="a"/> is negative.</exception>
    double Sqrt(double a);

    /// <summary>
    /// Calculates the percentage of a value.
    /// </summary>
    /// <param name="a">The base value.</param>
    /// <param name="percent">The percentage to apply (e.g., 20 for 20%).</param>
    /// <returns>The computed percentage of <paramref name="a"/>: <c>a * percent / 100.0</c>.</returns>
    double Percentage(double a, double percent);
}
