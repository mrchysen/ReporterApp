using ReporterApp.Core.Cars;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

public class NextCarCommand : BaseReportCommand
{
    public NextCarCommand(CarEnumerator carEnumerator) : base(carEnumerator) { }

    public override void Execute(object? parameter) 
        => _carEnumerator.MoveNext();
}
