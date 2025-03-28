﻿namespace ClassicCalculator.CalculatorState
{
    internal class InvalidState : CalculatorStateBase
    {
        public InvalidState(Calculator calculator, string displayValue) : base(
            calculator,
            firstOperand: null,
            currentOperation: null,
            secondOperand: null,
            displayValue)
        {
        }

        protected override void HandleDecimal()
        {
            // Ignore
        }

        protected override void HandleDigit(int digit)
        {
            // Ignore
        }

        protected override void Calculate()
        {
            // Ignore
        }

        protected override void CalculatePercentage()
        {
            // Ignore
        }

        protected override void CalculateSquareRoot()
        {
            // Ignore
        }

        protected override void SetOperation(OperationType operation)
        {
            // Ignore
        }

        protected override void ToggleSign()
        {
            // Ignore
        }
    }
}
