using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests
{
    public class DisplayLengthTests : CalculatorTestsBase
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(Calculator.MaxDisplayLength)]
        public void ShouldIgnoreAnyDigitsExceedingDisplayLength(int displayLength)
        {
            // Arrange
            var calculator = CreateCalculator(displayLength);

            // Act
            PressButton(calculator, CalculatorButton.One, displayLength + 2);

            // Assert
            var expectedDisplayValue = new string('1', displayLength);
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(Calculator.MaxDisplayLength)]
        public void ShouldNotCountMinusSignAndDecimalSignTowardsDisplayLength(int displayLength)
        {
            // Arrange
            var calculator = CreateCalculator(displayLength);

            // Act
            PressButton(calculator, CalculatorButton.One, displayLength + 2);
            calculator.PressButton(CalculatorButton.Decimal);
            calculator.PressButton(CalculatorButton.ToggleSign);

            // Assert
            var digits = new string('1', displayLength);
            var expectedDisplayValue = $"-{digits}.";
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(Calculator.MaxDisplayLength - 1)]
        public void ShouldSetInvalidState_WhenResultOfCalculationExceedsDisplayLength(int displayLength)
        {
            // Arrange
            var calculator = CreateCalculator(displayLength);

            // Act
            PressButton(calculator, CalculatorButton.One, displayLength);
            PressButtons(calculator, "* 10 =");

            // Assert
            VerifyStateAndDisplayValue<InvalidState>(calculator, "Display length exceeded");
        }
    }
}
