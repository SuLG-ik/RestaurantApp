using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.RegistrationProductAction;

public class RegistrationProductActionScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Добавление нового продукта";
    protected override string? CompleteMessage => "Добавление нового продукта завершенно";

    private ISupplierRepository _supplierRepository;

    private readonly Product.Builder _builder = new();

    protected override void Create()
    {
        base.Create();
        _supplierRepository = ServiceLocator.GetService<ISupplierRepository>();
    }
    protected override IScreenFactory[] ScreenFactories =>
    [
        new StringValueInputScreenFactory("Наименование", (value) => _builder.SetName(value)),
        new DecimalValueInputScreenFactory("Цена", (value) => _builder.SetPrice(value)),
        new EnumValueInputScreenFactory<Unit>("Единица измерения", (value) => _builder.SetUnit(value)),
        new IntValueInputScreenFactory("Количество", (value) => _builder.SetQuantity(value)),
        new SingleObjectSelectScreenFactory<SavedModel<Supplier>>("Поставщик", _supplierRepository.FindAll, OnSupplierSelected, OnFailed),
    ];

    private void OnFailed()
    {
        _console.WriteLine("К сожалению список поставщиков пуст, поэтому невозможно создать продукт.");
        Navigator?.Back();
    }

    private void OnSupplierSelected(SavedModel<Supplier> supplier)
    {
        _builder.SetSupplierId(supplier.Id);
    }

    protected override void Complete()
    {
        var repository = ServiceLocator.GetService<IProductRepository>();
        var supplier = _builder.Build();
        var savedSupplier = repository.Add(supplier);
        _console.WriteLine(savedSupplier);
    }
    
    public override void Display()
    {
    }
}