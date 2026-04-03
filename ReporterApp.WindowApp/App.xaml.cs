using ReporterApp.WindowApp.Configuration;
using ReporterApp.WindowApp.Navigation;
using ReporterApp.WindowApp.Services;
using ReporterApp.WindowApp.Windows.Main;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReporterApp.WindowApp.Pages.NewDesign.StartPage;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage;
using ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;
using ReporterApp.WindowApp.Pages.NewDesign.FileManagementPage;

namespace ReporterApp.WindowApp;

public partial class App : Application
{
    private readonly MainWindowConfigurationService
        _mainWindowConfigurationService = new();
    private MainWindow _mainWindow = null!;
    private IServiceProvider _serviceProvider = null!;

    public const string ApplicationName = "ReporterApp";

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ConfigureApplicationFileSystem();

        _mainWindow = new MainWindow();

        _serviceProvider = ConfigureServices(_mainWindow);

        // Инициализируем MainWindow
        _mainWindow.InitializeMainWindow(
            _serviceProvider.GetRequiredService<INavigationService>(),
            _serviceProvider.GetRequiredService<MainWindowViewModel>());

        var windowConfig = _mainWindowConfigurationService.ConfigureWindow(_mainWindow);

        LaunchWeb(windowConfig);

        MainWindow = _mainWindow;

        MainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _mainWindowConfigurationService.SaveWindowConfiguration(_mainWindow);
    }

    private IServiceProvider ConfigureServices(MainWindow mainWindow)
    {
        var services = new ServiceCollection();

        // Маршрутизация
        services.AddSingleton<IPageFactory, PageFactory>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<Frame>(_ => mainWindow.Frame);

        // Сервисы
        services.AddSingleton<ICarService, CarService>();
        services.AddSingleton<IReportService, ReportService>();
        services.AddSingleton<IFileManagementService, FileManagementService>();

        // ViewModel'и
        services.AddSingleton<StartPageViewModel>();
        services.AddSingleton<ReportPageViewModel>();
        services.AddSingleton<CarNumberViewModel>();
        services.AddSingleton<FileManagementPageViewModel>();
        services.AddSingleton<MainWindowViewModel>();

        // Страницы
        services.AddSingleton<StartPage>();
        services.AddSingleton<ReportPage>();
        services.AddSingleton<CarNumberPage>();
        services.AddSingleton<FileManagementPage>();

        return services.BuildServiceProvider();
    }

    private void LaunchWeb(WindowConfiguration windowConfig)
    {
        Process.Start(new ProcessStartInfo { FileName = windowConfig.UrlOfWebToLaunch, UseShellExecute = true });
    }

    private void ConfigureApplicationFileSystem()
    {
        Directory.CreateDirectory(FilesConfiguration.GetDataFolderPath);

        Debug.WriteLine($"Created directory:{FilesConfiguration.GetDataFolderPath}");

        if (!File.Exists(FilesConfiguration.GetCarsNumbersFilePath))
        {
            File.Create(FilesConfiguration.GetCarsNumbersFilePath);

            Debug.WriteLine($"Created file:{FilesConfiguration.GetCarsNumbersFilePath}");
        }
    }
}
