namespace ClassicCalculator
{
    public class ReadyForSecondOperandState(
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
