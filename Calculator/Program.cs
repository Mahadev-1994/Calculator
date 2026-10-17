using System.Text.Json.Serialization;


/// <summary>
/// Calculator Web Application
/// This application provides a web UI for performing various arithmetic and mathematical operations.
/// It uses dependency injection to manage the Calculator service.
/// </summary>

// Build the web application
var builder = WebApplication.CreateBuilder(args);

// Register the Calculator service in the dependency injection container
builder.Services.AddScoped<ICalculator, Calculator>();

// Add CORS support for UI
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

// Serve static files (HTML, CSS, JS) and set index.html as default
app.UseDefaultFiles(); // must come before UseStaticFiles
app.UseStaticFiles();

// API endpoints for calculator operations
app.MapPost("/api/calculate/{operation}", (string operation, CalculationRequest request, ICalculator calculator) =>
{
    try
    {
        double result = operation.ToLower() switch
        {
            "add" => calculator.Add(request.A, request.B),
            "subtract" => calculator.Subtract(request.A, request.B),
            "multiply" => calculator.Multiply(request.A, request.B),
            "divide" => calculator.Divide(request.A, request.B),
            "power" => calculator.Power(request.A, request.B),
            "sqrt" => calculator.Sqrt(request.A),
            "percentage" => calculator.Percentage(request.A, request.B),
            _ => throw new ArgumentException("Unknown operation")
        };

        return Results.Ok(new { success = true, result });
    }
    catch (DivideByZeroException)
    {
        return Results.BadRequest(new { success = false, error = "Division by zero" });
    }
    catch (ArgumentOutOfRangeException ex)
    {
        return Results.BadRequest(new { success = false, error = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, error = ex.Message });
    }
});

app.Run();

/// <summary>
/// Request model for calculator operations
/// </summary>
public class CalculationRequest
{
    [JsonPropertyName("a")]
    public double A { get; set; }

    [JsonPropertyName("b")]
    public double B { get; set; }
}
