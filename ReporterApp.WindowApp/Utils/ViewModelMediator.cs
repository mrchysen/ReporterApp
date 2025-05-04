using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Pages.NewDesign.FileManagementPage;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage;
using ReporterApp.WindowApp.Pages.NewDesign.StartPage;
using ReporterApp.WindowApp.Windows.Main;

namespace ReporterApp.WindowApp.Utils;

public class ViewModelMediator : IReportPageViewModelMediator, IStartPageViewModel
{
    private ReportPageViewModel? _reportPageViewModel;
    private StartPageViewModel? _startPageViewModel;
    private MainWindowViewModel? _mainWindowViewModel;
    private FileManagementPageViewModel? _fileManagementPageViewModel;

    public ReportPageViewModel? GetReportPageViewModel()
        => _reportPageViewModel;

    public StartPageViewModel? GetStartPageViewModel()
        => _startPageViewModel;

    public MainWindowViewModel? GetMainWindowViewModel()
        => _mainWindowViewModel;

    public FileManagementPageViewModel? GetFileManagementPageViewModel()
        => _fileManagementPageViewModel;

    public ReportPageViewModel CreateReportPageViewModel(
        IReportBuilder builder,
        List<Car> cars,
        bool createNew = false)
    {
        _fileManagementPageViewModel = _fileManagementPageViewModel ?? new();
        _reportPageViewModel = createNew ? new(builder, cars, this) : _reportPageViewModel ?? new(builder, cars, this);

        GetMainWindowViewModel()?.OpenReportSettingsCommand.RaiseCanExecuteChanged();
        GetMainWindowViewModel()?.OpenReportPageCommand.RaiseCanExecuteChanged();
        GetMainWindowViewModel()?.SaveReportCommand.RaiseCanExecuteChanged();

        return _reportPageViewModel;
    }

    public StartPageViewModel CreateStartPageViewModel(
        PageNavigatorService pageNavigatorService)
    {
        _startPageViewModel = _startPageViewModel ?? new(pageNavigatorService, this);

        return _startPageViewModel;
    }

    public MainWindowViewModel CreateMainWindowViewModel(
        PageNavigatorService pageNavigatorService)
    {
        _mainWindowViewModel = _mainWindowViewModel ?? new(this, pageNavigatorService);

        return _mainWindowViewModel;
    }

    public FileManagementPageViewModel CreateFileManagementPageViewModel()
    {
        _fileManagementPageViewModel = _fileManagementPageViewModel ?? new();

        return _fileManagementPageViewModel;
    }
}

internal interface IStartPageViewModel
{
    public StartPageViewModel? GetStartPageViewModel();
}

internal interface IReportPageViewModelMediator
{
    public ReportPageViewModel? GetReportPageViewModel();
}