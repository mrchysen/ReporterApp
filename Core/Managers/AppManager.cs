using System.ComponentModel;
using System.Runtime.CompilerServices;
using Core.Cars;
using Core.Reports;

namespace Core.Managers;

public class AppManager : INotifyPropertyChanged
{
    // Fields \\
    protected DateTime _Date = DateTime.Now.AddDays(-1);
    protected IReportBuilder? _ReportBuilder;
    protected List<Car> _Cars;
    protected bool Saved = false;
    protected CarIteratorClass _CarIteratorClass;

    public bool CarsChanged { get; set; } = false;

    // Constructors \\
    public AppManager(List<Car> cars) 
    { 
        Cars = cars;
    }
    public AppManager(AppManager Prototype) 
    {
        Cars = Prototype.Cars;
        CarsChanged = Prototype.CarsChanged;
    }
    public AppManager(AppManager Prototype, IReportBuilder ReportBuilder) : this(Prototype)
    {
        _ReportBuilder = ReportBuilder;
    }

    // Properties \\
    public string? TypeOfReport 
    {
        get
        {
            if(_ReportBuilder == null)
                return null;
            return _ReportBuilder.ReportType();
        }
    } 

    public DateTime Date 
    { 
        get 
        {
            return _Date;
        }
        set 
        {
            _Date = value;
            NotifyPropertyChanged();
        }
    }

    public bool IsSetCars 
    {
        get
        {
            return _Cars == null;
        } 
    }

    public bool IsSaved
    {
        get
        {
            return Saved;
        }
        set
        {
            Saved = value;
            NotifyPropertyChanged();
        }
    } 

    public List<Car>? Cars
    {
        get
        {
            return _Cars;
        }
        set
        {
            _Cars = value;
            _CarIteratorClass = new(value);
            NotifyPropertyChanged(new List<string>()
            {
                "Cars",
                "IsSetCars"
            });
        }
    }
    public CarIteratorClass Iterator
    {
        get
        {
            return _CarIteratorClass;
        }
    }
    public IReportBuilder? Builder => _ReportBuilder;

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
