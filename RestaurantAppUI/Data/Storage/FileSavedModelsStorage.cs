using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Storage;

namespace RestaurantAppUI.Data.Storage;

public class FileSavedModelsStorage<T>(string name) : ISavedModelsStorage<T> where T : class
{
    private readonly IStorage<List<SavedModel<T>>> _delegate =
        new SerializeStorage<List<SavedModel<T>>>(new FileSystemStorage($"{name}.json"), new JsonObjectSerializer());

    public void Save(List<SavedModel<T>> values)
    {
        _delegate.Save(values);
    }

    public List<SavedModel<T>>? Get()
    {
        return _delegate.Get();
    }
}