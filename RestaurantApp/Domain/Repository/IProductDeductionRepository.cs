using RestaurantApp.Domain.Model;

namespace RestaurantApp.Domain.Repository;

public interface IProductDeductionRepository : IRepository<ProductDeduction>
{
    public IEnumerable<SavedModel<ProductDeduction>> FindAllByRestaurantId(int id);
    public IEnumerable<SavedModel<ProductDeduction>> FindAllByRestaurantIdAndProductId(int restaurantId, int productId);
}