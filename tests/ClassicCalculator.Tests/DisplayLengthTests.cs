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
        public void ShouldSetInvalidState_WhenIntegerPartExceedsDisplayLength(int displayLength)
        {
            // Arrange
            var calculator = CreateCalculator(displayLength);

            // Act
            PressButton(calculator, CalculatorButton.One, displayLength);
            PressButtons(calculator, "* 10 =");

            // Assert
            VerifyStateAndDisplayValue<InvalidState>(calculator, "Display length exceeded");
        }

        [Theory]
        [InlineData(2, "3 / 9 =", "0.3")]
        [InlineData(10, "3 / 9 =", "0.333333333")]
        [InlineData(2, "0.1 / 10 =", "0")]
        [InlineData(10, "0.1 / 1000000000 =", "0")]
        [InlineData(2, "10 + 1.1 =", "11")]
        [InlineData(10, "100000000  + 0.11 =", "100000000.1")]
        public void ShouldTrimTheLeastMeaningfulDigits_WhenFractionalPartExceedsDisplayLength(int displayLength, string input, string expectedDisplayValue)
        {
            // Arrange
            var calculator = CreateCalculator(displayLength);

            // Act
            PressButtons(calculator, input);

            // Assert
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
        }

        [Theory]
        [InlineData(10, "3 / 9 * 3 =", "0.999999999")]
        [InlineData(2, "0.1 / 10 * 10 =", "0")]
        public void ShouldUseTheTrimmedValueInTheNextOperation(int displayLength, string input, string expectedDisplayValue)
        {
            // Arrange
            var calculator = CreateCalculator(displayLength);

            // Act
            PressButtons(calculator, input);

            // Assert
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
        }
    }
}
