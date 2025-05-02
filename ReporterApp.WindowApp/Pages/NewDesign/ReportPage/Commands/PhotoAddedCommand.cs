using ReporterApp.Core.Cars;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

public class PhotoAddedCommand : BaseReportCommand
{
    public PhotoAddedCommand(CarEnumerator carEnumerator) : base(carEnumerator) { }

    public override void Execute(object? parameter)
    {
        _carEnumerator.Current.WasScreen = !_carEnumerator.Current.WasScreen;
        _carEnumerator.NotifyCurrentChanged();
    }
}
