using ClassicCalculator;
using Moq;

namespace ClassicCalculatorWpfApp.Tests
{
    public class CalculatorViewModelTests
    {
        private readonly Mock<ICalculator> _mockCalculator;

        public CalculatorViewModelTests()
        {
            _mockCalculator = new Mock<ICalculator>();
        }

        [Theory]
        [InlineData(CalculatorButton.Zero)]
        [InlineData(CalculatorButton.One)]
        [InlineData(CalculatorButton.Two)]
        [InlineData(CalculatorButton.Three)]
        [InlineData(CalculatorButton.Four)]
        [InlineData(CalculatorButton.Five)]
        [InlineData(CalculatorButton.Six)]
        [InlineData(CalculatorButton.Seven)]
        [InlineData(CalculatorButton.Eight)]
        [InlineData(CalculatorButton.Nine)]
        [InlineData(CalculatorButton.Clear)]
        [InlineData(CalculatorButton.Percentage)]
        [InlineData(CalculatorButton.SquareRoot)]
        [InlineData(CalculatorButton.Multiply)]
        [InlineData(CalculatorButton.Subtract)]
        [InlineData(CalculatorButton.Add)]
        [InlineData(CalculatorButton.Divide)]
        [InlineData(CalculatorButton.ToggleSign)]
        [InlineData(CalculatorButton.Decimal)]
        [InlineData(CalculatorButton.Equals)]
        public void ExecutingPressButtonCommand_ShouldCallPressButtonWithProvidedArgument(CalculatorButton calculatorButton)
        {
            // Arrange
            var viewModel = new CalculatorViewModel(_mockCalculator.Object);

            // Act
            viewModel.PressButtonCommand.Execute(calculatorButton);

            // Assert
            _mockCalculator.Verify(c => c.PressButton(calculatorButton), Times.Once);
        }
    }
}