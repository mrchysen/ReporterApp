using ReporterApp.WindowApp.Navigation;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class OpenCarNumberPageCommand : BaseMainWindowCommand
{
    private readonly INavigationService _navigationService;

    public OpenCarNumberPageCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.NavigateTo<Pages.NewDesign.CarNumberPage.CarNumberPage>();
    }
}
