using RestaurantAppUI.Data.Repository;
using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;
using RestaurantAppUI.Domain.Service;
using MenuItem = RestaurantAppUI.Domain.Model.MenuItem;

namespace RestaurantAppUI.Data.Service;

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