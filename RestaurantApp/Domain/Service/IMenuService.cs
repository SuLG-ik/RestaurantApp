using RestaurantApp.Domain.Model;
using MenuItem = RestaurantApp.Domain.Model.MenuItem;

namespace RestaurantApp.Domain.Service;

public interface IMenuService
{
    public IEnumerable<SavedModel<MenuItem>> FindMenuItemsByRestaurantId(int restaurantId);
    public IEnumerable<SavedModel<Product>> FindRequiredProductsByRestaurantId(int restaurantId);
}