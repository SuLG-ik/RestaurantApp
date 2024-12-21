namespace RestaurantAppUI.Domain.Storage;

public interface IStorage<T> where T : class
{
    public T? Get();
    public void Save(T values);
}