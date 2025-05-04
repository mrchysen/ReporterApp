using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Pages.NewDesign.FileManagementPage;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage;
using ReporterApp.WindowApp.Pages.NewDesign.StartPage;
using ReporterApp.WindowApp.Windows.Main;

namespace ReporterApp.WindowApp.Utils;

public class ViewModelMediator
{
    private ReportPageViewModel _reportPageViewModel;
    private StartPageViewModel _startPageViewModel;
    private MainWindowViewModel _mainWindowViewModel;
    private FileManagementPageViewModel _fileManagementPageViewModel;

    public ViewModelMediator(
        PageNavigatorService pageNavigatorService)
    {
        _fileManagementPageViewModel = new();
        _reportPageViewModel = new(this, new DefaultReportBuilder());
        _startPageViewModel = new(this, pageNavigatorService);
        _mainWindowViewModel = new(this, pageNavigatorService);
    }

    public ReportPageViewModel ReportPageViewModel
        => _reportPageViewModel;

    public StartPageViewModel StartPageViewModel
        => _startPageViewModel;

    public MainWindowViewModel MainWindowViewModel
        => _mainWindowViewModel;

    public FileManagementPageViewModel FileManagementPageViewModel
        => _fileManagementPageViewModel;

    public void SetReportBuilder(IReportBuilder builder)
    {
        _reportPageViewModel.Builder = builder;
    }

    public void SetCars(List<Car> cars)
    {
        _reportPageViewModel.Cars = cars;
    }

    public void SetOpenReportStatus()
    {
        _startPageViewModel.OpenReport = true;
    }

    public void SetDate(DateTime date) 
    {
        _fileManagementPageViewModel.ReportDate = date;
    }
}