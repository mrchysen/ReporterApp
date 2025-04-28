using ReporterApp.Core.Cars;
using ReporterApp.Core.Managers;
using System.Windows;
using System.Windows.Controls;

namespace Reporter.Pages;

public partial class CarNumberPage : Page
{
    private List<Car> _cars;
    private AppManager _appManager;

    public CarNumberPage(List<Car> cars, AppManager appManager)
    {
        InitializeComponent();

        _cars = cars;
        _appManager = appManager;

        TextBox.Text = string.Join("\r\n", cars);
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var cars = TextBox.Text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(s => new Car() { Number = s }).ToList();

        _cars.Clear();
        _cars.AddRange(cars);

        SuccessStatus.Visibility = Visibility.Visible;

        _appManager.CarsChanged = true;
    }
}
