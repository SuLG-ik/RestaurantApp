using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;
using RestaurantApp.Service;

namespace RestaurantApp.Screen.Analytics;

public class RestaurantSalesRevenueScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Список продуктов в ресторане";

    private IRestaurantRepository _restaurantRepository;
    private ISaleService _saleService;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<SavedModel<Restaurant>>("Ресторан", _restaurantRepository.FindAll,
            OnRestaurantComplete,
            onFailed: OnRestaurantFailed),
    ];

    private void OnRestaurantComplete(SavedModel<Restaurant> restaurant)
    {
        var revenue = _saleService.CalculateSalesRevenue(restaurant.Id);
        _console.WriteLine($"Выручка ресторана {restaurant.Data.Name} (ID: {restaurant.Id}): {revenue}");
    }

    private void OnRestaurantFailed()
    {
        _console.WriteLine("Список ресторанов пуст!");
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
        _saleService = ServiceLocator.GetService<ISaleService>();
    }

    protected override void Complete()
    {
    }

    public override void Display()
    {
    }
}