using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests
{
    public class CalculatorTests : CalculatorTestsBase
    {
        [Theory]
        [InlineData("0 * 5 =", "0")]
        [InlineData(".", "0.")]
        [InlineData(". .", "0.")]
        [InlineData("000", "0")]
        [InlineData("5. +", "5")]
        [InlineData("5.0 +", "5")]
        [InlineData("5 + -", "5")]
        [InlineData("10 + 10 %", "11")]
        [InlineData("10 - 10 %", "9")]
        [InlineData("10 * 10 %", "1")]
        [InlineData("10 / 10 %", "100")]
        [InlineData("9 SR - 4 SR =", "1")]
        [InlineData("5 +/-", "-5")]
        [InlineData("5 C", "0")]
        [InlineData("5 + 3 =", "8")]
        [InlineData("5 - 3 =", "2")]
        [InlineData("5 * 3 =", "15")]
        [InlineData("6 / 3 =", "2")]
        [InlineData("5 + 3 = * 2 =", "16")]
        [InlineData("2 - 5 =", "-3")]
        [InlineData("9 / 3 * 3 =", "9")]
        [InlineData("9 + 3 - 3 =", "9")]
        [InlineData("-1 + 2.5 - 0.5 * 2 / 4 * 200 % + 9 SR =", "4")]
        public void ShouldShowCorrectDisplayValue(string input, string expectedDisplayValue)
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            PressButtons(calculator, input);

            // Assert
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
        }

        [Theory]
        [InlineData("-0.0000000000000000000000000001", "+", "-0.0000000000000000000000000002")]
        [InlineData("-0.0000000000000000000000000001", "-", "0")]
        [InlineData("-0.0000000000000000000000000001", "/", "1")]
        [InlineData("-10000000000000000000000000000", "+", "-20000000000000000000000000000")]
        [InlineData("-10000000000000000000000000000", "-", "0")]
        [InlineData("-10000000000000000000000000000", "/", "1")]
        public void ShouldShowCorrectResult_WhenPerformingOperationsOnVeryLargeAndSmallNumbers(
            string operand, string operation, string expectedDisplayValue)
        {
            // Arrange
            var calculator = CreateCalculator();
            var input = $"{operand} {operation} {operand} =";

            // Act
            PressButtons(calculator, input);

            // Assert
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
        }

        [Theory]
        [InlineData("5 / 0 =", "Divide by 0")]
        [InlineData("-9 SR", "Invalid input")]
        [InlineData("10000000000000000000000000000 * 10 =", "Overflow")]
        public void ShouldHandleInvalidOperations(string input, string expectedDisplayValue)
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            PressButtons(calculator, input);

            // Assert
            VerifyStateAndDisplayValue<InvalidState>(calculator, expectedDisplayValue);
        }
    }
}
