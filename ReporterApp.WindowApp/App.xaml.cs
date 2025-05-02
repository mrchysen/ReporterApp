using Reporter.Configuration;
using ReporterApp.WindowApp.Configuration;
using ReporterApp.WindowApp.Windows.Main;
using System.IO;
using System.Windows;

namespace WpfApp;

public partial class App : Application
{
    private readonly MainWindowConfigurationService _mainWindowConfigurationService = new();
    private MainWindow _mainWindow = null!;

    public const string ApplicationName = "ReporterApp";

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ConfigureApplicationFileSystem();

        _mainWindow = new();

        _mainWindowConfigurationService.ConfigureWindow(_mainWindow);

        MainWindow = _mainWindow;

        MainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _mainWindowConfigurationService.SaveWindowConfiguration(_mainWindow);
    }

    private void ConfigureApplicationFileSystem()
    {
        Directory.CreateDirectory(FilesConfiguration.GetDataFolderPath);

        if (!File.Exists(FilesConfiguration.GetCarsNumbersFilePath))
            File.Create(FilesConfiguration.GetCarsNumbersFilePath);
    }
}
