using Moq;
using Xunit;

namespace ClassicCalculator.Tests.UnitTests
{
    public class ReadyForFirstOperandStateTests
    {
        private readonly Mock<ICalculator> _mockCalculator;

        public ReadyForFirstOperandStateTests()
        {
            _mockCalculator = new Mock<ICalculator>();
        }

        [Fact]
        public void AppendDigit_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.AppendDigit(2);

            // Assert
            Assert.Equal("2", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<FirstOperandBeingEnteredState>(), Times.Once);
        }

        [Fact]
        public void AppendDigit_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, 1, OperationType.Add, 2, "3");

            // Act
            state.AppendDigit(4);

            // Assert
            Assert.Equal("4", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<FirstOperandBeingEnteredState>(), Times.Once);
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("0.", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<FirstOperandBeingEnteredState>(), Times.Once);
        }

        [Fact]
        public void AppendDecimal_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, 1, OperationType.Add, 2, "3");

            // Act
            state.AppendDecimal();

            // Assert
            Assert.Equal("0.", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<FirstOperandBeingEnteredState>(), Times.Once);
        }

        [Theory]
        [InlineData(OperationType.Add)]
        [InlineData(OperationType.Subtract)]
        [InlineData(OperationType.Multiply)]
        [InlineData(OperationType.Divide)]
        public void SetOperation_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne(OperationType operation)
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.SetOperation(operation);

            // Assert
            Assert.Equal("1", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<ReadyForSecondOperandState>(), Times.Once);
        }

        [Theory]
        [InlineData(OperationType.Add)]
        [InlineData(OperationType.Subtract)]
        [InlineData(OperationType.Multiply)]
        [InlineData(OperationType.Divide)]
        public void SetOperation_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation(OperationType operation)
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, 1, OperationType.Add, 2, "3");

            // Act
            state.SetOperation(operation);

            // Assert
            Assert.Equal("3", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<ReadyForSecondOperandState>(), Times.Once);
        }

        [Fact]
        public void Calculate_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.Calculate();

            // Assert
            Assert.Equal("1", state.DisplayValue);
        }

        [Theory]
        [InlineData(OperationType.Add, 5)]
        [InlineData(OperationType.Subtract, 1)]
        [InlineData(OperationType.Multiply, 6)]
        [InlineData(OperationType.Divide, 1.5)]
        public void Calculate_ShouldUpdateDisplayValue_WhenInitialStateIsOneWithOperation(
            OperationType operationType, double expectedResult)
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, 1, operationType, 2, "3");

            // Act
            state.Calculate();

            // Assert
            Assert.Equal(expectedResult.ToString(), state.DisplayValue);
        }

        [Fact]
        public void Clear_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.Clear();

            // Assert
            Assert.Equal("0", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<InitialState>(), Times.Once);
        }

        [Fact]
        public void Clear_ShouldUpdateDisplayValueAndSetState_WhenInitialStateIsOneWithOperation()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, 1, OperationType.Add, 2, "3");

            // Act
            state.Clear();

            // Assert
            Assert.Equal("0", state.DisplayValue);
            _mockCalculator.VerifySet(c => c.State = It.IsAny<InitialState>(), Times.Once);
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.CalculatePercentage();

            // Assert
            Assert.Equal("0", state.DisplayValue);
        }

        [Fact]
        public void CalculatePercentage_ShouldUpdateDisplayValue_WhenInitialStateIsOneWithOperation()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, 1, OperationType.Add, 2, "3");

            // Act
            state.CalculatePercentage();

            // Assert
            Assert.Equal("0", state.DisplayValue);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, null, null, null, "9");

            // Act
            state.CalculateSquareRoot();

            // Assert
            Assert.Equal("3", state.DisplayValue);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldUpdateDisplayValue_WhenInitialStateIsOneWithOperation()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, 1, OperationType.Add, 2, "9");

            // Act
            state.CalculateSquareRoot();

            // Assert
            Assert.Equal("3", state.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue_WhenInitialStateIsOne()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, null, null, null, "1");

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal("-1", state.DisplayValue);
        }

        [Fact]
        public void ToggleSign_ShouldUpdateDisplayValue_WhenInitialStateIsOneWithOperation()
        {
            // Arrange
            var state = new ReadyForFirstOperandState(_mockCalculator.Object, 1, OperationType.Add, 2, "3");

            // Act
            state.ToggleSign();

            // Assert
            Assert.Equal("-3", state.DisplayValue);
        }
    }
}