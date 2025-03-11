using ClassicCalculator;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ClassicCalculatorWpfApp
{
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddTransient<ICalculator, Calculator>(provider => new Calculator(displayLength: 10))
                    .AddTransient<CalculatorViewModel>()
                    .BuildServiceProvider());
        }
    }
}
