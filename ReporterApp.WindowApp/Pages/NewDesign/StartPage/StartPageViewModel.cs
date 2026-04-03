using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Navigation;
using ReporterApp.WindowApp.Services;

namespace ReporterApp.WindowApp.Pages.NewDesign.StartPage;

public partial class StartPageViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IReportService _reportService;

    public StartPageViewModel(
        INavigationService navigationService,
        IReportService reportService)
    {
        _navigationService = navigationService;
        _reportService = reportService;
    }

    [ObservableProperty]
    private bool _needToReadCar = true;

    [RelayCommand]
    private void DefaultReport()
    {
        _reportService.SetBuilder(new DefaultReportBuilder());
        NavigateToReportPage();
    }

    [RelayCommand]
    private void GasReport()
    {
        _reportService.SetBuilder(new GasReportBuilder());
        NavigateToReportPage();
    }

    [RelayCommand]
    private void GasAndDefaultReport()
    {
        _reportService.SetBuilder(new GasAndDefaultReportBuilder());
        NavigateToReportPage();
    }

    private void NavigateToReportPage()
    {
        _navigationService.NavigateTo<ReportPage.ReportPage>(NeedToReadCar);
        NeedToReadCar = false;
    }
}
