using ClassicCalculator.CalculatorState;

namespace ClassicCalculator
{
    public interface ICalculator
    {
        string DisplayValue { get; }

        public void PressButton(CalculatorButton button);
    }
}
