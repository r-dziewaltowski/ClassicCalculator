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
        public override void SetOperation(OperationType operation)
        {
            SetOperationOrCalculate(operation);
        }

        public override void Calculate()
        {
            SetOperationOrCalculate(operation: null);
        }

        public override void CalculatePercentage()
        {
            decimal result = 0;
            if (!FirstOperandAndOperationProvided())
            {
                SetFirstOperand(result);
            }
            else
            {
                result = CalculatePercentage(_firstOperand.Value, _currentOperation.Value, ConvertDisplayValueToNumber());
                _secondOperand = result;
            }

            UpdateDisplayValueAndSetOperandInputNotInProgressState(result);
        }

        public override void CalculateSquareRoot()
        {
            var operand = ConvertDisplayValueToNumber();
            if (operand < 0)
            {
                SetInvalidState("Invalid input");
                return;
            }

            var result = DecimalEx.Sqrt(operand);
            if (!FirstOperandAndOperationProvided())
            {
                SetFirstOperand(result);
            }
            else
            {
                _secondOperand = result;

            }

            UpdateDisplayValueAndSetOperandInputNotInProgressState(result);
        }

        public override void ToggleSign()
        {
            if (ConvertDisplayValueToNumber() == 0)
            {
                return;
            }

            DisplayValue = DisplayValue.StartsWith('-') ?
                DisplayValue[1..] :
                "-" + DisplayValue;
        }

        protected decimal ConvertDisplayValueToNumber()
        {
            var formattedDisplayValue = DisplayValue.EndsWith('.') ? DisplayValue[..^1] : DisplayValue;
            return decimal.Parse(formattedDisplayValue, CultureInfo.InvariantCulture);
        }

        protected static int GetNumberOfDigits(string value)
        {
            var numberOfDigits = value.Count(char.IsDigit);
            return numberOfDigits;
        }

        private static decimal PerformOperation(decimal firstOperand, OperationType operation, decimal secondOperand)
        {
            return operation switch
            {
                OperationType.Add => firstOperand + secondOperand,
                OperationType.Subtract => firstOperand - secondOperand,
                OperationType.Multiply => firstOperand * secondOperand,
                OperationType.Divide => secondOperand != 0 ? firstOperand / secondOperand : throw new DivideByZeroException(),
                _ => throw new InvalidOperationException("Invalid operation type")
            };
        }

        private static decimal CalculatePercentage(decimal firstOperand, OperationType operation, decimal secondOperand)
        {
            var percentage = secondOperand / 100;
            return operation switch
            {
                OperationType.Add => firstOperand + firstOperand * percentage,
                OperationType.Subtract => firstOperand - firstOperand * percentage,
                OperationType.Multiply => firstOperand * percentage,
                OperationType.Divide => firstOperand / percentage,
                _ => throw new InvalidOperationException("Invalid operation type"),
            };
        }

        private void UpdateDisplayValueAndSetOperandInputNotInProgressState(decimal value)
        {
            var newValue = value.ToString("0.#############################", CultureInfo.InvariantCulture);
            var numberOfDigits = GetNumberOfDigits(newValue);

            if (numberOfDigits > _calculator.DisplayLength)
            {
                SetInvalidState("Number exceeds the display length");
                return;
            }

            DisplayValue = newValue;
            _calculator.State = new OperandInputNotInProgressState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        private void SetOperationOrCalculate(OperationType? operation)
        {
            try
            {
                var value = OperandsAndOperationProvided() ?
                    PerformOperation(_firstOperand.Value, _currentOperation.Value, ConvertDisplayValueToNumber()) :
                    ConvertDisplayValueToNumber();
                SetFirstOperand(value);
            }
            catch (DivideByZeroException)
            {
                SetInvalidState("Cannot divide by 0");
                return;
            }

            if (operation != null)
            {
                _currentOperation = operation;
            }

            _secondOperand = null;
            UpdateDisplayValueAndSetOperandInputNotInProgressState(_firstOperand.Value);
        }

        private void SetInvalidState(string displayValue)
        {
            _calculator.State = new InvalidState(_calculator, displayValue);
        }
    }
}
