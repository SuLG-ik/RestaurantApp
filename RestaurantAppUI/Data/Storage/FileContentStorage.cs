using RestaurantApp.Domain.Storage;

namespace RestaurantAppUI.Data.Storage;

public class FileContentStorage(string path) : IContentStorage
{
    public string? Get()
    {
        if (!File.Exists(path)) return null;
        return File.ReadAllText(path);
    }

    public void Save(string content)
    {
        File.WriteAllText(path, content);
    }
}