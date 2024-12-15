using System.Text.Json;
using RestaurantApp.Model;

namespace RestaurantApp.Storage;

public interface ISavedModelsStorage<T> : IStorage<List<SavedModel<T>>> where T : class;