using Core.Cars;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace ReporterApp.WindowApp.Logic.ValueConverters;

public class ButtonColorConverter : IValueConverter
{
    protected Predicate<Car> Predicat;

    public ButtonColorConverter(Predicate<Car> predicat)
    {
        Predicat = predicat;
    }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (!Predicat((Car)value))? new SolidColorBrush(Color.FromArgb(255, 187, 120, 161)) : new SolidColorBrush(Color.FromArgb(255,161, 187, 120));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
