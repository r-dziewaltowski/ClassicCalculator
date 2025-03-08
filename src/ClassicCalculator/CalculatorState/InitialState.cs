namespace ClassicCalculator.CalculatorState
{
    internal class InitialState(Calculator calculator) :
        OperandInputNotInProgressState(
            calculator,
            firstOperand: null,
            currentOperation: null,
            secondOperand: null,
            displayValue: "0")
    {
    }
}
