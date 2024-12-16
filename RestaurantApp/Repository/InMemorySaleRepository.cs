using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemorySaleRepository(IEnumerable<SavedModel<Sale>> storage)
    : InMemoryBaseRepository<Sale>(storage), ISaleRepository
{
    public IEnumerable<SavedModel<Sale>> FindAllByRestaurantId(int id)
    {
        return _storage.Where((pair) => pair.Value.RestaurantId == id)
            .Select(pair => new SavedModel<Sale>(pair.Key, pair.Value));
    }
}