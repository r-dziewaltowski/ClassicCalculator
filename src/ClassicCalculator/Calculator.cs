using ClassicCalculator.CalculatorState;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ClassicCalculator
{
    public class Calculator : ICalculator
    {
        public string DisplayValue => State.DisplayValue;

        internal CalculatorStateBase State { get; set; }
        internal readonly ILogger<Calculator> Logger;

        public Calculator(ILogger<Calculator>? logger = null)
        {
            Logger = logger ?? NullLogger<Calculator>.Instance;
            State = new InitialState(this);
        }

        public void PressButton(CalculatorButton button)
        {
            Logger.LogInformation("Button pressed: {Button}", button);
            State.HandleButtonPressed(button);
        }
    }
}
