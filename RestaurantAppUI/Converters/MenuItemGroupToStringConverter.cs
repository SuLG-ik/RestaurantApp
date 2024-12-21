using System.Globalization;
using RestaurantAppUI.Model;

namespace RestaurantAppUI.Converters;

public class MenuItemGroupToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not MenuItemGroup group) return string.Empty;
        return group switch
        {
            MenuItemGroup.Appetisers => "Закуски",
            MenuItemGroup.FirstCourses => "Первые блюда",
            MenuItemGroup.SecondCourses => "Вторые блюда",
            MenuItemGroup.Dessert => "Десерты",
            MenuItemGroup.Drinks => "Напитки",
            MenuItemGroup.Pastries => "Выпечка",
            MenuItemGroup.Other => "Другое",
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}