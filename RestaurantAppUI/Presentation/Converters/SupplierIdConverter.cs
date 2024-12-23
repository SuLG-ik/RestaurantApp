using System.Globalization;
using RestaurantApp;
using RestaurantApp.Domain.Repository;
using RestaurantAppUI.Presentation.Formatter;

namespace RestaurantAppUI.Presentation.Converters;

public class SupplierIdConverter : IValueConverter
{
    private readonly ISupplierRepository _supplierRepository = ServiceLocator.GetService<ISupplierRepository>();
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