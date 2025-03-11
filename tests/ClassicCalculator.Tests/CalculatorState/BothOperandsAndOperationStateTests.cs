using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class BothOperandsAndOperationStateTests : CalculatorTestsBase
    {
        [Theory]
        [InlineData(CalculatorButton.Zero, "0", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.One, "1", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.Decimal, "0.", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.ToggleSign, "-4", typeof(BothOperandsAndOperationState))]
        [InlineData(CalculatorButton.Add, "9", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Subtract, "9", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Multiply, "9", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Divide, "9", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Equals, "9", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.Percentage, "5.2", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.SquareRoot, "2", typeof(BothOperandsAndOperationState))]
        [InlineData(CalculatorButton.Clear, "0", typeof(InitialState))]
        public void ShouldUpdateDisplayValueAndSetStateIfNecessary(
            CalculatorButton button, string expectedDisplayValue, Type expectedState)
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.State = new BothOperandsAndOperationState(calculator, 5, OperationType.Add, 4, "4");

            // Act
            calculator.PressButton(button);

            // Assert
            VerifyStateAndDisplayValue(calculator, expectedDisplayValue, expectedState);
        }
    }
}