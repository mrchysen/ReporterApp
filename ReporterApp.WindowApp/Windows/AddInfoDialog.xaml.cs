using System.Windows;

namespace ReporterApp.WindowApp.Windows;

public partial class AddInfoDialog : Window
{
    protected Car Car;

    public AddInfoDialog(Car Car)
    {
        InitializeComponent();

        this.Car = Car;

        AddInfoTextBox.Text = string.Join("\n", Car.AddInformation);
    }

    private void AcceptButton_Click(object sender, RoutedEventArgs e)
    {
        string lines = AddInfoTextBox.Text;

        if (string.IsNullOrEmpty(lines))
        {
            Car.AddInformation = new List<string>();
        }
        else
        {
            try
            {
                var AddInfo = lines.Split("\n", StringSplitOptions.TrimEntries)
                                   .Where(info => !string.IsNullOrWhiteSpace(info))
                                   .Select(info => info.Trim()).ToList();

                Car.AddInformation = AddInfo;
            }
            catch { }
        }

        DialogResult = true;
    }
}
