using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;

namespace RestaurantAppUI.Data.Repository;

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