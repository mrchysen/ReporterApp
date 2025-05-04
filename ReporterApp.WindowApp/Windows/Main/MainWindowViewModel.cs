using ReporterApp.Core.Utils;
using ReporterApp.WindowApp.Utils;
using ReporterApp.WindowApp.Windows.Main.Comands;
using ReporterApp.WindowApp.Windows.Main.Commands;

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
    private BaseMainWindowCommand _openStartPageCommand;
    private BaseMainWindowCommand _openCarNumberPageCommand;

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
        _openStartPageCommand = new OpenStartPageCommand(pageNavigatorService, _mediator);
        _openCarNumberPageCommand = new OpenCarNumberPageCommand(_mediator, pageNavigatorService);
    }

    public BaseMainWindowCommand SaveReportCommand => _saveReportCommand;
    public BaseMainWindowCommand OpenReportSettingsPageCommand => _openReportSettingsPageCommand;
    public BaseMainWindowCommand OpenReportPageCommand => _openReportPageCommand;
    public BaseMainWindowCommand OpenReportCommand => _openReportCommand;
    public BaseMainWindowCommand CopyToClipboardCommand => _copyToClipboardCommand;
    public BaseMainWindowCommand ResetCarsCommand => _resetCarsCommand;
    public BaseMainWindowCommand OpenStartPageCommand => _openStartPageCommand;
    public BaseMainWindowCommand OpenCarNumberPageCommand => _openCarNumberPageCommand;

    public void RaiseCanExecuteChanged()
    {
        OpenReportSettingsPageCommand.RaiseCanExecuteChanged();
        OpenReportPageCommand.RaiseCanExecuteChanged();
        CopyToClipboardCommand.RaiseCanExecuteChanged();
        OpenStartPageCommand.RaiseCanExecuteChanged();
        SaveReportCommand.RaiseCanExecuteChanged();
    }
}
