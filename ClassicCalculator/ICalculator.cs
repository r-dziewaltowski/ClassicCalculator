using ClassicCalculator.CalculatorState;

namespace ClassicCalculator
{
    public interface ICalculator
    {
        string DisplayValue { get; }
        public ICalculatorState State { get; set; }

        public void PressButton(CalculatorButton button);
    }
}
