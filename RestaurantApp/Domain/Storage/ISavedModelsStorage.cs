using RestaurantApp.Domain.Model;

namespace RestaurantApp.Domain.Storage;

public interface ISavedModelsStorage<T> : IStorage<List<SavedModel<T>>> where T : class;