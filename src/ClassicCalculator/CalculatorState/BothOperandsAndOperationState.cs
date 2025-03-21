﻿namespace ClassicCalculator.CalculatorState
{
    internal class BothOperandsAndOperationState : ValidStateBase
    {
        public BothOperandsAndOperationState(
            Calculator calculator,
            decimal firstOperand,
            OperationType operation,
            decimal secondOperand,
            string displayValue) : base(
                calculator,
                firstOperand,
                operation,
                secondOperand,
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

            _secondOperand = ConvertDisplayValueToNumber();
        }

        protected override void SetOperation(OperationType operation)
        {
            var result = PerformOperation(_firstOperand!.Value, _currentOperation!.Value, _secondOperand!.Value);
            _calculator.State = new FirstOperandAndOperationState(_calculator, result, operation, DisplayValue);
        }

        protected override void Calculate()
        {
            var result = PerformOperation(_firstOperand!.Value, _currentOperation!.Value, _secondOperand!.Value);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }

        protected override void CalculatePercentage()
        {
            var result = CalculatePercentage(_firstOperand!.Value, _currentOperation!.Value, _secondOperand!.Value);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }

        protected override void CalculateSquareRoot()
        {
            var result = CalculateSquareRoot(_secondOperand!.Value);
            _secondOperand = result;
        }
    }
}
