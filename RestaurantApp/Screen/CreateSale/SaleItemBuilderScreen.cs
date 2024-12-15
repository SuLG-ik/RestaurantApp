using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.CreateSale;

public class SaleItemBuilderScreen(int restaurantId, Action<SaleItem> onComplete, List<SaleItem> items)
    : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление пункта продажи";

    private readonly SaleItem.Builder _builder = new();
    private IMenuItemRepository _menuItemRepository;
    private IRestaurantMenuItemRepository _restaurantMenuItemRepository;

    private SavedModel<MenuItem> menuItem;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<SavedModel<MenuItem>>("Продукт", FindAllMenuItems, OnMenuItemComplete,
            onFailed: OnProductFailed),
        new IntValueInputScreenFactory("Количество", (value) => _builder.SetQuantity(value)),
        new OptionalDecimalValueInputScreenFactory(() => $"Стоимость. (пусто = {menuItem.Data.Price})", OnTotalPrice),
    ];

    private void OnMenuItemComplete(SavedModel<MenuItem> item)
    {
        menuItem = item;
        _builder.SetMenuItemId(item.Id);
    }

    private void OnTotalPrice(decimal? totalPrice)
    {
        var price = totalPrice ?? menuItem.Data.Price;
        _builder.SetPrice(price);
    }

    private IEnumerable<SavedModel<MenuItem>> FindAllMenuItems()
    {
        var restaurantMenuItemsIds = _restaurantMenuItemRepository.FindAllByRestaurantId(restaurantId)
            .Select(item => item.Data.MenuItemId);
        return _menuItemRepository.FindAllByIds(restaurantMenuItemsIds);
    }


    private void OnProductFailed()
    {
        _console.WriteLine("Нет продуктов для добавления к заявке");
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        ServiceLocator.GetService<IRestaurantRepository>();
        _menuItemRepository = ServiceLocator.GetService<IMenuItemRepository>();
        _restaurantMenuItemRepository = ServiceLocator.GetService<IRestaurantMenuItemRepository>();
    }

    protected override void Complete()
    {
        var saleItem = _builder.Build();
        onComplete(saleItem);
    }

    public override void Display()
    {
    }
}