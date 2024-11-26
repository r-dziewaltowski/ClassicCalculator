using ClassicCalculator.CalculatorState;

namespace ClassicCalculator
{
    public interface ICalculator
    {
        string DisplayValue { get; }
        public ICalculatorState State { get; set; }

        void AppendDigit(int number);
        void AppendDecimal();
        void SetOperation(OperationType operation);
        void Calculate();
        void Clear();
        void CalculatePercentage();
        void CalculateSquareRoot();
        void ToggleSign(); 
    }
}
