using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public abstract class InMemoryBaseRepository<T> : IRepository<T> where T : class
{
    private readonly Dictionary<int, T> _storage;
    private readonly IIdGenerator _idGenerator;

    protected InMemoryBaseRepository(List<SavedModel<T>> storage)
    {
        _storage = new Dictionary<int, T>();
        var maxId = 0;
        foreach (var savedModel in storage)
        {
            _storage[savedModel.Id] = savedModel.Data;
            maxId = Math.Max(maxId, savedModel.Id);
        }

        _idGenerator = new ConcurrentIdGenerator(maxId);
    }

    protected InMemoryBaseRepository() : this([])
    {
    }

    public SavedModel<T> Add(T data)
    {
        var id = _idGenerator.NextId();
        _storage[id] = data;
        return new SavedModel<T>.Builder()
            .SetId(id)
            .SetData(data)
            .Build();
    }


    public SavedModel<T> Update(int id, T data)
    {
        if (!_storage.ContainsKey(id)) throw new ArgumentException("Model with id " + id + " is not exists");
        _storage[id] = data;
        return new SavedModel<T>.Builder()
            .SetId(id)
            .SetData(data)
            .Build();
    }

    public bool Remove(int id)
    {
        return _storage.Remove(id);
    }

    public SavedModel<T>? Find(int id)
    {
        var exists = _storage.TryGetValue(id, out var data);
        if (!exists || data == null) return null;
        return new SavedModel<T>.Builder()
            .SetId(id)
            .SetData(data)
            .Build();
    }

    public List<SavedModel<T>> FindAll()
    {
        return _storage.Select(item => new SavedModel<T>.Builder()
            .SetId(item.Key)
            .SetData(item.Value)
            .Build()
        ).ToList();
    }

    public bool Exists(int id)
    {
        return _storage.ContainsKey(id);
    }

    public int Count()
    {
        return _storage.Count;
    }
}