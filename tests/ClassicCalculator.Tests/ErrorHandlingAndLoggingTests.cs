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
        public void Calculator_ShouldThrowAndLogWhenIncorrectDisplayLengthProvided(int displayLength)
        {
            // Act
            var act = () => new Calculator(displayLength, LoggerMock.Object);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
            VerifyLogging(LogLevel.Error, 1);
        }

        [Fact]
        public void Calculator_ShouldHandleUnexpectedErrors()
        {
            // Act
            Calculator.PressButton((CalculatorButton)(-1));

            // Assert
            VerifyStateSet<InvalidState>("Unexpected error");
        }

        [Fact]
        public void Calculator_ShouldLogUnexpectedErrors()
        {
            // Act
            LoggerMock.Invocations.Clear();
            Calculator.PressButton((CalculatorButton)(-1));

            // Assert
            VerifyLogging(LogLevel.Error, 1);
        }

        [Fact]
        public void Calculator_ShouldLogPressedButtons()
        {
            // Act
            LoggerMock.Invocations.Clear();
            Calculator.PressButton(CalculatorButton.One);
            Calculator.PressButton(CalculatorButton.Add);

            // Assert
            VerifyLogging(LogLevel.Information, 2);
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
