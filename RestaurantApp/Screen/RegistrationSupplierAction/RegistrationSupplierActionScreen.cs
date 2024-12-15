using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.RegistrationSupplierAction;

public class RegistrationSupplierActionScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление нового поставщика";
    protected override string? CompleteMessage => "Добавление нового поставщика завершенно";

    private readonly Supplier.Builder _builder = new();

    protected override IScreenFactory[] ScreenFactories =>
    [
        new StringValueInputScreenFactory("Наименование", (value) => _builder.SetName(value)),
        new StringValueInputScreenFactory("Адрес", (value) => _builder.SetAddress(value)),
        new StringValueInputScreenFactory("Директор", (value) => _builder.SetDirector(value)),
        new StringValueInputScreenFactory("Номер телефона", (value) => _builder.SetPhoneNumber(value)),
        new StringValueInputScreenFactory("Банк", (value) => _builder.SetBank(value)),
        new StringValueInputScreenFactory("Лицевой счёт", (value) => _builder.SetAccountNumber(value)),
        new StringValueInputScreenFactory("ИНН", (value) => _builder.SetInn(value))
    ];

    public override void Display()
    {
    }

    protected override void Complete()
    {
        var repository = ServiceLocator.GetService<ISupplierRepository>();
        var supplier = _builder.Build();
        var savedSupplier = repository.Add(supplier);
        _console.WriteLine(savedSupplier);
    }
}