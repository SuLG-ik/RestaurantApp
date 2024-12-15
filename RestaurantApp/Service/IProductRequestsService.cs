using RestaurantApp.Model;

namespace RestaurantApp.Service;

public interface IProductRequestsService
{
    public void AddProductRequest(ProductRequest request);

    public List<ProductEditing> GetProductEditing(List<ProductRequestItem> items);

    public bool IsProductRequestItemQuantityAvailable(ProductRequestItem request, List<ProductRequestItem> allItems);
}