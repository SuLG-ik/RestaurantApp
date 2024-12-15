using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemoryRestaurantRepository(List<SavedModel<Restaurant>> storage)
    : InMemoryBaseRepository<Restaurant>(storage), IRestaurantRepository;