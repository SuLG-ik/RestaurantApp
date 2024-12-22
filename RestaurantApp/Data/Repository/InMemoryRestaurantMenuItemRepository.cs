using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;

namespace RestaurantApp.Data.Repository;

public class InMemoryRestaurantMenuItemRepository(List<SavedModel<RestaurantMenuItem>> storage)
    : InMemoryBaseRepository<RestaurantMenuItem>(storage), IRestaurantMenuItemRepository
{
    public IEnumerable<SavedModel<RestaurantMenuItem>> FindAllByRestaurantId(int id)
    {
        return _storage.Where((pair) => pair.Value.RestaurantId == id)
            .Select(pair => new SavedModel<RestaurantMenuItem>(pair.Key, pair.Value));
    }
}