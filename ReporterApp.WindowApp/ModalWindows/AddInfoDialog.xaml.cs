using Core.Cars;
using System.Windows;

namespace Reporter.ModalWindows;

public partial class AddInfoDialog : Window
{
    private Car _car;

    public AddInfoDialog(Car Car)
    {
        InitializeComponent();

        _car = Car;

        AddInfoTextBox.Text = string.Join("\n", Car.AddInformation);
    }

    private void AcceptButton_Click(object sender, RoutedEventArgs e)
    {
        string lines = AddInfoTextBox.Text;

        if (string.IsNullOrEmpty(lines))
        {
            _car.AddInformation = new List<string>();
        }
        else
        {
            try
            {
                var AddInfo = lines.Split("\n", StringSplitOptions.TrimEntries)
                                   .Where(info => !string.IsNullOrWhiteSpace(info))
                                   .Select(info => info.Trim()).ToList();

                _car.AddInformation = AddInfo;
            }
            catch { }
        }

        DialogResult = true;
    }
}
