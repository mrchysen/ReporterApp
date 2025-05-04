using ReporterApp.WindowApp.Pages.NewDesign.ReportPage;
using ReporterApp.WindowApp.Utils;

namespace ReporterApp.WindowApp.Windows.Main.Comands;

public class OpenReportPageCommand : BaseMainWindowCommand
{
    private PageNavigatorService _pageNavigatorService;
    private ViewModelMediator _mediator;

    public OpenReportPageCommand(
        PageNavigatorService pageNavigatorService,
        ViewModelMediator mediator)
    {
        _pageNavigatorService = pageNavigatorService;
        _mediator = mediator;
    }

    public override bool CanExecute(object? parameter)
        => _mediator.GetReportPageViewModel() != null;

    public override void Execute(object? parameter)
    {
        _pageNavigatorService.NavigateTo(
            new ReportPage(_mediator.GetReportPageViewModel()!.Builder, _mediator));
    }
}
