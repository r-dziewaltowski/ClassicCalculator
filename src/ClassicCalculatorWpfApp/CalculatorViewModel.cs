using ClassicCalculator;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassicCalculatorWpfApp
{
    public partial class CalculatorViewModel(ICalculator calculator) : ObservableObject
    {
        public string DisplayValue => _calculator.DisplayValue;


        private readonly ICalculator _calculator = calculator;

        [RelayCommand]
        private void PressButton(CalculatorButton button)
        {
            _calculator.PressButton(button);
            OnPropertyChanged(nameof(DisplayValue));
        }
    }
}
