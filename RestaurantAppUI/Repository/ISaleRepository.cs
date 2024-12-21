using RestaurantAppUI.Model;

namespace RestaurantAppUI.Repository;

public interface ISaleRepository : IRepository<Sale>
{
    public IEnumerable<SavedModel<Sale>> FindAllByRestaurantId(int id);
}