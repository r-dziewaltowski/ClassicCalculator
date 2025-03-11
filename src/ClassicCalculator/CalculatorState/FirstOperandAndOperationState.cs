namespace ClassicCalculator.CalculatorState
{
    internal class FirstOperandAndOperationState : ValidStateBase
    {
        public FirstOperandAndOperationState(
            Calculator calculator,
            decimal firstOperand,
            OperationType operation,
            string displayValue) : base(
                calculator,
                firstOperand,
                operation,
                secondOperand: null,
                displayValue)
        {
        }

        protected override void HandleDigit(int digit)
        {
            _calculator.State = new SecondOperandInputInProgressState(
                _calculator, _firstOperand!.Value, _currentOperation!.Value, digit.ToString());
        }

        protected override void HandleDecimal()
        {
            _calculator.State = new SecondOperandInputInProgressState(
                _calculator, _firstOperand!.Value, _currentOperation!.Value, "0.");
        }

        protected override void ToggleSign()
        {
            base.ToggleSign();

            var secondOperand = ConvertDisplayValueToNumber();
            _calculator.State = new BothOperandsAndOperationState(
                _calculator, _firstOperand!.Value, _currentOperation!.Value, secondOperand, DisplayValue);
        }

        protected override void SetOperation(OperationType operation)
        {
            _currentOperation = operation;
        }

        protected override void Calculate()
        {
            var result = PerformOperation(_firstOperand!.Value, _currentOperation!.Value, _firstOperand!.Value);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }

        protected override void CalculatePercentage()
        {
            var result = CalculatePercentage(_firstOperand!.Value, _currentOperation!.Value, _firstOperand!.Value);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }

        protected override void CalculateSquareRoot()
        {
            var result = CalculateSquareRoot(_firstOperand!.Value);
            _calculator.State = new BothOperandsAndOperationState(
                _calculator, _firstOperand!.Value, _currentOperation!.Value, result, DisplayValue);
        }
    }
}
