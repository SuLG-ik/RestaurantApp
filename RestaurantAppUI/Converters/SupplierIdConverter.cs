using System.Globalization;
using RestaurantAppUI.Repository;

namespace RestaurantAppUI.Converters;

public class SupplierIdConverter : IValueConverter
{
    private readonly ISupplierRepository _supplierRepository = ServiceLocator.GetService<ISupplierRepository>();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not int id) return string.Empty;
        var supplier = _supplierRepository.Find(id);
        if (supplier == null) return string.Empty;
        return $"{supplier.Data.Name} (Id: {supplier.Id})";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}