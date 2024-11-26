namespace ClassicCalculator
{
    public class OperandInputNotInProgress(
        ICalculator calculator,
        double? firstOperand,
        OperationType? currentOperation,
        double? secondOperand,  
        string displayValue)
        : CalculatorStateBase(
            calculator, 
            firstOperand,
            currentOperation,
            secondOperand,  
            displayValue)
    {
        public override void AppendDigit(int digit)
        {
            _calculator.State = new OperandInputInProgressState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, digit.ToString());
        }

        public override void AppendDecimal()
        {
            _calculator.State = new OperandInputInProgressState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, "0.");
        }

        public override void SetOperation(OperationType operation)
        {
            _calculator.State = new InvalidState(_calculator, DisplayValue);
        }

        public override void Calculate()
        {
        }

        public override void CalculatePercentage()
        {
            ResetDisplayValue();
        }

        public override void CalculateSquareRoot()
        {
            var value = ConvertDisplayValueToNumber();
            var result = CalculateSquareRoot(value);
            UpdateDisplayValue(result);
        }
    }
}
