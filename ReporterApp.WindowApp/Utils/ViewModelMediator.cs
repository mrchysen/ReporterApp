using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;
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
    private CarNumberViewModel _carNumberViewModel;

    public ViewModelMediator(
        PageNavigatorService pageNavigatorService)
    {
        _fileManagementPageViewModel = new();
        _reportPageViewModel = new(this);
        _startPageViewModel = new(this, pageNavigatorService);
        _mainWindowViewModel = new(this, pageNavigatorService);
        _carNumberViewModel = new(this);
    }

    public ReportPageViewModel ReportPageViewModel
        => _reportPageViewModel;

    public StartPageViewModel StartPageViewModel
        => _startPageViewModel;

    public MainWindowViewModel MainWindowViewModel
        => _mainWindowViewModel;

    public FileManagementPageViewModel FileManagementPageViewModel
        => _fileManagementPageViewModel;

    public CarNumberViewModel CarNumberViewModel
        => _carNumberViewModel;

    public void SetReportBuilder(IReportBuilder builder)
    {
        MainWindowViewModel.RaiseCanExecuteChanged();

        _reportPageViewModel.Builder = builder;
    }

    public void SetCars(List<Car> cars, bool withReset = false)
    {
        _reportPageViewModel.Cars = cars;

        if (withReset)
        {
            _reportPageViewModel.ResetCarInEnumerator();
        }
    }

    public void SetOpenReportStatus(bool needToReadCar = true)
    {
        _startPageViewModel.NeedToReadCar = needToReadCar;
    }

    public void SetDate(DateTime date) 
    {
        _fileManagementPageViewModel.ReportDate = date;
    }
}