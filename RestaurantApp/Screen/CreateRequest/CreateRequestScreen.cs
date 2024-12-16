using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;
using RestaurantApp.Service;

namespace RestaurantApp.Screen.CreateRequest;

public class CreateRequestScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Создание заявки на продукты";
    protected override string? CompleteMessage => "Создание заявки на поступление продукта";

    private ProductRequest.Builder _builder = new();
    private IRestaurantRepository _restaurantRepository;
    private IProductsService _iProductsService;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<SavedModel<Restaurant>>("Ресторан", _restaurantRepository.FindAll,
            (value) => _builder.SetRestaurantId(value.Id),
            onFailed: OnRestaurantFailed),
        new DateTimeValueInputScreenFactory("Дата заявки", (value) => _builder.SetRequestDate(value)),
        new MultipleItemsBuildingScreenFactory<ProductRequestItem>(
            "Продукты",
            new ProductRequestItemBuilderScreenFactory(),
            onComplete: (value) => _builder.AddProductRequestItems(value),
            required: true
        ),
    ];


    private void OnRestaurantFailed()
    {
        _console.WriteLine("Нет ни одного ресторана. Невозможно создать заявку");
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
        _iProductsService = ServiceLocator.GetService<IProductsService>();
    }

    protected override void Complete()
    {
        var productRequest = _builder.Build();
        _iProductsService.AddProductRequest(productRequest);
    }

    public override void Display()
    {
    }
}