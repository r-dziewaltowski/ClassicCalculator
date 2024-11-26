using System.Diagnostics.CodeAnalysis;

namespace ClassicCalculator
{
    public class OperandInputInProgressState(
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
        public override void AppendDigit(int digit)
        {
            if (DisplayValue == "0")
            {
                DisplayValue = digit.ToString();
            }
            else
            {
                DisplayValue += digit.ToString();
            }
        }

        public override void AppendDecimal()
        {
            if (!DisplayValue.Contains('.'))
            {
                DisplayValue += ".";
            }
        }

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
                ResetDisplayValue();
            }
            else
            {
                var result = CalculatePercentage(_firstOperand.Value, _currentOperation.Value, ConvertDisplayValueToNumber());
                UpdateDisplayValue(result);
            }

            TransitionToOperandInputNotInProgressState();
        }

        public override void CalculateSquareRoot()
        {
            var result = CalculateSquareRoot(ConvertDisplayValueToNumber());
            if (double.IsNaN(result))
            {
                TransitionToInvalidState("Invalid input");
            }
            else
            {
                UpdateDisplayValue(result);
                TransitionToOperandInputNotInProgressState();
            }
        }

        private void SetOperationOrCalculate(OperationType? operation)
        {
            try
            {
                _firstOperand = FirstOperandAndOperationProvided() ?
                    PerformOperation(_firstOperand.Value, _currentOperation.Value, ConvertDisplayValueToNumber()) :
                    ConvertDisplayValueToNumber();
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

            UpdateDisplayValue(_firstOperand.Value);
            TransitionToOperandInputNotInProgressState();
        }

        private void TransitionToOperandInputNotInProgressState()
        {
            _calculator.State = new OperandInputNotInProgress(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        private void TransitionToInvalidState(string displayValue)
        {
            _calculator.State = new InvalidState(_calculator, displayValue);
        }
    }
}
