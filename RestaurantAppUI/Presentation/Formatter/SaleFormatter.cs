using System.Text;
using RestaurantApp;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;

namespace RestaurantAppUI.Presentation.Formatter;

public class SaleFormatter(IFormatter parent) : BaseFormatter<Sale>
{
    private IRestaurantRepository? _restaurantRepository;

    private IRestaurantRepository RestaurantRepository =>
        _restaurantRepository ??= ServiceLocator.GetService<IRestaurantRepository>();

    protected override string Format(Sale value)
    {
        var restaurant = Validator.RequireNotNull(RestaurantRepository.Find(value.RestaurantId));
        return new StringBuilder()
            .Append("Продажа: ")
            .Append("дата продажи: ").Append(parent.Format(value.Date))
            .Append(", ресторан: ").Append($"{restaurant.Data.Name} (ID: {restaurant.Id})")
            .Append(", элементы: ").Append(parent.Format(value.SaleItems))
            .ToString();
    }
}