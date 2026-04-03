using ReporterApp.DAL.FileAccess;
using ReporterApp.WindowApp.Configuration;
using ReporterApp.WindowApp.Navigation;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage;

public partial class ReportPage : Page, IPageWithParameter
{
    private readonly ReportPageViewModel _viewModel;

    public ReportPage(ReportPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = viewModel;

        CarIteratorComponent.DataContext = DataContext;
    }

    public void SetParameter(object? parameter)
    {
        if (parameter is bool needToReadCars && needToReadCars)
        {
            var cars = new CarsFileReader()
                .ReadOnlyNumbers(FilesConfiguration.GetCarsNumbersFilePath)
                .Cars ?? [];

            _viewModel.Cars = cars;
        }
    }
}
