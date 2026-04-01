using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL.FileAccess;
using Reporter.Configuration;
using ReporterApp.DAL.FileAccess;
using ReporterApp.WindowApp.Utils;
using System.Windows;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;

public partial class CarNumberViewModel : ObservableObject
{
    private readonly IViewModelMediator _mediator;
    private readonly ICarsFileWriter _carsFileWriter;

    public CarNumberViewModel(
        IViewModelMediator mediator,
        ICarsFileWriter? carsFileWriter = null)
    {
        _mediator = mediator;
        _carsFileWriter = carsFileWriter ?? new CarsFileWriter();

        var cars = new CarsFileReader()
                .ReadOnlyNumbers(FilesConfiguration.GetCarsNumbersFilePath)
                .Cars ?? [];

        NumberText = string.Join("\r\n", cars.Select(x => x.Number));

        SaveCommand = new RelayCommand(Save);
    }

    [ObservableProperty]
    private string _numberText = string.Empty;

    [ObservableProperty]
    private Visibility _imageVisibility = Visibility.Hidden;

    public ICommand SaveCommand { get; }

    private void Save()
    {
        var carsWithNewNumbers = NumberText
            .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(num => new Car() { Number = num })
            .ToList();

        MergeActualCarsWithNew(_mediator.ReportPageViewModel.Cars, carsWithNewNumbers);

        _mediator.SetCars(carsWithNewNumbers, true);

        var carsFileWriter = _carsFileWriter
            .WriteCarNumbers(carsWithNewNumbers, FilesConfiguration.GetCarsNumbersFilePath);

        ImageVisibility = Visibility.Visible;
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
