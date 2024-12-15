using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.CreateRequest;

public class ProductRequestItemBuilderScreen(Action<ProductRequestItem> onComplete) : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление пункта заявки";
    protected override string? CompleteMessage => "Добавление пункта заявки";

    private readonly ProductRequestItem.Builder _builder = new();
    private IProductRepository _productRepository;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<Product>("Продукт", _productRepository,
            value => _builder.SetProductId(value.Id), onFailed: OnProductFailed),
        new DecimalValueInputScreenFactory("Количество", (value) => _builder.SetQuantity(value)),
    ];

    private void OnProductFailed()
    {
        _console.WriteLine("Нет продуктов для добавления к заявке");
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        _productRepository = ServiceLocator.GetService<IProductRepository>();
    }

    protected override void Complete()
    {
        var menuItem = _builder.Build();
        onComplete(menuItem);
    }

    public override void Display()
    {
    }
}