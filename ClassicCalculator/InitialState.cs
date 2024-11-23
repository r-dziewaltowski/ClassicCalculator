namespace ClassicCalculator
{
    public class InitialState(ICalculator calculator) :
            CalculatorStateBase(
                calculator,
                firstOperand: null,
                currentOperation: null,
                secondOperand: null,
                displayValue: "0")
    {
        public override void AppendDigit(int digit)
        {
            DisplayValue = digit.ToString();
            _calculator.State = new FirstOperandBeingEnteredState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        public override void AppendDecimal()
        {
            DisplayValue = "0.";
            _calculator.State = new FirstOperandBeingEnteredState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        public override void SetOperation(OperationType operation)
        {
            _firstOperand = 0;
            _currentOperation = operation;
            _calculator.State = new ReadyForSecondOperandState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }
    }
}
