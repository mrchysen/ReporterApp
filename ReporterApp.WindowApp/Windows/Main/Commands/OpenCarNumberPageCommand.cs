using ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;
using ReporterApp.WindowApp.Utils;
using ReporterApp.WindowApp.Windows.Main.Comands;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class OpenCarNumberPageCommand : BaseMainWindowCommand
{
    private ViewModelMediator _mediator;
    private PageNavigatorService _pageNavigatorService;

    public OpenCarNumberPageCommand(
        ViewModelMediator mediator, 
        PageNavigatorService pageNavigatorService)
    {
        _mediator = mediator;
        _pageNavigatorService = pageNavigatorService;
    }

    public override void Execute(object? parameter)
    {
        _pageNavigatorService.NavigateTo(new CarNumberPage(_mediator));
    }
}
