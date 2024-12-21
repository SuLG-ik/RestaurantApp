using RestaurantAppUI.Domain.Storage;

namespace RestaurantAppUI.Data.Storage;

public class FileSystemStorage(string path) : IContentStorage
{
    private string _filePath = Path.Combine(FileSystem.Current.AppDataDirectory, path);

    public string? Get()
    {
        if (!File.Exists(_filePath)) return null;
        return File.ReadAllText(_filePath);
    }

    public void Save(string values)
    {
        File.WriteAllText(_filePath, values);
    }
}