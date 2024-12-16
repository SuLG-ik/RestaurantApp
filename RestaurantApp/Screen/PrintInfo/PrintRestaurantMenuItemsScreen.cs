using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.PrintInfo;

public class PrintRestaurantMenuItemsScreen : ObjectBuildingScreen
{
    public override void Display()
    {
    }

    protected override string? HeaderMessage => null;
    protected override string? CompleteMessage => null;

    private IRestaurantRepository _restaurantRepository;
    private IRestaurantMenuItemRepository _restaurantMenuItemRepository;
    private IMenuItemRepository _menuItemRepository;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<SavedModel<Restaurant>>("Выберите ресторан для вывода меню",
            _restaurantRepository.FindAll, OnRestaurantSelected, OnRestaurantFailed)
    ];

    private void OnRestaurantFailed()
    {
        _console.WriteLine("Список ресторанов пуст");
    }

    private void OnRestaurantSelected(SavedModel<Restaurant> restaurant)
    {
        var restaurantMenuItems = _restaurantMenuItemRepository.FindAllByRestaurantId(restaurant.Id).ToList();
        if (restaurantMenuItems.Count == 0)
        {
            _console.WriteLine("Меню пустое.");
            return;
        }

        var restaurantMenuItemsIds = restaurantMenuItems.Select(item => item.Data.MenuItemId);
        var menuItems = _menuItemRepository.FindAllByIds(restaurantMenuItemsIds);

        foreach (var menuItem in menuItems)
        {
            _console.WriteLine(menuItem.Data);
        }
    }

    protected override void Create()
    {
        base.Create();
        _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
        _restaurantMenuItemRepository = ServiceLocator.GetService<IRestaurantMenuItemRepository>();
        _menuItemRepository = ServiceLocator.GetService<IMenuItemRepository>();
    }

    protected override void Complete()
    {
    }
}