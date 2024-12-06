using ClassicCalculator;

namespace ClassicCalculatorWpfApp
{
    public class CalculatorViewModel(ICalculator calculator)
    {
        private readonly ICalculator _calculator = calculator;

        public string DisplayValue => _calculator.DisplayValue;

        public void PressButton(CalculatorButton button)
        {
            _calculator.PressButton(button);
        }
    }
}
