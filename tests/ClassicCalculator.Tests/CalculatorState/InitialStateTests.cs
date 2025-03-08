using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class InitialStateTests : StateTestsBase
    {
        private readonly InitialState _state;

        public InitialStateTests()
        {
            _state = new InitialState(Calculator);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AppendDigit_ShouldUpdateDisplayValue_WhenInitialStateIsZero(int digit)
        {
            // Act
            _state.AppendDigit(digit);

            // Assert
            VerifyStateSet<OperandInputInProgressState>(digit.ToString());
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Act
            _state.AppendDecimal();

            // Assert
            VerifyStateSet<OperandInputInProgressState>("0.");
        }

        [Theory]
        [InlineData(OperationType.Add)]
        [InlineData(OperationType.Subtract)]
        [InlineData(OperationType.Multiply)]
        [InlineData(OperationType.Divide)]
        internal void SetOperation_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne(OperationType operation)
        {
            // Act
            _state.SetOperation(operation);

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>("0");
        }

        [Fact]
        public void Calculate_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Act
            _state.Calculate();

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>("0");
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Act
            _state.CalculatePercentage();

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>("0");
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsFour()
        {
            // Act
            _state.CalculateSquareRoot();

            // Assert
            VerifyStateSet<OperandInputNotInProgressState>("0");
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Act
            _state.ToggleSign();

            // Assert
            Assert.Equal("0", _state.DisplayValue);
        }

        [Fact]
        public void Clear_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Act
            _state.Clear();

            // Assert
            VerifyStateSet<InitialState>("0");
        }
    }
}