using ReporterApp.Core.Cars;
using ReporterApp.Core.Utils;

namespace ReporterApp.Core.Reports;

public class ReportParams : ViewModelBase
{
    private DateTime _date;

    public ReportParams(DateTime? date = null)
    {
        _date = date ?? DateTime.Now.AddDays(-1);
    }

    public DateTime Date 
    { 
        get => _date;
        set
        {
            _date = value;
            NotifyPropertyChanged();
        }
    }
}
