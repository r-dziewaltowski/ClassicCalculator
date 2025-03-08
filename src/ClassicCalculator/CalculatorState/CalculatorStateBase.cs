using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace ClassicCalculator.CalculatorState
{
    internal abstract class CalculatorStateBase
    {
        public string DisplayValue { get; set; }

        public abstract void AppendDigit(int digit);
        public abstract void AppendDecimal();
        public abstract void SetOperation(OperationType operation);
        public abstract void Calculate();
        public abstract void CalculatePercentage();
        public abstract void CalculateSquareRoot();
        public abstract void ToggleSign();

        protected readonly Calculator _calculator;
        protected decimal? _firstOperand;
        protected OperationType? _currentOperation;
        protected decimal? _secondOperand;

        public CalculatorStateBase(
            Calculator calculator,
            decimal? firstOperand,
            OperationType? currentOperation,
            decimal? secondOperand,
            string displayValue)
        {
            DisplayValue = displayValue;
            _calculator = calculator;
            _firstOperand = firstOperand;
            _currentOperation = currentOperation;
            _secondOperand = secondOperand;

            _calculator.Logger.LogDebug(
                "Calculator state initialized with first operand: {FirstOperand}, current operation: {CurrentOperation}, second operand: {SecondOperand}, display value: {DisplayValue}",
                _firstOperand,
                _currentOperation,
                _secondOperand,
                DisplayValue);
        }

        public void HandleButtonPressed(CalculatorButton button)
        {
            try
            {
                switch (button)
                {
                    case CalculatorButton.Zero:
                    case CalculatorButton.One:
                    case CalculatorButton.Two:
                    case CalculatorButton.Three:
                    case CalculatorButton.Four:
                    case CalculatorButton.Five:
                    case CalculatorButton.Six:
                    case CalculatorButton.Seven:
                    case CalculatorButton.Eight:
                    case CalculatorButton.Nine:
                        HandleDigit((int)button);
                        break;
                    case CalculatorButton.Decimal:
                        AppendDecimal();
                        break;
                    case CalculatorButton.Add:
                        SetOperation(OperationType.Add);
                        break;
                    case CalculatorButton.Subtract:
                        SetOperation(OperationType.Subtract);
                        break;
                    case CalculatorButton.Multiply:
                        SetOperation(OperationType.Multiply);
                        break;
                    case CalculatorButton.Divide:
                        SetOperation(OperationType.Divide);
                        break;
                    case CalculatorButton.Equals:
                        Calculate();
                        break;
                    case CalculatorButton.Clear:
                        Clear();
                        break;
                    case CalculatorButton.Percentage:
                        CalculatePercentage();
                        break;
                    case CalculatorButton.SquareRoot:
                        CalculateSquareRoot();
                        break;
                    case CalculatorButton.ToggleSign:
                        ToggleSign();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(button), button, "Invalid calculator button.");
                }
            }
            catch (OverflowException ex)
            {
                _calculator.Logger.LogWarning(ex, "Overflow occurred");
                SetInvalidState("Overflow");
            }
            //catch (DivideByZeroException ex)
            //{
            //    _calculator.Logger.LogError(ex, "Divide by zero occurred");
            //    _calculator.State = new InvalidState(_calculator, "Divide by zero");
            //}
            catch (Exception ex)
            {
                _calculator.Logger.LogError(ex, "Unexpected error occurred");
                SetInvalidState("Unexpected error");
            }
        }

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

        protected void SetInvalidState(string displayValue)
        {
            _calculator.State = new InvalidState(_calculator, displayValue);
        }

        private void HandleDigit(int number)
        {
            if (number < 0 || number > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(number), number, "Number must be between 0 and 9.");
            }

            AppendDigit(number);
        }
    }
}
