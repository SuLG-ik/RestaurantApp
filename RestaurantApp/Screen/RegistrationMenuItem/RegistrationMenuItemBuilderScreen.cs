using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.RegistrationMenuItem;

public class RegistrationMenuItemBuilderScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление пункта меню";
    protected override string? CompleteMessage => "Добавление пункта меню завершено";

    private readonly MenuItem.Builder _builder = new();

    private IMenuItemRepository _menuItemRepository;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new StringValueInputScreenFactory("Наименование", (value) => _builder.SetName(value)),
        new DecimalValueInputScreenFactory("Цена", (value) => _builder.SetPrice(value)),
        new EnumValueInputScreenFactory<MenuItemGroup>("Группа", (value) => _builder.SetGroup(value)),
    ];

    protected override void Create()
    {
        base.Create();
        _menuItemRepository = ServiceLocator.GetService<IMenuItemRepository>();
    }

    protected override void Complete()
    {
        var menuItem = _builder.Build();
        _menuItemRepository.Add(menuItem);
    }

    public override void Display()
    {
    }
}