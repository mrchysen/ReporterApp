using ReporterApp.Core.Cars;
using ReporterApp.WindowApp.Windows;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

public class ChangeAddedInfoCommand : BaseReportCommand
{
    public ChangeAddedInfoCommand(CarEnumerator carEnumerator) : base(carEnumerator) { }

    public override void Execute(object? parameter)
    {
        AddInfoDialog addInfoDialog = new AddInfoDialog(_carEnumerator.Current);

        addInfoDialog.ShowDialog();

        _carEnumerator.NotifyCurrentChanged();
    }
}
