namespace ClassicCalculator.Tests.Parser
{
    public static class Parser
    {
        private static readonly Dictionary<string, CalculatorButton> _buttonMap = new()
        {
            { "C", CalculatorButton.Clear },
            { "%", CalculatorButton.Percentage },
            { "SR", CalculatorButton.SquareRoot },
            { "*", CalculatorButton.Multiply },
            { "-", CalculatorButton.Subtract },
            { "+", CalculatorButton.Add },
            { "/", CalculatorButton.Divide },
            { "+/-", CalculatorButton.ToggleSign },
            { ".", CalculatorButton.Decimal },
            { "=", CalculatorButton.Equals }
        };

        public static List<CalculatorButton> ParseInputToButtonSequence(string input)
        {
            var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var result = new List<CalculatorButton>();

            foreach (var token in tokens)
            {
                if (decimal.TryParse(token, out _))
                {
                    result.AddRange(ParseNumberToButtonSequence(token));
                }
                else
                {
                    result.Add(_buttonMap[token]);
                }
            }

            return result;
        }

        private static List<CalculatorButton> ParseNumberToButtonSequence(string number)
        {
            var negativeNumber = false;
            var result = new List<CalculatorButton>();

            foreach (var digit in number)
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
