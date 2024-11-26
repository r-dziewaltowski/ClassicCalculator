namespace ClassicCalculator
{
    public class ValidStateBase(
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
                _firstOperand = 0;
                ResetDisplayValue();
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
            var result = CalculateSquareRoot(ConvertDisplayValueToNumber());
            if (double.IsNaN(result))
            {
                TransitionToInvalidState("Invalid input");
                return;
            }

            if (!OperandsAndOperationProvided())
            {
                _firstOperand = result;
            }
            else
            {
                _secondOperand = result;

            }

            UpdateDisplayValue(result);
            TransitionToOperandInputNotInProgressState();
        }

        private void SetOperationOrCalculate(OperationType? operation)
        {
            try
            {
                _firstOperand = OperandsAndOperationProvided() ?
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
