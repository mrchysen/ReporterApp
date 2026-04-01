using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReporterApp.WindowApp.Utils;
using ReporterApp.WindowApp.Windows.Main.Comands;
using ReporterApp.WindowApp.Windows.Main.Commands;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Windows.Main;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ViewModelMediator _mediator;
    private readonly PageNavigatorService _pageNavigatorService;

    private SaveReportCommand _saveReportCommand;
    private OpenReportSettingsPageCommand _openReportSettingsPageCommand;
    private OpenReportPageCommand _openReportPageCommand;
    private OpenReportCommand _openReportCommand;
    private CopyToClipboardCommand _copyToClipboardCommand;
    private ResetCarsCommand _resetCarsCommand;
    private OpenStartPageCommand _openStartPageCommand;
    private OpenCarNumberPageCommand _openCarNumberPageCommand;
    private OpenDataFolderCommand _openDataFolderCommand;
    private OpenConfigurationFolderCommand _openConfigurationFolderCommand;

    public MainWindowViewModel(
        ViewModelMediator mediator,
        PageNavigatorService pageNavigatorService)
    {
        _mediator = mediator;
        _pageNavigatorService = pageNavigatorService;

        _saveReportCommand = new(_mediator);
        _openReportSettingsPageCommand = new(_pageNavigatorService, _mediator);
        _openReportPageCommand = new(_pageNavigatorService, _mediator);
        _openReportCommand = new(_mediator, _pageNavigatorService);
        _copyToClipboardCommand = new(_mediator);
        _resetCarsCommand = new(_mediator);
        _openStartPageCommand = new(_pageNavigatorService, _mediator);
        _openCarNumberPageCommand = new(_mediator, _pageNavigatorService);
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

    public void RaiseCanExecuteChanged()
    {
        _openReportSettingsPageCommand.RaiseCanExecuteChanged();
        _openReportPageCommand.RaiseCanExecuteChanged();
        _copyToClipboardCommand.RaiseCanExecuteChanged();
        _openStartPageCommand.RaiseCanExecuteChanged();
        _saveReportCommand.RaiseCanExecuteChanged();
    }
}
