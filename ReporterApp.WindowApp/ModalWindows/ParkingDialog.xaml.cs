using ReporterApp.Core.Cars;
using System.Windows;

namespace Reporter.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для ParkingDialog.xaml
    /// </summary>
    public partial class ParkingDialog : Window
    {
        protected Car Car;

        public ParkingDialog(Car Car)
        {
            InitializeComponent();

            this.Car = Car;

            ParkingTextBox.Text = string.Join(" ", Car.Parking);
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string lines = ParkingTextBox.Text;

            if (string.IsNullOrEmpty(lines))
            {
                Car.Parking = new List<int>();
            }
            else
            {
                try
                {
                    var Parking = lines.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                       .Select((el) => int.Parse(el)).Where(el => el > 0).ToList();

                    Parking.Sort();

                    Car.Parking = Parking;
                }
                catch { }
            }

            DialogResult = true;
        }
    }
}
