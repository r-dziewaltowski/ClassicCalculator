namespace ClassicCalculator
{
    public class InitialState(ICalculator calculator) :
        OperandInputNotInProgress(
            calculator,
            firstOperand: null,
            currentOperation: null,
            secondOperand: null,
            displayValue: "0")
    {
        public override void Calculate()
        {
            ChangeState();
        }

        public override void CalculatePercentage()
        {
            ChangeState();
        }

        public override void CalculateSquareRoot()
        {
            ChangeState();
        }

        public override void ToggleSign()
        {
            ChangeState();
        }

        private void ChangeState()
        {
            _calculator.State = new OperandInputNotInProgress(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }
    }
}
