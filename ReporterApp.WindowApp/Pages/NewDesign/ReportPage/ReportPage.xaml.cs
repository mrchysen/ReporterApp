using DAL.FileAccess;
using Reporter.Configuration;
using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Utils;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage;

public partial class ReportPage : Page
{
    private readonly ViewModelMediator _mediator;
    private readonly IReportBuilder _builder;

    public ReportPage(
        IReportBuilder builder, 
        ViewModelMediator mediator)
    {
        InitializeComponent();

        _builder = builder;
        _mediator = mediator;

        var cars = new CarsFileReader()
            .ReadOnlyNumbers(FilesConfiguration.GetCarsNumbersFilePath)
            .Cars ?? [];

        DataContext = mediator.CreateReportPageViewModel(_builder, cars);

        CarIteratorComponent.DataContext = DataContext;
    }
}
