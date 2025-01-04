namespace ClassicCalculator.Tests.IntegrationTests
{
    public class CalculatorTests
    {
        [Fact]
        public void PerformOperationWithZeroAsFirstOperand_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Zero);
            calculator.PressButton(CalculatorButton.Multiply);
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Equals);

            // Assert
            Assert.Equal("0", calculator.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_WhenDisplayIsZero_ShouldShowZeroDecimal()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Decimal);

            // Assert
            Assert.Equal("0.", calculator.DisplayValue);
        }

        [Fact]
        public void AppendZeroThreeTimes_ShouldShowZero()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Zero);
            calculator.PressButton(CalculatorButton.Zero);
            calculator.PressButton(CalculatorButton.Zero);

            // Assert
            Assert.Equal("0", calculator.DisplayValue);
        }

        [Fact]
        public void AppendNumberThenDecimalThenOperation_ShouldShowNumberWithoutDecimal()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Decimal);
            calculator.PressButton(CalculatorButton.Add);

            // Assert
            Assert.Equal("5", calculator.DisplayValue);
        }

        [Fact]
        public void AppendNumberThenDecimalThenZeroThenOperation_ShouldShowNumberWithoutDecimal()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Decimal);
            calculator.PressButton(CalculatorButton.Zero);
            calculator.PressButton(CalculatorButton.Add);

            // Assert
            Assert.Equal("5", calculator.DisplayValue);
        }

        [Fact]
        public void DivideByZero_ShouldShowCannotDivideByZero()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Divide);
            calculator.PressButton(CalculatorButton.Zero);
            calculator.PressButton(CalculatorButton.Equals);

            // Assert
            Assert.Equal("Cannot divide by 0", calculator.DisplayValue);
        }

        [Fact]
        public void PerformOperation_ShouldReplacePreviousOperation()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Add);
            calculator.PressButton(CalculatorButton.Subtract);

            // Assert
            Assert.Equal("5", calculator.DisplayValue);
        }

        [Theory]
        [InlineData(CalculatorButton.Add, "11")]
        [InlineData(CalculatorButton.Subtract, "9")]
        [InlineData(CalculatorButton.Multiply, "1")]
        [InlineData(CalculatorButton.Divide, "100")]
        public void CalculatePercentage_ShouldShowCorrectResult(CalculatorButton operation, string expectedDisplayValue)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.One);
            calculator.PressButton(CalculatorButton.Zero);
            calculator.PressButton(operation);
            calculator.PressButton(CalculatorButton.One);
            calculator.PressButton(CalculatorButton.Zero);
            calculator.PressButton(CalculatorButton.Percentage);

            // Assert
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Nine);
            calculator.PressButton(CalculatorButton.SquareRoot);

            // Assert
            Assert.Equal("3", calculator.DisplayValue);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldShowInvalidInput()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Nine);
            calculator.PressButton(CalculatorButton.ToggleSign);
            calculator.PressButton(CalculatorButton.SquareRoot);

            // Assert
            Assert.Equal("Invalid input", calculator.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.ToggleSign);

            // Assert
            Assert.Equal("-5", calculator.DisplayValue);
        }

        [Fact]
        public void Clear_ShouldResetCalculator()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Clear);

            // Assert
            Assert.Equal("0", calculator.DisplayValue);
        }

        [Fact]
        public void PerformAddition_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Add);
            calculator.PressButton(CalculatorButton.Three);
            calculator.PressButton(CalculatorButton.Equals);

            // Assert
            Assert.Equal("8", calculator.DisplayValue);
        }

        [Fact]
        public void PerformSubtraction_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Subtract);
            calculator.PressButton(CalculatorButton.Three);
            calculator.PressButton(CalculatorButton.Equals);

            // Assert
            Assert.Equal("2", calculator.DisplayValue);
        }

        [Fact]
        public void PerformMultiplication_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Multiply);
            calculator.PressButton(CalculatorButton.Three);
            calculator.PressButton(CalculatorButton.Equals);

            // Assert
            Assert.Equal("15", calculator.DisplayValue);
        }

        [Fact]
        public void PerformDivision_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Six);
            calculator.PressButton(CalculatorButton.Divide);
            calculator.PressButton(CalculatorButton.Three);
            calculator.PressButton(CalculatorButton.Equals);

            // Assert
            Assert.Equal("2", calculator.DisplayValue);
        }

        [Fact]
        public void PerformMultipleOperations_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.PressButton(CalculatorButton.Five);
            calculator.PressButton(CalculatorButton.Add);
            calculator.PressButton(CalculatorButton.Three);
            calculator.PressButton(CalculatorButton.Equals);
            calculator.PressButton(CalculatorButton.Multiply);
            calculator.PressButton(CalculatorButton.Two);
            calculator.PressButton(CalculatorButton.Equals);

            // Assert
            Assert.Equal("16", calculator.DisplayValue);
        }
    }
}





