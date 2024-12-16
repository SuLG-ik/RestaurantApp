using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public interface ISaleRepository : IRepository<Sale>
{
    public IEnumerable<SavedModel<Sale>> FindAllByRestaurantId(int id);
}