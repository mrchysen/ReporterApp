using ReporterApp.Core.Reports;
using ReporterApp.Core.Utils;
using ReporterApp.WindowApp.Pages.NewDesign.StartPage.Commands;
using ReporterApp.WindowApp.Utils;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.StartPage;

public class StartPageViewModel : ViewModelBase
{
    private ICommand _gasReportChosenCommand;
    private ICommand _defaultReportChosenCommand;
    private ICommand _gasAndDefaultReportChosenCommand;

    public StartPageViewModel(PageNavigatorService pageNavigatorService)
    {
        _gasReportChosenCommand = new ReportChooseCommand<GasReportBuilder>(pageNavigatorService);
        _defaultReportChosenCommand = new ReportChooseCommand<DefaultReportBuilder>(pageNavigatorService);
        _gasAndDefaultReportChosenCommand = new ReportChooseCommand<GasAndDefaultReportBuilder>(pageNavigatorService);
    }

    public ICommand DefaultReportCommand => _defaultReportChosenCommand; 

    public ICommand GasReportChosenCommand => _gasReportChosenCommand; 

    public ICommand GasAndDefaultReportChosenCommand => _gasAndDefaultReportChosenCommand; 
}
