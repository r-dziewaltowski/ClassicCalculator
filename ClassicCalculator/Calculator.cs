namespace ClassicCalculator;

public class Calculator
{
    private const string zeroDigit = "0";
    private string displayValue = zeroDigit;
    private double currentNumber;
    private double previousNumber;
    private OperationType? currentOperation;
    private CalculatorState state = CalculatorState.Ready;

    public string DisplayValue => displayValue;

    public void AppendDigit(string digit)
    {
        switch (state)
        {
            case CalculatorState.Ready:
            case CalculatorState.NewEntry:
                displayValue = digit;
                state = CalculatorState.EnteredNumber;
                break;
            case CalculatorState.EnteredNumber:
                if (displayValue == zeroDigit)
                {
                    displayValue = digit;
                }
                else
                {
                    displayValue += digit;
                }
                break;
        }
        currentNumber = double.Parse(displayValue);
    }

    public void AppendDecimal()
    {
        switch (state)
        {
            case CalculatorState.Ready:
            case CalculatorState.NewEntry:
                displayValue = "0.";
                state = CalculatorState.EnteredNumber;
                break;
            case CalculatorState.EnteredNumber:
                if (!displayValue.Contains("."))
                {
                    displayValue += ".";
                }
                break;
        }
    }

    public void SetOperation(OperationType operation)
    {
        switch (state)
        {
            case CalculatorState.Ready:
                previousNumber = currentNumber;
                currentOperation = operation;
                state = CalculatorState.NewEntry;
                break;
            case CalculatorState.EnteredNumber:
                if (currentOperation.HasValue)
                {
                    Calculate();
                }
                previousNumber = currentNumber;
                currentOperation = operation;
                state = CalculatorState.NewEntry;
                break;
            case CalculatorState.NewEntry:
                currentOperation = operation;
                break;
        }
        if (displayValue.EndsWith("."))
        {
            displayValue = displayValue.TrimEnd('.');
        }
    }

    public void Calculate()
    {
        if (currentOperation.HasValue)
        {
            try
            {
                currentNumber = PerformOperation(previousNumber, currentNumber, currentOperation.Value);
                displayValue = currentNumber.ToString();
            }
            catch (DivideByZeroException)
            {
                displayValue = "Cannot divide by zero";
            }
            currentOperation = null;
            state = CalculatorState.Ready;
        }
    }

    public void Clear()
    {
        displayValue = zeroDigit;
        currentNumber = 0;
        previousNumber = 0;
        currentOperation = null;
        state = CalculatorState.Ready;
    }

    public void CalculatePercentage()
    {
        currentNumber = currentNumber / 100;
        displayValue = currentNumber.ToString();
        state = CalculatorState.Ready;
    }

    public void CalculateSquareRoot()
    {
        currentNumber = Math.Sqrt(currentNumber);
        displayValue = currentNumber.ToString();
        state = CalculatorState.Ready;
    }

    public void ToggleSign()
    {
        currentNumber = -currentNumber;
        displayValue = currentNumber.ToString();
    }

    private double PerformOperation(double left, double right, OperationType operation)
    {
        return operation switch
        {
            OperationType.Add => left + right,
            OperationType.Subtract => left - right,
            OperationType.Multiply => left * right,
            OperationType.Divide => right == 0 ? throw new DivideByZeroException() : left / right,
            _ => throw new InvalidOperationException("Invalid operation"),
        };
    }

    private enum CalculatorState
    {
        Ready,
        EnteredNumber,
        NewEntry
    }
}
