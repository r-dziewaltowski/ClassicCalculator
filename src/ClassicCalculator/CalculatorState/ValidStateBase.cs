using ClassicCalculator.CalculatorState.Exceptions;
using DecimalMath;
using System.Globalization;

namespace ClassicCalculator.CalculatorState
{
    internal abstract class ValidStateBase(
        Calculator calculator,
        decimal? firstOperand,
        OperationType? currentOperation,
        decimal? secondOperand,
        string displayValue)
        : CalculatorStateBase(
            calculator,
            firstOperand,
            currentOperation,
            secondOperand,
            displayValue)
    {
        protected override void ToggleSign()
        {
            DisplayValue = DisplayValue.StartsWith('-') ?
                DisplayValue[1..] :
                "-" + DisplayValue;
        }

        protected decimal ConvertDisplayValueToNumber()
        {
            var formattedDisplayValue = DisplayValue.EndsWith('.') ? DisplayValue[..^1] : DisplayValue;
            return decimal.Parse(formattedDisplayValue, CultureInfo.InvariantCulture);
        }

        protected void UpdateDisplayValue(decimal value)
        {
            var newValue = value.ToString("0.#############################", CultureInfo.InvariantCulture);
            var numberOfDigits = GetNumberOfDigits(newValue);

            if (numberOfDigits > _calculator.DisplayLength)
            {
                throw new DisplayLengthExceededException($"Value {newValue} exceeds display length");
            }

            DisplayValue = newValue;
        }

        protected static int GetNumberOfDigits(string value)
        {
            var numberOfDigits = value.Count(char.IsDigit);
            return numberOfDigits;
        }

        protected static decimal PerformOperation(decimal firstOperand, OperationType operation, decimal secondOperand)
        {
            return operation switch
            {
                OperationType.Add => firstOperand + secondOperand,
                OperationType.Subtract => firstOperand - secondOperand,
                OperationType.Multiply => firstOperand * secondOperand,
                OperationType.Divide => secondOperand != 0 ? firstOperand / secondOperand : throw new DivideByZeroException(),
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, "Invalid operation")
            };
        }

        protected static decimal CalculatePercentage(decimal firstOperand, OperationType operation, decimal secondOperand)
        {
            var percentage = secondOperand / 100;
            return operation switch
            {
                OperationType.Add => firstOperand + firstOperand * percentage,
                OperationType.Subtract => firstOperand - firstOperand * percentage,
                OperationType.Multiply => firstOperand * percentage,
                OperationType.Divide => firstOperand / percentage,
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, "Invalid operation")
            };
        }

        protected static decimal CalculateSquareRoot(decimal value)
        {
            if (value < 0)
            {
                throw new InvalidInputException($"Square root of a negative number {value} is not a valid operation");
            }

            var result = DecimalEx.Sqrt(value);
            return result;
        }
    }
}
