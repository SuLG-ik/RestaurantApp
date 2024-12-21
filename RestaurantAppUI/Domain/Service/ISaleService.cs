using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Domain.Service;

public interface ISaleService
{
    public bool AddSale(Sale sale);
    public decimal CalculateSalesRevenue(int restaurantId);
}