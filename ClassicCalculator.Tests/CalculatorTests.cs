namespace ClassicCalculator.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void AppendDecimal_WhenDisplayIsZero_ShouldShowZeroDecimal()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDecimal();

            // Assert
            Assert.Equal("0.", calculator.DisplayValue);
        }

        [Fact]
        public void AppendZeroThreeTimes_ShouldShowZero()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("0");
            calculator.AppendDigit("0");
            calculator.AppendDigit("0");

            // Assert
            Assert.Equal("0", calculator.DisplayValue);
        }

        [Fact]
        public void AppendNumberThenDecimalThenOperation_ShouldShowNumberWithoutDecimal()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("5");
            calculator.AppendDecimal();
            calculator.SetOperation(OperationType.Add);

            // Assert
            Assert.Equal("5", calculator.DisplayValue);
        }

        [Fact]
        public void DivideByZero_ShouldShowCannotDivideByZero()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("5");
            calculator.SetOperation(OperationType.Divide);
            calculator.AppendDigit("0");
            calculator.Calculate();

            // Assert
            Assert.Equal("Cannot divide by zero", calculator.DisplayValue);
        }

        [Fact]
        public void PerformAnyOperationTwice_ShouldReplaceFirstOperation()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("5");
            calculator.SetOperation(OperationType.Add);
            calculator.SetOperation(OperationType.Subtract);

            // Assert
            Assert.Equal("5", calculator.DisplayValue);
        }

        [Fact]
        public void CalculatePercentage_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("50");
            calculator.CalculatePercentage();

            // Assert
            Assert.Equal("0.5", calculator.DisplayValue);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("9");
            calculator.CalculateSquareRoot();

            // Assert
            Assert.Equal("3", calculator.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("5");
            calculator.ToggleSign();

            // Assert
            Assert.Equal("-5", calculator.DisplayValue);
        }

        [Fact]
        public void Clear_ShouldResetCalculator()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("5");
            calculator.Clear();

            // Assert
            Assert.Equal("0", calculator.DisplayValue);
        }

        [Fact]
        public void PerformAddition_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("5");
            calculator.SetOperation(OperationType.Add);
            calculator.AppendDigit("3");
            calculator.Calculate();

            // Assert
            Assert.Equal("8", calculator.DisplayValue);
        }

        [Fact]
        public void PerformSubtraction_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("5");
            calculator.SetOperation(OperationType.Subtract);
            calculator.AppendDigit("3");
            calculator.Calculate();

            // Assert
            Assert.Equal("2", calculator.DisplayValue);
        }

        [Fact]
        public void PerformMultiplication_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("5");
            calculator.SetOperation(OperationType.Multiply);
            calculator.AppendDigit("3");
            calculator.Calculate();

            // Assert
            Assert.Equal("15", calculator.DisplayValue);
        }

        [Fact]
        public void PerformDivision_ShouldShowCorrectResult()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            calculator.AppendDigit("6");
            calculator.SetOperation(OperationType.Divide);
            calculator.AppendDigit("3");
            calculator.Calculate();

            // Assert
            Assert.Equal("2", calculator.DisplayValue);
        }
    }
}





