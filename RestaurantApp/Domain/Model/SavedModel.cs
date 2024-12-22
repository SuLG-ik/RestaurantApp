using System.Text.Json.Serialization;

namespace RestaurantApp.Domain.Model;

public interface ISavedModel<out T> where T : class
{
    public int Id { get; }
    public T Data { get; }
}

[method: JsonConstructor]
public class SavedModel<T>(int id, T data) : ISavedModel<T>
    where T : class
{
    public int Id { get; } = Validator.RequireGreaterThan(id, 0);
    public T Data { get; } = data;
}