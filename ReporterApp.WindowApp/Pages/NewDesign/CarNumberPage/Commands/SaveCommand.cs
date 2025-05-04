using ReporterApp.WindowApp.Utils;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using DAL.FileAccess;
using Reporter.Configuration;

namespace ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage.Commands;

public class SaveCommand : ICommand
{
    private CarNumberViewModel _carNumberViewModel;
    private ViewModelMediator _mediator;

    public SaveCommand(CarNumberViewModel carNumberViewModel, ViewModelMediator mediator)
    {
        _carNumberViewModel = carNumberViewModel;
        _mediator = mediator;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        var cars = _carNumberViewModel.NumberText
            .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(num => new Car() { Number = num })
            .ToList();

        _mediator.SetCars(cars, true);

        var carsFileWriter = new CarsFileWriter();

        carsFileWriter.WriteCarNumbers(cars, FilesConfiguration.GetCarsNumbersFilePath);

        _carNumberViewModel.Visibility = Visibility.Visible;
    }
}
