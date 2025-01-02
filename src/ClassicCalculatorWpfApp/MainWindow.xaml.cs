using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ClassicCalculatorWpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<CalculatorViewModel>();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}



