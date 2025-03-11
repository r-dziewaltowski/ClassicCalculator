namespace ClassicCalculator.CalculatorState.Exceptions
{
    // Used to indicate that the result of the operation exceeds the maximum display length
    internal class DisplayLengthExceededException(string message) : Exception(message)
    {
    }
}
