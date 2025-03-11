namespace ClassicCalculator
{
    /// <summary>
    /// Interface representing a calculator.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Gets the current display value of the calculator.
        /// </summary>
        string DisplayValue { get; }

        /// <summary>
        /// Gets the configured length of the display, i.e. the number of digits that may be displayed.
        /// </summary>
        int DisplayLength { get; }

        /// <summary>
        /// Simulates pressing a button on the calculator.
        /// </summary>
        /// <param name="button">The button to be pressed.</param>
        /// /// <example>
        /// <code>
        /// calculator.PressButton(CalculatorButton.One);
        /// </code>
        /// </example>
        void PressButton(CalculatorButton button);
    }
}
