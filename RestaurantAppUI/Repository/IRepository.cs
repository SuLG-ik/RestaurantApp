using RestaurantAppUI.Model;

namespace RestaurantAppUI.Repository;

public interface IRepository<T> where T : class
{
    public SavedModel<T> Add(T data);
    public IEnumerable<SavedModel<T>> AddAll(IEnumerable<T> data);

    public SavedModel<T> Update(int id, T data);

    public bool Remove(int id);

    public SavedModel<T>? Find(int id);

    public List<SavedModel<T>> FindAll();
    
    public List<SavedModel<T>> FindAllByIds(IEnumerable<int> ids);

    public bool Exists(int id);

    public int Count();
}