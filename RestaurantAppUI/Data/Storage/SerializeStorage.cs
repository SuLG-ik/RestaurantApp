using RestaurantApp.Domain.Storage;

namespace RestaurantAppUI.Data.Storage;

public class SerializeStorage<T>(
    IContentStorage storage,
    IObjectSerializer objectSerializer
) : IStorage<T> where T : class
{
    public T? Get()
    {
        var content = storage.Get();
        if (content == null) return null;
        return objectSerializer.Deserialize<T>(content);
    }

    public void Save(T values)
    {
        var content = objectSerializer.Serialize(values);
        storage.Save(content);
    }
}