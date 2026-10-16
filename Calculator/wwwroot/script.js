// History array to store calculation results
let calculationHistory = [];

/**
 * Performs a calculation operation by calling the backend API
 * @param {string} operation - The operation to perform (add, subtract, multiply, divide, power, sqrt, percentage)
 */
async function performOperation(operation) {
    try {
        // Get input values based on operation type
        let a, b;
        
        switch (operation) {
            case 'add':
                a = parseFloat(document.getElementById('add-a').value);
                b = parseFloat(document.getElementById('add-b').value);
                break;
            case 'subtract':
                a = parseFloat(document.getElementById('sub-a').value);
                b = parseFloat(document.getElementById('sub-b').value);
                break;
            case 'multiply':
                a = parseFloat(document.getElementById('mul-a').value);
                b = parseFloat(document.getElementById('mul-b').value);
                break;
            case 'divide':
                a = parseFloat(document.getElementById('div-a').value);
                b = parseFloat(document.getElementById('div-b').value);
                break;
            case 'power':
                a = parseFloat(document.getElementById('pow-a').value);
                b = parseFloat(document.getElementById('pow-b').value);
                break;
            case 'percentage':
                a = parseFloat(document.getElementById('perc-a').value);
                b = parseFloat(document.getElementById('perc-b').value);
                break;
            case 'sqrt':
                a = parseFloat(document.getElementById('sqrt-a').value);
                b = 0; // Not used for sqrt
                break;
            default:
                showError('Unknown operation');
                return;
        }

        // Validate inputs
        if (isNaN(a)) {
            showError('Invalid input for first number');
            return;
        }

        if (operation !== 'sqrt' && isNaN(b)) {
            showError('Invalid input for second number');
            return;
        }

        // Call the backend API
        const response = await fetch(`/api/calculate/${operation}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ a, b })
        });

        const data = await response.json();

        if (data.success) {
            // Display result
            const result = data.result;
            displayResult(result, operation, a, b);
            addToHistory(operation, a, b, result);
        } else {
            showError(data.error || 'Calculation failed');
        }
    } catch (error) {
        console.error('Error:', error);
        showError('Error communicating with server: ' + error.message);
    }
}

/**
 * Displays the calculation result
 * @param {number} result - The result of the calculation
 * @param {string} operation - The operation performed
 * @param {number} a - First operand
 * @param {number} b - Second operand
 */
function displayResult(result, operation, a, b) {
    const resultDisplay = document.getElementById('result');
    
    // Format the result to a reasonable number of decimal places
    const formattedResult = formatNumber(result);
    
    resultDisplay.value = formattedResult;
    
    // Create operation string for display
    let operationStr = getOperationString(operation, a, b);
    console.log(`${operationStr} = ${formattedResult}`);
}

/**
 * Gets a formatted string representation of the operation
 * @param {string} operation - The operation type
 * @param {number} a - First operand
 * @param {number} b - Second operand
 * @returns {string} Formatted operation string
 */
function getOperationString(operation, a, b) {
    switch (operation) {
        case 'add':
            return `${a} + ${b}`;
        case 'subtract':
            return `${a} - ${b}`;
        case 'multiply':
            return `${a} × ${b}`;
        case 'divide':
            return `${a} ÷ ${b}`;
        case 'power':
            return `${a} ^ ${b}`;
        case 'percentage':
            return `${a} of ${b}%`;
        case 'sqrt':
            return `√${a}`;
        default:
            return 'Unknown operation';
    }
}

/**
 * Formats a number to avoid too many decimal places
 * @param {number} num - The number to format
 * @returns {string} Formatted number
 */
function formatNumber(num) {
    // If the number is an integer, return it as is
    if (Number.isInteger(num)) {
        return num.toString();
    }
    
    // Otherwise, round to 10 decimal places and remove trailing zeros
    return parseFloat(num.toFixed(10)).toString();
}

/**
 * Adds a calculation to the history
 * @param {string} operation - The operation performed
 * @param {number} a - First operand
 * @param {number} b - Second operand
 * @param {number} result - The result
 */
function addToHistory(operation, a, b, result) {
    const entry = {
        operation,
        a,
        b,
        result,
        timestamp: new Date().toLocaleTimeString()
    };
    
    calculationHistory.unshift(entry); // Add to beginning of array
    
    // Keep only last 50 calculations
    if (calculationHistory.length > 50) {
        calculationHistory.pop();
    }
    
    updateHistoryDisplay();
}

/**
 * Updates the history display in the UI
 */
function updateHistoryDisplay() {
    const historyList = document.getElementById('history-list');
    historyList.innerHTML = '';
    
    if (calculationHistory.length === 0) {
        historyList.innerHTML = '<div style="text-align: center; color: #999; padding: 20px;">No calculations yet</div>';
        return;
    }
    
    calculationHistory.forEach(entry => {
        const item = document.createElement('div');
        item.className = 'history-item';
        
        const operationStr = getOperationString(entry.operation, entry.a, entry.b);
        const formattedResult = formatNumber(entry.result);
        
        item.innerHTML = `
            <div class="history-item-operation">${operationStr}</div>
            <div class="history-item-result">= ${formattedResult}</div>
            <div style="font-size: 11px; color: #999; margin-top: 3px;">${entry.timestamp}</div>
        `;
        
        historyList.appendChild(item);
    });
}

/**
 * Displays an error message
 * @param {string} message - The error message
 */
function showError(message) {
    const resultDisplay = document.getElementById('result');
    resultDisplay.value = 'Error: ' + message;
    resultDisplay.style.borderColor = '#ff6b6b';
    
    setTimeout(() => {
        resultDisplay.style.borderColor = '#667eea';
    }, 3000);
}

/**
 * Clears all input fields
 */
function clearAll() {
    document.getElementById('add-a').value = '';
    document.getElementById('add-b').value = '';
    document.getElementById('sub-a').value = '';
    document.getElementById('sub-b').value = '';
    document.getElementById('mul-a').value = '';
    document.getElementById('mul-b').value = '';
    document.getElementById('div-a').value = '';
    document.getElementById('div-b').value = '';
    document.getElementById('pow-a').value = '';
    document.getElementById('pow-b').value = '';
    document.getElementById('perc-a').value = '';
    document.getElementById('perc-b').value = '';
    document.getElementById('sqrt-a').value = '';
    document.getElementById('result').value = '';
}

/**
 * Clears the calculation history
 */
function clearHistory() {
    if (calculationHistory.length === 0) {
        alert('History is already empty');
        return;
    }
    
    if (confirm('Are you sure you want to clear the calculation history?')) {
        calculationHistory = [];
        updateHistoryDisplay();
    }
}

// Initialize the history display when the page loads
document.addEventListener('DOMContentLoaded', () => {
    updateHistoryDisplay();
});

// Allow Enter key to trigger calculations
document.addEventListener('keypress', (event) => {
    if (event.key === 'Enter') {
        // Determine which operation field has focus and trigger that operation
        const activeElement = document.activeElement;
        
        if (activeElement.id.startsWith('add-')) {
            performOperation('add');
        } else if (activeElement.id.startsWith('sub-')) {
            performOperation('subtract');
        } else if (activeElement.id.startsWith('mul-')) {
            performOperation('multiply');
        } else if (activeElement.id.startsWith('div-')) {
            performOperation('divide');
        } else if (activeElement.id.startsWith('pow-')) {
            performOperation('power');
        } else if (activeElement.id.startsWith('perc-')) {
            performOperation('percentage');
        } else if (activeElement.id.startsWith('sqrt-')) {
            performOperation('sqrt');
        }
    }
});
