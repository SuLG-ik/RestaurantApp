using RestaurantApp.Domain.Model;

namespace RestaurantApp.Domain.Service;

public interface ISaleService
{
    public bool AddSale(Sale sale);
    public decimal CalculateSalesRevenue(int restaurantId);
}