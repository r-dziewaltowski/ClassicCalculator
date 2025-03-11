namespace ClassicCalculator.CalculatorState
{
    internal class InitialState : FirstOperandState
    {
        public InitialState(Calculator calculator) : base(
            calculator,
            0,
            "0")
        {
        }
    }
}
