using ClassicCalculator.CalculatorState;
using Microsoft.Extensions.Logging;
using Moq;

namespace ClassicCalculator.Tests
{
    public class ErrorHandlingAndLoggingTests : CalculatorTestsBase
    {
        [Theory]
        [InlineData(0)]
        [InlineData(Calculator.MaxDisplayLength + 1)]
        public void ShouldThrowAndLog_WhenIncorrectDisplayLengthProvided(int displayLength)
        {
            // Act
            var act = () => CreateCalculator(displayLength);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
            VerifyLogging(LogLevel.Error, 1);
        }

        [Fact]
        public void ShouldHandleUnexpectedErrors()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            calculator.PressButton((CalculatorButton)(-1));

            // Assert
            VerifyStateAndDisplayValue<InvalidState>(calculator, "Unexpected error");
        }

        [Fact]
        public void ShouldLogUnexpectedErrors()
        {
            // Arrange
            var calculator = CreateCalculator();
            LoggerMock.Invocations.Clear();

            // Act
            calculator.PressButton((CalculatorButton)(-1));

            // Assert
            VerifyLogging(LogLevel.Error, 1);
        }

        [Fact]
        public void ShouldLogPressedButtonsAndDisplayValue()
        {
            // Arrange
            var calculator = CreateCalculator();
            LoggerMock.Invocations.Clear();

            // Act
            calculator.PressButton(CalculatorButton.One);
            calculator.PressButton(CalculatorButton.Add);

            // Assert
            VerifyLogging(LogLevel.Information, 4);
        }

        private void VerifyLogging(LogLevel logLevel, int times)
        {
            LoggerMock.Verify(logger => logger.Log(
                    logLevel,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, @type) => true),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Exactly(times));
        }
    }
}
