using Bunit;
using ClassicCalculator;
using ClassicCalculatorBlazorApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq.Expressions;

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

        [Fact]
        public void HomeComponent_ShouldRenderCorrectly()
        {
            // Arrange
            _mockCalculator.Setup(c => c.DisplayValue).Returns("TestDisplayValue");
            var cut = RenderComponent<Home>();

            // Act
            var display = cut.Find("#calculator-display");

            // Assert
            Assert.Equal("TestDisplayValue", display.GetAttribute("value"));
        }

        [Theory]
        [InlineData("#digit-0-button", 0)]
        [InlineData("#digit-1-button", 1)]
        [InlineData("#digit-2-button", 2)]
        [InlineData("#digit-3-button", 3)]
        [InlineData("#digit-4-button", 4)]
        [InlineData("#digit-5-button", 5)]
        [InlineData("#digit-6-button", 6)]
        [InlineData("#digit-7-button", 7)]
        [InlineData("#digit-8-button", 8)]
        [InlineData("#digit-9-button", 9)]
        public void ClickingDigitButton_ShouldCallAppendDigit(string buttonId, int digit)
        {
            VerifyButtonClick(buttonId, c => c.AppendDigit(digit));
        }

        [Fact]
        public void ClickingClearButton_ShouldCallClear()
        {
            VerifyButtonClick("#clear-button", c => c.Clear());
        }

        [Fact]
        public void ClickingPercentageButton_ShouldCallCalculatePercentage()
        {
            VerifyButtonClick("#percentage-button", c => c.CalculatePercentage());
        }

        [Fact]
        public void ClickingSquareRootButton_ShouldCallCalculateSquareRoot()
        {
            VerifyButtonClick("#sqrt-button", c => c.CalculateSquareRoot());
        }

        [Theory]
        [InlineData("#multiply-button", OperationType.Multiply)]
        [InlineData("#subtract-button", OperationType.Subtract)]
        [InlineData("#add-button", OperationType.Add)]
        [InlineData("#divide-button", OperationType.Divide)]
        public void ClickingOperationButton_ShouldCallSetOperation(string buttonId, OperationType operationType)
        {
            VerifyButtonClick(buttonId, c => c.SetOperation(operationType));
        }

        [Fact]
        public void ClickingToggleSignButton_ShouldCallToggleSign()
        {
            VerifyButtonClick("#toggle-sign-button", c => c.ToggleSign());
        }

        [Fact]
        public void ClickingDecimalButton_ShouldCallAppendDecimal()
        {
            VerifyButtonClick("#decimal-button", c => c.AppendDecimal());
        }

        [Fact]
        public void ClickingEqualsButton_ShouldCallCalculate()
        {
            VerifyButtonClick("#equals-button", c => c.Calculate());
        }

        private void VerifyButtonClick(string buttonId, Expression<Action<ICalculator>> verifyAction)
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            cut.Find(buttonId).Click();

            // Assert
            _mockCalculator.Verify(verifyAction, Times.Once);
        }
    }
}


