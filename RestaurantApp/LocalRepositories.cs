using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Storage;

namespace RestaurantApp;

public class LocalRepositories : IRepositories
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
    }

    public void Destroy()
    {
        SaveAll(ServiceLocator.GetService<ISupplierRepository>(), KeySupplier);
        SaveAll(ServiceLocator.GetService<IProductRepository>(), KeyProducts);
        SaveAll(ServiceLocator.GetService<IRestaurantRepository>(), KeyRestaurants);
        SaveAll(ServiceLocator.GetService<IProductRequestRepository>(), KeyProductRequests);
    }

    private static List<SavedModel<T>> GetAll<T>(string name) where T : class
    {
        var storage = new FileSavedModelsStorage<T>(name);
        return storage.Get() ?? [];
    }

    private static void SaveAll<T>(IRepository<T> repository, string name) where T : class
    {
        var storage = new FileSavedModelsStorage<T>(name);
        storage.Save(repository.FindAll());
    }

    private static readonly string KeySupplier = "suppliers";
    private static readonly string KeyProducts = "products";
    private static readonly string KeyRestaurants = "restaurants";
    private static readonly string KeyProductRequests = "productrequests";
}