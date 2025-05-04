using DAL.FileAccess;
using Reporter.Configuration;
using ReporterApp.Core.Utils;
using ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage.Commands;
using ReporterApp.WindowApp.Utils;
using System.Security.Permissions;
using System.Windows;

namespace ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;

public class CarNumberViewModel : ViewModelBase
{
    private ViewModelMediator _mediator;
    private SaveCommand _saveCommand;

    private string _numberText = string.Empty;
    private Visibility _visibility = Visibility.Hidden;

    public CarNumberViewModel(ViewModelMediator mediator)
    {
        _mediator = mediator;

        var cars = new CarsFileReader()
                .ReadOnlyNumbers(FilesConfiguration.GetCarsNumbersFilePath)
                .Cars ?? [];

        NumberText = string.Join("\r\n", cars.Select(x => x.Number));

        _saveCommand = new SaveCommand(this, mediator);
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

    public Visibility Visibility
    {
        get => _visibility;
        set
        {
            _visibility = value;
            NotifyPropertyChanged();
        }
    }
}
