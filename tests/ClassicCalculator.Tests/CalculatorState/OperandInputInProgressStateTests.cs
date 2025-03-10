using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class OperandInputInProgressStateTests : CalculatorTestsBase
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AppendDigit_ShouldUpdateDisplayValue_WhenInitialStateIsZero(int digit)
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "0");

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
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "1");

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
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "1.");

            // Act
            state.AppendDigit(digit);

            // Assert
            Assert.Equal(expectedDisplayValue, state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "1");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("1.", state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldNotChangeDisplayValue_WhenInitialStateIsOneDecimal()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "1.");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("1.", state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldNotChangeDisplayValue_WhenInitialStateIsZeroPointOne()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "0.1");

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
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, displayValue);

            // Act
            state.SetOperation(operation);

            // Assert
            VerifyStateAndDisplayValue<OperandInputNotInProgressState>(calculator, "1");
        }

        [Theory]
        [InlineData(OperationType.Add, "3")]
        [InlineData(OperationType.Subtract, "-1")]
        [InlineData(OperationType.Multiply, "2")]
        [InlineData(OperationType.Divide, "0.5")]
        internal void SetOperation_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation(OperationType operation, string expectedDisplayValue)
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, 1, operation, null, "2");

            // Act
            state.SetOperation(OperationType.Add);

            // Assert
            VerifyStateAndDisplayValue<OperandInputNotInProgressState>(calculator, expectedDisplayValue);
        }

        [Fact]
        public void SetOperation_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithDivideByZero()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, 1, OperationType.Divide, null, "0");

            // Act
            state.SetOperation(OperationType.Divide);

            // Assert
            VerifyStateAndDisplayValue<InvalidState>(calculator, "Cannot divide by 0");
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1.")]
        public void Calculate_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne(string displayValue)
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, displayValue);

            // Act
            state.Calculate();

            // Assert
            VerifyStateAndDisplayValue<OperandInputNotInProgressState>(calculator, "1");
        }

        [Theory]
        [InlineData(OperationType.Add, "3")]
        [InlineData(OperationType.Subtract, "-1")]
        [InlineData(OperationType.Multiply, "2")]
        [InlineData(OperationType.Divide, "0.5")]
        internal void Calculate_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation(OperationType operation, string expectedDisplayValue)
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, 1, operation, null, "2");

            // Act
            state.Calculate();

            // Assert
            VerifyStateAndDisplayValue<OperandInputNotInProgressState>(calculator, expectedDisplayValue);
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "1");

            // Act
            state.CalculatePercentage();

            // Assert
            VerifyStateAndDisplayValue<OperandInputNotInProgressState>(calculator, "0");
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
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, 10, operation, null, currentDisplayValue);

            // Act
            state.CalculatePercentage();

            // Assert
            VerifyStateAndDisplayValue<OperandInputNotInProgressState>(calculator, expectedDisplayValue);
        }

        [Theory]
        [InlineData("4")]
        [InlineData("4.")]
        public void CalculateSquareRoot_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsFour(string displayValue)
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, displayValue);

            // Act
            state.CalculateSquareRoot();

            // Assert
            VerifyStateAndDisplayValue<OperandInputNotInProgressState>(calculator, "2");
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, 1, OperationType.Add, null, "4");

            // Act
            state.CalculateSquareRoot();

            // Assert
            VerifyStateAndDisplayValue<OperandInputNotInProgressState>(calculator, "2");
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithNegativeOperation()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, 1, OperationType.Add, null, "-4");

            // Act
            state.CalculateSquareRoot();

            // Assert
            VerifyStateAndDisplayValue<InvalidState>(calculator, "Invalid input");
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "1");

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal("-1", state.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue_WhenInitialStateIsNegativeOne()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "-1");

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
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, displayValue);

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal(displayValue, state.DisplayValue);
        }

        [Fact]
        public void Clear_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Arrange
            var calculator = CreateCalculator();
            var state = new OperandInputInProgressState(calculator, null, null, null, "1");

            // Act
            state.Clear();

            // Assert
            VerifyStateAndDisplayValue<InitialState>(calculator, "0");
        }
    }
}
