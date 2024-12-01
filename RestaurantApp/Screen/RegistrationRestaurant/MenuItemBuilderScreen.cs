using RestaurantApp.Model;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.RegistrationRestaurant;

public class MenuItemBuilderScreen(
    Action<MenuItem> onComplete
) : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление пункта меню";
    protected override string? CompleteMessage => "Добавление пункта меню завершено";

    private readonly MenuItem.Builder _builder = new();

    protected override IScreenFactory[] ScreenFactories =>
    [
        new StringValueInputScreenFactory("Наименование", (value) => _builder.SetName(value)),
        new DecimalValueInputScreenFactory("Цена", (value) => _builder.SetPrice(value)),
        new EnumValueInputScreenFactory<MenuItemGroup>("Группа", (value) => _builder.SetGroup(value)),
    ];

    protected override void Complete()
    {
        var menuItem = _builder.Build();
        onComplete(menuItem);
    }

    public override void Display()
    {
    }
}