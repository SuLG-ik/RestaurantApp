using RestaurantAppUI.Model;

namespace RestaurantAppUI.Repository;

public class InMemoryProductRequestRepository(List<SavedModel<ProductRequest>> storage)
    : InMemoryBaseRepository<ProductRequest>(storage), IProductRequestRepository
{
    public IEnumerable<SavedModel<ProductRequest>> FindAllByRestaurantId(int id)
    {
        return _storage.Where(pair => pair.Value.RestaurantId == id)
            .Select(pair => new SavedModel<ProductRequest>(pair.Key, pair.Value));
    }

    public IEnumerable<SavedModel<ProductRequest>> FindAllByRestaurantIdAndContainsItemWithProductId(int restaurantId,
        int productId)
    {
        return _storage.Where(pair =>
                pair.Value.RestaurantId == restaurantId &&
                pair.Value.ProductRequestItems.Any(item => item.ProductId == productId))
            .Select(pair => new SavedModel<ProductRequest>(pair.Key, pair.Value));
    }
}