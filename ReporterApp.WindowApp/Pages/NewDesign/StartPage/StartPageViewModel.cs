using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Utils;

namespace ReporterApp.WindowApp.Pages.NewDesign.StartPage;

public partial class StartPageViewModel : ObservableObject
{
    private readonly ViewModelMediator _mediator;
    private readonly PageNavigatorService _pageNavigatorService;

    public StartPageViewModel(
        ViewModelMediator mediator,
        PageNavigatorService pageNavigatorService)
    {
        _mediator = mediator;
        _pageNavigatorService = pageNavigatorService;
    }

    [ObservableProperty]
    private bool _needToReadCar = true;

    [RelayCommand]
    private void DefaultReport()
    {
        _mediator.SetReportBuilder(new DefaultReportBuilder());
        NavigateToReportPage();
    }

    [RelayCommand]
    private void GasReport()
    {
        _mediator.SetReportBuilder(new GasReportBuilder());
        NavigateToReportPage();
    }

    [RelayCommand]
    private void GasAndDefaultReport()
    {
        _mediator.SetReportBuilder(new GasAndDefaultReportBuilder());
        NavigateToReportPage();
    }

    private void NavigateToReportPage()
    {
        _pageNavigatorService.NavigateTo(new ReportPage.ReportPage(
            _mediator,
            NeedToReadCar));
        NeedToReadCar = false;
    }
}
