using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.RegistrationRestaurant;

public class RegistrationRestaurantActionScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление нового ресторана";
    protected override string? CompleteMessage => "Добавление нового ресторана завершенно";

    private readonly Restaurant.Builder _builder = new();

    private IEnumerable<SavedModel<MenuItem>> _menuItems;

    private IMenuItemRepository _menuItemRepository;
    private IRestaurantMenuItemRepository _restaurantMenuItemRepository;
    private IRestaurantRepository _restaurantRepository;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new StringValueInputScreenFactory("Наименование", (value) => _builder.SetName(value)),
        new StringValueInputScreenFactory("Адрес", (value) => _builder.SetAddress(value)),
        new StringValueInputScreenFactory("Директор", (value) => _builder.SetDirectorFullname(value)),
        new StringValueInputScreenFactory("Номер телефона", (value) => _builder.SetPhoneNumber(value)),
        new MultipleObjectsSelectScreenFactory<MenuItem>("Пункты меню", _menuItemRepository,
            OnMenuItemComplete,
            OnMenuItemFailed),
    ];

    private void OnMenuItemComplete(List<SavedModel<MenuItem>> items)
    {
        _menuItems = items;
    }

    private void OnMenuItemFailed()
    {
        _console.WriteLine("Не создано ни одного пункта меню. Невозможно создать ресторан");
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        _menuItemRepository = ServiceLocator.GetService<IMenuItemRepository>();
        _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
        _restaurantMenuItemRepository = ServiceLocator.GetService<IRestaurantMenuItemRepository>();
    }

    protected override void Complete()
    {
        var restaurant = _builder.Build();
        var savedRestaurant = _restaurantRepository.Add(restaurant);
        var restaurantMenuItems = _menuItems
            .Select(item => new RestaurantMenuItem(savedRestaurant.Id, item.Id));
        _restaurantMenuItemRepository.AddAll(restaurantMenuItems);
        _console.WriteLine(savedRestaurant);
    }

    public override void Display()
    {
    }
}