using RestaurantApp.Model;

namespace RestaurantApp.Service;

public interface IProductsService
{
    public decimal CalculateProductsQuantityInRestaurant(int restaurantId, int productId);
    public void AddProductRequest(ProductRequest request);

    public List<ProductEditing> GetProductEditing(List<ProductRequestItem> items);

    public bool IsProductRequestItemQuantityAvailable(ProductRequestItem request, List<ProductRequestItem> allItems);
}