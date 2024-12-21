using RestaurantAppUI.Model;

namespace RestaurantAppUI.Storage;

public interface ISavedModelsStorage<T> : IStorage<List<SavedModel<T>>> where T : class;