using ReporterApp.Core.Cars;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

public abstract class BaseReportCommand : ICommand
{
    protected CarEnumerator _carEnumerator;

    protected BaseReportCommand(CarEnumerator carEnumerator)
    {
        _carEnumerator = carEnumerator;
    }

    public event EventHandler? CanExecuteChanged;

    public virtual bool CanExecute(object? parameter) => true;

    public abstract void Execute(object? parameter);
}
