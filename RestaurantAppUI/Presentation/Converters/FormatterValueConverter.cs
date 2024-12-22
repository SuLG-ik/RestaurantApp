using System.Globalization;
using RestaurantApp;
using RestaurantAppUI.Presentation.Formatter;

namespace RestaurantAppUI.Presentation.Converters;

public class FormatterValueConverter : IValueConverter
{
    private readonly IFormatter _formatter = ServiceLocator.GetService<IFormatter>();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return _formatter.Format(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}