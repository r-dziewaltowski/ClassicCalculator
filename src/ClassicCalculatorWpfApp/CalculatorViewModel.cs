using ClassicCalculator;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ClassicCalculatorWpfApp
{
    public class CalculatorViewModel : ObservableObject
    {
        public string DisplayValue => _calculator.DisplayValue;

        public ICommand PressButtonCommand { get; }

        private readonly ICalculator _calculator;

        public CalculatorViewModel(ICalculator calculator)
        {
            _calculator = calculator;
            PressButtonCommand = new RelayCommand<CalculatorButton>(PressButton);
        }

        public void PressButton(CalculatorButton button)
        {
            _calculator.PressButton(button);
            OnPropertyChanged(nameof(DisplayValue));
        }
    }
}
