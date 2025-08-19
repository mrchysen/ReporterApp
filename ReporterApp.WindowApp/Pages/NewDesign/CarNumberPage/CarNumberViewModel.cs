using DAL.FileAccess;
using Reporter.Configuration;
using ReporterApp.Core.Utils;
using ReporterApp.DAL.FileAccess;
using ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage.Commands;
using ReporterApp.WindowApp.Utils;
using System.Windows;

namespace ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;

public interface ICarNumberViewModel
{
    string NumberText { get; set; }
    
    SaveCommand SaveCommand { get; }

    Visibility ImageVisibility { get; set; }
}

public class CarNumberViewModel : ViewModelBase, ICarNumberViewModel
{
    private ViewModelMediator _mediator;
    private SaveCommand _saveCommand;

    private string _numberText = string.Empty;
    private Visibility _imageVisibility = Visibility.Hidden;

    public CarNumberViewModel(ViewModelMediator mediator)
    {
        _mediator = mediator;

        var cars = new CarsFileReader()
                .ReadOnlyNumbers(FilesConfiguration.GetCarsNumbersFilePath)
                .Cars ?? [];

        NumberText = string.Join("\r\n", cars.Select(x => x.Number));

        _saveCommand = new SaveCommand(this, mediator, new CarsFileWriter());
    }

    public string NumberText
    {
        get => _numberText;
        set
        {
            _numberText = value;
            NotifyPropertyChanged();
        }
    }

    public SaveCommand SaveCommand => _saveCommand;

    public Visibility ImageVisibility
    {
        get => _imageVisibility;
        set
        {
            _imageVisibility = value;
            NotifyPropertyChanged();
        }
    }
}
