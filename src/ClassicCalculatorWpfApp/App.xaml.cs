using ClassicCalculator;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ClassicCalculatorWpfApp
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<ICalculator, Calculator>();
            services.AddTransient<CalculatorViewModel>();
            services.AddTransient<MainWindow>();
        }
    }
}
