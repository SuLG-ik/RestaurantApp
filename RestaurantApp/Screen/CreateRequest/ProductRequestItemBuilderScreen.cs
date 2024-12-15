using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;
using RestaurantApp.Service;

namespace RestaurantApp.Screen.CreateRequest;

public class ProductRequestItemBuilderScreen(Action<ProductRequestItem> onComplete, List<ProductRequestItem> items)
    : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление пункта заявки";
    protected override string? CompleteMessage => "Добавление пункта заявки";

    private readonly ProductRequestItem.Builder _builder = new();
    private IProductRepository _productRepository;
    private IProductsService _iProductsService;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<ProductEditing>("Продукт", FindAllProducts,
            value => _builder.SetProductId(value.Product.Id), onFailed: OnProductFailed),
        new DecimalValueInputScreenFactory("Количество", (value) => _builder.SetQuantity(value)),
    ];

    private List<ProductEditing> FindAllProducts()
    {
        return _iProductsService.GetProductEditing(items);
    }

    private void OnProductFailed()
    {
        _console.WriteLine("Нет продуктов для добавления к заявке");
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        _productRepository = ServiceLocator.GetService<IProductRepository>();
        _iProductsService = ServiceLocator.GetService<IProductsService>();
    }

    protected override void Complete()
    {
        var menuItem = _builder.Build();
        if (!_iProductsService.IsProductRequestItemQuantityAvailable(menuItem, items))
        {
            _console.WriteLine("Невозможно заказать больше, чем есть на складе!");
            return;
        }

        onComplete(menuItem);
    }

    public override void Display()
    {
    }
}