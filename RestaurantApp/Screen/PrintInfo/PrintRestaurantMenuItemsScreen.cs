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

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<Restaurant>("Выберите ресторан для вывода меню",
            _restaurantRepository, onRestaurantSelected, onRestaurantFailed)
    ];

    private void onRestaurantFailed()
    {
        _console.WriteLine("Список ресторанов пуст");
    }

    private void onRestaurantSelected(SavedModel<Restaurant> restaurant)
    {
        if (restaurant.Data.Menu.Count == 0)
        {
            _console.WriteLine("Меню пустое.");
            return;
        }
        foreach (var menuItem in restaurant.Data.Menu)
        {
            _console.WriteLine(menuItem);
        }
    }

    protected override void Create()
    {
        base.Create();
        _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
    }

    protected override void Complete()
    {
    }
}