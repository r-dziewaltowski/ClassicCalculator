namespace ClassicCalculator.CalculatorState
{
    public class InitialState(Calculator calculator) :
        OperandInputNotInProgressState(
            calculator,
            firstOperand: null,
            currentOperation: null,
            secondOperand: null,
            displayValue: "0")
    {
    }
}
