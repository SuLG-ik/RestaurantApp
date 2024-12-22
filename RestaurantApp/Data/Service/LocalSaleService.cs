using RestaurantApp.Data.Repository;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;
using RestaurantApp.Domain.Service;

namespace RestaurantApp.Data.Service;

public class LocalSaleService(
    ISaleRepository saleRepository,
    IProductDeductionRepository productDeductionRepository,
    IMenuItemRepository menuItemRepository,
    IProductsService productsService
) : ISaleService
{
    public bool AddSale(Sale sale)
    {
        var ingredients = menuItemRepository.FindAllByIds(sale.SaleItems.Select(item => item.MenuItemId))
            .SelectMany(item => item.Data.Ingredients);
        var productDeductions =
            ingredients.Select(
                    item => new ProductDeduction(sale.RestaurantId, sale.Date, item.ProductId, item.Quantity))
                .ToList();
        var groupedDeductions = productDeductions.GroupBy(item => item.ProductId);
        var isEnoughProductsToDeduct = groupedDeductions.All(grouping =>
            IsProductsQuantityInRestaurantDeductionAvailable(sale.RestaurantId, grouping.Key, grouping));
        if (!isEnoughProductsToDeduct)
        {
            return false;
        }

        saleRepository.Add(sale);
        productDeductionRepository.AddAll(productDeductions);
        return true;
    }

    private bool IsProductsQuantityInRestaurantDeductionAvailable(int restaurantId, int productId,
        IEnumerable<ProductDeduction> additionalDeductions)
    {
        return productsService.CalculateProductsQuantityInRestaurant(restaurantId, productId) -
            additionalDeductions.Sum(item => item.Quantity) >= 0;
    }

    public decimal CalculateSalesRevenue(int restaurantId)
    {
        return saleRepository.FindAllByRestaurantId(restaurantId).Sum(item => item.Data.TotalPrice);
    }
}