using System.Windows;

namespace WpfApp;

public partial class App : Application
{
    public const string ApplicationName = "ReporterApp";

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
