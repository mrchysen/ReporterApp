using ReporterApp.WindowApp.Utils;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;

public partial class CarNumberPage : Page
{
    public CarNumberPage(ViewModelMediator mediator)
    {
        InitializeComponent();

        DataContext = mediator.CarNumberViewModel;
    }
}
