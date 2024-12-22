using RestaurantApp.Data.Repository;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;
using RestaurantApp.Domain.Service;
using MenuItem = RestaurantApp.Domain.Model.MenuItem;

namespace RestaurantApp.Data.Service;

public class LocalMenuService(
    IRestaurantMenuItemRepository restaurantMenuItemRepository,
    IMenuItemRepository menuItemRepository,
    IProductRepository productRepository
) : IMenuService
{
    public IEnumerable<SavedModel<MenuItem>> FindMenuItemsByRestaurantId(int restaurantId)
    {
        var menuItems = restaurantMenuItemRepository.FindAllByRestaurantId(restaurantId)
            .Select(item => item.Data.MenuItemId);
        return menuItemRepository.FindAllByIds(menuItems);
    }

    public IEnumerable<SavedModel<Product>> FindRequiredProductsByRestaurantId(int restaurantId)
    {
        var menuItems = FindMenuItemsByRestaurantId(restaurantId);
        var ingredients = menuItems.SelectMany(item => item.Data.Ingredients);
        var products = ingredients.Select(item => item.ProductId);
        return productRepository.FindAllByIds(products);
    }
}