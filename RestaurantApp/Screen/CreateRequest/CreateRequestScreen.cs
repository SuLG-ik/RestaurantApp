using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.CreateRequest;

public class CreateRequestScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Создание заявки на продукты";
    protected override string? CompleteMessage => "Создание заявки на поступление продукта";

    private ProductRequest.Builder _builder = new();
    private IRestaurantRepository _restaurantRepository;
    private IProductRequestRepository _productRequestRepository;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<Restaurant>("Ресторан", _restaurantRepository,
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
        _productRequestRepository = ServiceLocator.GetService<IProductRequestRepository>();
    }

    protected override void Complete()
    {
        var productRequest = _builder.Build();
        _productRequestRepository.Add(productRequest);
    }

    public override void Display()
    {
    }
}