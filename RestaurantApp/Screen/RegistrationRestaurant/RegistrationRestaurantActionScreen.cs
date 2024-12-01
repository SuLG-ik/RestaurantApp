using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.RegistrationRestaurant;

public class RegistrationRestaurantActionScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление нового ресторана";
    protected override string? CompleteMessage => "Добавление нового ресторана завершенно";

    private readonly Restaurant.Builder _builder = new();

    protected override IScreenFactory[] ScreenFactories =>
    [
        new StringValueInputScreenFactory("Наименование", (value) => _builder.SetName(value)),
        new StringValueInputScreenFactory("Адрес", (value) => _builder.SetAddress(value)),
        new StringValueInputScreenFactory("Директор", (value) => _builder.SetDirectorFullname(value)),
        new StringValueInputScreenFactory("Номер телефона", (value) => _builder.SetPhoneNumber(value)),
        new MultipleItemsBuildingScreenFactory<MenuItem>("Пункты меню", new MenuItemBuilderScreenFactory(),
            OnMenuItemComplete),
    ];

    private void OnMenuItemComplete(List<MenuItem> items)
    {
        _builder.AddMenuItems(items);
    }
    protected override void Complete()
    {
        var repository = ServiceLocator.GetService<IRestaurantRepository>();
        var restaurant = _builder.Build();
        var savedSupplier = repository.Add(restaurant);
        _console.WriteLine(savedSupplier);
    }

    public override void Display()
    {
    }
}