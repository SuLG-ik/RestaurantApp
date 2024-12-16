using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public interface IProductRequestRepository : IRepository<ProductRequest>
{
    public IEnumerable<SavedModel<ProductRequest>> FindAllByRestaurantId(int id);
    public IEnumerable<SavedModel<ProductRequest>> FindAllByRestaurantIdAndContainsItemWithProductId(int restaurantId, int productId);
}