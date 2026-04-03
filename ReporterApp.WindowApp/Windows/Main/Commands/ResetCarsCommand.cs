using ReporterApp.DAL.FileAccess;
using ReporterApp.WindowApp.Configuration;
using ReporterApp.WindowApp.Services;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class ResetCarsCommand : BaseMainWindowCommand
{
    private readonly ICarService _carService;

    public ResetCarsCommand(ICarService carService)
    {
        _carService = carService;
    }

    public override void Execute(object? parameter)
    {
        var cars = new CarsFileReader()
            .ReadOnlyNumbers(FilesConfiguration.GetCarsNumbersFilePath)
            .Cars ?? [];

        _carService.SetCarsWithReset(cars);
    }
}
