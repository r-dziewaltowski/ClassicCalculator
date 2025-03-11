namespace ClassicCalculator.CalculatorState
{
    internal class FirstOperandState(
        Calculator calculator,
        decimal firstOperand,
        string displayValue) :
        ValidStateBase(
            calculator,
            firstOperand,
            currentOperation: null,
            secondOperand: null,
            displayValue)
    {
        protected override void HandleDigit(int digit)
        {
            _calculator.State = new FirstOperandInputInProgressState(_calculator, digit.ToString());
        }

        protected override void HandleDecimal()
        {
            _calculator.State = new FirstOperandInputInProgressState(_calculator, "0.");
        }

        protected override void ToggleSign()
        {
            base.ToggleSign();

            _firstOperand = ConvertDisplayValueToNumber();
        }

        protected override void SetOperation(OperationType operation)
        {
            _calculator.State = new FirstOperandAndOperationState(
                _calculator, _firstOperand!.Value, operation, DisplayValue);
        }

        protected override void Calculate()
        {
            // Ignore
        }

        protected override void CalculatePercentage()
        {
            const int result = 0;
            _firstOperand = result;
            UpdateDisplayValue(result);
        }

        protected override void CalculateSquareRoot()
        {
            var result = CalculateSquareRoot(_firstOperand!.Value);
            _firstOperand = result;
        }
    }
}
