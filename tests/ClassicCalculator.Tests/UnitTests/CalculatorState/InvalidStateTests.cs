using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.UnitTests.CalculatorState
{
    public class InvalidStateTests : StateTestsBase
    {
        private const string DisplayValue = "Test invalid state";
        private readonly InvalidState _state;

        public InvalidStateTests()
        {
            _state = new InvalidState(Calculator, DisplayValue);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AppendDigit_ShouldUpdateDisplayValue_WhenInitialStateIsZero(int digit)
        {
            // Act
            _state.AppendDigit(digit);

            // Assert
            Assert.Equal(DisplayValue, _state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Act
            _state.AppendDecimal();

            // Assert
            Assert.Equal(DisplayValue, _state.DisplayValue);
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
            Assert.Equal(DisplayValue, _state.DisplayValue);
        }

        [Fact]
        public void Calculate_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Act
            _state.Calculate();

            // Assert
            Assert.Equal(DisplayValue, _state.DisplayValue);
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Act
            _state.CalculatePercentage();

            // Assert
            Assert.Equal(DisplayValue, _state.DisplayValue);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsFour()
        {
            // Act
            _state.CalculateSquareRoot();

            // Assert
            Assert.Equal(DisplayValue, _state.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Act
            _state.ToggleSign();

            // Assert
            Assert.Equal(DisplayValue, _state.DisplayValue);
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