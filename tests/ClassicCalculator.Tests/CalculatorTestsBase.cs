using ClassicCalculator.CalculatorState;
using Microsoft.Extensions.Logging;
using Moq;

namespace ClassicCalculator.Tests
{
    public class CalculatorTestsBase
    {
        protected readonly Mock<ILogger<Calculator>> LoggerMock = new();
        protected readonly Calculator Calculator;

        public CalculatorTestsBase()
        {
            Calculator = new(Calculator.MaxDisplayLength, LoggerMock.Object);
        }

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
