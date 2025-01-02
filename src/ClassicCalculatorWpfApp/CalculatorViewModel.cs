using ClassicCalculator;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClassicCalculatorWpfApp
{
    public class CalculatorViewModel(ICalculator calculator) : ObservableObject
    {
        private readonly ICalculator _calculator = calculator;

        public string DisplayValue => _calculator.DisplayValue;

        public void PressButton(CalculatorButton button)
        {
            _calculator.PressButton(button);
            OnPropertyChanged(nameof(DisplayValue));
        }
    }
}
