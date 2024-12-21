using RestaurantAppUI.Model;

namespace RestaurantAppUI.Repository;

public interface IRestaurantMenuItemRepository : IRepository<RestaurantMenuItem>
{
    IEnumerable<SavedModel<RestaurantMenuItem>> FindAllByRestaurantId(int id);
}