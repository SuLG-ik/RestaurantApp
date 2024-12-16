using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public interface IProductDeductionRepository : IRepository<ProductDeduction>
{
    public IEnumerable<SavedModel<ProductDeduction>> FindAllByRestaurantId(int id);
    public IEnumerable<SavedModel<ProductDeduction>> FindAllByRestaurantIdAndProductId(int restaurantId, int productId);
}