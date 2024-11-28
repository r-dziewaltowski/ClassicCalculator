namespace ClassicCalculator.CalculatorState
{
    public class InitialState(ICalculator calculator) :
        OperandInputNotInProgressState(
            calculator,
            firstOperand: null,
            currentOperation: null,
            secondOperand: null,
            displayValue: "0")
    {
    }
}
