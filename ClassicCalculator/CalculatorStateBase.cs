namespace ClassicCalculator
{
    public abstract class CalculatorStateBase(
        ICalculator calculator,
        double? firstOperand,
        OperationType? currentOperation,
        double? secondOperand,
        string displayValue) : ICalculatorState
    {
        protected readonly ICalculator _calculator = calculator;
        protected double? _firstOperand = firstOperand;
        protected OperationType? _currentOperation = currentOperation;
        protected double? _secondOperand = secondOperand;

        public string DisplayValue { get; set; } = displayValue;

        public virtual void AppendDecimal()
        {
        }

        public virtual void AppendDigit(int digit)
        {
        }

        public virtual void Calculate()
        {
        }

        public virtual void CalculatePercentage()
        {
        }

        public virtual void CalculateSquareRoot()
        {
        }

        public void Clear()
        {
            ResetDisplayValue();
            _calculator.State = new InitialState(_calculator);
        }

        public virtual void SetOperation(OperationType operation)
        {
        }

        public virtual void ToggleSign()
        {
        }

        protected void ResetDisplayValue()
        {
            DisplayValue = "0";
        }
    }
}
