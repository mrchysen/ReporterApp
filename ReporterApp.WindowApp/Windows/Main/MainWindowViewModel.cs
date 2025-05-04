using ReporterApp.Core.Utils;
using ReporterApp.WindowApp.Utils;
using ReporterApp.WindowApp.Windows.Main.Comands;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Windows.Main;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelMediator _mediator;

    private BaseMainWindowCommand _saveReportCommand;
    private BaseMainWindowCommand _openReportSettingsCommand;
    private BaseMainWindowCommand _openReportPageCommand;
    private BaseMainWindowCommand _openReportCommand;

    public MainWindowViewModel(
        ViewModelMediator mediator,
        PageNavigatorService pageNavigatorService)
    {
        _mediator = mediator;

        _saveReportCommand = 
            new SaveReportCommand(_mediator);
        _openReportSettingsCommand = 
            new OpenReportSettingsPageCommand(pageNavigatorService, _mediator);
        _openReportPageCommand = 
            new OpenReportPageCommand(pageNavigatorService, _mediator);
        _openReportCommand = 
            new OpenReportCommand(_mediator, pageNavigatorService);
    }

    public BaseMainWindowCommand SaveReportCommand => _saveReportCommand;
    public BaseMainWindowCommand OpenReportSettingsCommand => _openReportSettingsCommand;
    public BaseMainWindowCommand OpenReportPageCommand => _openReportPageCommand;
    public BaseMainWindowCommand OpenReportCommand => _openReportCommand;
}
