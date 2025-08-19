using ReporterApp.WindowApp.Utils;
using System.Windows;
using System.Windows.Input;
using Reporter.Configuration;
using ReporterApp.DAL.FileAccess;

namespace ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage.Commands;

public class SaveCommand : ICommand
{
    private ICarNumberViewModel _carNumberViewModel;
    private IViewModelMediator _mediator;
    private ICarsFileWriter _carsFileWriter;

    public SaveCommand(
        ICarNumberViewModel carNumberViewModel, 
        IViewModelMediator mediator,
        ICarsFileWriter carsFileWriter)
    {
        _carNumberViewModel = carNumberViewModel;
        _mediator = mediator;
        _carsFileWriter = carsFileWriter;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        var carsWithNewNumbers = _carNumberViewModel.NumberText
            .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(num => new Car() { Number = num })
            .ToList();

        MergeActualCarsWithNew(_mediator.ReportPageViewModel.Cars, carsWithNewNumbers);

        _mediator.SetCars(carsWithNewNumbers, true);

        var carsFileWriter = _carsFileWriter
            .WriteCarNumbers(carsWithNewNumbers, FilesConfiguration.GetCarsNumbersFilePath);

        _carNumberViewModel.ImageVisibility = Visibility.Visible;
    }

    private void MergeActualCarsWithNew(List<Car> oldCarsToMerge, List<Car> newCar)
    {
        foreach (var car in oldCarsToMerge) 
        { 
            var index = newCar.FindIndex(c => c.Number == car.Number);

            if (index != -1)
            {
                car.CloneTo(newCar[index]);
            }
        }
    }
}
