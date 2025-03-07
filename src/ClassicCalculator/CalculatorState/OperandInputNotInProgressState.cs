namespace ClassicCalculator.CalculatorState
{
    public class OperandInputNotInProgressState(
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
