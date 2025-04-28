using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage;

public partial class ReportPage : Page
{
    private readonly BaseReportBuilder _builder;
    private List<Car> _cars = new(); 

    public ReportPage(BaseReportBuilder builder)
    {
        InitializeComponent();

        _builder = builder;

        DataContext = new ReportPageViewModel(_builder, [
            new(){
                Number = "111",
                IsWorked=true
            },
            new(){
                Number = "2"
            },
            new(){
                Number = "3111"
            }
            ]);

        CarIteratorComponent.DataContext = DataContext;
    }
}
