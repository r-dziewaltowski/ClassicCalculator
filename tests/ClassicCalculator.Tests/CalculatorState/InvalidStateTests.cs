using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class InvalidStateTests : StateTestsBase
    {
        private const string DisplayValue = "Test invalid state";

        public InvalidStateTests()
        {
            Calculator.State = new InvalidState(Calculator, DisplayValue);
        }

        [Theory]
        [InlineData(CalculatorButton.Zero)]
        [InlineData(CalculatorButton.One)]
        [InlineData(CalculatorButton.Decimal)]
        [InlineData(CalculatorButton.Add)]
        [InlineData(CalculatorButton.Subtract)]
        [InlineData(CalculatorButton.Multiply)]
        [InlineData(CalculatorButton.Divide)]
        [InlineData(CalculatorButton.Equals)]
        [InlineData(CalculatorButton.Percentage)]
        [InlineData(CalculatorButton.SquareRoot)]
        [InlineData(CalculatorButton.ToggleSign)]
        public void HandleButtonPressed_ShouldHaveNoEffect(CalculatorButton button)
        {
            PerformTest(button, DisplayValue, typeof(InvalidState));
        }

        [Fact]
        public void ClearButton_ShouldResetCalculator()
        {
            PerformTest(CalculatorButton.Clear, "0", typeof(InitialState));
        }
    }
}