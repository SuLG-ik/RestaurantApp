using RestaurantAppUI.Model;

namespace RestaurantAppUI.Repository;

public interface IProductRequestRepository : IRepository<ProductRequest>
{
    public IEnumerable<SavedModel<ProductRequest>> FindAllByRestaurantId(int id);
    public IEnumerable<SavedModel<ProductRequest>> FindAllByRestaurantIdAndContainsItemWithProductId(int restaurantId, int productId);
}