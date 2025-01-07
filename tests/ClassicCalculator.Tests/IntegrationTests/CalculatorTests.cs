using System.Globalization;

namespace ClassicCalculator.Tests.IntegrationTests
{
    public class CalculatorTests
    {
        [Fact]
        public void PerformOperationWithZeroAsFirstOperand_ShouldShowCorrectResult() =>
            TestCalculator([
                CalculatorButton.Zero,
                CalculatorButton.Multiply,
                CalculatorButton.Five,
                CalculatorButton.Equals
            ], "0");

        [Fact]
        public void AppendDecimal_WhenDisplayIsZero_ShouldShowZeroDecimal() =>
            TestCalculator([CalculatorButton.Decimal], "0.");

        [Fact]
        public void AppendMultipleDecimals_ShouldShowSingleDecimal() =>
            TestCalculator([
                CalculatorButton.Decimal,
                CalculatorButton.Decimal
            ], "0.");

        [Fact]
        public void AppendZeroThreeTimes_ShouldShowZero() =>
            TestCalculator([
                CalculatorButton.Zero,
                CalculatorButton.Zero,
                CalculatorButton.Zero
            ], "0");

        [Fact]
        public void AppendNumberThenDecimalThenOperation_ShouldShowNumberWithoutDecimal() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.Decimal,
                CalculatorButton.Add
            ], "5");

        [Fact]
        public void AppendNumberThenDecimalThenZeroThenOperation_ShouldShowNumberWithoutDecimal() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.Decimal,
                CalculatorButton.Zero,
                CalculatorButton.Add
            ], "5");

        [Fact]
        public void DivideByZero_ShouldShowCannotDivideByZero() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.Divide,
                CalculatorButton.Zero,
                CalculatorButton.Equals
            ], "Cannot divide by 0");

        [Fact]
        public void PerformOperation_ShouldReplacePreviousOperation() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.Add,
                CalculatorButton.Subtract
            ], "5");

        [Theory]
        [InlineData(CalculatorButton.Add, "11")]
        [InlineData(CalculatorButton.Subtract, "9")]
        [InlineData(CalculatorButton.Multiply, "1")]
        [InlineData(CalculatorButton.Divide, "100")]
        public void CalculatePercentage_ShouldShowCorrectResult(CalculatorButton operation, string expectedDisplayValue) =>
            TestCalculator([
                CalculatorButton.One,
                CalculatorButton.Zero,
                operation,
                CalculatorButton.One,
                CalculatorButton.Zero,
                CalculatorButton.Percentage
            ], expectedDisplayValue);

        [Fact]
        public void CalculateSquareRoot_ShouldShowCorrectResult() =>
            TestCalculator([
                CalculatorButton.Nine,
                CalculatorButton.SquareRoot
            ], "3");

        [Fact]
        public void CalculateSquareRoot_ShouldShowInvalidInput() =>
            TestCalculator([
                CalculatorButton.Nine,
                CalculatorButton.ToggleSign,
                CalculatorButton.SquareRoot
            ], "Invalid input");

        [Fact]
        public void ToggleSign_ShouldShowCorrectResult() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.ToggleSign
            ], "-5");

        [Fact]
        public void Clear_ShouldResetCalculator() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.Clear
            ], "0");

        [Fact]
        public void PerformAddition_ShouldShowCorrectResult() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.Add,
                CalculatorButton.Three,
                CalculatorButton.Equals
            ], "8");

        [Fact]
        public void PerformSubtraction_ShouldShowCorrectResult() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.Subtract,
                CalculatorButton.Three,
                CalculatorButton.Equals
            ], "2");

        [Fact]
        public void PerformMultiplication_ShouldShowCorrectResult() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.Multiply,
                CalculatorButton.Three,
                CalculatorButton.Equals
            ], "15");

        [Fact]
        public void PerformDivision_ShouldShowCorrectResult() =>
            TestCalculator([
                CalculatorButton.Six,
                CalculatorButton.Divide,
                CalculatorButton.Three,
                CalculatorButton.Equals
            ], "2");

        [Fact]
        public void PerformMultipleOperations_ShouldShowCorrectResult() =>
            TestCalculator([
                CalculatorButton.Five,
                CalculatorButton.Add,
                CalculatorButton.Three,
                CalculatorButton.Equals,
                CalculatorButton.Multiply,
                CalculatorButton.Two,
                CalculatorButton.Equals
            ], "16");

        [Fact]
        public void SubtractLargerFromSmaller_ShouldShowNegativeResult() => 
            TestCalculator([
                CalculatorButton.Two,
                CalculatorButton.Subtract,
                CalculatorButton.Five,
                CalculatorButton.Equals
            ], "-3");

        [Theory]
        [InlineData(CalculatorButton.Add, "-0.0000000000000000000000000002")]
        [InlineData(CalculatorButton.Subtract, "0")]
        [InlineData(CalculatorButton.Divide, "1")]
        public void PerformFloatingPointOperation_ShouldShowCorrectResult(CalculatorButton operation, string expectedDisplayValue)
        {
            const decimal SmallestFractionPossible = -0.0000000000000000000000000001m;
            var operandButtons = ConvertNumberToButtonSequence(SmallestFractionPossible);
            var buttonsPressed = 
                operandButtons
                .Concat([operation])
                .Concat(operandButtons)
                .Concat([CalculatorButton.Equals]);

            TestCalculator(buttonsPressed, expectedDisplayValue);
        }

        private static void TestCalculator(IEnumerable<CalculatorButton> buttonsPressed, string expectedDisplayValue)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            foreach (var button in buttonsPressed)
            {
                calculator.PressButton(button);
            }

            // Assert
            Assert.Equal(expectedDisplayValue, calculator.DisplayValue);
        }

        private static List<CalculatorButton> ConvertNumberToButtonSequence(decimal number)
        {
            var negativeNumber = false;
            var result = new List<CalculatorButton>();

            var digits = number.ToString("0.#############################", CultureInfo.InvariantCulture);
            foreach (var digit in digits)
            {
                switch (digit)
                {
                    case '-':
                        negativeNumber = true;
                        break;
                    case '.':
                        result.Add(CalculatorButton.Decimal);
                        break;
                    default:
                        result.Add((CalculatorButton)int.Parse(digit.ToString()));
                        break;
                }
            }

            if (negativeNumber)
            {
                result.Add(CalculatorButton.ToggleSign);
            }

            return result;
        }
    }
}
