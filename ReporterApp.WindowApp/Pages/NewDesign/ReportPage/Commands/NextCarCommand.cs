using ReporterApp.Core.Cars;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

public class NextCarCommand : ICommand
{
    private CarEnumerator _carEnumerator;

    public NextCarCommand(CarEnumerator carEnumerator)
    {
        _carEnumerator = carEnumerator;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true; // Тип если машин < 1, то какой смысл?

    public void Execute(object? parameter) 
        => _carEnumerator.MoveNext();
}
