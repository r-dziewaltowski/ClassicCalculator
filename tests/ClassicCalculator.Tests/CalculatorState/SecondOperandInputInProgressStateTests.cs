using ClassicCalculator.CalculatorState;

namespace ClassicCalculator.Tests.CalculatorState
{
    public class SecondOperandInputInProgressStateTests : CalculatorTestsBase
    {
        [Theory]
        [InlineData(CalculatorButton.Zero, "16.0", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.One, "16.1", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.Decimal, "16.", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.ToggleSign, "-16.", typeof(SecondOperandInputInProgressState))]
        [InlineData(CalculatorButton.Add, "-64", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Subtract, "-64", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Multiply, "-64", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Divide, "-64", typeof(FirstOperandAndOperationState))]
        [InlineData(CalculatorButton.Equals, "-64", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.Percentage, "-0.64", typeof(FirstOperandState))]
        [InlineData(CalculatorButton.SquareRoot, "4", typeof(BothOperandsAndOperationState))]
        [InlineData(CalculatorButton.Clear, "0", typeof(InitialState))]
        public void ShouldUpdateDisplayValueAndSetStateIfNecessary(
            CalculatorButton button, string expectedDisplayValue, Type expectedState)
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.State = new SecondOperandInputInProgressState(calculator, -4, OperationType.Multiply, "16.");

            // Act
            calculator.PressButton(button);

            // Assert
            VerifyStateAndDisplayValue(calculator, expectedDisplayValue, expectedState);
        }
    }
}