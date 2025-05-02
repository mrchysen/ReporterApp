using ReporterApp.Core.Cars;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

public class Change24ETStatusCommand : BaseReportCommand
{
    public Change24ETStatusCommand(CarEnumerator carEnumerator) : base(carEnumerator) { }

    public override void Execute(object? parameter)
    {
        _carEnumerator.Current.Was24kmET = !_carEnumerator.Current.Was24kmET;
        _carEnumerator.NotifyCurrentChanged();
    }
}
