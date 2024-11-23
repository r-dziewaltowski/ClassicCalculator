using Moq;
using Xunit;

namespace ClassicCalculator.Tests.UnitTests
{
    public class InitialStateTests
    {
        private readonly Mock<ICalculator> _mockCalculator;
        private readonly InitialState _state;

        public InitialStateTests()
        {
            _mockCalculator = new Mock<ICalculator>();
            _state = new InitialState(_mockCalculator.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AppendDigit_ShouldUpdateDisplayValueAndSetState(int digit)
        {
            // Act
            _state.AppendDigit(digit);

            // Assert
            Assert.Equal(digit.ToString(), _state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<FirstOperandBeingEnteredState>(), Times.Once);
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValueAndSetState()
        {
            // Act
            _state.AppendDecimal();

            // Assert
            Assert.Equal("0.", _state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<FirstOperandBeingEnteredState>(), Times.Once);
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
            Assert.Equal("0", _state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<ReadyForSecondOperandState>(), Times.Once);
        }

        [Fact]
        public void Calculate_ShouldUpdateDisplayValue()
        {
            // Act
            _state.Calculate();

            // Assert
            Assert.Equal("0", _state.DisplayValue);
        }

        [Fact]
        public void Clear_ShouldUpdateDisplayValue()
        {
            // Act
            _state.Clear();

            // Assert
            Assert.Equal("0", _state.DisplayValue);
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValue()
        {
            // Act
            _state.CalculatePercentage();

            // Assert
            Assert.Equal("0", _state.DisplayValue);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValue()
        {
            // Act
            _state.CalculateSquareRoot();

            // Assert
            Assert.Equal("0", _state.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue()
        {
            // Act
            _state.ToggleSign();

            // Assert
            Assert.Equal("0", _state.DisplayValue);
        }
    }
}