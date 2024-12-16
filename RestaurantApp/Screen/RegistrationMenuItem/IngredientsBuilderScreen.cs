using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.RegistrationProduct;

public class IngredientsBuilderScreen(Action<Ingredient> onComplete)
    : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление пункта заявки";
    protected override string? CompleteMessage => "Добавление пункта заявки";

    private readonly Ingredient.Builder _builder = new();
    private IProductRepository _productRepository;


    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<SavedModel<Product>>("Продукт", _productRepository.FindAll,
            OnProductComplete,
            onFailed: OnProductFailed),
        new DecimalValueInputScreenFactory("Количество", (value) => _builder.SetQuantity(value)),
    ];

    private void OnProductComplete(SavedModel<Product> item)
    {
        _builder.SetProductId(item.Id);
    }

    private void OnProductFailed()
    {
        _console.WriteLine("Нет продуктов для добавления к ингредиентам");
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        ServiceLocator.GetService<IRestaurantRepository>();
        ServiceLocator.GetService<IProductRepository>();
        _productRepository = ServiceLocator.GetService<IProductRepository>();
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