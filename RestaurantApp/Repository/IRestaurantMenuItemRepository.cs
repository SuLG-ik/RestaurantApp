using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public interface IRestaurantMenuItemRepository : IRepository<RestaurantMenuItem>
{
    IEnumerable<SavedModel<RestaurantMenuItem>> FindAllByRestaurantId(int id);
}