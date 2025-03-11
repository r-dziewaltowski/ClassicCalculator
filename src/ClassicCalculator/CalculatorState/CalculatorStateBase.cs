using ClassicCalculator.CalculatorState.Exceptions;
using Microsoft.Extensions.Logging;
using System;

namespace ClassicCalculator.CalculatorState
{
    internal abstract class CalculatorStateBase
    {
        public string DisplayValue { get; set; }

        protected abstract void HandleDigit(int digit);
        protected abstract void HandleDecimal();
        protected abstract void SetOperation(OperationType operation);
        protected abstract void Calculate();
        protected abstract void CalculatePercentage();
        protected abstract void CalculateSquareRoot();
        protected abstract void ToggleSign();

        protected readonly Calculator _calculator;
        protected decimal? _firstOperand;
        protected OperationType? _currentOperation;
        protected decimal? _secondOperand;

        protected CalculatorStateBase(
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
                "Calculator state initialized with " +
                "first operand: {FirstOperand}, " +
                "current operation: {CurrentOperation}, " +
                "second operand: {SecondOperand}, " +
                "display value: {DisplayValue}",
                _firstOperand,
                _currentOperation,
                _secondOperand,
                DisplayValue);
        }

        public void HandleButtonPressed(CalculatorButton button)
        {
            try
            {
                HandleButton(button);
            }
            catch (DisplayLengthExceededException ex)
            {
                HandleException(LogLevel.Warning, ex, "Display length exceeded", "Display length exceeded");
            }
            catch (InvalidInputException ex)
            {
                HandleException(LogLevel.Warning, ex, "Invalid input provided", "Invalid input");
            }
            catch (DivideByZeroException ex)
            {
                HandleException(LogLevel.Warning, ex, "Divide by 0 occurred", "Divide by 0");
            }
            catch (OverflowException ex)
            {
                HandleException(LogLevel.Warning, ex, "Overflow occurred", "Overflow");
            }
            catch (Exception ex)
            {
                HandleException(LogLevel.Error, ex, "Unexpected error occurred", "Unexpected error");
            }
        }

        private void HandleButton(CalculatorButton button)
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
                    ValidateDigit((int)button);
                    break;
                case CalculatorButton.Decimal:
                    HandleDecimal();
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
                    throw new ArgumentOutOfRangeException(nameof(button), button, "Invalid calculator button");
            }
        }

        private void Clear()
        {
            _calculator.State = new InitialState(_calculator);
        }

        private void HandleException(LogLevel logLevel, Exception ex, string logMessage, string displayValue)
        {
            _calculator.Logger.Log(logLevel, ex, logMessage);
            _calculator.State = new InvalidState(_calculator, displayValue);
        }

        private void ValidateDigit(int number)
        {
            if (number < 0 || number > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(number), number, "Number must be between 0 and 9.");
            }

            HandleDigit(number);
        }
    }
}
