using ReporterApp.Core.Cars;
using ReporterApp.WindowApp.Windows;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

public class ChangeParkingCommand : BaseReportCommand
{
    public ChangeParkingCommand(CarEnumerator carEnumerator) : base(carEnumerator) { }

    public override void Execute(object? parameter)
    {
        ParkingDialog parkingDialog = new ParkingDialog(_carEnumerator.Current);

        parkingDialog.ShowDialog();

        _carEnumerator.NotifyCurrentChanged();
    }
}
