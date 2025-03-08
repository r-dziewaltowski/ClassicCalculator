using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests
{
    public class DisplayLengthTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(Calculator.MaxDisplayLength)]
        public void Calculator_ShouldIgnoreAnyDigitsExceedingDisplayLength(int displayLength)
        {
            // Arrange
            var calculator = new Calculator(displayLength);

            // Act
            ExceedDisplayLength(calculator, 2);

            // Assert
            var expectedDisplayValue = new string('1', displayLength);
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(Calculator.MaxDisplayLength)]
        public void Calculator_ShouldNotCountMinusSignAndDecimalSignTowardsDisplayLength(int displayLength)
        {
            // Arrange
            var calculator = new Calculator(displayLength);

            // Act
            ExceedDisplayLength(calculator, 2);
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
        public void Calculator_ShouldEnterInvalidStateWhenResultOfCalculationExceedsDisplayLength(int displayLength)
        {
            // Arrange
            var calculator = new Calculator(displayLength);

            // Act
            ExceedDisplayLength(calculator, 0);
            calculator.PressButton(CalculatorButton.Multiply);
            calculator.PressButton(CalculatorButton.One);
            calculator.PressButton(CalculatorButton.Zero);
            calculator.PressButton(CalculatorButton.Equals);

            // Assert
            Assert.Equal("Number exceeds the display length", calculator.DisplayValue);
            Assert.IsType<InvalidState>(calculator.State);
        }

        private static void ExceedDisplayLength(Calculator calculator, int extraPresses)
        {
            for (var i = 0; i < calculator.DisplayLength + extraPresses; i++)
            {
                calculator.PressButton(CalculatorButton.One);
            }
        }
    }
}
