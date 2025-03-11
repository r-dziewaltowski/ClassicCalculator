namespace ClassicCalculator.CalculatorState
{
    internal class FirstOperandInputInProgressState(
        Calculator calculator,
        string displayValue) :
        ValidStateBase(
            calculator,
            firstOperand : null,
            currentOperation: null,
            secondOperand: null,
            displayValue)
    {
        protected override void AppendDigit(int digit)
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

        protected override void AppendDecimal()
        {
            if (!DisplayValue.Contains('.'))
            {
                DisplayValue += ".";
            }
        }

        protected override void SetOperation(OperationType operation)
        {
            var firstOperand = ConvertDisplayValueToNumber();
            UpdateDisplayValue(firstOperand);
            _calculator.State = new FirstOperandAndOperationState(
                _calculator, firstOperand, operation, DisplayValue);
        }

        protected override void Calculate()
        {
            var firstOperand = ConvertDisplayValueToNumber();
            UpdateDisplayValue(firstOperand);
            _calculator.State = new FirstOperandState(_calculator, firstOperand, DisplayValue);
        }

        protected override void CalculatePercentage()
        {
            const int result = 0;
            UpdateDisplayValue(result);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }

        protected override void CalculateSquareRoot()
        {
            var operand = ConvertDisplayValueToNumber();
            var result = CalculateSquareRoot(operand);
            UpdateDisplayValue(result);
            _calculator.State = new FirstOperandState(_calculator, result, DisplayValue);
        }
    }
}
