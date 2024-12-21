using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Domain.Service;

public interface IProductsService
{
    public decimal CalculateProductsQuantityInRestaurant(int restaurantId, int productId);
    public void AddProductRequest(ProductRequest request);

    public List<ProductEditing> GetProductEditing(List<ProductRequestItem> items);

    public bool IsProductRequestItemQuantityAvailable(ProductRequestItem request, List<ProductRequestItem> allItems);
}