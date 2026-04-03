using ReporterApp.WindowApp.Navigation;
using ReporterApp.WindowApp.Services;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class OpenStartPageCommand : BaseMainWindowCommand
{
    private readonly INavigationService _navigationService;
    private readonly IReportService _reportService;
    private readonly ICarService _carService;

    public OpenStartPageCommand(
        INavigationService navigationService,
        IReportService reportService,
        ICarService carService)
    {
        _navigationService = navigationService;
        _reportService = reportService;
        _carService = carService;
    }

    public override void Execute(object? parameter)
    {
        var needToReadCar = _carService.Cars.Count == 0 || _reportService.Builder == null;
        _navigationService.NavigateTo<Pages.NewDesign.StartPage.StartPage>(needToReadCar);
    }
}
