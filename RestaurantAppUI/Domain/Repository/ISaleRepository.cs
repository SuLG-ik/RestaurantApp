using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Domain.Repository;

public interface ISaleRepository : IRepository<Sale>
{
    public IEnumerable<SavedModel<Sale>> FindAllByRestaurantId(int id);
}