using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.UnitTests.CalculatorState
{
    public class StateTestsBase
    {
        protected readonly Calculator Calculator = new();

        protected void VerifyStateSet<TState>(string displayValue) where TState : ICalculatorState
        {
            Assert.Equal(displayValue, Calculator.DisplayValue);
            Assert.IsType<TState>(Calculator.State);
        }
    }
}
