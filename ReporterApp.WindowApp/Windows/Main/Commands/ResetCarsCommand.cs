using DAL.FileAccess;
using Reporter.Configuration;
using ReporterApp.WindowApp.Utils;
using ReporterApp.WindowApp.Windows.Main.Comands;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class ResetCarsCommand : BaseMainWindowCommand
{
    private ViewModelMediator _mediator;

    public ResetCarsCommand(ViewModelMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Execute(object? parameter)
    {
        var cars = new CarsFileReader()
            .ReadOnlyNumbers(FilesConfiguration.GetCarsNumbersFilePath)
            .Cars ?? [];

        _mediator.SetCars(cars);
        _mediator.ReportPageViewModel.ResetCarInEnumerator();
    }
}
