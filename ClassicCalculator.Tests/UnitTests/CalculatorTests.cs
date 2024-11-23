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
        public void AppendDigit_ShouldCallStateAppendDigit()
        {
            // Act
            _calculator.AppendDigit(5);

            // Assert
            _mockState.Verify(s => s.AppendDigit(5), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void AppendDigit_ShouldThrowException_WhenNumberIsNotDigit(int number)
        {
            // Act
            void act() => _calculator.AppendDigit(number);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void AppendDecimal_ShouldCallStateAppendDecimal()
        {
            // Act
            _calculator.AppendDecimal();

            // Assert
            _mockState.Verify(s => s.AppendDecimal(), Times.Once);
        }

        [Fact]
        public void SetOperation_ShouldCallStateSetOperation()
        {
            // Act
            _calculator.SetOperation(OperationType.Add);

            // Assert
            _mockState.Verify(s => s.SetOperation(OperationType.Add), Times.Once);
        }

        [Fact]
        public void Calculate_ShouldCallStateCalculate()
        {
            // Act
            _calculator.Calculate();

            // Assert
            _mockState.Verify(s => s.Calculate(), Times.Once);
        }

        [Fact]
        public void Clear_ShouldCallStateClear()
        {
            // Act
            _calculator.Clear();

            // Assert
            _mockState.Verify(s => s.Clear(), Times.Once);
        }

        [Fact]
        public void CalculatePercentage_ShouldCallStateCalculatePercentage()
        {
            // Act
            _calculator.CalculatePercentage();

            // Assert
            _mockState.Verify(s => s.CalculatePercentage(), Times.Once);
        }

        [Fact]
        public void CalculateSquareRoot_ShouldCallStateCalculateSquareRoot()
        {
            // Act
            _calculator.CalculateSquareRoot();

            // Assert
            _mockState.Verify(s => s.CalculateSquareRoot(), Times.Once);
        }

        [Fact]
        public void ToggleSign_ShouldCallStateToggleSign()
        {
            // Act
            _calculator.ToggleSign();

            // Assert
            _mockState.Verify(s => s.ToggleSign(), Times.Once);
        }
    }
}
