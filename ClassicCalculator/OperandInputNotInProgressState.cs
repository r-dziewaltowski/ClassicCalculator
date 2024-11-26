namespace ClassicCalculator
{
    public class OperandInputNotInProgressState(
        ICalculator calculator,
        double? firstOperand,
        OperationType? currentOperation,
        double? secondOperand,  
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
            _calculator.State = new OperandInputInProgressState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, digit.ToString());
        }

        public override void AppendDecimal()
        {
            _calculator.State = new OperandInputInProgressState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, "0.");
        }
    }
}
