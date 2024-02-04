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
    /// Логика взаимодействия для AddInfoDialog.xaml
    /// </summary>
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
}
