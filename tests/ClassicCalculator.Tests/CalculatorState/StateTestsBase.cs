using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class StateTestsBase
    {
        protected readonly Calculator Calculator = new();

        protected void PerformTest(
            CalculatorButton button, string expectedDisplayValue, Type expectedState) 
        {
            // Act
            Calculator.PressButton(button);

            // Assert
            Assert.Equal(expectedDisplayValue, Calculator.DisplayValue);
            Assert.IsType(expectedState, Calculator.State);
        }

        internal void VerifyStateSet<TState>(string displayValue) where TState : CalculatorStateBase
        {
            Assert.Equal(displayValue, Calculator.DisplayValue);
            Assert.IsType<TState>(Calculator.State);
        }
    }
}
