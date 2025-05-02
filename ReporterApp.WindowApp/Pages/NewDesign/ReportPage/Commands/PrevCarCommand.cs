using ReporterApp.Core.Cars;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

public class PrevCarCommand : BaseReportCommand
{
    public PrevCarCommand(CarEnumerator carEnumerator) : base(carEnumerator) { }

    public override void Execute(object? parameter)
        => _carEnumerator.MoveBack();
}
