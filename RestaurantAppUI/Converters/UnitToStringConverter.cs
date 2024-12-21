using System.Globalization;
using RestaurantAppUI.Model;

namespace RestaurantAppUI.Converters;

public class UnitToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Unit unit) return string.Empty;
        return unit switch
        {
            Unit.Kg => "кг",
            Unit.Liter => "л",
            Unit.Item => "шт",
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}