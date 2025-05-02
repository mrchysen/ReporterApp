using System.Windows;

namespace ReporterApp.WindowApp.Windows;

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
