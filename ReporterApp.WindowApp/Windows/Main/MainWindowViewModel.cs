using ReporterApp.Core.Utils;
using ReporterApp.WindowApp.Utils;
using ReporterApp.WindowApp.Windows.Main.Comands;
using ReporterApp.WindowApp.Windows.Main.Commands;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Windows.Main;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelMediator _mediator;

    private BaseMainWindowCommand _saveReportCommand;
    private BaseMainWindowCommand _openReportSettingsPageCommand;
    private BaseMainWindowCommand _openReportPageCommand;
    private BaseMainWindowCommand _openReportCommand;
    private BaseMainWindowCommand _copyToClipboardCommand;
    private BaseMainWindowCommand _resetCarsCommand;

    public MainWindowViewModel(
        ViewModelMediator mediator,
        PageNavigatorService pageNavigatorService)
    {
        _mediator = mediator;

        _saveReportCommand = 
            new SaveReportCommand(_mediator);
        _openReportSettingsPageCommand = 
            new OpenReportSettingsPageCommand(pageNavigatorService, _mediator);
        _openReportPageCommand = 
            new OpenReportPageCommand(pageNavigatorService, _mediator);
        _openReportCommand = 
            new OpenReportCommand(_mediator, pageNavigatorService);
        _copyToClipboardCommand = new CopyToClipboardCommand(_mediator);
        _resetCarsCommand = new ResetCarsCommand(_mediator);
    }

    public BaseMainWindowCommand SaveReportCommand => _saveReportCommand;
    public BaseMainWindowCommand OpenReportSettingsPageCommand => _openReportSettingsPageCommand;
    public BaseMainWindowCommand OpenReportPageCommand => _openReportPageCommand;
    public BaseMainWindowCommand OpenReportCommand => _openReportCommand;
    public BaseMainWindowCommand CopyToClipboardCommand => _copyToClipboardCommand;
    public BaseMainWindowCommand ResetCarsCommand => _resetCarsCommand;
}
