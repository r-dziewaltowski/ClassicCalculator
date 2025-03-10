using Microsoft.Extensions.Logging;
using Moq;

namespace ClassicCalculator.Tests
{
    public class CalculatorTestsBase
    {
        protected readonly Mock<ILogger<Calculator>> LoggerMock = new();

        protected Calculator CreateCalculator(int displayLength = Calculator.MaxDisplayLength)
        {
            return new Calculator(displayLength, LoggerMock.Object);
        }

        protected static void VerifyStateAndDisplayValue(Calculator calculator, string expectedDisplayValue, Type expectedState)
        {
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
            Assert.IsType(expectedState, calculator.State);
        }

        protected static void VerifyStateAndDisplayValue<TState>(Calculator calculator, string expectedDisplayValue)
        {
            var type = typeof(TState);
            VerifyStateAndDisplayValue(calculator, expectedDisplayValue, type);
        }

        protected static void PressButtons(Calculator calculator, string input)
        {
            var buttons = Parser.Parser.ParseInputToButtonSequence(input);
            foreach (var button in buttons)
            {
                calculator.PressButton(button);
            }
        }

        protected static void PressButton(Calculator calculator, CalculatorButton button, int numberOfPresses)
        {
            for (var i = 0; i < numberOfPresses; i++)
            {
                calculator.PressButton(button);
            }
        }
    }
}
