using ReporterApp.Core.Utils;

namespace ReporterApp.WindowApp.Pages.NewDesign.FileManagementPage;

public class FileManagementPageViewModel : ViewModelBase
{
    private DateTime _reportDate = DateTime.Now.AddDays(-1);

    public DateTime ReportDate 
    { 
        get => _reportDate;
        set
        {
            _reportDate = value;
            NotifyPropertyChanged();
        }
    }
}
