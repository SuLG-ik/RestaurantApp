using RestaurantAppUI.Domain.Model;
using MenuItem = RestaurantAppUI.Domain.Model.MenuItem;

namespace RestaurantAppUI.Domain.Service;

public interface IMenuService
{
    public IEnumerable<SavedModel<MenuItem>> FindMenuItemsByRestaurantId(int restaurantId);
    public IEnumerable<SavedModel<Product>> FindRequiredProductsByRestaurantId(int restaurantId);
}