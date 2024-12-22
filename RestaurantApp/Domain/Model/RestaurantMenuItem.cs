namespace RestaurantApp.Domain.Model;

public class RestaurantMenuItem(int restaurantId, int menuItemId)
{
    public int RestaurantId { get; } = restaurantId;
    public int MenuItemId { get; } = menuItemId;
}