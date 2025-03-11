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
            var parts = newValue.Split('.'); // Split by the decimal point
            var numberOfIntegerDigits = GetNumberOfDigits(parts[0]); // Count the number of digits before the decimal point

            // Check if the number of digits before the decimal point exceeds the display length
            if (numberOfIntegerDigits > _calculator.DisplayLength)
            {
                throw new DisplayLengthExceededException($"Value {newValue} exceeds display length");
            }

            // If there is a fractional part, we may have to trim it to fit the display length
            if (parts.Length == 2)
            {
                // Calculate the number of digits that can be displayed after the decimal point
                var remainingLength = _calculator.DisplayLength - numberOfIntegerDigits;

                // Trim the fractional part to fit the display length if necessary
                var fractionalPart = parts[1];
                var fractionalPartTrimmed = fractionalPart.Length > remainingLength
                    ? fractionalPart[..remainingLength]
                    : fractionalPart;

                // If the trimmed fractional part is not zero, include it in the display value
                if (fractionalPartTrimmed.Length > 0 && fractionalPartTrimmed.Any(digit => digit != '0'))
                {
                    newValue = $"{parts[0]}.{fractionalPartTrimmed}";
                }
                // If the trimmed fractional part is zero, exclude it from the display value
                else
                {
                    newValue = parts[0];
                }
            } 

            DisplayValue = newValue;
        }

        protected static int GetNumberOfDigits(string value)
        {
            var numberOfDigits = value.Count(char.IsDigit);
            return numberOfDigits;
        }

        protected decimal PerformOperation(decimal firstOperand, OperationType operation, decimal secondOperand)
        {
            var result = operation switch
            {
                OperationType.Add => firstOperand + secondOperand,
                OperationType.Subtract => firstOperand - secondOperand,
                OperationType.Multiply => firstOperand * secondOperand,
                OperationType.Divide => secondOperand != 0 ? firstOperand / secondOperand : throw new DivideByZeroException(),
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, "Invalid operation")
            };

            return UpdateDisplayValueAndGetAdjustedResult(result);
        }

        protected decimal CalculatePercentage(decimal firstOperand, OperationType operation, decimal secondOperand)
        {
            var percentage = secondOperand / 100;
            var result = operation switch
            {
                OperationType.Add => firstOperand + firstOperand * percentage,
                OperationType.Subtract => firstOperand - firstOperand * percentage,
                OperationType.Multiply => firstOperand * percentage,
                OperationType.Divide => firstOperand / percentage,
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, "Invalid operation")
            };

            return UpdateDisplayValueAndGetAdjustedResult(result);
        }

        protected decimal CalculateSquareRoot(decimal value)
        {
            if (value < 0)
            {
                throw new InvalidInputException($"Square root of a negative number {value} is not a valid operation");
            }

            var result = DecimalEx.Sqrt(value);
            return UpdateDisplayValueAndGetAdjustedResult(result);
        }

        private decimal UpdateDisplayValueAndGetAdjustedResult(decimal result)
        {
            UpdateDisplayValue(result);

            // Some results may have to be adjusted to fit the display length
            var adjustedResult = ConvertDisplayValueToNumber();
            return adjustedResult;
        }
    }
}
