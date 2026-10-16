# Calculator Application - Testing Summary

## Project Structure Refactoring

### Files Created/Modified:

1. **ICalculator.cs** - Interface defining the calculator contract
   - Add(double a, double b)
   - Subtract(double a, double b)
   - Multiply(double a, double b)
   - Divide(double a, double b) - throws DivideByZeroException
   - Power(double a, double b)
   - Sqrt(double a) - throws ArgumentOutOfRangeException for negative numbers
   - Percentage(double a, double percent)

2. **Calculator.cs** - Implementation class
   - Implements all methods from ICalculator interface
   - Includes proper error handling for edge cases
   - Fully documented with XML comments

3. **Program.cs** - ASP.NET Core Web Application
   - Registered Calculator service with dependency injection (AddScoped<ICalculator, Calculator>)
   - API endpoint: POST /api/calculate/{operation}
   - Accepts CalculationRequest with properties A and B
   - Returns JSON response with success status and result

4. **wwwroot/index.html** - Web UI
   - Beautiful, responsive interface
   - Input fields for all operations
   - Calculation history tracking

5. **wwwroot/script.js** - Client-side logic
   - API communication
   - Error handling
   - History management

6. **wwwroot/styles.css** - Styling
   - Modern gradient design
   - Responsive layout

## Build Status: ✅ SUCCESS

Build succeeded in 3.0 seconds with all files compiled correctly.

Generated artifacts:
- Calculator.dll
- Calculator.exe
- Calculator.pdb
- Static web assets

## Application Server Status: ✅ RUNNING

Server is now listening on: **http://localhost:5000**

### Hosting Information:
- Environment: Development
- Content root path: C:\GIT\Calculator\Calculator
- Port: 5000

## How to Test the Calculator

### Option 1: Web UI (Recommended)
1. Open browser to: http://localhost:5000
2. Enter numbers in the input fields
3. Click the operation button
4. View results in the display area
5. Check calculation history on the right panel

### Option 2: API Testing with PowerShell

#### Test Add (10 + 5 = 15)
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/calculate/add" -Method Post `
  -Headers @{"Content-Type"="application/json"} `
  -Body '{"a": 10, "b": 5}'
# Expected: $response.result = 15
```

#### Test Subtract (10 - 5 = 5)
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/calculate/subtract" -Method Post `
  -Headers @{"Content-Type"="application/json"} `
  -Body '{"a": 10, "b": 5}'
# Expected: $response.result = 5
```

#### Test Multiply (10 × 5 = 50)
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/calculate/multiply" -Method Post `
  -Headers @{"Content-Type"="application/json"} `
  -Body '{"a": 10, "b": 5}'
# Expected: $response.result = 50
```

#### Test Divide (10 ÷ 5 = 2)
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/calculate/divide" -Method Post `
  -Headers @{"Content-Type"="application/json"} `
  -Body '{"a": 10, "b": 5}'
# Expected: $response.result = 2
```

#### Test Power (2 ^ 8 = 256)
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/calculate/power" -Method Post `
  -Headers @{"Content-Type"="application/json"} `
  -Body '{"a": 2, "b": 8}'
# Expected: $response.result = 256
```

#### Test Square Root (√16 = 4)
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/calculate/sqrt" -Method Post `
  -Headers @{"Content-Type"="application/json"} `
  -Body '{"a": 16, "b": 0}'
# Expected: $response.result = 4
```

#### Test Percentage (100 of 25% = 25)
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/calculate/percentage" -Method Post `
  -Headers @{"Content-Type"="application/json"} `
  -Body '{"a": 100, "b": 25}'
# Expected: $response.result = 25
```

#### Test Division by Zero Error
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/calculate/divide" -Method Post `
  -Headers @{"Content-Type"="application/json"} `
  -Body '{"a": 10, "b": 0}'
# Expected: $response.success = false, $response.error = "Division by zero"
```

#### Test Negative Square Root Error
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/calculate/sqrt" -Method Post `
  -Headers @{"Content-Type"="application/json"} `
  -Body '{"a": -9, "b": 0}'
# Expected: $response.success = false, $response.error contains "Cannot take square root of negative number"
```

## Architecture Benefits

1. **Separation of Concerns**: Calculator logic is isolated in a dedicated class
2. **Interface-Based Design**: ICalculator interface allows for easy mocking and testing
3. **Dependency Injection**: ASP.NET Core DI container manages the Calculator lifecycle
4. **Testability**: Each method can be unit tested independently
5. **Maintainability**: Changes to calculator logic only need to be made in one place
6. **Scalability**: Easy to add new operations or implementations

## Key Features

✅ All 7 calculator operations implemented
✅ Proper error handling for edge cases
✅ Beautiful, responsive web UI
✅ Calculation history tracking
✅ Real-time API communication
✅ CORS support for cross-origin requests
✅ Well-documented code with XML comments
✅ Clean architecture with interface-based design
✅ Dependency injection pattern

## Next Steps (Optional Enhancements)

- Add unit tests for Calculator class
- Add integration tests for API endpoints
- Add more mathematical operations (logarithm, trigonometry, etc.)
- Add persistent storage for calculation history
- Add user authentication
- Containerize with Docker
- Deploy to cloud (Azure, AWS, etc.)
