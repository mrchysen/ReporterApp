using ReporterApp.WindowApp.Navigation;
using ReporterApp.WindowApp.Services;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class OpenReportSettingsPageCommand : BaseMainWindowCommand
{
    private readonly INavigationService _navigationService;
    private readonly IReportService _reportService;

    public OpenReportSettingsPageCommand(
        INavigationService navigationService,
        IReportService reportService)
    {
        _navigationService = navigationService;
        _reportService = reportService;
    }

    public override bool CanExecute(object? parameter)
        => _reportService.Builder != null;

    public override void Execute(object? parameter)
    {
        _navigationService.NavigateTo<Pages.NewDesign.FileManagementPage.FileManagementPage>();
    }
}
