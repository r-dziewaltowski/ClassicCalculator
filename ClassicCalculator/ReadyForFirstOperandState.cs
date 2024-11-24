namespace ClassicCalculator
{
    public class ReadyForFirstOperandState(
        ICalculator calculator,
        double? firstOperand,
        OperationType? currentOperation,
        double? secondOperand,  
        string displayValue) : 
        CalculatorStateBase(
            calculator, 
            firstOperand,
            currentOperation,
            secondOperand,  
            displayValue)
    {
        public override void AppendDigit(int digit)
        {
            UpdateDisplayValue(digit);
            _calculator.State = new FirstOperandBeingEnteredState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        public override void AppendDecimal()
        {
            DisplayValue = "0.";
            _calculator.State = new FirstOperandBeingEnteredState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        public override void SetOperation(OperationType operation)
        {
            _firstOperand = ConvertDisplayValueToNumber();
            _currentOperation = operation;
            _calculator.State = new ReadyForSecondOperandState(
                _calculator, _firstOperand, _currentOperation, _secondOperand, DisplayValue);
        }

        public override void Calculate()
        {
            // Assuming the calculation logic is to add the display value to the second operand
            if (_secondOperand.HasValue)
            {
                double result = _currentOperation switch
                {
                    OperationType.Add => double.Parse(DisplayValue) + _secondOperand.Value,
                    OperationType.Subtract => double.Parse(DisplayValue) - _secondOperand.Value,
                    OperationType.Multiply => double.Parse(DisplayValue) * _secondOperand.Value,
                    OperationType.Divide => double.Parse(DisplayValue) / _secondOperand.Value,
                    _ => throw new InvalidOperationException("Invalid operation")
                };

                UpdateDisplayValue(result);
            }
        }

        public override void CalculatePercentage()
        {
            ResetDisplayValue();
        }

        public override void CalculateSquareRoot()
        {
            var value = ConvertDisplayValueToNumber();
            var result = CalculateSquareRoot(value);
            UpdateDisplayValue(result);
        }
    }
}
