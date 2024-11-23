using System;
using System.Windows;
using System.Windows.Input;
using ClassicCalculator;

namespace WpfCalculatorApp
{
    public partial class MainWindow : Window
    {
        private readonly Calculator calculator = new Calculator();

        public MainWindow()
        {
            InitializeComponent();
            Display.Text = calculator.DisplayValue;
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            calculator.AppendDigit(int.Parse(button?.Content.ToString()!));
            Display.Text = calculator.DisplayValue;
        }

        private void AppendDecimal_Click(object sender, RoutedEventArgs e)
        {
            calculator.AppendDecimal();
            Display.Text = calculator.DisplayValue;
        }

        private void SetOperation(OperationType operation)
        {
            calculator.SetOperation(operation);
            Display.Text = calculator.DisplayValue;
        }

        private void Calculate()
        {
            calculator.Calculate();
            Display.Text = calculator.DisplayValue;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            calculator.Clear();
            Display.Text = calculator.DisplayValue;
        }

        private void Percentage_Click(object sender, RoutedEventArgs e)
        {
            calculator.CalculatePercentage();
            Display.Text = calculator.DisplayValue;
        }

        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            calculator.CalculateSquareRoot();
            Display.Text = calculator.DisplayValue;
        }

        private void ToggleSign_Click(object sender, RoutedEventArgs e)
        {
            calculator.ToggleSign();
            Display.Text = calculator.DisplayValue;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(OperationType.Add);
        }

        private void Subtract_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(OperationType.Subtract);
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(OperationType.Multiply);
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(OperationType.Divide);
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
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



