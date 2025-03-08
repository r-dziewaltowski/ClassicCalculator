namespace ClassicCalculator.Tests.Parser
{
    public class ParserTests
    {
        [Fact]
        public void ParseInputToButtonSequence_ShouldReturnCorrectResult()
        {
            // Act
            const string input = "-1.2 + - * / . +/- % C SR =";
            var result = Parser.ParseInputToButtonSequence(input);
            
            // Assert
            var expectedResult = new List<CalculatorButton>
            {
                CalculatorButton.One,
                CalculatorButton.Decimal,
                CalculatorButton.Two,
                CalculatorButton.ToggleSign,
                CalculatorButton.Add,
                CalculatorButton.Subtract,
                CalculatorButton.Multiply,
                CalculatorButton.Divide,
                CalculatorButton.Decimal,
                CalculatorButton.ToggleSign,
                CalculatorButton.Percentage,
                CalculatorButton.Clear,
                CalculatorButton.SquareRoot,
                CalculatorButton.Equals
            };
            Assert.Equal(expectedResult, result);
        }
    }
}
