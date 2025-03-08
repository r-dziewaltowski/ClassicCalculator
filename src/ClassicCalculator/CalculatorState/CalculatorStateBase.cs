using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ClassicCalculator.CalculatorState
{
    internal abstract class CalculatorStateBase(
        Calculator calculator,
        decimal? firstOperand,
        OperationType? currentOperation,
        decimal? secondOperand,
        string displayValue) : ICalculatorState
    {
        protected readonly Calculator _calculator = calculator;
        protected decimal? _firstOperand = firstOperand;
        protected OperationType? _currentOperation = currentOperation;
        protected decimal? _secondOperand = secondOperand;

        public string DisplayValue { get; set; } = displayValue;

        public abstract void AppendDigit(int digit);
        public abstract void AppendDecimal();
        public abstract void SetOperation(OperationType operation);
        public abstract void Calculate();
        public abstract void CalculatePercentage();
        public abstract void CalculateSquareRoot();
        public abstract void ToggleSign();

        public void Clear()
        {
            _calculator.State = new InitialState(_calculator);
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

        [MemberNotNull(nameof(_firstOperand))]
        protected void SetFirstOperand(decimal value)
        {
            _firstOperand = value;
            _currentOperation = null;
            _secondOperand = null;
        }
    }
}
