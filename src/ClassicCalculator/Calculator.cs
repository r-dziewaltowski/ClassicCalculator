using ClassicCalculator.CalculatorState;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ClassicCalculator
{
    /// <summary>
    /// Represents a classic calculator.
    /// </summary>
    public class Calculator : ICalculator
    {
        /// <summary>
        /// The maximum possible length of the display, i.e. the number of digits that may be displayed.
        /// </summary>
        public const int MaxDisplayLength = 29;

        /// <summary>
        /// Gets the current display value of the calculator.
        /// </summary>
        public string DisplayValue => State.DisplayValue;

        /// <summary>
        /// Gets the configured length of the display, i.e. the number of digits that may be displayed.
        /// </summary>
        public int DisplayLength { get; }

        /// <summary>
        /// Gets or sets the current state of the calculator.
        /// </summary>
        internal CalculatorStateBase State { get; set; }

        /// <summary>
        /// Gets the logger instance for logging calculator operations.
        /// </summary>
        internal ILogger<Calculator> Logger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Calculator"/> class.
        /// </summary>
        /// <param name="displayLength">The display length of the calculator, i.e. the number of digits that may be displayed.</param>
        /// <param name="logger">The logger instance for logging calculator operations.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the display length is out of the valid range (1 to <see cref="Calculator.MaxDisplayLength"/>).</exception>
        /// <example>
        /// <code>
        /// var calculator = new Calculator(10);
        /// </code>
        /// </example>
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

        /// <summary>
        /// Simulates pressing a button on the calculator.
        /// </summary>
        /// <param name="button">The button to be pressed.</param>
        public void PressButton(CalculatorButton button)
        {
            Logger.LogInformation("Button pressed: {Button}", button);
            State.HandleButtonPressed(button);
            Logger.LogInformation("Display value: {DisplayValue}", DisplayValue);
        }
    }
}
