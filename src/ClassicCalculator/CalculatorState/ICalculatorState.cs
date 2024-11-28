namespace ClassicCalculator.CalculatorState
{
    public interface ICalculatorState
    {
        string DisplayValue { get; }

        void AppendDigit(int digit);
        void AppendDecimal();
        void SetOperation(OperationType operation);
        void Calculate();
        void Clear();
        void CalculatePercentage();
        void CalculateSquareRoot();
        void ToggleSign();
    }
}
