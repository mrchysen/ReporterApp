using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;
using ReporterApp.Core.Utils;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage;

public class ReportPageViewModel : ViewModelBase
{
    private string _reportText;
    private CarEnumerator _carEnumerator;
    private BaseReportBuilder _reportBuilder;

    private ICommand _nextCarCommand;
    private ICommand _prevCarCommand;
    private ICommand _workStatusChangeCommand;

    public ReportPageViewModel(BaseReportBuilder reportBuilder, List<Car> cars)
    {
        _reportBuilder = reportBuilder;

        _carEnumerator = new(cars);

        _nextCarCommand = new NextCarCommand(_carEnumerator);
        _prevCarCommand = new PrevCarCommand(_carEnumerator);
        _workStatusChangeCommand = new WorkStatusChangeCommand(_carEnumerator);

        ReportText = _reportBuilder.AddBodyReportText(cars, true).Build().ToString();

        _carEnumerator.PropertyChanged += (o, e) =>
        {
            if (e.PropertyName == "Current")
            {
                NotifyPropertyChanged("CurrentCarNumber");
                NotifyPropertyChanged("IsCurrentCarWorked");

                ReportText = _reportBuilder.AddBodyReportText(cars, true).Build().ToString();
            }
        };
    }

    public string CurrentCarNumber => _carEnumerator.Current.Number;
    public bool IsCurrentCarWorked => _carEnumerator.Current.IsWorked;

    public string ReportText 
    { 
        get => _reportText;
        set 
        {
            _reportText = value;
            NotifyPropertyChanged();
        }
    }

    public ICommand NextCarCommand => _nextCarCommand;
    public ICommand PrevCarCommand => _prevCarCommand;
    public ICommand WorkStatusChangeCommand => _workStatusChangeCommand;
}
