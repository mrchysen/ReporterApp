using ReporterApp.Core.Cars;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

class WorkStatusChangeCommand : ICommand
{
    private CarEnumerator _carEnumerator;

    public WorkStatusChangeCommand(CarEnumerator carEnumerator)
    {
        _carEnumerator = carEnumerator;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter)
    {
        _carEnumerator.Current.IsWorked = !_carEnumerator.Current.IsWorked;
        _carEnumerator.NotifyCurrentChanged();
    }
}
