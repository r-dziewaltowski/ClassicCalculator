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

        public void AppendDigit(int number)
        {            
            if (number < 0 || number > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 0 and 9.");
            }

            State.AppendDigit(number);
        }

        public void AppendDecimal() => State.AppendDecimal();
        public void SetOperation(OperationType operation) => State.SetOperation(operation);
        public void Calculate() => State.Calculate();
        public void Clear() => State.Clear();
        public void CalculatePercentage() => State.CalculatePercentage();
        public void CalculateSquareRoot() => State.CalculateSquareRoot();
        public void ToggleSign() => State.ToggleSign();
    }
}
