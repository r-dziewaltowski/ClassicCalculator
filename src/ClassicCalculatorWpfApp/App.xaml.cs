using ClassicCalculator;
using System.Windows;

namespace ClassicCalculatorWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var calculator = new Calculator();
            var mainWindow = new MainWindow(calculator);
            mainWindow.Show();
        }
    }
}
