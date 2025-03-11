using System;

namespace ClassicCalculator.CalculatorState.Exceptions
{
    // Used to indicate that the input for the requested operation is invalid (e.g. square root of a negative number) 
    internal class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }
}
