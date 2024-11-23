namespace ClassicCalculator
{
    public class ReadyForFirstOperandState(
        Calculator calculator,
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
