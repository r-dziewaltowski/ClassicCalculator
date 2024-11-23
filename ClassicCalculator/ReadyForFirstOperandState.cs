namespace ClassicCalculator
{
    public class ReadyForFirstOperandState(
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
    }
}
