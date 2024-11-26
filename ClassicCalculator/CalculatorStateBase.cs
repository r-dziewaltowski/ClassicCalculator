using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ClassicCalculator
{
    public abstract class CalculatorStateBase(
        ICalculator calculator,
        double? firstOperand,
        OperationType? currentOperation,
        double? secondOperand,
        string displayValue) : ICalculatorState
    {
        protected readonly ICalculator _calculator = calculator;
        protected double? _firstOperand = firstOperand;
        protected OperationType? _currentOperation = currentOperation;
        protected double? _secondOperand = secondOperand;

        public string DisplayValue { get; set; } = displayValue;

        public virtual void AppendDecimal()
        {
        }

        public virtual void AppendDigit(int digit)
        {
        }

        public virtual void Calculate()
        {
        }

        public virtual void CalculatePercentage()
        {
        }

        public virtual void CalculateSquareRoot()
        {
        }

        public void Clear()
        {
            _calculator.State = new InitialState(_calculator);
        }

        public virtual void SetOperation(OperationType operation)
        {
        }

        public virtual void ToggleSign()
        {
        }

        protected static double PerformOperation(double firstOperand, OperationType operation, double secondOperand)
        {
            return operation switch
            {
                OperationType.Add => firstOperand + secondOperand,
                OperationType.Subtract => firstOperand - secondOperand,
                OperationType.Multiply => firstOperand * secondOperand,
                OperationType.Divide => secondOperand != 0 ? firstOperand / secondOperand : throw new DivideByZeroException(),
                _ => throw new InvalidOperationException("Invalid operation type")
            };
        }

        protected static double CalculatePercentage(double firstOperand, OperationType operation, double secondOperand)
        {
            var percentage = secondOperand / 100;
            return operation switch
            {
                OperationType.Add => firstOperand + firstOperand * percentage,
                OperationType.Subtract => firstOperand - firstOperand * percentage,
                OperationType.Multiply => firstOperand * percentage,
                OperationType.Divide => firstOperand / percentage,
                _ => throw new InvalidOperationException("Invalid operation type"),
            };
        }

        protected static double CalculateSquareRoot(double value)
        {
            return Math.Sqrt(value);
        }

        protected void ResetDisplayValue()
        {
            DisplayValue = "0";
        }

        protected double ConvertDisplayValueToNumber()
        {
            var formattedDisplayValue = DisplayValue.EndsWith('.') ? DisplayValue[..^1] : DisplayValue;
            return double.Parse(formattedDisplayValue, CultureInfo.InvariantCulture);
        }

        protected void UpdateDisplayValue(double value)
        {
            DisplayValue = value.ToString(CultureInfo.InvariantCulture);
        }

        [MemberNotNullWhen(true, nameof(_firstOperand), nameof(_currentOperation))]
        protected bool FirstOperandAndOperationProvided()
        {
            return _firstOperand != null && _currentOperation != null;
        }

        [MemberNotNullWhen(true, nameof(_firstOperand), nameof(_currentOperation), nameof(_secondOperand))]
        protected bool OperandsAndOperationProvided()
        {
            return FirstOperandAndOperationProvided() && _secondOperand != null;
        }
    }
}
