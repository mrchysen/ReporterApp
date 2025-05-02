using ReporterApp.Core.Cars;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

class WorkStatusChangeCommand : BaseReportCommand
{
    public WorkStatusChangeCommand(CarEnumerator carEnumerator) : base(carEnumerator) { }
    
    public override void Execute(object? parameter)
    {
        _carEnumerator.Current.IsWorked = !_carEnumerator.Current.IsWorked;
        _carEnumerator.NotifyCurrentChanged();
    }
}
