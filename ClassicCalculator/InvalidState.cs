namespace ClassicCalculator
{
    public class InvalidState(ICalculator calculator, string displayValue)
        : CalculatorStateBase(
            calculator,
            null, 
            null,
            null, 
            displayValue)
    {
    }
}
