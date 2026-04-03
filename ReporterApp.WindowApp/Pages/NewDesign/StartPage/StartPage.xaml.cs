using ReporterApp.WindowApp.Navigation;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.StartPage;

public partial class StartPage : Page, IPageWithParameter
{
    private readonly StartPageViewModel _viewModel;

    public StartPage(StartPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = viewModel;
    }

    public void SetParameter(object? parameter)
    {
        if (parameter is bool needToReadCar)
        {
            _viewModel.NeedToReadCar = needToReadCar;
        }
    }
}
