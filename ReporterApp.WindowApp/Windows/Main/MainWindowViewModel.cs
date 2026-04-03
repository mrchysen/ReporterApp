using CommunityToolkit.Mvvm.ComponentModel;
using ReporterApp.WindowApp.Navigation;
using ReporterApp.WindowApp.Services;
using ReporterApp.WindowApp.Windows.Main.Commands;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Windows.Main;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly SaveReportCommand _saveReportCommand;
    private readonly OpenReportSettingsPageCommand _openReportSettingsPageCommand;
    private readonly OpenReportPageCommand _openReportPageCommand;
    private readonly OpenReportCommand _openReportCommand;
    private readonly CopyToClipboardCommand _copyToClipboardCommand;
    private readonly ResetCarsCommand _resetCarsCommand;
    private readonly OpenStartPageCommand _openStartPageCommand;
    private readonly OpenCarNumberPageCommand _openCarNumberPageCommand;
    private readonly OpenDataFolderCommand _openDataFolderCommand;
    private readonly OpenConfigurationFolderCommand _openConfigurationFolderCommand;

    public MainWindowViewModel(
        INavigationService navigationService,
        IReportService reportService,
        ICarService carService,
        IFileManagementService fileManagementService)
    {
        _saveReportCommand = new(reportService, carService, fileManagementService);
        _openReportSettingsPageCommand = new(navigationService, reportService);
        _openReportPageCommand = new(navigationService, reportService);
        _openReportCommand = new(carService, fileManagementService, navigationService);
        _copyToClipboardCommand = new(reportService);
        _resetCarsCommand = new(carService);
        _openStartPageCommand = new(navigationService, reportService, carService);
        _openCarNumberPageCommand = new(navigationService);
        _openDataFolderCommand = new();
        _openConfigurationFolderCommand = new();
    }

    public ICommand SaveReportCommand => _saveReportCommand;
    public ICommand OpenReportSettingsPageCommand => _openReportSettingsPageCommand;
    public ICommand OpenReportPageCommand => _openReportPageCommand;
    public ICommand OpenReportCommand => _openReportCommand;
    public ICommand CopyToClipboardCommand => _copyToClipboardCommand;
    public ICommand ResetCarsCommand => _resetCarsCommand;
    public ICommand OpenStartPageCommand => _openStartPageCommand;
    public ICommand OpenCarNumberPageCommand => _openCarNumberPageCommand;
    public ICommand OpenDataFolderCommand => _openDataFolderCommand;
    public ICommand OpenConfigurationFolderCommand => _openConfigurationFolderCommand;
}
