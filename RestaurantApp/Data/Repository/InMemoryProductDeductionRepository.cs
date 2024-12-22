using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;

namespace RestaurantApp.Data.Repository;

public class InMemoryProductDeductionRepository(IEnumerable<SavedModel<ProductDeduction>> storage)
    : InMemoryBaseRepository<ProductDeduction>(storage), IProductDeductionRepository
{
    public IEnumerable<SavedModel<ProductDeduction>> FindAllByRestaurantId(int id)
    {
        return _storage.Where((pair) => pair.Value.RestaurantId == id)
            .Select(pair => new SavedModel<ProductDeduction>(pair.Key, pair.Value));
    }

    public IEnumerable<SavedModel<ProductDeduction>> FindAllByRestaurantIdAndProductId(int restaurantId, int productId)
    {
        return _storage.Where((pair) => pair.Value.RestaurantId == restaurantId && pair.Value.ProductId == productId)
            .Select(pair => new SavedModel<ProductDeduction>(pair.Key, pair.Value));
    }
}