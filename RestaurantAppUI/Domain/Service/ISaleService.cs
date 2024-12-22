using RestaurantAppUI.Domain.Model;
using MenuItem = RestaurantAppUI.Domain.Model.MenuItem;

namespace RestaurantAppUI.Domain.Service;

public interface ISaleService
{
    public bool AddSale(Sale sale);
    public decimal CalculateSalesRevenue(int restaurantId);
}