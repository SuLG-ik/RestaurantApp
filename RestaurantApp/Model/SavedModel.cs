using System.Text.Json.Serialization;

namespace RestaurantApp.Model;

public interface ISavedModel<out T> where T : class
{
    public int Id { get; }
    public T Data { get; }
}

public class SavedModel<T> : ISavedModel<T> where T : class
{
    public int Id { get; }
    public T Data { get; }

    [JsonConstructor]
    private SavedModel(int id, T data)
    {
        Id = id;
        Data = data;
    }

    public class Builder
    {
        private int? _id;
        private T? _data;

        public Builder SetId(int id)
        {
            _id = Validator.RequireGreaterThan(id, 0, nameof(_id));
            return this;
        }

        public Builder SetData(T data)
        {
            _data = Validator.RequireNotNull(data, nameof(_data));
            return this;
        }

        public SavedModel<T> Build()
        {
            var id = Validator.RequireNotNull(_id, nameof(_id));
            var data = Validator.RequireNotNull(_data, nameof(_data));
            return new SavedModel<T>(id, data);
        }

        public static Builder From(SavedModel<T> source)
        {
            return new Builder()
                .SetData(source.Data)
                .SetId(source.Id);
        }
    }
}