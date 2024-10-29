using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionProcessor);
            base.OnStartup(e);
        }

        private void UnhandledExceptionProcessor(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception ?? new Exception("Пустая ошибка");
            
            MessageBox.Show(
                    "Not handled Error: " + exception.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }
    }

}
