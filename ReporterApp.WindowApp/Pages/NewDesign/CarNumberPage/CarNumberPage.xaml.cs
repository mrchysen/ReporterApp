using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;

public partial class CarNumberPage : Page
{
    public CarNumberPage(CarNumberViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
