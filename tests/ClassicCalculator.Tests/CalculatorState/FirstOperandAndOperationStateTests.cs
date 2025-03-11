using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class FirstOperandAndOperationStateTests : CalculatorTestsBase
    {
        [Theory]
        [InlineData(CalculatorButton.Zero, "0", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.One, "1", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.Decimal, "0.", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.ToggleSign, "-4", typeof(BothOperandsAndOperationState))]
        [InlineData(CalculatorButton.Add, "4", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Subtract, "4", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Multiply, "4", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Divide, "4", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Equals, "8", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.Percentage, "4.16", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.SquareRoot, "2", typeof(BothOperandsAndOperationState))]
        [InlineData(CalculatorButton.Clear, "0", typeof(InitialState))]
        public void ShouldUpdateDisplayValueAndSetStateIfNecessary(
            CalculatorButton button, string expectedDisplayValue, Type expectedState)
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.State = new FirstOperandAndOperationState(calculator, 4, OperationType.Add, "4");

            // Act
            calculator.PressButton(button);

            // Assert
            VerifyStateAndDisplayValue(calculator, expectedDisplayValue, expectedState);
        }
    }
}