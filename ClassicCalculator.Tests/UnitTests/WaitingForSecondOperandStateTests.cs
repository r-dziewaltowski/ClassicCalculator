namespace ClassicCalculator.Tests.UnitTests
{
    public class WaitingForSecondOperandStateTests : StateTestsBase
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AppendDigit_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne(int digit)
        {
            // Arrange
            var state = CreateState("1");

            // Act
            state.AppendDigit(digit);

            // Assert
            VerifyStateSet<OperandInputInProgressState>(digit.ToString());
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Arrange
            var state = CreateState("1");

            // Act
            state.AppendDecimal();

            // Assert
            VerifyStateSet<OperandInputInProgressState>("0.");
        }

        [Theory]
        [InlineData(OperationType.Add)]
        [InlineData(OperationType.Subtract)]
        [InlineData(OperationType.Multiply)]
        [InlineData(OperationType.Divide)]
        public void SetOperation_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne(OperationType operation)
        {
            // Arrange
            var state = CreateState("1");

            // Act
            state.SetOperation(operation);

            // Assert
            VerifyStateSet<InvalidState>("1");
        }

        [Fact]
        public void Calculate_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var state = CreateState("1");

            // Act
            state.Calculate();

            // Assert
            Assert.Equal("1", state.DisplayValue);
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var state = CreateState("1");

            // Act
            state.CalculatePercentage();

            // Assert
            Assert.Equal("0", state.DisplayValue);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValue_WhenInitialStateIsFour()
        {
            // Arrange
            var state = CreateState("4");

            // Act
            state.CalculateSquareRoot();

            // Assert
            Assert.Equal("2", state.DisplayValue);
        }

        [Theory]
        [InlineData("1", "-1")]
        [InlineData("-1", "1")]
        public void ToggleSign_ShouldUpdateDisplayValue(string initialState, string expectedDisplayValue)
        {
            // Arrange
            var state = CreateState(initialState);

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal(expectedDisplayValue, state.DisplayValue);
        }

        [Fact]
        public void Clear_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsZero()
        {
            // Arrange
            var state = CreateState("1");

            // Act
            state.Clear();

            // Assert
            VerifyStateSet<InitialState>("0");
        }

        private OperandInputNotInProgress CreateState(string displayValue)
        {
            return new OperandInputNotInProgress(MockCalculator.Object, 1, OperationType.Add, null, displayValue);
        }
    }
}
