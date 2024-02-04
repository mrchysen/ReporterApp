using KrasTechMontacApplication.Logic.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

namespace WpfApp1.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для FuelDialog.xaml
    /// </summary>
    public partial class FuelDialog : Window
    {
        protected Car Car;

        public FuelDialog(Car car)
        {
            InitializeComponent();

            Car = car;

            StartFuelTextBox.Text = car.FuelBegin.ToString();
            EndFuelTextBox.Text = car.FuelEnd.ToString();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            int startFuel = 0;
            int endFuel = 0;

            bool flag1 = int.TryParse(StartFuelTextBox.Text, out startFuel);
            bool flag2 = int.TryParse(EndFuelTextBox.Text, out endFuel);

            if(!(flag1 && flag2)) 
            {
                DialogResult = false;
            }
            else
            {
                Car.FuelBegin = startFuel;
                Car.FuelEnd = endFuel;

                DialogResult = true;
            }
        }
    }
}
