namespace ClassicCalculator.CalculatorState
{
    internal class InvalidState(Calculator calculator, string displayValue)
        : CalculatorStateBase(
            calculator,
            firstOperand: null,
            currentOperation: null,
            secondOperand: null,
            displayValue)
    {
        protected override void AppendDecimal()
        {
            // Ignore
        }

        protected override void AppendDigit(int digit)
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
