using RestaurantApp.Model;
using RestaurantApp.Repository;

namespace RestaurantApp.Screen.PrintInfo;

public class PrintItemsScreen : MenuOptionsScreen<PrintItemsOptions>
{
    public override string? HeaderMessage => "Вывод данных";

    private IProductRequestRepository _productRequestRepository;
    private IRestaurantRepository _restaurantRepository;
    private ISupplierRepository _supplierRepository;
    private IProductRepository _productRepository;

    public override Dictionary<PrintItemsOptions, MenuOption> Options => new()
    {
        { PrintItemsOptions.Products, new MenuOption("Продукты", OnProducts) },
        { PrintItemsOptions.Restaurants, new MenuOption("Рестораны", OnRestaurants) },
        { PrintItemsOptions.RestaurantMenu, new MenuOption("Меню ресторана", OnRestaurantsMenu) },
        { PrintItemsOptions.Suppliers, new MenuOption("Поставщики", OnSuppliers) },
        { PrintItemsOptions.ProductRequests, new MenuOption("Заявки на продукты", OnProductRequests) },
        { PrintItemsOptions.Quit, new MenuOption("Назад", OnQuit) },
    };

    private void OnProductRequests()
    {
        var items = _productRequestRepository.FindAll();
        Navigator?.NavigateTo(new PrintItemsInfoScreen<SavedModel<ProductRequest>>(items));
    }

    private void OnRestaurants()
    {
        var items = _restaurantRepository.FindAll();
        Navigator?.NavigateTo(new PrintItemsInfoScreen<SavedModel<Restaurant>>(items));
    }

    private void OnRestaurantsMenu()
    {
        Navigator?.NavigateTo(new PrintRestaurantMenuItemsScreen());
    }

    private void OnSuppliers()
    {
        var items = _supplierRepository.FindAll();
        Navigator?.NavigateTo(new PrintItemsInfoScreen<SavedModel<Supplier>>(items));
    }

    private void OnProducts()
    {
        var items = _productRepository.FindAll();
        Navigator?.NavigateTo(new PrintItemsInfoScreen<SavedModel<Product>>(items));
    }

    private void OnQuit()
    {
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        _productRequestRepository = ServiceLocator.GetService<IProductRequestRepository>();
        _productRepository = ServiceLocator.GetService<IProductRepository>();
        _supplierRepository = ServiceLocator.GetService<ISupplierRepository>();
        _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
    }
}