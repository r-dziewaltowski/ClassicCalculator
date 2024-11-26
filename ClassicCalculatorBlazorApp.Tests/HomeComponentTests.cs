using Bunit;
using ClassicCalculator;
using ClassicCalculatorBlazorApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ClassicCalculatorBlazorApp.Tests
{
    public class HomeComponentTests : TestContext
    {
        private readonly Mock<ICalculator> _mockCalculator;

        public HomeComponentTests()
        {
            _mockCalculator = new Mock<ICalculator>();

            // Register the mock Calculator service
            Services.AddSingleton(_mockCalculator.Object);
        }

        [Theory]
        [InlineData("#digit-0-button", CalculatorButton.Zero)]
        [InlineData("#digit-1-button", CalculatorButton.One)]
        [InlineData("#digit-2-button", CalculatorButton.Two)]
        [InlineData("#digit-3-button", CalculatorButton.Three)]
        [InlineData("#digit-4-button", CalculatorButton.Four)]
        [InlineData("#digit-5-button", CalculatorButton.Five)]
        [InlineData("#digit-6-button", CalculatorButton.Six)]
        [InlineData("#digit-7-button", CalculatorButton.Seven)]
        [InlineData("#digit-8-button", CalculatorButton.Eight)]
        [InlineData("#digit-9-button", CalculatorButton.Nine)]
        [InlineData("#clear-button", CalculatorButton.Clear)]
        [InlineData("#percentage-button", CalculatorButton.Percentage)]
        [InlineData("#sqrt-button", CalculatorButton.SquareRoot)]
        [InlineData("#multiply-button", CalculatorButton.Multiply)]
        [InlineData("#subtract-button", CalculatorButton.Subtract)]
        [InlineData("#add-button", CalculatorButton.Add)]
        [InlineData("#divide-button", CalculatorButton.Divide)]
        [InlineData("#toggle-sign-button", CalculatorButton.ToggleSign)]
        [InlineData("#decimal-button", CalculatorButton.Decimal)]
        [InlineData("#equals-button", CalculatorButton.Equals)]
        public void ClickingButton_ShouldCallPressButtonWithCorrectArgument(string buttonId, CalculatorButton expectedButton)
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            cut.Find(buttonId).Click();

            // Assert
            _mockCalculator.Verify(c => c.PressButton(expectedButton), Times.Once);
        }
    }
}


