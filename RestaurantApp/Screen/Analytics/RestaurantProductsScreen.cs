using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectBuilding;
using RestaurantApp.Service;

namespace RestaurantApp.Screen.Analytics;

public class RestaurantProductsScreen : ObjectBuildingScreen
{
    protected override string? HeaderMessage => "Список продуктов в ресторане";

    private IRestaurantRepository _restaurantRepository;
    private IRestaurantMenuItemRepository _restaurantMenuItemRepository;
    private IMenuItemRepository _menuItemRepository;
    private IProductRepository _productRepository;
    private IProductsService _productsService;

    protected override IScreenFactory[] ScreenFactories =>
    [
        new SingleObjectSelectScreenFactory<SavedModel<Restaurant>>("Ресторан", _restaurantRepository.FindAll,
            OnRestaurantComplete,
            onFailed: OnRestaurantFailed),
    ];

    private void OnRestaurantComplete(SavedModel<Restaurant> restaurant)
    {
        var menuItemIds = _restaurantMenuItemRepository.FindAllByRestaurantId(restaurant.Id)
            .Select(item => item.Data.MenuItemId);
        var menuItems = _menuItemRepository.FindAllByIds(menuItemIds);
        var requiredProductIds = menuItems.SelectMany(item => item.Data.Ingredients)
            .Select(item => item.ProductId)
            .Distinct().ToList();
        var requiredProducts = _productRepository.FindAllByIds(requiredProductIds);
        _console.WriteLine("\tНеобходимые продукты: ");

        foreach (var product in requiredProducts)
        {
            var quantity = _productsService.CalculateProductsQuantityInRestaurant(restaurant.Id, product.Id);
            PrintProduct(product, quantity);
        }

        var allProducts = _productRepository.FindAll();
        var notRequiredProducts = allProducts.Where(item => !requiredProductIds.Contains(item.Id));

        _console.WriteLine("\tОстальные продукты: ");
        foreach (var product in notRequiredProducts)
        {
            var quantity = _productsService.CalculateProductsQuantityInRestaurant(restaurant.Id, product.Id);
            if (quantity != 0)
            {
                PrintProduct(product, quantity);
            }
        }
    }

    private void PrintProduct(SavedModel<Product> product, decimal quantity)
    {
        _console.WriteLine($"Название: {product.Data.Name} (ID: {product.Id}). Количество в ресторане: {quantity}");
    }

    private void OnRestaurantFailed()
    {
        _console.WriteLine("Список ресторанов пуст!");
        Navigator?.Back();
    }

    protected override void Create()
    {
        base.Create();
        _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
        _restaurantMenuItemRepository = ServiceLocator.GetService<IRestaurantMenuItemRepository>();
        _menuItemRepository = ServiceLocator.GetService<IMenuItemRepository>();
        _productRepository = ServiceLocator.GetService<IProductRepository>();
        _productsService = ServiceLocator.GetService<IProductsService>();
    }

    protected override void Complete()
    {
    }

    public override void Display()
    {
    }
}