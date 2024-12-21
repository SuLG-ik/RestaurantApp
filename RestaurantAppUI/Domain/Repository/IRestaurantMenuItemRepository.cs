using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Domain.Repository;

public interface IRestaurantMenuItemRepository : IRepository<RestaurantMenuItem>
{
    IEnumerable<SavedModel<RestaurantMenuItem>> FindAllByRestaurantId(int id);
}