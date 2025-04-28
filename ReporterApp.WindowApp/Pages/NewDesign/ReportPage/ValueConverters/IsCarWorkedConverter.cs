using ReporterApp.Core.Cars;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WpfApp;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.ValueConverters;

public class IsCarWorkedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? (SolidColorBrush)App.Current.Resources["Success"] : (SolidColorBrush)App.Current.Resources["Alert"];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
