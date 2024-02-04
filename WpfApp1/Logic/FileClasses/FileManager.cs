using KrasTechMontacApplication.Logic.Reports;
using KrasTechMontacApplication.Logic.Cars;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KrasTechMontacApplication.Logic.FileClasses;

public class FileManager : INotifyPropertyChanged
{
    // Fields \\
    protected DateTime _Date = DateTime.Now.AddDays(-1);
    protected IReportBuilder? _ReportBuilder;
    protected List<Car>? _Cars;
    protected bool Saved = false;
    protected CarIteratorClass _CarIteratorClass;

    // Constructors \\
    public FileManager(IReportBuilder ReportBuilder)
    {
        _ReportBuilder = ReportBuilder;
    }
    public FileManager() { }
    public FileManager(FileManager Prototype) 
    {
        Cars = Prototype.Cars;
    }
    public FileManager(FileManager Prototype, IReportBuilder ReportBuilder)
    {
        Cars = Prototype.Cars;
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
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void NotifyPropertyChanged(List<string> propertyNames)
    {
        propertyNames.ForEach(method => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(method)));
    }
}
