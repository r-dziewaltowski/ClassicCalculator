using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class FirstOperandStateTests : CalculatorTestsBase
    {
        [Theory]
        [InlineData(CalculatorButton.Zero, "0", typeof(FirstOperandInputInProgressState))]
        [InlineData(CalculatorButton.One, "1", typeof(FirstOperandInputInProgressState))]
        [InlineData(CalculatorButton.Decimal, "0.", typeof(FirstOperandInputInProgressState))]
        [InlineData(CalculatorButton.ToggleSign, "-4", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.Add, "4", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Subtract, "4", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Multiply, "4", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Divide, "4", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Equals, "4", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.Percentage, "0", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.SquareRoot, "2", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.Clear, "0", typeof(InitialState))]
        public void ShouldUpdateDisplayValueAndSetStateIfNecessary(
            CalculatorButton button, string expectedDisplayValue, Type expectedState)
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.State = new FirstOperandState(calculator, 4, "4");

            // Act
            calculator.PressButton(button);

            // Assert
            VerifyStateAndDisplayValue(calculator, expectedDisplayValue, expectedState);
        }
    }
}