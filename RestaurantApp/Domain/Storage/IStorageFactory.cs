namespace RestaurantApp.Domain.Storage;

public interface IStorageFactory
{
    ISavedModelsStorage<T> GetStorage<T>(string path) where T : class;
}