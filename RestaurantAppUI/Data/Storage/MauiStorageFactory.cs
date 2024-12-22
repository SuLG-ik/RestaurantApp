using RestaurantApp.Domain.Storage;

namespace RestaurantAppUI.Data.Storage;

public class MauiStorageFactory: IStorageFactory
{
    public ISavedModelsStorage<T> GetStorage<T>(string path) where T : class
    {
        return new FileSavedModelsStorage<T>(path);
    }
}