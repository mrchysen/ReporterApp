using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;

namespace ReporterApp.Core.Managers;

public class AppManager
{
    private ReportParams _reportParams;
    private BaseReportBuilder _reportBuilder;
    private List<Car> _cars;
    private bool _saved = false;
    private CarEnumerator _carIteratorClass;

    public bool CarsChanged { get; set; } = false;

    public AppManager(List<Car> cars) 
    {
        _cars = cars;
        _carIteratorClass = new(_cars);
        _reportBuilder = new DefaultReportBuilder(ReportParams);
    }

    public AppManager(AppManager prototype) : this(prototype.Cars)
    {
        CarsChanged = prototype.CarsChanged;
    }

    public AppManager(AppManager prototype, BaseReportBuilder eeportBuilder) : this(prototype)
    {
        _reportBuilder = eeportBuilder;
    }

    public string? TypeOfReport => _reportBuilder.GetReportType();

    public bool IsSetCars 
    {
        get
        {
            return _cars == null;
        } 
    }

    public bool IsSaved
    {
        get
        {
            return _saved;
        }
        set
        {
            _saved = value;
            NotifyPropertyChanged();
        }
    } 

    public List<Car> Cars
    {
        get
        {
            return _cars;
        }
        set
        {
            _cars = value;
            _carIteratorClass = new(value);
            NotifyPropertyChanged(new List<string>()
            {
                "Cars",
                "IsSetCars"
            });
        }
    }
    public CarEnumerator Iterator
    {
        get
        {
            return _carIteratorClass;
        }
    }
    public BaseReportBuilder? Builder => _reportBuilder;

    public ReportParams ReportParams { get => _reportParams; set => _reportParams = value; }

    // For DataBindings \\
    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void NotifyPropertyChanged(List<string> propertyNames)
    {
        propertyNames.ForEach(method => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(method)));
    }
}
