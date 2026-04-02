using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Components;

public partial class CarIteratorComponent : StackPanel
{
    public CarIteratorComponent()
    {
        InitializeComponent();

        LeftButton.Content = "<--";
        RightButton.Content = "-->";
    }
}
