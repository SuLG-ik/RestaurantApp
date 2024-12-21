using RestaurantAppUI.Model;

namespace RestaurantAppUI.Repository;

public class InMemoryRestaurantRepository(List<SavedModel<Restaurant>> storage)
    : InMemoryBaseRepository<Restaurant>(storage), IRestaurantRepository;