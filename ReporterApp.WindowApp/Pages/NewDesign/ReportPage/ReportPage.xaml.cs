using DAL.FileAccess;
using Reporter.Configuration;
using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Utils;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage;

public partial class ReportPage : Page
{
    public ReportPage(ViewModelMediator mediator, bool needToReadCars = true)
    {
        InitializeComponent();

        if (needToReadCars)
        {
            var cars = new CarsFileReader()
            .ReadOnlyNumbers(FilesConfiguration.GetCarsNumbersFilePath)
            .Cars ?? [];

            mediator.SetCars(cars);
        }

        DataContext = mediator.ReportPageViewModel;

        CarIteratorComponent.DataContext = DataContext;
    }
}
