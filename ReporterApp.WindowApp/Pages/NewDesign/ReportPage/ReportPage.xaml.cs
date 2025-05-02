using DAL.FileAccess;
using Reporter.Configuration;
using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage;

public partial class ReportPage : Page
{
    private readonly BaseReportBuilder _builder;

    public ReportPage(BaseReportBuilder builder)
    {
        InitializeComponent();

        _builder = builder;

        var cars = new CarsFileReader()
            .ReadOnlyNumbers(FilesConfiguration.GetCarsNumbersFilePath)
            .Cars ?? [];

        DataContext = new ReportPageViewModel(_builder, cars);

        CarIteratorComponent.DataContext = DataContext;
    }
}
