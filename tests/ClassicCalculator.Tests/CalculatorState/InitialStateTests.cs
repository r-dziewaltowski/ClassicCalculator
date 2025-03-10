using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class InitialStateTests : CalculatorTestsBase
    {
        [Theory]
        [InlineData(CalculatorButton.Zero, "0", typeof(OperandInputInProgressState))]
        [InlineData(CalculatorButton.One, "1", typeof(OperandInputInProgressState))]
        [InlineData(CalculatorButton.Decimal, "0.", typeof(OperandInputInProgressState))]
        [InlineData(CalculatorButton.Add, "0", typeof(OperandInputNotInProgressState))]
        [InlineData(CalculatorButton.Subtract, "0", typeof(OperandInputNotInProgressState))]
        [InlineData(CalculatorButton.Multiply, "0", typeof(OperandInputNotInProgressState))]
        [InlineData(CalculatorButton.Divide, "0", typeof(OperandInputNotInProgressState))]
        [InlineData(CalculatorButton.Equals, "0", typeof(OperandInputNotInProgressState))]
        [InlineData(CalculatorButton.Percentage, "0", typeof(OperandInputNotInProgressState))]
        [InlineData(CalculatorButton.SquareRoot, "0", typeof(OperandInputNotInProgressState))]
        [InlineData(CalculatorButton.ToggleSign, "0", typeof(InitialState))]
        [InlineData(CalculatorButton.Clear, "0", typeof(InitialState))]
        public void ShouldUpdateDisplayValueAndSetStateIfNecessary(
            CalculatorButton button, string expectedDisplayValue, Type expectedState)
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.State = new InitialState(calculator);

            // Act
            calculator.PressButton(button);

            // Assert
            VerifyStateAndDisplayValue(calculator, expectedDisplayValue, expectedState);
        }
    }
}