using RestaurantApp.Domain.Model;

namespace RestaurantApp.Domain.Repository;

public interface IRestaurantMenuItemRepository : IRepository<RestaurantMenuItem>
{
    IEnumerable<SavedModel<RestaurantMenuItem>> FindAllByRestaurantId(int id);
}