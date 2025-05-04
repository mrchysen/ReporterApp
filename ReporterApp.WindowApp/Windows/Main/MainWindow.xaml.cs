using System.Windows;
using ReporterApp.WindowApp.Pages.NewDesign.StartPage;
using ReporterApp.WindowApp.Utils;

namespace ReporterApp.WindowApp.Windows.Main;

public partial class MainWindow : Window
{
    private PageNavigatorService _pageNavigationService;
    private ViewModelMediator _mediator;
    public MainWindow()
    {
        InitializeComponent();

        _pageNavigationService = new(Frame);
        _mediator = new(_pageNavigationService);
        _pageNavigationService.NavigateTo(new StartPage(_mediator));

        DataContext = _mediator.MainWindowViewModel;
    }
}