using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;
using RestaurantApp.Service;

namespace RestaurantApp.Screen.CreateSale;

public class CreateSaleScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Создание заявки на продукты";
    protected override string? CompleteMessage => "Создание заявки на поступление продукта";

    private Sale.Builder _builder = new();
    private IRestaurantRepository _restaurantRepository;
    private ISaleService _saleService;
    private SavedModel<Restaurant> _restaurant;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<SavedModel<Restaurant>>("Ресторан", _restaurantRepository.FindAll,
            OnRestaurantComplete,
            onFailed: OnRestaurantFailed),
        new DateTimeValueInputScreenFactory("Дата продажи", (value) => _builder.SetDate(value)),
        new MultipleItemsBuildingScreenFactory<SaleItem>(
            "Пункт меню",
            new SaleItemBuilderScreenFactory(GetRestaurantId),
            onComplete: (value) => _builder.AddSaleItems(value),
            required: true
        ),
    ];


    private int GetRestaurantId()
    {
        return _restaurant.Id;
    }

    private void OnRestaurantComplete(SavedModel<Restaurant> value)
    {
        _restaurant = value;
        _builder.SetRestaurantId(value.Id);
    }

    private void OnRestaurantFailed()
    {
        _console.WriteLine("Нет ни одного ресторана. Невозможно создать заявку");
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
        _saleService = ServiceLocator.GetService<ISaleService>();
    }

    protected override void Complete()
    {
        var saleRequest = _builder.Build();
        if (_saleService.AddSale(saleRequest))
        {
            _console.WriteLine("Продажа создана!");
        }
        else
        {
            _console.WriteLine("Продажа не создана. Недостаточно продуктов!");
        }
    }

    public override void Display()
    {
    }
}