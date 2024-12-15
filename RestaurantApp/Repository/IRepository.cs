using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public interface IRepository<T> where T : class
{
    public SavedModel<T> Add(T data);

    public SavedModel<T> Update(int id, T data);

    public bool Remove(int id);

    public SavedModel<T>? Find(int id);

    public List<SavedModel<T>> FindAll();

    public bool Exists(int id);

    public int Count();
}