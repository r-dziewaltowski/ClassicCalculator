namespace ClassicCalculator.CalculatorState
{
    internal class FirstOperandInputInProgressState : ValidStateBase
    {
        public FirstOperandInputInProgressState(
            Calculator calculator,
            string displayValue) : base(
                calculator,
                firstOperand : null,
                currentOperation: null,
                secondOperand: null,
                displayValue)
        {
        }

        protected override void HandleDigit(int digit)
        {
            AppendDigit(digit);
        }

        protected override void HandleDecimal()
        {
            AppendDecimal();
        }

        protected override void SetOperation(OperationType operation)
        {
            var firstOperand = ConvertDisplayValueToNumber();
            UpdateDisplayValue(firstOperand);
            _calculator.State = new FirstOperandAndOperationState(
                _calculator, firstOperand, operation, DisplayValue);
        }

        protected override void Calculate()
        {
            var firstOperand = ConvertDisplayValueToNumber();
            UpdateDisplayValue(firstOperand);
            _calculator.State = new FirstOperandState(_calculator, firstOperand, DisplayValue);
        }

        protected override void CalculatePercentage()
        {
            const int result = 0;
            UpdateDisplayValue(result);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }

        protected override void CalculateSquareRoot()
        {
            var operand = ConvertDisplayValueToNumber();
            var result = CalculateSquareRoot(operand);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }
    }
}
