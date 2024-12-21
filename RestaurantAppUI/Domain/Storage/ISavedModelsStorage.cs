using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Domain.Storage;

public interface ISavedModelsStorage<T> : IStorage<List<SavedModel<T>>> where T : class;