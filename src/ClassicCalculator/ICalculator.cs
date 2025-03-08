namespace ClassicCalculator
{
    public interface ICalculator
    {
        string DisplayValue { get; }

        int DisplayLength { get; }

        public void PressButton(CalculatorButton button);
    }
}
