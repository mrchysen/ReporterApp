using ReporterApp.Core.Reports;
using ReporterApp.Core.Utils;
using ReporterApp.WindowApp.Pages.NewDesign.StartPage.Commands;
using ReporterApp.WindowApp.Utils;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.StartPage;

public class StartPageViewModel : ViewModelBase
{
    private ViewModelMediator _mediator;

    private ICommand _gasReportChosenCommand;
    private ICommand _defaultReportChosenCommand;
    private ICommand _gasAndDefaultReportChosenCommand;
    
    public StartPageViewModel(
        ViewModelMediator mediator,
        PageNavigatorService pageNavigatorService)
    {
        _gasReportChosenCommand = 
            new ReportChooseCommand<GasReportBuilder>(pageNavigatorService, mediator);
        _defaultReportChosenCommand = 
            new ReportChooseCommand<DefaultReportBuilder>(pageNavigatorService, mediator);
        _gasAndDefaultReportChosenCommand = 
            new ReportChooseCommand<GasAndDefaultReportBuilder>(pageNavigatorService, mediator);
        _mediator = mediator;
    }

    public bool OpenReport { get; set; }

    public ICommand DefaultReportCommand => _defaultReportChosenCommand; 

    public ICommand GasReportChosenCommand => _gasReportChosenCommand; 

    public ICommand GasAndDefaultReportChosenCommand => _gasAndDefaultReportChosenCommand; 
}
