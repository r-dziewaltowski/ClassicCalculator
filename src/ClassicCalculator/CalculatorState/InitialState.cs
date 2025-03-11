namespace ClassicCalculator.CalculatorState
{
    internal class InitialState(Calculator calculator)
        : FirstOperandState(
            calculator,
            0,
            "0")
    {
    }
}
