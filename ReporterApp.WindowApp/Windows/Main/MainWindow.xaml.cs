using System.Windows;
using ReporterApp.WindowApp.Navigation;
using ReporterApp.WindowApp.Pages.NewDesign.StartPage;

namespace ReporterApp.WindowApp.Windows.Main;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void InitializeMainWindow(
        INavigationService navigationService,
        MainWindowViewModel mainWindowViewModel)
    {
        navigationService.NavigateTo<StartPage>(true);
        DataContext = mainWindowViewModel;
    }
}