using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;
using ReporterApp.Core.Utils;

namespace ReporterApp.WindowApp.ViewModels;

public class ReportPageViewModel : ViewModelBase
{
    private BaseReportBuilder _reportBuilder;

    public ReportPageViewModel(List<Car> cars, BaseReportBuilder reportBuilder)
    {
        _reportBuilder = reportBuilder;

        CarEnumerator = new CarEnumerator(cars);
        Cars = cars;

        Text = _reportBuilder.AddBodyReportText(Cars).Build().ToString();

        PropertyChanged += (o, e) =>
        {
            if (e.PropertyName == nameof(CarEnumerator.Current))
            {
                Text = _reportBuilder.AddBodyReportText(Cars).Build().ToString();
                NotifyPropertyChanged("Text");
            }
        };
    }

    public CarEnumerator CarEnumerator { get; set; }

    public List<Car> Cars { get; set; }

    public string Text { get; set; }
}
