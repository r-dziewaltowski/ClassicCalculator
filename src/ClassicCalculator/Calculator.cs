using ClassicCalculator.CalculatorState;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ClassicCalculator
{
    public class Calculator : ICalculator
    {
        public const int MaxDisplayLength = 29;

        public string DisplayValue => State.DisplayValue;
        public int DisplayLength { get; }

        internal CalculatorStateBase State { get; set; }
        internal ILogger<Calculator> Logger { get; }

        public Calculator(int displayLength, ILogger<Calculator>? logger = null)
        {
            Logger = logger ?? NullLogger<Calculator>.Instance;

            if (displayLength < 1 || displayLength > MaxDisplayLength)
            {
                Logger.LogError("Incorrect display length provided: {DisplayLength}", displayLength);
                throw new ArgumentOutOfRangeException(nameof(displayLength), displayLength, $"Display length must be between 1 and {MaxDisplayLength}.");
            }
            DisplayLength = displayLength;

            State = new InitialState(this);
        }

        public void PressButton(CalculatorButton button)
        {
            Logger.LogInformation("Button pressed: {Button}", button);
            State.HandleButtonPressed(button);
        }
    }
}
