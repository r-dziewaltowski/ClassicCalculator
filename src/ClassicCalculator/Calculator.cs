using ClassicCalculator.CalculatorState;

namespace ClassicCalculator
{
    public class Calculator : ICalculator
    {
        public ICalculatorState State { get; set; }

        public string DisplayValue => State.DisplayValue;

        public Calculator()
        {
            State = new InitialState(this);
        }

        public void PressButton(CalculatorButton button)
        {
            switch (button)
            {
                case CalculatorButton.Zero:
                case CalculatorButton.One:
                case CalculatorButton.Two:
                case CalculatorButton.Three:
                case CalculatorButton.Four:
                case CalculatorButton.Five:
                case CalculatorButton.Six:
                case CalculatorButton.Seven:
                case CalculatorButton.Eight:
                case CalculatorButton.Nine:
                    AppendDigit((int)button);
                    break;
                case CalculatorButton.Decimal:
                    State.AppendDecimal();
                    break;
                case CalculatorButton.Add:
                    State.SetOperation(OperationType.Add);
                    break;
                case CalculatorButton.Subtract:
                    State.SetOperation(OperationType.Subtract);
                    break;
                case CalculatorButton.Multiply:
                    State.SetOperation(OperationType.Multiply);
                    break;
                case CalculatorButton.Divide:
                    State.SetOperation(OperationType.Divide);
                    break;
                case CalculatorButton.Equals:
                    State.Calculate();
                    break;
                case CalculatorButton.Clear:
                    State.Clear();
                    break;
                case CalculatorButton.Percentage:
                    State.CalculatePercentage();
                    break;
                case CalculatorButton.SquareRoot:
                    State.CalculateSquareRoot();
                    break;
                case CalculatorButton.ToggleSign:
                    State.ToggleSign();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(button), "Invalid calculator button.");
            }
        }

        private void AppendDigit(int number)
        {
            if (number < 0 || number > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 0 and 9.");
            }

            State.AppendDigit(number);
        }
    }
}
