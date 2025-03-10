﻿namespace ClassicCalculator.CalculatorState
{
    internal class OperandInputInProgressState(
        Calculator calculator,
        decimal? firstOperand,
        OperationType? currentOperation,
        decimal? secondOperand,
        string displayValue)
        : ValidStateBase(
            calculator,
            firstOperand,
            currentOperation,
            secondOperand,
            displayValue)
    {
        public override void AppendDigit(int digit)
        {
            var numberOfDigits = GetNumberOfDigits(DisplayValue);
            if (numberOfDigits == _calculator.DisplayLength)
            {
                return;
            }

            if (DisplayValue == "0")
            {
                DisplayValue = digit.ToString();
            }
            else
            {
                DisplayValue += digit.ToString();
            }
        }

        public override void AppendDecimal()
        {
            if (!DisplayValue.Contains('.'))
            {
                DisplayValue += ".";
            }
        }

        public override void SetOperation(OperationType operation)
        {
            SetSecondOperandIfWaitingForIt();

            base.SetOperation(operation);
        }

        public override void Calculate()
        {
            SetSecondOperandIfWaitingForIt();

            base.Calculate();
        }

        private void SetSecondOperandIfWaitingForIt()
        {
            if (FirstOperandAndOperationProvided())
            {
                _secondOperand = ConvertDisplayValueToNumber();
            }
        }
    }
}
