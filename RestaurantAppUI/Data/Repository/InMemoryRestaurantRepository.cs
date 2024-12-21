using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;

namespace RestaurantAppUI.Data.Repository;

public class InMemoryRestaurantRepository(List<SavedModel<Restaurant>> storage)
    : InMemoryBaseRepository<Restaurant>(storage), IRestaurantRepository;