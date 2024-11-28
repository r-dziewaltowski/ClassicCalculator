using System.Globalization;

namespace ClassicCalculator.CalculatorState
{
    public abstract class ValidStateBase(
            ICalculator calculator,
            double? firstOperand,
            OperationType? currentOperation,
            double? secondOperand,
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
            var result = Math.Sqrt(ConvertDisplayValueToNumber());
            if (double.IsNaN(result))
            {
                TransitionToInvalidState("Invalid input");
                return;
            }

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

        protected double ConvertDisplayValueToNumber()
        {
            var formattedDisplayValue = DisplayValue.EndsWith('.') ? DisplayValue[..^1] : DisplayValue;
            return double.Parse(formattedDisplayValue, CultureInfo.InvariantCulture);
        }

        private static double PerformOperation(double firstOperand, OperationType operation, double secondOperand)
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

        private static double CalculatePercentage(double firstOperand, OperationType operation, double secondOperand)
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

        private void UpdateDisplayValue(double value)
        {
            DisplayValue = value.ToString(CultureInfo.InvariantCulture);
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
