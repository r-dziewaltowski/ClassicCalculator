namespace ClassicCalculator.CalculatorState
{
    internal interface ICalculatorState
    {
        string DisplayValue { get; }

        void HandleButtonPressed(CalculatorButton button);
    }
}
