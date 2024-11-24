namespace ClassicCalculator
{
    public class InitialState(ICalculator calculator) :
            ReadyForFirstOperandState(
                calculator,
                firstOperand: null,
                currentOperation: null,
                secondOperand: null,
                displayValue: "0")
    {
    }
}
