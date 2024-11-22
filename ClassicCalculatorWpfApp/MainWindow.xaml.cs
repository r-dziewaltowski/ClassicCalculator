using System;
using System.Windows;
using ClassicCalculator;

namespace WpfCalculatorApp
{
    public partial class MainWindow : Window
    {
        private const string zeroDigit = "0";
        private string displayValue = zeroDigit;
        private double currentNumber;
        private double previousNumber;
        private double memory;
        private OperationType? currentOperation;
        private readonly Calculator calculator = new Calculator();

        public MainWindow()
        {
            InitializeComponent();
            Display.Text = displayValue;
        }

        private void AppendDigit(string digit)
        {
            if (displayValue == zeroDigit)
            {
                displayValue = digit;
            }
            else
            {
                displayValue += digit;
            }
            currentNumber = double.Parse(displayValue);
            Display.Text = displayValue;
        }

        private void SetOperation(OperationType operation)
        {
            if (currentOperation.HasValue)
            {
                Calculate();
            }
            previousNumber = currentNumber;
            currentOperation = operation;
            displayValue = zeroDigit;
            Display.Text = currentNumber.ToString();
        }

        private void Calculate()
        {
            if (currentOperation.HasValue)
            {
                currentNumber = calculator.PerformOperation(previousNumber, currentNumber, currentOperation.Value);
                displayValue = currentNumber.ToString();
                currentOperation = null;
                Display.Text = displayValue;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            displayValue = zeroDigit;
            currentNumber = 0;
            previousNumber = 0;
            currentOperation = null;
            Display.Text = displayValue;
        }

        private void MemoryClear_Click(object sender, RoutedEventArgs e)
        {
            memory = 0;
        }

        private void MemoryRecall_Click(object sender, RoutedEventArgs e)
        {
            displayValue = memory.ToString();
            currentNumber = memory;
            Display.Text = displayValue;
        }

        private void MemoryAdd_Click(object sender, RoutedEventArgs e)
        {
            memory += currentNumber;
        }

        private void MemorySubtract_Click(object sender, RoutedEventArgs e)
        {
            memory -= currentNumber;
        }

        private void Percentage_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = currentNumber / 100;
            displayValue = currentNumber.ToString();
            Display.Text = displayValue;
        }

        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = Math.Sqrt(currentNumber);
            displayValue = currentNumber.ToString();
            Display.Text = displayValue;
        }

        private void ToggleSign_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = -currentNumber;
            displayValue = currentNumber.ToString();
            Display.Text = displayValue;
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            AppendDigit(button.Content.ToString());
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
    }
}
