namespace RestaurantAppUI.Domain.Storage;

public interface IValuesStorage
{
    public void Get<T>(string key);
    public void Save<T>(string key, T value);
}