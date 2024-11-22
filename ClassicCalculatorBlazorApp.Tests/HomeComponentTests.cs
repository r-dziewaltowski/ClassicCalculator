using Bunit;
using ClassicCalculator;
using ClassicCalculatorBlazorApp.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace ClassicCalculatorBlazorApp.Tests
{
    public class HomeComponentTests : TestContext
    {
        public HomeComponentTests()
        {
            // Register the Calculator service
            Services.AddSingleton<ICalculator, Calculator>();
        }

        [Fact]
        public void HomeComponent_ShouldRenderCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            var display = cut.Find("#calculator-display");

            // Assert
            Assert.Equal("0", display.GetAttribute("value"));
        }

        [Fact]
        public void ClickingDigitButton_ShouldUpdateDisplay()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            cut.Find("#digit-1-button").Click();
            var display = cut.Find("#calculator-display");

            // Assert
            Assert.Equal("1", display.GetAttribute("value"));
        }

        [Fact]
        public void ClickingClearButton_ShouldResetDisplay()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            cut.Find("#clear-button").Click();
            var display = cut.Find("#calculator-display");

            // Assert
            Assert.Equal("0", display.GetAttribute("value"));
        }

        [Fact]
        public void PerformingAddition_ShouldShowCorrectResult()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            cut.Find("#digit-1-button").Click(); // Click "1"
            cut.Find("#digit-2-button").Click(); // Click "2"
            cut.Find("#add-button").Click(); // Click "+"
            cut.Find("#digit-3-button").Click(); // Click "3"
            cut.Find("#equals-button").Click(); // Click "="
            var display = cut.Find("#calculator-display");

            // Assert
            Assert.Equal("15", display.GetAttribute("value"));
        }
    }
}
