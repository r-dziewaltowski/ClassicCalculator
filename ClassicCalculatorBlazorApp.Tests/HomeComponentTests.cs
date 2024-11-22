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

        [Fact]
        public void ClickingDigit1Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-1-button", "1");
        }

        [Fact]
        public void ClickingDigit2Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-2-button", "2");
        }

        [Fact]
        public void ClickingDigit3Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-3-button", "3");
        }

        [Fact]
        public void ClickingDigit4Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-4-button", "4");
        }

        [Fact]
        public void ClickingDigit5Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-5-button", "5");
        }

        [Fact]
        public void ClickingDigit6Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-6-button", "6");
        }

        [Fact]
        public void ClickingDigit7Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-7-button", "7");
        }

        [Fact]
        public void ClickingDigit8Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-8-button", "8");
        }

        [Fact]
        public void ClickingDigit9Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-9-button", "9");
        }

        [Fact]
        public void ClickingDigit0Button_ShouldCallAppendDigit()
        {
            VerifyDigitButtonClick("#digit-0-button", "0");
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

        [Fact]
        public void ClickingDivideButton_ShouldCallSetOperation()
        {
            VerifyOperationButtonClick("#divide-button", OperationType.Divide);
        }

        [Fact]
        public void ClickingMultiplyButton_ShouldCallSetOperation()
        {
            VerifyOperationButtonClick("#multiply-button", OperationType.Multiply);
        }

        [Fact]
        public void ClickingSubtractButton_ShouldCallSetOperation()
        {
            VerifyOperationButtonClick("#subtract-button", OperationType.Subtract);
        }

        [Fact]
        public void ClickingAddButton_ShouldCallSetOperation()
        {
            VerifyOperationButtonClick("#add-button", OperationType.Add);
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

        private void VerifyDigitButtonClick(string buttonId, string digit)
        {
            VerifyButtonClick(buttonId, c => c.AppendDigit(digit));
        }

        private void VerifyOperationButtonClick(string buttonId, OperationType operationType)
        {
            VerifyButtonClick(buttonId, c => c.SetOperation(operationType));
        }
    }
}


