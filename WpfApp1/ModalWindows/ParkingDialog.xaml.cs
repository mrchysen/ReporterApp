using KrasTechMontacApplication.Logic.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
