using System.Text;
using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;

namespace RestaurantAppUI.Formatter;

public class SaleFormatter(IFormatter parent) : BaseFormatter<Sale>
{
    private IRestaurantRepository? _restaurantRepository;

    private IRestaurantRepository RestaurantRepository =>
        _restaurantRepository ??= ServiceLocator.GetService<IRestaurantRepository>();

    protected override string Format(Sale value)
    {
        var restaurant = Domain.Validator.RequireNotNull(RestaurantRepository.Find(value.RestaurantId));
        return new StringBuilder()
            .Append("Продажа: ")
            .Append("дата продажи: ").Append(parent.Format(value.Date))
            .Append(", ресторан: ").Append($"{restaurant.Data.Name} (ID: {restaurant.Id})")
            .Append(", элементы: ").Append(parent.Format(value.SaleItems))
            .ToString();
    }
}