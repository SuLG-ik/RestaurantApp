using RestaurantApp.Domain.Model;

namespace RestaurantApp.Domain.Repository;

public interface ISaleRepository : IRepository<Sale>
{
    public IEnumerable<SavedModel<Sale>> FindAllByRestaurantId(int id);
}