using Moq;
using Xunit;

namespace ClassicCalculator.Tests.UnitTests
{
    public class FirstOperandBeingEnteredStateTests
    {
        private readonly Mock<ICalculator> _mockCalculator;

        public FirstOperandBeingEnteredStateTests()
        {
            _mockCalculator = new Mock<ICalculator>();
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(1, "1")]
        public void AppendDigit_ShouldUpdateDisplayValue_WhenInitialStateIsZeroOrOne(int digit, string expectedDisplayValue)
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "0");

            // Act
            state.AppendDigit(digit);

            // Assert
            Assert.Equal(expectedDisplayValue, state.DisplayValue);
        }

        [Theory]
        [InlineData(0, "10")]
        [InlineData(1, "11")]
        public void AppendDigit_ShouldUpdateDisplayValue_WhenInitialStateIsOne(int digit, string expectedDisplayValue)
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "1");

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
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "1.");

            // Act
            state.AppendDigit(digit);

            // Assert
            Assert.Equal(expectedDisplayValue, state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("1.", state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldNotChangeDisplayValue_WhenInitialStateIsOneDecimal()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "1.");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("1.", state.DisplayValue);
        }

        [Fact]
        public void AppendDecimal_ShouldNotChangeDisplayValue_WhenInitialStateIsZeroPointOne()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "0.1");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("0.1", state.DisplayValue);
        }

        [Theory]
        [InlineData(OperationType.Add)]
        [InlineData(OperationType.Subtract)]
        [InlineData(OperationType.Multiply)]
        [InlineData(OperationType.Divide)]
        public void SetOperation_ShouldUpdateDisplayValueAndSetState(OperationType operation)
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.SetOperation(operation);

            // Assert
            Assert.Equal("1", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<ReadyForSecondOperandState>(), Times.Once);
        }

        [Fact]
        public void Calculate_ShouldUpdateDisplayValueAndSetState()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.Calculate();

            // Assert
            Assert.Equal("1", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<ReadyForFirstOperandState>(), Times.Once);
        }

        [Fact]
        public void Clear_ShouldUpdateDisplayValueAndSetState()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.Clear();

            // Assert
            Assert.Equal("0", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<InitialState>(), Times.Once);
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValueAndSetState()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.CalculatePercentage();

            // Assert
            Assert.Equal("0", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<ReadyForFirstOperandState>(), Times.Once);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValueAndSetState()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "9");

            // Act
            state.CalculateSquareRoot();

            // Assert
            Assert.Equal("3", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<ReadyForFirstOperandState>(), Times.Once);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsZero()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "0");

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal("0", state.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsZeroDecimal()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "0.");

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal("-0.", state.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal("-1", state.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsNegativeOne()
        {
            // Arrange
            var state = new FirstOperandBeingEnteredState(_mockCalculator.Object, null, null, null, "-1");

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal("1", state.DisplayValue);
        }
    }
}

