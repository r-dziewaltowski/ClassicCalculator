using ClassicCalculator.CalculatorState;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ClassicCalculator
{
    public class Calculator : ICalculator
    {
        public string DisplayValue => State.DisplayValue;
        internal CalculatorStateBase State { get; set; }
        private readonly ILogger<Calculator> _logger;

        public Calculator(ILogger<Calculator>? logger = null)
        {
            _logger = logger ?? NullLogger<Calculator>.Instance;
            State = new InitialState(this);
        }

        public void PressButton(CalculatorButton button)
        {
            State.HandleButtonPressed(button);
        }
    }
}
