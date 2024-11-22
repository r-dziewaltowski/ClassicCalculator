namespace ClassicCalculator
{
    public interface ICalculator
    {
        string DisplayValue { get; }
        void AppendDigit(string digit);
        void AppendDecimal();
        void SetOperation(OperationType operation);
        void Calculate();
        void Clear();
        void CalculatePercentage();
        void CalculateSquareRoot();
        void ToggleSign();
    }
}
