using System.Globalization;
using RestaurantAppUI.Data.Repository;
using RestaurantAppUI.Presentation.Formatter;

namespace RestaurantAppUI.Presentation.Converters;

public class MenuItemIdConverter : IValueConverter
{
    private readonly IMenuItemRepository _supplierRepository = ServiceLocator.GetService<IMenuItemRepository>();
    private readonly IFormatter _formatter = ServiceLocator.GetService<IFormatter>();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not int id) return string.Empty;
        var model = _supplierRepository.Find(id);
        if (model == null) return string.Empty;
        return _formatter.Format(model);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}