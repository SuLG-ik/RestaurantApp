using System.Text.Json;

namespace RestaurantApp.Storage;

public class JsonObjectSerializer : IObjectSerializer
{
    public T? Deserialize<T>(string data)
    {
        return JsonSerializer.Deserialize<T>(data, Options);
    }

    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }

    private static readonly JsonSerializerOptions Options = new()
    {
        IncludeFields = true,
    };
}