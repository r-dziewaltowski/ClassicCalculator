namespace ClassicCalculator
{
    public class FirstOperandBeingEnteredState(
        ICalculator calculator,
        double? firstOperand,
        OperationType? currentOperation,
        double? secondOperand,  
        string displayValue) : 
        CalculatorStateBase(
            calculator, 
            firstOperand,
            currentOperation,
            secondOperand,  
            displayValue)
    {
        public override void AppendDigit(int digit)
        {
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
            _firstOperand = ConvertDisplayValueToNumber();
            _currentOperation = operation;
            _calculator.State = new ReadyForSecondOperandState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        public override void Calculate()
        {
            _calculator.State = new ReadyForFirstOperandState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        public override void CalculatePercentage()
        {
            ResetDisplayValue();
            _calculator.State = new ReadyForFirstOperandState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        public override void CalculateSquareRoot()
        {
            _firstOperand = ConvertDisplayValueToNumber();
            var result = CalculateSquareRoot(_firstOperand.Value);
            UpdateDisplayValue(result);
            _calculator.State = new ReadyForFirstOperandState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }
    }
}
