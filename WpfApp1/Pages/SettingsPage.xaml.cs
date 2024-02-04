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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Reporter.Logic.Configuration;

namespace Reporter.Pages
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        protected WindowConfiguration Configuration;

        public SettingsPage(WindowConfiguration configuration)
        {
            InitializeComponent();

            Configuration = configuration;
            DataContext = Configuration;
        }

        private void FolderPathTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();

            if(openFolderDialog.ShowDialog() == true)
            {
                Configuration.PathOfCarsFolder = openFolderDialog.FolderName;
            }
        }
    }
}
