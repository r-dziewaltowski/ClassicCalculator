using ClassicCalculator.CalculatorState;

namespace ClassicCalculator
{
    public class Calculator : ICalculator
    {
        public string DisplayValue => State.DisplayValue;
        internal ICalculatorState State { get; set; }

        public Calculator()
        {
            State = new InitialState(this);
        }

        public void PressButton(CalculatorButton button)
        {
            State.HandleButtonPressed(button);
        }
    }
}
