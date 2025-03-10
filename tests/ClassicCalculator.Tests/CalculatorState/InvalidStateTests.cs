using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class InvalidStateTests : CalculatorTestsBase
    {
        private const string DisplayValue = "Test invalid state";

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
        public void ShouldIgnoreAllButtonsExceptClear(CalculatorButton button)
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.State = new InvalidState(calculator, DisplayValue);

            // Act
            calculator.PressButton(button);

            // Assert
            VerifyStateAndDisplayValue<InvalidState>(calculator, DisplayValue);
        }

        [Fact]
        public void ShouldReset_WhenClearButtonPressed()
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.State = new InvalidState(calculator, DisplayValue);

            // Act
            calculator.PressButton(CalculatorButton.Clear);

            // Assert
            VerifyStateAndDisplayValue<InitialState>(calculator, "0");
        }
    }
}