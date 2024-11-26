namespace ClassicCalculator.Tests.UnitTests
{
    public class InitialStateTests : StateTestsBase
    {
        private readonly InitialState _state;

        public InitialStateTests()
        {
            _state = new InitialState(MockCalculator.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AppendDigit_ShouldUpdateDisplayValueAndSetState(int digit)
        {
            // Act
            _state.AppendDigit(digit);

            // Assert
            VerifyStateSet<OperandInputInProgressState>(digit.ToString());
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValueAndSetState()
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
        public void SetOperation_ShouldUpdateDisplayValueAndSetState(OperationType operation)
        {
            // Act
            _state.SetOperation(operation);

            // Assert
            VerifyStateSet<InvalidState>("0");
        }

        [Fact]
        public void Calculate_ShouldUpdateDisplayValue()
        {
            // Act
            _state.Calculate();

            // Assert
            VerifyStateSet<OperandInputNotInProgress>("0");
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValue()
        {
            // Act
            _state.CalculatePercentage();

            // Assert
            VerifyStateSet<OperandInputNotInProgress>("0");
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValue()
        {
            // Act
            _state.CalculateSquareRoot();

            // Assert
            VerifyStateSet<OperandInputNotInProgress>("0");
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue()
        {
            // Act
            _state.ToggleSign();

            // Assert
            VerifyStateSet<OperandInputNotInProgress>("0");
        }

        [Fact]
        public void Clear_ShouldUpdateDisplayValue()
        {
            // Act
            _state.Clear();

            // Assert
            VerifyStateSet<InitialState>("0");
        }
    }
}