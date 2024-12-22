using RestaurantApp.Data.Repository;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;
using RestaurantApp.Domain.Storage;
using MenuItem = RestaurantApp.Domain.Model.MenuItem;

namespace RestaurantApp;

public class LocalRepositories(IStorageFactory storageFactory) : IRepositories
{
    public void Initialize()
    {
        ServiceLocator.Register<ISupplierRepository>(
            new InMemorySupplierRepository(GetAll<Supplier>(KeySupplier)));
        ServiceLocator.Register<IProductRepository>(
            new InMemoryProductRepository(GetAll<Product>(KeyProducts)));
        ServiceLocator.Register<IRestaurantRepository>(
            new InMemoryRestaurantRepository(GetAll<Restaurant>(KeyRestaurants)));
        ServiceLocator.Register<IProductRequestRepository>(
            new InMemoryProductRequestRepository(GetAll<ProductRequest>(KeyProductRequests)));
        ServiceLocator.Register<IMenuItemRepository>(
            new InMemoryMenuItemRepository(GetAll<MenuItem>(KeyMenuItems)));
        ServiceLocator.Register<IRestaurantMenuItemRepository>(
            new InMemoryRestaurantMenuItemRepository(GetAll<RestaurantMenuItem>(KeyRestaurantMenuItems)));
        ServiceLocator.Register<ISaleRepository>(new InMemorySaleRepository(GetAll<Sale>(KeySales)));
        ServiceLocator.Register<IProductDeductionRepository>(
            new InMemoryProductDeductionRepository(GetAll<ProductDeduction>(KeyProductDeductions)));
    }

    public void Destroy()
    {
        SaveAll(ServiceLocator.GetService<ISupplierRepository>(), KeySupplier);
        SaveAll(ServiceLocator.GetService<IProductRepository>(), KeyProducts);
        SaveAll(ServiceLocator.GetService<IRestaurantRepository>(), KeyRestaurants);
        SaveAll(ServiceLocator.GetService<IProductRequestRepository>(), KeyProductRequests);
        SaveAll(ServiceLocator.GetService<IMenuItemRepository>(), KeyMenuItems);
        SaveAll(ServiceLocator.GetService<IRestaurantMenuItemRepository>(), KeyRestaurantMenuItems);
        SaveAll(ServiceLocator.GetService<ISaleRepository>(), KeySales);
    }

    private List<SavedModel<T>> GetAll<T>(string name) where T : class
    {
        var storage = storageFactory.GetStorage<T>(name);
        return storage.Get() ?? [];
    }

    private void SaveAll<T>(IRepository<T> repository, string name) where T : class
    {
        var storage = storageFactory.GetStorage<T>(name);
        storage.Save(repository.FindAll());
    }

    private static readonly string KeySupplier = "suppliers";
    private static readonly string KeyProducts = "products";
    private static readonly string KeyRestaurants = "restaurants";
    private static readonly string KeyProductRequests = "productrequests";
    private static readonly string KeyMenuItems = "menuitems";
    private static readonly string KeyRestaurantMenuItems = "restaurantmenuitems";
    private static readonly string KeySales = "sales";
    private static readonly string KeyProductDeductions = "productdeductions";
}