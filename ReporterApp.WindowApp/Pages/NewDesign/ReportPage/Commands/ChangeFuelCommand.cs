using ReporterApp.Core.Cars;
using ReporterApp.WindowApp.Windows;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

public class ChangeFuelCommand : BaseReportCommand
{
    public ChangeFuelCommand(CarEnumerator carEnumerator) : base(carEnumerator) { }

    public override void Execute(object? parameter)
    {
        FuelDialog fuelDialog = new FuelDialog(_carEnumerator.Current);

        fuelDialog.ShowDialog();

        _carEnumerator.NotifyCurrentChanged();
    }
}
