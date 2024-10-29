using Core.Cars;
using Reporter.Logic.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Reporter.Pages
{
    /// <summary>
    /// Логика взаимодействия для CarNumberPage.xaml
    /// </summary>
    public partial class CarNumberPage : Page
    {
        private List<Car> _cars;
        private WindowConfiguration _windowConfiguration;

        public CarNumberPage(List<Car> cars, WindowConfiguration windowConfiguration)
        {
            InitializeComponent();

            _cars = cars;
            _windowConfiguration = windowConfiguration;

            TextBox.Text = string.Join("\r\n", cars);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var cars = TextBox.Text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(s => new Car() { Number = s }).ToList();

            _cars.Clear();
            _cars.AddRange(cars);

            _windowConfiguration.CarsChanged = true;
        }
    }
}
