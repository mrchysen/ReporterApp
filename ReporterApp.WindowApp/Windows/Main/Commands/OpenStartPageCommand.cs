using ReporterApp.WindowApp.Pages.NewDesign.StartPage;
using ReporterApp.WindowApp.Utils;
using ReporterApp.WindowApp.Windows.Main.Comands;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class OpenStartPageCommand : BaseMainWindowCommand
{
    private PageNavigatorService _pageNavigationService;
    private ViewModelMediator _mediator;

    public OpenStartPageCommand(PageNavigatorService pageNavigationService, ViewModelMediator mediator)
    {
        _pageNavigationService = pageNavigationService;
        _mediator = mediator;
    }

    public override void Execute(object? parameter)
    {
        if(_mediator.ReportPageViewModel.Builder == null)
        {
            _mediator.SetOpenReportStatus();
        }
        
        _pageNavigationService.NavigateTo(
            new StartPage(_mediator));
    }
}
