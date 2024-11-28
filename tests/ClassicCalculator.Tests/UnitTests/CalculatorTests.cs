using ClassicCalculator.CalculatorState;
using Moq;

namespace ClassicCalculator.Tests.UnitTests
{
    public class CalculatorTests
    {
        private readonly Mock<ICalculatorState> _mockState;
        private readonly Calculator _calculator;

        public CalculatorTests()
        {
            _mockState = new Mock<ICalculatorState>();
            _calculator = new Calculator { State = _mockState.Object };
        }

        [Fact]
        public void Constructor_ShouldInitializeState()
        {
            // Act
            var calculator = new Calculator();

            // Assert
            Assert.NotNull(calculator.State);
            Assert.IsType<InitialState>(calculator.State);
        }

        [Fact]
        public void PressButton_ShouldCallStateAppendDigit()
        {
            // Act
            _calculator.PressButton(CalculatorButton.Five);

            // Assert
            _mockState.Verify(s => s.AppendDigit(5), Times.Once);
        }

        [Fact]
        public void PressButton_ShouldCallStateAppendDecimal()
        {
            // Act
            _calculator.PressButton(CalculatorButton.Decimal);

            // Assert
            _mockState.Verify(s => s.AppendDecimal(), Times.Once);
        }

        [Theory]
        [InlineData(CalculatorButton.Add, OperationType.Add)]
        [InlineData(CalculatorButton.Subtract, OperationType.Subtract)]
        [InlineData(CalculatorButton.Multiply, OperationType.Multiply)]
        [InlineData(CalculatorButton.Divide, OperationType.Divide)]
        public void PressButton_ShouldCallStateSetOperation(CalculatorButton button, OperationType operation)
        {
            // Act
            _calculator.PressButton(button);

            // Assert
            _mockState.Verify(s => s.SetOperation(operation), Times.Once);
        }

        [Fact]
        public void PressButton_ShouldCallStateCalculate()
        {
            // Act
            _calculator.PressButton(CalculatorButton.Equals);

            // Assert
            _mockState.Verify(s => s.Calculate(), Times.Once);
        }

        [Fact]
        public void PressButton_ShouldCallStateClear()
        {
            // Act
            _calculator.PressButton(CalculatorButton.Clear);

            // Assert
            _mockState.Verify(s => s.Clear(), Times.Once);
        }

        [Fact]
        public void PressButton_ShouldCallStateCalculatePercentage()
        {
            // Act
            _calculator.PressButton(CalculatorButton.Percentage);

            // Assert
            _mockState.Verify(s => s.CalculatePercentage(), Times.Once);
        }

        [Fact]
        public void PressButton_ShouldCallStateCalculateSquareRoot()
        {
            // Act
            _calculator.PressButton(CalculatorButton.SquareRoot);

            // Assert
            _mockState.Verify(s => s.CalculateSquareRoot(), Times.Once);
        }

        [Fact]
        public void PressButton_ShouldCallStateToggleSign()
        {
            // Act
            _calculator.PressButton(CalculatorButton.ToggleSign);

            // Assert
            _mockState.Verify(s => s.ToggleSign(), Times.Once);
        }
    }
}
