using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Utils;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.StartPage.Commands;

public class ReportChooseCommand<T> : ICommand where T : IReportBuilder, new()
{
    private ViewModelMediator _mediator;
    private PageNavigatorService _navigationServie;

    public ReportChooseCommand(
        PageNavigatorService navigationServie, 
        ViewModelMediator mediator)
    {
        _navigationServie = navigationServie;
        _mediator = mediator;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        _navigationServie.NavigateTo(new ReportPage.ReportPage(new T(), _mediator));
    }
}
