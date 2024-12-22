namespace RestaurantApp.Domain.Storage;

public interface IObjectSerializer
{
    public T? Deserialize<T>(string data);
    public string Serialize<T>(T obj);
}