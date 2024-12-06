using System.Windows;
using System.Windows.Input;
using ClassicCalculator;

namespace ClassicCalculatorWpfApp
{
    public partial class MainWindow : Window
    {
        private readonly CalculatorViewModel _calculatorViewModel;

        public MainWindow(CalculatorViewModel calculator)
        {
            InitializeComponent();
            _calculatorViewModel = calculator;
            Display.Text = _calculatorViewModel.DisplayValue;
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var digit = int.Parse(button?.Content.ToString()!);
            PressButton((CalculatorButton)digit);
        }

        private void AppendDecimal_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.Decimal);
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.Equals);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.Clear);
        }

        private void Percentage_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.Percentage);
        }

        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.SquareRoot);
        }

        private void ToggleSign_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.ToggleSign);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.Add);
        }

        private void Subtract_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.Subtract);
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.Multiply);
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            PressButton(CalculatorButton.Divide);
        }

        private void PressButton(CalculatorButton button)
        {
            _calculatorViewModel.PressButton(button);
            Display.Text = _calculatorViewModel.DisplayValue;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}



