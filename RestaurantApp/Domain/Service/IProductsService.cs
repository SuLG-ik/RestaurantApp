using RestaurantApp.Domain.Model;

namespace RestaurantApp.Domain.Service;

public interface IProductsService
{
    public decimal CalculateProductsQuantityInRestaurant(int restaurantId, int productId);
    public void AddProductRequest(ProductRequest request);

    public IEnumerable<ProductEditing> GetProductEditing(IEnumerable<ProductRequestItem> items);

    public bool IsProductRequestItemQuantityAvailable(ProductRequestItem request,
        IEnumerable<ProductRequestItem> allItems);

    public decimal
        CalculateProductRequestItemQuantityAvailable(int productId, IEnumerable<ProductRequestItem> allItems);

    public IEnumerable<SavedModel<Product>> FindRequiredInMenuProducts(int restaurantId);
}