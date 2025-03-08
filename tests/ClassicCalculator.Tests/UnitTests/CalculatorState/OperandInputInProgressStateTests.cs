using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.UnitTests.CalculatorState
{
    public class OperandInputInProgressStateTests : StateTestsBase
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AppendDigit_ShouldUpdateDisplayValue_WhenInitialStateIsZero(int digit)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "0");

            // Act
            state.AppendDigit(digit);

            // Assert
            Assert.Equal(digit.ToString(), state.DisplayValue);
        }

        [Theory]
        [InlineData(0, "10")]
        [InlineData(1, "11")]
        public void AppendDigit_ShouldUpdateDisplayValue_WhenInitialStateIsOne(int digit, string expectedDisplayValue)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "1");

            // Act
            state.AppendDigit(digit);

            // Assert
            Assert.Equal(expectedDisplayValue, state.DisplayValue);
        }

        [Theory]
        [InlineData(0, "1.0")]
        [InlineData(1, "1.1")]
        public void AppendDigit_ShouldUpdateDisplayValue_WhenInitialStateIsOneDecimal(int digit, string expectedDisplayValue)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "1.");

            // Act
            state.AppendDigit(digit);

            // Assert
            Assert.Equal(expectedDisplayValue, state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "1");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("1.", state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldNotChangeDisplayValue_WhenInitialStateIsOneDecimal()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "1.");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("1.", state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldNotChangeDisplayValue_WhenInitialStateIsZeroPointOne()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "0.1");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("0.1", state.DisplayValue);
        }

        [Theory]
        [InlineData(OperationType.Add, "1")]
        [InlineData(OperationType.Subtract, "1.")]
        [InlineData(OperationType.Multiply, "1")]
        [InlineData(OperationType.Divide, "1.")]
        internal void SetOperation_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne(OperationType operation, string displayValue)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, displayValue);

            // Act
            state.SetOperation(operation);

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>("1");
        }

        [Theory]
        [InlineData(OperationType.Add, "3")]
        [InlineData(OperationType.Subtract, "-1")]
        [InlineData(OperationType.Multiply, "2")]
        [InlineData(OperationType.Divide, "0.5")]
        internal void SetOperation_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation(OperationType operation, string expectedDisplayValue)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, 1, operation, null, "2");

            // Act
            state.SetOperation(OperationType.Add);

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>(expectedDisplayValue);
        }

        [Fact]
        public void SetOperation_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithDivideByZero()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, 1, OperationType.Divide, null, "0");

            // Act
            state.SetOperation(OperationType.Divide);

            // Assert
            VerifyStateSet<InvalidState>("Cannot divide by 0");
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1.")]
        public void Calculate_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne(string displayValue)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, displayValue);

            // Act
            state.Calculate();

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>("1");
        }

        [Theory]
        [InlineData(OperationType.Add, "3")]
        [InlineData(OperationType.Subtract, "-1")]
        [InlineData(OperationType.Multiply, "2")]
        [InlineData(OperationType.Divide, "0.5")]
        internal void Calculate_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation(OperationType operation, string expectedDisplayValue)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, 1, operation, null, "2");

            // Act
            state.Calculate();

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>(expectedDisplayValue);
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "1");

            // Act
            state.CalculatePercentage();

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>("0");
        }

        [Theory]
        [InlineData(OperationType.Add, "10", "11")]
        [InlineData(OperationType.Subtract, "10.", "9")]
        [InlineData(OperationType.Multiply, "10", "1")]
        [InlineData(OperationType.Divide, "10.", "100")]
        internal void CalculatePercentage_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsTenWithOperation(
            OperationType operation, string currentDisplayValue, string expectedDisplayValue)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, 10, operation, null, currentDisplayValue);

            // Act
            state.CalculatePercentage();

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>(expectedDisplayValue);
        }

        [Theory]
        [InlineData("4")]
        [InlineData("4.")]
        public void CalculateSquareRoot_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsFour(string displayValue)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, displayValue);

            // Act
            state.CalculateSquareRoot();

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>("2");
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, 1, OperationType.Add, null, "4");

            // Act
            state.CalculateSquareRoot();

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>("2");
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithNegativeOperation()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, 1, OperationType.Add, null, "-4");

            // Act
            state.CalculateSquareRoot();

            // Assert
            VerifyStateSet<InvalidState>("Invalid input");
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "1");

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal("-1", state.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue_WhenInitialStateIsNegativeOne()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "-1");

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal("1", state.DisplayValue);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("0.")]
        [InlineData("0.0")]
        public void ToggleSign_ShouldUpdateDisplayValue_WhenInitialStateIsZero(string displayValue)
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, displayValue);

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal(displayValue, state.DisplayValue);
        }

        [Fact]
        public void Clear_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new OperandInputInProgressState(Calculator, null, null, null, "1");

            // Act
            state.Clear();

            // Assert
            VerifyStateSet<InitialState>("0");
        }
    }
}
