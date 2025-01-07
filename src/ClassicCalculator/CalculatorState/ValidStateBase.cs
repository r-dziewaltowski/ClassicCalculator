using DecimalMath;
using System.Globalization;

namespace ClassicCalculator.CalculatorState
{
    public abstract class ValidStateBase(
            ICalculator calculator,
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
            if (!FirstOperandAndOperationProvided())
            {
                SetFirstOperand(0);
                DisplayValue = "0";
            }
            else
            {
                var result = CalculatePercentage(_firstOperand.Value, _currentOperation.Value, ConvertDisplayValueToNumber());
                _secondOperand = result;
                UpdateDisplayValue(result);
            }

            TransitionToOperandInputNotInProgressState();
        }

        public override void CalculateSquareRoot()
        {
            var operand = ConvertDisplayValueToNumber();
            if (operand < 0)
            {
                TransitionToInvalidState("Invalid input");
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

            UpdateDisplayValue(result);
            TransitionToOperandInputNotInProgressState();
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

        private void UpdateDisplayValue(decimal value)
        {
            DisplayValue = value.ToString("0.#############################", CultureInfo.InvariantCulture);
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
                TransitionToInvalidState("Cannot divide by 0");
                return;
            }

            if (operation != null)
            {
                _currentOperation = operation;
            }

            _secondOperand = null;
            UpdateDisplayValue(_firstOperand.Value);
            TransitionToOperandInputNotInProgressState();
        }

        private void TransitionToOperandInputNotInProgressState()
        {
            _calculator.State = new OperandInputNotInProgressState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        private void TransitionToInvalidState(string displayValue)
        {
            _calculator.State = new InvalidState(_calculator, displayValue);
        }
    }
}
