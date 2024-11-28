using ClassicCalculator.CalculatorState;
using Moq;

namespace ClassicCalculator.Tests.UnitTests.CalculatorState
{
    public class StateTestsBase
    {
        protected readonly Mock<ICalculator> MockCalculator = new();

        protected void VerifyStateSet<TState>(string displayValue) where TState : ICalculatorState
        {
            MockCalculator.VerifySet(c => c.State = It.Is<TState>(state => state.DisplayValue == displayValue), Times.Once);
        }
    }
}
