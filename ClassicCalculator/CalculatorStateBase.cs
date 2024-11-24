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

        public void ToggleSign()
        {
            if (DisplayValue == "0")
            {
                return;
            }

            DisplayValue = DisplayValue.StartsWith('-') ?
                DisplayValue.Substring(1) :
                "-" + DisplayValue;
        }

        protected void ResetDisplayValue()
        {
            DisplayValue = "0";
        }

        protected double ConvertDisplayValueToNumber()
        {
            return double.Parse(DisplayValue);
        }

        protected static double CalculateSquareRoot(double value)
        {
            return Math.Sqrt(value);
        }

        protected void UpdateDisplayValue(double value)
        {
            DisplayValue = value.ToString();
        }
    }
}
