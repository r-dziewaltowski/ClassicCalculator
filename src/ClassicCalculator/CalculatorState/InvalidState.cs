namespace ClassicCalculator.CalculatorState
{
    public class InvalidState(Calculator calculator, string displayValue)
        : CalculatorStateBase(
            calculator,
            null,
            null,
            null,
            displayValue)
    {
        public override void AppendDecimal()
        {
        }

        public override void AppendDigit(int digit)
        {
        }

        public override void Calculate()
        {
        }

        public override void CalculatePercentage()
        {
        }

        public override void CalculateSquareRoot()
        {
        }

        public override void SetOperation(OperationType operation)
        {
        }

        public override void ToggleSign()
        {
        }
    }
}
