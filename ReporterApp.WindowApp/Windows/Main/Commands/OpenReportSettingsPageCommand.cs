using ReporterApp.WindowApp.Pages.NewDesign.FileManagementPage;
using ReporterApp.WindowApp.Utils;

namespace ReporterApp.WindowApp.Windows.Main.Comands;

public class OpenReportSettingsPageCommand : BaseMainWindowCommand
{
    private PageNavigatorService _pageNavigatorService;
    private ViewModelMediator _mediator;

    public OpenReportSettingsPageCommand(
        PageNavigatorService pageNavigatorService, 
        ViewModelMediator mediator)
    {
        _pageNavigatorService = pageNavigatorService;
        _mediator = mediator;
    }

    public override bool CanExecute(object? parameter)
        => _mediator.ReportPageViewModel.Builder != null;

    public override void Execute(object? parameter)
    {
        _pageNavigatorService.NavigateTo(
            new FileManagementPage(_mediator));
    }
}
