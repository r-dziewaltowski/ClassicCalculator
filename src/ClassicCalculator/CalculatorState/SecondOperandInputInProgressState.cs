﻿namespace ClassicCalculator.CalculatorState
{
    internal class SecondOperandInputInProgressState : ValidStateBase
    {
        public SecondOperandInputInProgressState(
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
            AppendDigit(digit);
        }

        protected override void HandleDecimal()
        {
            AppendDecimal();
        }

        protected override void SetOperation(OperationType operation)
        {
            var secondOperand = ConvertDisplayValueToNumber();
            var result = PerformOperation(_firstOperand!.Value, _currentOperation!.Value, secondOperand);
            _calculator.State = new FirstOperandAndOperationState(
                _calculator, result, operation, DisplayValue);
        }

        protected override void Calculate()
        {
            var secondOperand = ConvertDisplayValueToNumber();
            var result = PerformOperation(_firstOperand!.Value, _currentOperation!.Value, secondOperand);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }

        protected override void CalculatePercentage()
        {
            var secondOperand = ConvertDisplayValueToNumber();
            var result = CalculatePercentage(_firstOperand!.Value, _currentOperation!.Value, secondOperand);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }

        protected override void CalculateSquareRoot()
        {
            var operand = ConvertDisplayValueToNumber();
            var result = CalculateSquareRoot(operand);
            _calculator.State = new BothOperandsAndOperationState(
                _calculator, _firstOperand!.Value, _currentOperation!.Value, result, DisplayValue);
        }
    }
}
